using UnityEngine;

public class AnimalVisual : MonoBehaviour
{
    [SerializeField] GameObject _maskVisual;

    [SerializeField] protected Animal _animal;

    protected virtual void Start()
    {
        _animal.OnGainControl += EquipMask;
    }

    void EquipMask()
    {
        _maskVisual.SetActive(true);
    }

    void Die()
    {

    }
}
