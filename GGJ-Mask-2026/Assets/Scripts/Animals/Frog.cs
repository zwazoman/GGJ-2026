using System;
using UnityEngine;

public class Frog : Animal
{
    public Vector2 Velocity { get;private set; }
    Collider2D _collider;
    
    [Header("settings")]
    [SerializeField] private ContactFilter2D _contactFilter;
    [SerializeField] private float _jumpAngle;
    [SerializeField] private float _jumpStrength;
    [SerializeField] private float _jumpStrengthWithInput;
    [SerializeField] private float _gravity;

    [SerializeField] private int _orientation = 1;
    public event Action OnJump, OnLand, OnBounce;
    
    protected override void Awake()
    {
        base.Awake();
        TryGetComponent(out _collider);
        print(_collider.name);
    }

    private void Start()
    {
        //Jump();
        OnLand?.Invoke();
    }

    void Jump()
    {
        float angle = _jumpAngle * Mathf.Deg2Rad;
        Velocity = 
            new Vector2(Mathf.Cos(angle) * _orientation, Mathf.Sin(angle))
            * (!Input.GetKey(KeyCode.Mouse0) ? _jumpStrength : _jumpStrengthWithInput); 
        Debug.DrawRay(transform.position, Velocity, Color.red,1);
        OnJump?.Invoke();

        SFXManager.Instance.PlaySFXClip(Sounds.Whoosh);
    }

    void FixedUpdate()
    {
        //gravity
        Velocity = new Vector2(Velocity.x, Velocity.y - _gravity * Time.fixedDeltaTime);
        
        //collision
        int collisionCount = 0;
        RaycastHit2D[] hits = new RaycastHit2D[1];
        while ( Physics2D.CircleCast(transform.position,.5f,Velocity,_contactFilter,hits,Velocity.magnitude*Time.deltaTime)>0 && collisionCount < 5 )
        {
            if (Vector2.Dot(hits[0].normal, Vector2.up) > .7f)
                if (isControlled) {Jump();} else {Velocity = Vector2.zero; OnLand?.Invoke();}
            else
            {Velocity = Vector2.Reflect(Velocity, hits[0].normal)*1f; OnBounce?.Invoke();}

            _orientation = Velocity.x > 0 ? 1 : -1;
            collisionCount ++;
        }
        
        //apply velocity
        transform.position += (Vector3)(Velocity * Time.fixedDeltaTime);
    }
    
    public override void GainControl(Pawn oldPawn)
    {
        base.GainControl(oldPawn);
        Jump();
    }

    public override void LooseControl()
    {
        base.LooseControl();
    }
}
