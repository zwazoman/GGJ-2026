using UnityEngine;
using UnityEngine.WSA;

public class Bee : Animal
{
    Rigidbody2D _rb;
    [SerializeField] float _speed;

    bool _move = false;
    Vector2 direction = Vector2.zero;

    private void Awake()
    {
        TryGetComponent(out _rb);
    }

    private void Update()
    {
        if (isControlled)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && _move == false)
            {
                direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
                _move = true;
            }
            if (_move)
                transform.Translate(direction * _speed * Time.deltaTime);

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameManager.Instance.Restart();
    }

}
