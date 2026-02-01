using System;
using Unity.VisualScripting;
using UnityEngine;

public class Mole : Animal
{
    public event Action OnDig, OnReach;

    [SerializeField] Transform _collCenter;
    [SerializeField] LayerMask _layerMask;

    [SerializeField] float _speed = 10;

    bool _moves = false;
    bool _input;

    AudioSource _digSource;

    protected override void Update()
    {
        base.Update();

        _input |= Input.GetKeyDown(KeyCode.Mouse0) && isControlled;
    }

    private void FixedUpdate()
    {
        if (_input && !_moves)
        {
            float dot = Vector2.Dot((Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized, -transform.right);

            print(dot);

            if (dot <= -0.05)
            {
                OnDig?.Invoke();

                _digSource = SFXManager.Instance.PlaySFXClipAtPosition(Sounds.MoleDig, transform.position);

                Vector2 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
                transform.rotation = Quaternion.AngleAxis(Mathf.Rad2Deg * Mathf.Atan2(direction.y, direction.x) + 180, transform.forward);

                _moves = true;
            }
        }

        if (_moves)
        {
            transform.Translate(-transform.right * _speed * Time.deltaTime, Space.World);

            Collider2D[] colliders = Physics2D.OverlapPointAll(_collCenter.position, _layerMask);

            if (colliders.Length == 0)
            {
                RaycastHit2D hit = Physics2D.Raycast(_collCenter.position, transform.right, 1, _layerMask);
                transform.rotation = Quaternion.AngleAxis(Mathf.Rad2Deg * Mathf.Atan2(hit.normal.y, hit.normal.x) + 180, transform.forward);
                _moves = false;

                SFXManager.Instance.PlaySFXClipAtPosition(Sounds.MoleSpeak, transform.position);

                _digSource.Stop();

                OnReach?.Invoke();
                _input = false;
            }
            else
                foreach (Collider2D collider2d in colliders)
                {
                    if (collider2d.gameObject.layer == 6)
                    {
                        Die();
                    }
                }
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && !_moves && Vector2.Dot((Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized, -transform.right) >= 0.05)
        {
            PossessPawn(mask);
        }
    }

    void Die()
    {
        GameManager.Instance.Restart();
    }

}
