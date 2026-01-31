using UnityEngine;

public class Mole : Animal
{
    [SerializeField] Transform _collCenter;
    [SerializeField] LayerMask _layerMask;

    [SerializeField] float _speed = 1;

    bool _stopped = true;
    bool _moves = false;

    Vector2 direction = Vector2.zero;

    protected override void Update()
    {
        if (!isControlled)
            return;

        if (Input.GetKeyDown(KeyCode.Mouse0) && _stopped == true)
        {
            RaycastHit2D? hit = Physics2D.Raycast(_collCenter.position, (Camera.main.ScreenToWorldPoint(Input.mousePosition) - _collCenter.position).normalized, 1, _layerMask);

            if (hit != null)
            {
                print("ça part");
                direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                _moves = true;
            }
            else
                print("ça part pas");
        }

        if (_moves)
        {
            transform.Translate(direction * _speed * Time.deltaTime); 
        }

        if(Physics2D.OverlapCircle(_collCenter.position, .5f))
        {

        }
    }
}
