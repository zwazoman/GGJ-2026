using UnityEngine;
using UnityEngine.SceneManagement;

public class Mask : Pawn
{
    [Header("references")]
    [SerializeField] Rigidbody _rb;

    [Header("parameters")]
    [SerializeField] float _launchStrength;


    private void Awake()
    {
        TryGetComponent(out _rb);
    }

    public override void GainControl(Pawn targetPawn)
    {
        base.GainControl(targetPawn);

        gameObject.SetActive(true);

        Launch(Input.mousePosition);
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
            LooseControl();

            animal.GainControl();
        }
        else
            Die();
    }

    void Launch(Vector2 direction)
    {
        Vector2 pos = new Vector2(transform.position.x, transform.position.y);
        _rb.AddForce((direction - pos).normalized * _launchStrength);
    }

    void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
