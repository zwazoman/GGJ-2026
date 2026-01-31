using UnityEngine;

public class Fish : Animal
{
    [SerializeField] Rigidbody2D _rb;

    [SerializeField] float _jumpStrength = 2;
    [SerializeField] float _torqueStrength = 5;


    protected override void Awake()
    {
        base.Awake();

        TryGetComponent(out _rb);
    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _rb.AddForce(transform.up * _jumpStrength, ForceMode2D.Impulse);
            _rb.AddTorque(_torqueStrength, ForceMode2D.Impulse);
        }
    }

}
