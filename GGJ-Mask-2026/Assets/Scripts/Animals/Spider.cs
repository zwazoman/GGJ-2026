using UnityEditor;
using UnityEngine;

public class Spider : Animal
{
    [Header("settings")]
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _swingSpeed;

    private float _orientation = 1;
    
    //states
    public bool _isGrappling { get; private set; } = false; 
    
    //grapple
    private Vector3 _grapplePoint;
    private float _grappleLength;

    private bool _wantsToGrapple;
    
    private Vector3 right => _orientation * transform.right;

    protected override void Update()
    {
        base.Update();
        _wantsToGrapple |= Input.GetKeyDown(KeyCode.Mouse0) && isControlled;
    }
    
    void Swing()
    {
        Debug.DrawLine(transform.position, _grapplePoint, Color.red*.8f);
        
        //check for collision
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, .01f, right, .2f, _layerMask);
        if (hit)
        {
            print("collision "+hit.transform.name);
            
            Vector2 currentRight = right;
            transform.rotation = Quaternion.AngleAxis(Mathf.Rad2Deg * Mathf.Atan2(hit.normal.y, hit.normal.x)-90, Vector3.forward);
            _orientation = Mathf.Sign(Vector2.Dot(currentRight, right));
            transform.position = hit.point + hit.normal*.5f;
            _isGrappling = false;
        }
        else
        {
            //rotate around point
            transform.RotateAround(_grapplePoint, Vector3.forward, _orientation * _swingSpeed * Time.deltaTime / _grappleLength);
        }
    } 
    
    void WalkAlongWalls()
    {
        RaycastHit2D hit;
            
        //state change
        if (_wantsToGrapple)
        {
            _wantsToGrapple = false;
            Debug.DrawRay(transform.position,transform.up, Color.blue*.8f,1);
            hit = Physics2D.Raycast(transform.position, transform.up, 20f, _layerMask);
            if (hit)
            {
                _grapplePoint = hit.point;
                _grappleLength = hit.distance;
                Debug.DrawLine(transform.position, _grapplePoint, Color.red,1);
                _isGrappling = true;
            }
        }
        
        //Debug
        Debug.DrawRay(transform.position + (right - transform.up)*.6f, -right*.6f, Color.red);
        Debug.DrawRay(transform.position + right*.6f, - transform.up*.6f, Color.blue);
        
        //checkFor wall
        Debug.DrawRay(transform.position, right*.6f, Color.green);
        hit = Physics2D.Raycast(transform.position , right, .6f, _layerMask);
        if (hit) //un mur devant
        {
            print("wall");
            transform.position = hit.point + hit.normal*.5f;
            transform.rotation = Quaternion.AngleAxis(Mathf.Rad2Deg * Mathf.Atan2(hit.normal.y, hit.normal.x)-90, Vector3.forward);
        }
        
        //checkForCorner
        hit = Physics2D.Raycast(transform.position + (right - transform.up)*.6f, -right, .6f, _layerMask);
        if (!Physics2D.Raycast(transform.position + right*.6f, -transform.up, .6f, _layerMask) //pas de sol devant
            && hit) //un mur devant en dessous
        {
            transform.position = hit.point + hit.normal*.5f;
            transform.rotation = Quaternion.AngleAxis(Mathf.Rad2Deg * Mathf.Atan2(hit.normal.y, hit.normal.x)-90, Vector3.forward);
        }
        
        //move
        transform.position += (right * (Time.deltaTime * _walkSpeed));
    }
    
    void FixedUpdate()
    {
        if(_isGrappling)
            Swing();
        else
            WalkAlongWalls();
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

#if UNITY_EDITOR
[CustomEditor(typeof(Spider))]
class SpiderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        GUILayout.Label("is grappling : " + ((Spider)target)._isGrappling);
    }
}
#endif

