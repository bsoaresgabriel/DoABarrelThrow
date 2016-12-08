using UnityEngine;
using System.Collections;

public class RaycastController : MonoBehaviour
{

    public LayerMask collisionMask;
    public LayerMask miscMask;

    public const float skinWidth = 0.015f; //skinwidth (serve para que o objeto nao flutue e pareça ter um contato natural com as coisas
    public int horizontalRayCount = 4; //numero de raios disparados horizontalmente
    public int verticalRayCount = 4; //numero de raios disparados verticalmente
    [HideInInspector]
    public float horizontalRaySpacing; //espaço entre raios horizontais
    [HideInInspector]
    public float verticalRaySpacing; //espaço entre raios verticais

    [HideInInspector]
    public BoxCollider2D collider; //variavel referente ao collider do objeto
    public RaycastOrigins raycastOrigins; //origem dos raios
    public CircleCollider2D circleCollider;

    public virtual void Awake()
    {
        if (!circleCollider)
        {
            collider = GetComponent<BoxCollider2D>();
        }
    }

    public virtual void Start()
    {
        CalculateRaySpacing();
    }

    public void UpdateRaycastOrigins() //funçao que atualiza a origem dos raios
    {
        if (!circleCollider)
        {
            Bounds bounds = collider.bounds;
            bounds.Expand(skinWidth * -2);

            raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
            raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
            raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
            raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
        }
        if (circleCollider)
        {
            Bounds circleBound = circleCollider.bounds;
            raycastOrigins.center = new Vector2(circleBound.center.x, circleBound.center.y);
        }
    }

    public void CalculateRaySpacing() //funçao que atualiza o espaço entre os raios
    {
        if (!circleCollider)
        {
            Bounds bounds = collider.bounds;
            bounds.Expand(skinWidth * -2);

            horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue);
            verticalRayCount = Mathf.Clamp(verticalRayCount, 2, int.MaxValue);

            horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
            verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
        }
    }

    public struct RaycastOrigins //estrutura que armazena as informacoes das vertices do objeto, mais especificamente o collider, (Topo esquerda, Topo Direita, Baixo Esquerda, Baixo direita)
    {
        public Vector2 topLeft, topRight;
        public Vector2 bottomLeft, bottomRight;
        public Vector2 center;
    }
}