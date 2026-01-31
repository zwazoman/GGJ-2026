using System;
using UnityEditor;
using UnityEngine;

public class Spider : Animal
{
    public enum State
    {
        Walking,
        Swinging,
        ThrowingWeb
    }
    
    [Header("SceneReferences")]
    [SerializeField] private LineRenderer _lineRenderer;
    
    [Header("settings")]
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _walkSpeed;
    [SerializeField] private float _walkSpeedWhenPossessed;
    [SerializeField] private float _swingSpeed;

    public event Action OnSwingStart, OnLand;

    public float orientation { get; set; } = 1;
    
    //states
    public State _state { get; private set; } = State.Walking; 
    
    //grapple
    private float _grappleStartTime;
    private Vector3 _grapplePoint;
    private float _grappleLength;
    private bool _wantsToGrapple;
    [SerializeField] private float _grappleLaunchDuration = .4f;
    [SerializeField] private float _maxGrappleLength = 5;

    private Vector3 right => orientation * transform.right;

    protected override void Update()
    {
        base.Update();
        _wantsToGrapple |= Input.GetKeyDown(KeyCode.Mouse0) && isControlled;
    }
    
    void Swing()
    {
        UpdateGrappleVisuals();
        Debug.DrawLine(transform.position, _grapplePoint, Color.red*.8f);
        
        //check for collision
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, .01f, right, .2f, _layerMask);
        if (hit)
        {
            print("collision "+hit.transform.name);
            
            Vector2 currentRight = right;
            transform.rotation = Quaternion.AngleAxis(Mathf.Rad2Deg * Mathf.Atan2(hit.normal.y, hit.normal.x)-90, Vector3.forward);
            orientation = Mathf.Sign(Vector2.Dot(currentRight, right));
            transform.position = hit.point + hit.normal*.5f;
            _state = State.Walking;
            OnLand?.Invoke();
        }
        else
        {
            //rotate around point
            transform.RotateAround(_grapplePoint, Vector3.forward, orientation * _swingSpeed * Time.deltaTime / _grappleLength);
        }
    } 
    
    void WalkAlongWalls()
    {
        RaycastHit2D hit;
            
        //state change
        if (_wantsToGrapple)
        {
            _wantsToGrapple = false;
            StartGrapple();
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
        transform.position += (right * (Time.deltaTime * (isControlled ? _walkSpeedWhenPossessed : _walkSpeed)));
    }

    void StartGrapple()
    {
        _grappleStartTime = Time.time;
        _state = State.ThrowingWeb;
    }

    void UpdateGrappleVisuals()
    {
        float alpha = (Time.time - _grappleStartTime)/_grappleLaunchDuration;
        float parabola01 = Mathf.Clamp01( (alpha * (1.0f-alpha))*4f);
        for (int i = 0; i < _lineRenderer.positionCount; i++)
        {
            float pAlpha = (float)i/(float)_lineRenderer.positionCount;
            _lineRenderer.SetPosition(i,
                transform.position 
                + transform.up * (_grappleLength * pAlpha) 
                + right * (Mathf.Sin(-Time.time * 7 + pAlpha * 2 * Mathf.PI * 3) * parabola01 * .5f)
            );
        }
    }
    
    void UpdateGrapple()
    {
        _lineRenderer.enabled = true;
        
        float alpha = (Time.time - _grappleStartTime)/_grappleLaunchDuration;
        float parabola01 = (alpha * (1.0f-alpha))*4f;
        if(alpha>1) {_state = State.Walking; return; }
        
        _grappleLength = parabola01 * _maxGrappleLength;
        Debug.DrawRay(transform.position,transform.up*_grappleLength, Color.blue*.8f,1);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, _grappleLength, _layerMask);

        UpdateGrappleVisuals();
        
        if (hit)
        {
            _lineRenderer.enabled = false;
            _grapplePoint = hit.point;
            _grappleLength = hit.distance;
            Debug.DrawLine(transform.position, _grapplePoint, Color.red,1);
            _state = State.Swinging;
            OnSwingStart?.Invoke();
        }
    }
    
    void FixedUpdate()
    {
        switch (_state)
        {
            case State.Walking:
                WalkAlongWalls();
                break;
            case State.Swinging:
                Swing();
                break;
            case State.ThrowingWeb:
                UpdateGrapple();
                break;
        }
        
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
        GUILayout.Label("current state : " + ((Spider)target)._state.ToString());
    }
}
#endif

