using Unity.VisualScripting;
using UnityEngine;

public class Mole : Animal
{
    [SerializeField] Transform _collCenter;
    [SerializeField] LayerMask _layerMask;

    [SerializeField] float _speed = 10;

    bool _stopped = true;
    bool _moves = false;

    protected override void Update()
    {
        if (!isControlled)
            return;

        if (Input.GetKeyDown(KeyCode.Mouse0) && _stopped == true)
        {
            float dot = (Vector2.Dot((Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized, -transform.right));

            print(dot);

            if (dot <= -0.05)
            {
                print("ça part");

                Vector2 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
                transform.rotation = Quaternion.AngleAxis(Mathf.Rad2Deg * Mathf.Atan2(direction.y, direction.x) +180, transform.forward);

                _stopped = false;
                _moves = true;
            }
        }

        if (_moves)
        {
            transform.Translate(-transform.right * _speed * Time.deltaTime, Space.World);
        }

        Collider2D collider = Physics2D.OverlapPoint(_collCenter.position, _layerMask);
        if (!collider)
        {
            RaycastHit2D hit = Physics2D.Raycast(_collCenter.position, transform.right, 1, _layerMask);
            transform.rotation = Quaternion.AngleAxis(Mathf.Rad2Deg * Mathf.Atan2(hit.normal.y, hit.normal.x) + 180, transform.forward);
            _stopped = true;
            _moves = false;
        }
        else if (collider.gameObject.layer == 6)
        {
            Die();
        }
    }

    void Die()
    {
        GameManager.Instance.Restart();
    }

}
