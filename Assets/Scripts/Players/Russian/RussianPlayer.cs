using UnityEngine;
using System.Collections;
public class RussianPlayer : MonoBehaviour
{
    #region Variaveis de movimentação
    public float maxJumpHeight = 4;
    public float minJumpHeight = 1;
    public float timeToJumpApex = .4f;
    public float timeToBarrelApex = 0.8f;
    public float accelerationTimeAirborne = .2f;
    public float accelerationTimeGrounded = .1f;
    public float moveSpeed = 5;
    public float throwForce = 5;
    bool ispressing;
    int directionThrow;
    RussianUI russianUI;
    float gravity,barrelGravity;
    float maxJumpVelocity;
    public GameObject[] barrelCountDisplay = new GameObject[1];
    public AudioClip deathSound;
    public float maxBarrellDistance = 8;
    public float minBarrellDistance = 2;
    float minBarrelVelocity;
    float maxBarrelVelocity;
    InputHandler mobileInput;
    Vector3 velocity;
    bool Death;
    float velocityXSmoothing;
    #endregion
    Controller2D controller; //Classe que controla colisoes e fisica do objeto controlado
    RussianAnimator russiNimator;
    public GameObject Barrel;
    Vector2 input;
    void Start()
    {
        #region Valores Iniciais
        controller = GetComponent<Controller2D>();
        barrelGravity = (2*maxBarrellDistance) / Mathf.Pow(timeToBarrelApex, 2);
        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        maxBarrelVelocity = Mathf.Abs(barrelGravity) * timeToBarrelApex;
        minBarrelVelocity = Mathf.Sqrt(2 * Mathf.Abs(barrelGravity) * minBarrellDistance);
        print("Gravity: " + gravity + "  Jump Velocity: " + maxJumpVelocity);
        russiNimator = GetComponent<RussianAnimator>();
        russianUI = FindObjectOfType<RussianUI>() ;
        directionThrow = 3;
        mobileInput = GetComponent<InputHandler>();
        controller.collisions.dying = false;
        Death = false;
        russiNimator.currAnimator.SetBool("Thrown", true);
        BarrelTypes.instance.collectableInfo.regularBarrelCount = 0;
        BarrelTypes.instance.collectableInfo.hasRegularBarrel = false;
        mobileInput.firstTouch = false;
        #endregion
    }

    void Update()
    {
        if (!controller.collisions.dying)
        {
           input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
#if UNITY_WSA_10_0||UNITY_ANDROID
            if (input.x == 0)
            {
                input = Input.acceleration * 5;
            }
            mobileInput.TouchInput();
#endif
            if (BarrelTypes.instance.collectableInfo.regularBarrelCount>0)
            {
                BarrelTypes.instance.collectableInfo.hasRegularBarrel = true;
            }
            float targetVelocityX; //velocidade que vai ser alterada e depois passada para o controller
            targetVelocityX = moveSpeed * input.x;
            velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
                velocity.y += gravity * Time.deltaTime; //velocidade no eixo Y, queda e pulo)
            if ((Input.GetKeyDown(KeyCode.Space)
                || mobileInput.touch.phase == TouchPhase.Began&&mobileInput.firstTouch)
                && controller.collisions.jump && BarrelTypes.instance.collectableInfo.hasRegularBarrel)
            {
                velocity = BarrelJump(velocity, directionThrow);
            }
            if (BarrelTypes.instance.collectableInfo.regularBarrelCount == 2)
            {
               for (int i = 0; i < 2; i++)
                {
                    barrelCountDisplay[i].SetActive(true);
                }
            }
            else if (BarrelTypes.instance.collectableInfo.regularBarrelCount == 1)
            {
                barrelCountDisplay[0].SetActive(true);
                barrelCountDisplay[1].SetActive(false);
            }
            else
            {
                barrelCountDisplay[0].SetActive(false);
                barrelCountDisplay[1].SetActive(false);
            }
            if ((Input.GetKeyUp(KeyCode.Space) || mobileInput.touch.phase == TouchPhase.Canceled || mobileInput.touch.phase == TouchPhase.Ended) && controller.collisions.jump)
            {
                velocity.y = (velocity.y > minBarrelVelocity) ? minBarrelVelocity : velocity.y;
            }
            if (((Input.GetKeyDown(KeyCode.Space) || (mobileInput.touch.phase == TouchPhase.Began && mobileInput.firstTouch)) && controller.collisions.jump == false))
            {
                velocity = Jump(velocity);
            }
            if (BarrelTypes.instance.collectableInfo.hasRegularBarrel)
            {
                russiNimator.resetThrown();
            }
            controller.Move(velocity * Time.deltaTime, input); //passa a velocidade no eixo Y para o controller
            if (input.x != 0)
            {
                ispressing = true;
            }
            else
            {
                ispressing = false;
            }

            russiNimator.Running(controller.collisions.faceDir, velocity, ispressing, controller.collisions.below);
            if ((controller.collisions.above || controller.collisions.below)) //checa se tem colisoes em cima ou em baixo do objeto para ele nao passar varando kk
            {
                velocity.y = 0;
            }
        }
        else if (controller.collisions.dying && Death == false)
        {
            Audioa4Manager.instance.PlaySound2D("Death");
            russiNimator.StartDeathProcedure();
            Death = true;
        }
        if (controller.collisions.dying && Death)
        {
            velocity.y = 0 +gravity * Time.deltaTime;
            velocity.x = 0;
            controller.Move(velocity*Time.deltaTime,input);
            

        }
    }
    Vector3 Jump (Vector3 velocity)
    {
        velocity.y = maxJumpVelocity;
        controller.collisions.jump = true;
        russiNimator.Jump(controller.collisions.jump, controller.collisions.below);
        return velocity;
    }
    Vector3 BarrelJump(Vector3 velocity, int direction)
    {
            velocity.y = maxBarrelVelocity;
            GameObject BarrelThrown = Instantiate(Barrel, new Vector3(transform.position.x, transform.position.y), Quaternion.Euler(0, 0, 90)) as GameObject;
            BarrelThrown.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -throwForce*4), ForceMode2D.Impulse);
            controller.collisions.jump = true;
        BarrelTypes.instance.collectableInfo.regularBarrelCount--;
        if (BarrelTypes.instance.collectableInfo.regularBarrelCount <= 0)
        {
            BarrelTypes.instance.collectableInfo.hasRegularBarrel = false;
        }
        russiNimator.Throws(3, false, controller.collisions.faceDir);
        Audioa4Manager.instance.PlaySound2D("ThrowBarrel");
        return velocity;
    }
    void OnBecameInvisible()
    {
        try {
            if (!FindObjectOfType<StageController>().LoadingScene)
            { 
                FindObjectOfType<StageController>().DeathProcedure(1, gameObject);
                GetComponent<RussianPlayer>().enabled = false;
            }
        }
        catch
        {

        }
    }
    public void DeadForReal()
    {
        FindObjectOfType<StageController>().DeathProcedure( 2, gameObject);
        GetComponent<RussianPlayer>().enabled = false;

    }
    internal void changeHasBarrelState(bool barrel)
    {
        russiNimator.currAnimator.SetBool("HasBarrel", barrel);
    }
    
}