using UnityEngine;

public class Animal : Pawn
{
    Pawn mask;

    public override void GainControl(Pawn lastPawn = null)
    {
        base.GainControl(lastPawn);

        mask = lastPawn;
    }

    private void Update()
    {
        if ((isControlled))
        {
            if(Input.GetKeyDown(KeyCode.Mouse0))
                PossessPawn(mask);
        }
    }
}
