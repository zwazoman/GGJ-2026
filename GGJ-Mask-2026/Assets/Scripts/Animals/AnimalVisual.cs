using UnityEngine;

public class AnimalVisual : MonoBehaviour
{
    [SerializeField] GameObject _maskVisual;

    [SerializeField] protected Animal _animal;

    protected virtual void Start()
    {
        _animal.OnGainControl += EquipMask;
        _animal.OnLooseControl += UnEquipMask;
    }

    void EquipMask()
    {
        if(_maskVisual != null)
            _maskVisual.SetActive(true);
    }

    void UnEquipMask()
    {
        if (_maskVisual != null)
            _maskVisual.SetActive(false);
    }

    void Die()
    {

    }
}
