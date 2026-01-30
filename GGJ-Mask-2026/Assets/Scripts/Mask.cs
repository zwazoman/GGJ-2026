using UnityEngine;
using UnityEngine.SceneManagement;

public class Mask : Pawn
{
    public override void GainControl(Pawn targetPawn)
    {
        base.GainControl(targetPawn);

        gameObject.SetActive(true);
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

    

    public void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
