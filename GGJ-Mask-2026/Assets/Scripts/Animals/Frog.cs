using System;
using UnityEngine;

public class Frog : Animal
{
    Vector2 _velocity;
    Collider2D _collider;
    
    [Header("settings")]
    [SerializeField] private ContactFilter2D _contactFilter;
    [SerializeField] private float _jumpAngle;
    [SerializeField] private float _jumpStrength;
    [SerializeField] private float _jumpStrengthWithInput;
    [SerializeField] private float _gravity;

    private int _orientation = 1;

    protected override void Awake()
    {
        TryGetComponent(out _collider);
        print(_collider.name);
    }

    private void Start()
    {
        //Jump();
    }

    void Jump()
    {
        float angle = _jumpAngle * Mathf.Deg2Rad;
        _velocity = 
            new Vector2(Mathf.Cos(angle) * _orientation, Mathf.Sin(angle))
            * (!Input.GetKey(KeyCode.Mouse0) ? _jumpStrength : _jumpStrengthWithInput); 
        Debug.DrawRay(transform.position, _velocity, Color.red,1);
    }

    void FixedUpdate()
    {
        //gravity
        _velocity.y = _velocity.y - _gravity * Time.fixedDeltaTime;
        
        //collision
        int collisionCount = 0;
        RaycastHit2D[] hits = new RaycastHit2D[1];
        while ( Physics2D.CircleCast(transform.position,.5f,_velocity,_contactFilter,hits,_velocity.magnitude*Time.deltaTime)>0 && collisionCount < 5 )
        {
            if (Vector2.Dot(hits[0].normal, Vector2.up) > .7f)
                if (isControlled) Jump(); else _velocity = Vector2.zero;
            else
                _velocity = Vector2.Reflect(_velocity, hits[0].normal)*1f;

            _orientation = _velocity.x > 0 ? 1 : -1;
            collisionCount ++;
        }
        
        //apply velocity
        transform.position += (Vector3)(_velocity * Time.fixedDeltaTime);
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
