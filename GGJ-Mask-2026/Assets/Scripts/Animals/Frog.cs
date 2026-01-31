using UnityEngine;

public class Frog : Animal
{
    Vector2 _velocity;
    Collider2D _collider;
    
    [Header("Contact Filter")]
    [SerializeField] private ContactFilter2D _contactFilter;
    
    [Header("settings")]
    [SerializeField] private float _jumpAngle;
    [SerializeField] private float _jumpStrength;
    [SerializeField] private float _jumpStrengthWithInput;

    private int _orientation = 1;
    
    void Jump()
    {
        _velocity = 
            new Vector2(Mathf.Cos(_jumpAngle) * _orientation, Mathf.Sin(_jumpAngle))
            * (!Input.GetKey(KeyCode.Mouse0) ? _jumpStrength : _jumpStrengthWithInput); 
    }

    void FixedUpdate()
    {
        int collisionCount = 0;
        RaycastHit2D[] hits = new RaycastHit2D[2];
        while (_collider.Cast(_velocity, _contactFilter, hits) > 0 && collisionCount < 5)
        {
            if (Vector2.Dot(hits[0].normal, Vector2.up) > .7f)
                Jump();
            else
                _velocity = Vector2.Reflect(_velocity, hits[0].normal)*1.1f;
            
            collisionCount ++;
        }
        
        //apply velocity
        transform.position += (Vector3)(_velocity * Time.fixedDeltaTime);
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
