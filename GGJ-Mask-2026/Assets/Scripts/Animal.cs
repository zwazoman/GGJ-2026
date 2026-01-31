using UnityEngine;

public class Animal : Pawn
{
    [SerializeField] public Transform maskSocket;

    Pawn mask;
    Collider2D _coll;

    private void Awake()
    {
        TryGetComponent(out _coll);
    }

    public override void GainControl(Pawn lastPawn = null)
    {
        base.GainControl(lastPawn);

        mask = lastPawn;
    }

    private void Update()
    {
        if ((isControlled))
        {
            if(Input.GetKeyDown(KeyCode.Mouse1))
                PossessPawn(mask);
        }
    }

    public override void LooseControl()
    {
        base.LooseControl();

        _coll.enabled = false;
        Destroy(gameObject);
    }
}
