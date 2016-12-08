using UnityEngine;
using System.Collections;

public class Controller2D : RaycastController
{
    public int teleportCount = 0;
    Vector3 PlayerPosition; //posição do player pra teleportar
    #region angulos maximos em q o objeto pode subir ou descer
    float maxDescendAngle = 80;
    #endregion
    public CollisionInfo collisions; //estrutura que armazena as colisoes no objeto
    [HideInInspector]
    public Vector2 playerInput; //Input do jogador
    ObjectChanger changerGlobal;
    public float movingScore;
    int[] loopstoscene = { 20, 50, 100 };
    public float[] initialy = { 7.629f, 112.47f, 217.281f };
    int indice;

    public override void Start()
    {//seta valores inicais
        base.Start(); //inicia o metodo Start do RaycastController (essa classe é filho do RaycasController)
        collisions.faceDir = 1; //inicia com direçao Direita
        changerGlobal = FindObjectOfType<ObjectChanger>();
        indice = 0;
    }

    public void Move(Vector3 velocity, bool standingOnPlatform)
    {
        Move(velocity, Vector2.zero, standingOnPlatform); //ficar parado em plataforma movel, vou remover dps pq nao precisa
    }

    public void Move(Vector3 velocity, Vector2 input, bool standingOnPlatform = false)
    {
        UpdateRaycastOrigins(); //atualiza a origin dos raycasts
        collisions.Reset(); //reseta as informaçoes de colisao
        collisions.velocityOld = velocity; //salva a velocidade anterior a alteracao de movimento
        playerInput = input; //pega o input da classe Player
        MiscCollisions(ref velocity);
        if (velocity.x != 0) //se a velocidade no eixo X for diferente de 0, seta a direcao como 1 para olhando para direita e -1 para olhando para esquerda
        {
            collisions.faceDir = (int)Mathf.Sign(velocity.x);
        }
        HorizontalCollisions(ref velocity); //calcula as colisoes horizontais passando uma referencia da velocidade
        if (velocity.y!=0) //se a velocidade no eixo Y for diferente de zero, calcula as colisoes Verticais
        {
            VerticalCollisions(ref velocity);
        }
        gameObject.transform.Translate(velocity); //finalmente translada o objeto após todos os calculos
        if (velocity.y > 0&&!collisions.dying)
        {
            if (BarrelTypes.instance.collectableInfo.hasDoubleScore)
            {
                LocalScore.instance.addScore(movingScore * 2);
            }
            else
            {
                LocalScore.instance.addScore(movingScore);
            }
        }
        if (velocity.y < 0 && !collisions.dying)
        {
            LocalScore.instance.addScore(-movingScore);
        }
        if (standingOnPlatform)
        {
            collisions.below = true;
        }
    }
    #region Calcular Colisoes Horizontais
    void HorizontalCollisions(ref Vector3 velocity)
    {
        float directionX = collisions.faceDir;
        float rayLength = Mathf.Abs(velocity.x) + skinWidth;

        if (Mathf.Abs(velocity.x) < skinWidth)
        {
            rayLength = 2 * skinWidth;
        }

        for (int i = 0; i < horizontalRayCount; i++)
        {
            Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
            rayOrigin += Vector2.up * (horizontalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);

            if (hit)
            {

                if (hit.distance == 0)
                {
                    break;
                }
                    velocity.x = (hit.distance - skinWidth) * directionX;
                    rayLength = hit.distance;
                    collisions.left = directionX == -1;
                    collisions.right = directionX == 1;
                }
        }
    }
    #endregion
    #region Calcular Colisoes Verticais
    void VerticalCollisions(ref Vector3 velocity)
    {
        float directionY = Mathf.Sign(velocity.y);
        float rayLength = Mathf.Abs(velocity.y) + skinWidth;

        for (int i = 0; i < verticalRayCount; i++)
        {

            Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
            rayOrigin += Vector2.right * (verticalRaySpacing * i + velocity.x);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, collisionMask);

            if (hit)
            { 
                velocity.y = (hit.distance - skinWidth) * directionY;
                rayLength = hit.distance;
                collisions.below = directionY == -1;
                collisions.above = directionY == 1;
                collisions.jump = false;
            }
        }
    }
    #endregion
    void MiscCollisions(ref Vector3 velocity) //colisoes mistas (covers, coletaveis, etc)
    {
        float directionX = Mathf.Sign(velocity.x);
        float directionY = Mathf.Sign(velocity.y);
        float rayLength = Mathf.Abs(velocity.x) + skinWidth;


        for (int i = 0; i < horizontalRayCount; i++)
        {
            Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
            rayOrigin += Vector2.up * (horizontalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, miscMask);

            Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLength, Color.red);

            if (hit)
            {
                BarrelTypes.instance.collectedSomething(hit.collider.gameObject,velocity,transform);
                if (hit.collider.tag != "Teleport" && hit.collider.tag != "Espinho" && hit.collider.tag != "LazerBeam")
                {
                    Destroy(hit.collider.gameObject);
                }
                if (hit.collider.tag == "Espinho" || hit.collider.tag == "LazerBeam")
                {
                        collisions.dying = true;
                        velocity.y = 0;
                }
            }

        }
    
        for (int i = 0; i < verticalRayCount; i++)
        {
            Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
            rayOrigin += Vector2.right * (verticalRaySpacing * i + velocity.x);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, miscMask);
            if (hit)
            {
                    BarrelTypes.instance.collectedSomething(hit.collider.gameObject,velocity,transform);
                if (hit.collider.tag != "Teleport"&&hit.collider.tag!="Espinho"&&hit.collider.tag!="LazerBeam")
                {
                    Destroy(hit.collider.gameObject);
                }


                if (hit.collider.tag == "Espinho" || hit.collider.tag == "LazerBeam")
                {
                        collisions.dying = true;
                        velocity.y = 0;
                    

                }

                if (hit.collider.tag == "Teleport"&&!collisions.justReset)
                {
                    //Debug.Log("Indice: " + indice + "  Initialy[indice]: " + initialy[indice] + "  loopscene[indice]: " + loopstoscene[indice]);
                    teleportCount = teleportCount + 1;
                    if (teleportCount < loopstoscene[indice]||indice==2)
                    {
                        PlayerPosition = transform.position;
                        Camera.main.GetComponent<CameraFollow>().cameraLimit = initialy[indice];
                        transform.position = new Vector3(PlayerPosition.x, initialy[indice], PlayerPosition.z);
                        FindObjectOfType<SpawnController>().RandomSpawn();
                        FindObjectOfType<SpawnController>().RandomSpawnBack();
                        FindObjectOfType<SpawnController>().RandomSpawnSpike();
                        FindObjectOfType<SpawnController>().LazerSpawn();
                        FindObjectOfType<SpawnController>().RandomSpawnBarrilEscudo();
                        FindObjectOfType<SpawnController>().RandomSpawnBarrilDouble();
                        collisions.justReset = true;
                        StartCoroutine(ResettheReset());
                    }

                    else
                    {
                        indice = indice + 1;
                        Destroy(hit.collider.gameObject);
                        FindObjectOfType<SpawnController>().RandomSpawn();
                        FindObjectOfType<SpawnController>().RandomSpawnBack();
                        FindObjectOfType<SpawnController>().RandomSpawnSpike();
                        FindObjectOfType<SpawnController>().LazerSpawn();
                        FindObjectOfType<SpawnController>().RandomSpawnBarrilEscudo();
                        FindObjectOfType<SpawnController>().RandomSpawnBarrilDouble();
                        collisions.justReset = true;
                        StartCoroutine(ResettheReset());
                    }
                }

            }

            }
        }
   
    void ResetFallingThroughPlatform()
    {
        collisions.fallingThroughPlatform = false;
    }

    public struct CollisionInfo //declaração da estrutura
    {
        public bool above, below; //booleanas para saber se está colidindo em cima ou em baixo
        public bool left, right;//booleanas para esquerda e direita
        public bool justReset;
        public bool dying;
        public bool climbingSlope; //subindo Inclinação ?
        public bool jump;
        public bool descendingSlope; //descendo inclinação ?
        public float slopeAngle, slopeAngleOld; //angulos da inclinação, angle old é para quando a inclinação varia
        public Vector3 velocityOld; //velocidade anterior a atualizaçao atual, serve pra SMOOTHING !
        public int faceDir; //direçao para qual o objeto está olhando, é sempre 1 ou -1
        public bool fallingThroughPlatform; //booleana para ver se o objeto está passando por uma plataforma ou não
        public void Reset() //função para resetar a maioria das informações da estrutura
        {
            above = below = false;
            left = right = false;
            climbingSlope = false;
            descendingSlope = false;
            slopeAngleOld = slopeAngle;
            slopeAngle = 0;
        }
    }
    IEnumerator ResettheReset()
    {
        yield return new WaitForSeconds(0.75f);
        collisions.justReset = false;
        yield break;
    }
}
