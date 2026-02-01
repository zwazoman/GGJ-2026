using System;
using TMPro;
using UnityEngine;

public class Fish : Animal
{
    public event Action OnJump, OnHitGround;

    [SerializeField] Rigidbody2D _rb;
    [SerializeField] LayerMask _layerMask;

    [SerializeField] float _jumpStrength = 2;
    [SerializeField] float _torqueStrength = 1;
    [SerializeField] float _groundedHeight = 1;

    bool _isGrounded = true;
    bool _input;

    protected override void Awake()
    {
        base.Awake();

        TryGetComponent(out _rb);
    }

    protected override void Update()
    {
        base.Update();

        if (Physics2D.Raycast(transform.position, Vector2.down, _groundedHeight, _layerMask))
            _isGrounded = true;
        else
            _isGrounded = false;

        _input |= Input.GetKeyDown(KeyCode.Mouse0) && isControlled;
    }

    private void FixedUpdate()
    {
        if (_input && _isGrounded)
        {
            print("frétille");

            SFXManager.Instance.PlaySFXClipAtPosition(Sounds.FrogLand, transform.position);

            _rb.AddForce(Vector2.up * _jumpStrength, ForceMode2D.Impulse);
            _rb.AddTorque(_torqueStrength, ForceMode2D.Impulse);
            _input = false;
        }
    }

}
