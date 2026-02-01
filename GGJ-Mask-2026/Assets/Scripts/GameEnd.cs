using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnd : MonoBehaviour
{
    [SerializeField] private float radius;

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
    void FixedUpdate()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, radius, LayerMask.GetMask("Animal"));
        if (hit)
        {
            if (hit.gameObject.TryGetComponent(out Pawn animal) )
            {
                GameManager.Instance.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
            }
        }
    }
}
