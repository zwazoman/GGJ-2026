using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mask : Pawn
{
    [Header("references")]
    [SerializeField] Rigidbody2D _rb;

    [Header("parameters")]
    [SerializeField] float _launchStrength;

    private Pawn _lastPawn = null;
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
        
        _lastPawn = lastPawn;
        Animal animal = lastPawn as Animal;

        transform.position = animal.maskSocket.position;
        gameObject.SetActive(true);
        
        Launch(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
    }

    public override void LooseControl()
    {
        base.LooseControl();

        gameObject.SetActive(false);
    }

    void FixedUpdate()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, .5f, LayerMask.GetMask("Animal"));
        if (hit)
        {
            if (hit.gameObject.TryGetComponent(out Pawn animal) && _lastPawn != animal)
            {
                SFXManager.Instance.PlaySFXClipAtPosition(Sounds.MaskAttach, transform.position);
                PossessPawn(animal);
            }
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isControlled)
            return;

        Die();
    }

    void Launch(Vector2 direction)
    {
        SFXManager.Instance.PlaySFXClipAtPosition(Sounds.MaskLaunch, transform.position);

        _rb.AddForce(direction.normalized * _launchStrength,ForceMode2D.Impulse);
    }

    void Die()
    {
        GameManager.Instance.Restart();
        print("MEURSSSSSSSSSSSSSSSSSSS");
    }
}
