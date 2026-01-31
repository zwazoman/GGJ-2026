using UnityEngine;
using UnityEngine.WSA;

public class Bee : Animal
{
    Rigidbody2D _rb;
    [SerializeField] float _speed;
    [SerializeField] LayerMask _layerMask;

    bool _move = false;
    Vector2 direction = Vector2.zero;

    protected override void Awake()
    {
        base.Awake();
        TryGetComponent(out _rb);
    }

    private void FixedUpdate()
    {
        if (isControlled)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && _move == false)
            {
                direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
                _move = true;
            }
            if (_move)
            {
                transform.Translate(direction * _speed * Time.deltaTime);
                if (Physics2D.OverlapCircle(transform.position,.5f,_layerMask))
                    Die();
            }
        }
    }

    void Die()
    {
        GameManager.Instance.Restart();
    }

}
