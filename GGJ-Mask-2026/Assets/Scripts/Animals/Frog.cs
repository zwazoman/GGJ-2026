using UnityEngine;

public class Frog : Controllable
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
        RaycastHit2D[] hits = new RaycastHit2D[2];
        if(_collider.Cast(_velocity,_contactFilter,hits))
        
        //apply velocity
        transform.position += (Vector3)(_velocity * Time.fixedDeltaTime);
    }
    public override void GainControl()
    {
        base.GainControl();
    }

    public override void LooseControl()
    {
        base.LooseControl();
    }
}
