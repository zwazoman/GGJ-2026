using UnityEngine;

public class Spider : Animal
{
    [Header("settings")]
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _speed;

    void FixedUpdate()
    {
        //Debug
        Debug.DrawRay(transform.position + (transform.right - transform.up)*.6f, -transform.right*.6f, Color.red);
        Debug.DrawRay(transform.position + transform.right*.6f, - transform.up*.6f, Color.blue);
        
        //checkForAngle
        RaycastHit2D hit = Physics2D.Raycast(transform.position + (transform.right - transform.up)*.6f, -transform.right, .6f, _layerMask);
        if (!Physics2D.Raycast(transform.position + transform.right*.6f, -transform.up, .6f, _layerMask) //pas de sol devant
            && hit) //un mur devant en dessous
        {
            transform.position = hit.point + hit.normal*.5f;
            //transform.right = -Vector2.Perpendicular(hit.normal);
            //transform.up = hit.normal;
            //Quaternion.LookRotation(hit.normal, transform.up);
            transform.rotation = Quaternion.AngleAxis(Mathf.Rad2Deg * Mathf.Atan2(hit.normal.y, hit.normal.x)-90, Vector3.forward);
        }
        
        //move
        transform.position += (Vector3)(transform.right*Time.deltaTime);
    }
    
    public override void GainControl(Pawn oldPawn)
    {
        base.GainControl(oldPawn);
    }

    public override void LooseControl()
    {
        base.LooseControl();
    }
}
