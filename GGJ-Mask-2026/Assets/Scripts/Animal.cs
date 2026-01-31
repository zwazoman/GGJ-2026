using System;
using UnityEngine;

public class Animal : Pawn
{
    public event Action OnGainControl, OnLooseControl;

    [SerializeField] public Transform maskSocket;

    protected Pawn mask;
    protected Collider2D coll;

    protected virtual void Awake()
    {
        TryGetComponent(out coll);
    }

    public override void GainControl(Pawn lastPawn = null)
    {
        base.GainControl(lastPawn);
        mask = lastPawn;
    }

    protected virtual void Update()
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

        coll.enabled = false;
        Destroy(gameObject);
    }
}
