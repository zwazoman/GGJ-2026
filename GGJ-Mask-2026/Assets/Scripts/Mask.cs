using UnityEngine;

public class Mask : Controllable
{
    private void Start()
    {
        GainControl();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent(out Controllable animal))
        {
            LooseControl();

            animal.GainControl();
        }
    }
}
