using UnityEngine;

public class Pawn : MonoBehaviour
{
    [HideInInspector] public bool isControlled = false;

    public void PossessPawn(Pawn targetPawn)
    {
        LooseControl();
        targetPawn.GainControl(this);
    }

    public virtual void GainControl(Pawn lastPawn = null)
    {
        isControlled = true;
    }

    public virtual void LooseControl()
    {
        isControlled = false;
    }
}
