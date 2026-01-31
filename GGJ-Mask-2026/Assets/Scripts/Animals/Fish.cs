using UnityEngine;

public class Fish : Animal
{
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] LayerMask _layerMask;

    [SerializeField] float _jumpStrength = 2;
    [SerializeField] float _torqueStrength = 1;
    [SerializeField] float _groundedHeight = 1;

    bool _isGrounded = true;

    protected override void Awake()
    {
        base.Awake();

        TryGetComponent(out _rb);
    }

    protected override void Update()
    {
        base.Update();

        if (!isControlled)
            return;

        if (Physics2D.Raycast(transform.position, Vector2.down, _groundedHeight, _layerMask))
            _isGrounded = true;
        else
            _isGrounded = false;

        if (Input.GetKeyDown(KeyCode.Mouse0) && _isGrounded)
        {
            print("frétille");
            _rb.AddForce(transform.up * _jumpStrength, ForceMode2D.Impulse);
            _rb.AddTorque(_torqueStrength, ForceMode2D.Impulse);
        }
    }

}
