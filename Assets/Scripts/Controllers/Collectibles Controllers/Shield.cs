using UnityEngine;
using System.Collections;

public class Shield : RaycastController
{
    public override void Start()
    {//seta valores inicais
        base.Start(); //inicia o metodo Start do RaycastController (essa classe é filho do RaycasController)
    }
    public void DetectHorizontal(Vector3 velocity)
    {
        UpdateRaycastOrigins();
        float circleRayDistance = Mathf.Abs(velocity.x) + skinWidth;
        float directionX = Mathf.Sign(velocity.x);
        float directionY = Mathf.Sign(velocity.y);
        float circleRadius = circleCollider.radius;
        Vector2 rayCircleOrigin = raycastOrigins.center;
        RaycastHit2D circleHit = Physics2D.CircleCast(raycastOrigins.center, circleRadius, new Vector2(directionX, directionY), circleRayDistance, miscMask);
        Debug.DrawRay(rayCircleOrigin, new Vector2(directionX, directionY));
        if (circleHit)
        {
            Audioa4Manager.instance.PlaySound2D("Invencible");
            if (BarrelTypes.instance.collectableInfo.hasDoubleScore)
            {
                LocalScore.instance.addScore(50*2);
            }
            else
            {
                LocalScore.instance.addScore(50);
            }
            Destroy(circleHit.transform.parent.gameObject);
        }


    }
    public void DetectVertical(Vector3 velocity)
    {
        UpdateRaycastOrigins();
        float circleRayDistance = Mathf.Abs(velocity.y) + skinWidth;
        float directionX = Mathf.Sign(velocity.x);
        float directionY = Mathf.Sign(velocity.y);
        float circleRadius = circleCollider.radius;
        Vector2 rayCircleOrigin = raycastOrigins.center;
        RaycastHit2D circleHit = Physics2D.CircleCast(raycastOrigins.center, circleRadius, new Vector2(directionX, directionY), circleRayDistance, miscMask);
        if (circleHit)
        {
            Audioa4Manager.instance.PlaySound2D("Invencible");
            if (BarrelTypes.instance.collectableInfo.hasDoubleScore)
            {
                LocalScore.instance.addScore(50 * 2);
            }
            else
            {
                LocalScore.instance.addScore(50);
            }
            Destroy(circleHit.transform.parent.gameObject);
        }
    }
}
