using UnityEngine;

public class BeeVisuals : AnimalVisual
{
    Bee _bee;

    [Header("Parameters")]
    [SerializeField] float flightRotation = 10;

    private void Awake()
    {
        _bee = _animal as Bee;
    }

    protected override void Start()
    {
        base.Start();

        _bee.OnFly += Fly;
    }

    void Fly()
    {
        transform.GetChild(0).rotation = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z + flightRotation));
        transform.localScale = new Vector3(Mathf.Sign(_bee.direction.x), transform.localScale.y, transform.localScale.z);
    }
}
