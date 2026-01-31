using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mask : Pawn
{
    [Header("references")]
    [SerializeField] Rigidbody2D _rb;

    [Header("parameters")]
    [SerializeField] float _launchStrength;

    private void Awake()
    {
        TryGetComponent(out _rb);
    }

    private void Update()
    {
        //transform.right = _rb.linearVelocity;
    }

    public override void GainControl(Pawn lastPawn)
    {
        base.GainControl(lastPawn);

        Animal animal = lastPawn as Animal;

        transform.position = animal.maskSocket.position;
        gameObject.SetActive(true);

        Launch(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }

    public override void LooseControl()
    {
        base.LooseControl();

        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isControlled)
            return;

        if (collision.gameObject.TryGetComponent(out Pawn animal))
        {
            PossessPawn(animal);
        }
        else
            Die();
    }

    void Launch(Vector2 direction)
    {
        Vector2 pos = new Vector2(transform.position.x, transform.position.y);
        _rb.AddForce((direction - pos).normalized * _launchStrength,ForceMode2D.Impulse);
    }

    void Die()
    {
        GameManager.Instance.Restart();
        print("MEURSSSSSSSSSSSSSSSSSSS");
    }
}
