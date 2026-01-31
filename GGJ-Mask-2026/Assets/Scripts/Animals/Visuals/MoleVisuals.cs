using System.Globalization;
using UnityEngine;

public class MoleVisuals : AnimalVisual
{
    [SerializeField] Animator _animator;

    Mole _mole;

    private void Awake()
    {
        _mole = _animal as Mole;
    }

    protected override void Start()
    {
        base.Start();

        _mole.OnDig += Dig;
        _mole.OnReach += Reach;
    }

    void Dig()
    {
        _animator.SetBool("isDigging", true);
    }

    void Reach()
    {
        _animator.SetBool("isDigging", false);
    }
}
