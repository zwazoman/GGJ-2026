using System;
using UnityEngine;

public class Bee : Animal
{
    public event Action OnFly, OnCollide,OnDie;

    Rigidbody2D _rb;
    [SerializeField] float _speed;
    [SerializeField] LayerMask _layerMask;
    bool _isDead = false;
    bool _move = false;
    public Vector2 direction = Vector2.zero;

    bool _input;
    AudioSource _flySource;

    protected override void Awake()
    {
        base.Awake();
        TryGetComponent(out _rb);
    }

    protected override void Update()
    {
        base.Update();
        _input |= Input.GetKeyDown(KeyCode.Mouse0) && isControlled;
    }

    private void FixedUpdate()
    {
        if (_input && !_move)
        {
            direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
            _move = true;
            _flySource = SFXManager.Instance.PlaySFXClipAtPosition(Sounds.BeeHum, transform.position);
        }
        if (_move && !_isDead)
        {
            transform.Translate(direction * _speed * Time.fixedDeltaTime);
            if (Physics2D.OverlapCircle(transform.position, .5f, _layerMask))
                Die();
        }
    }

    async void Die()
    {
        _isDead = true;
        OnDie?.Invoke();
        await Awaitable.WaitForSecondsAsync(.2f);

        SFXManager.Instance.PlaySFXClipAtPosition(Sounds.BeeHit, transform.position);
        _flySource.Stop();

        if(isControlled) GameManager.Instance.Restart();
        else Destroy(gameObject);
    }

}
