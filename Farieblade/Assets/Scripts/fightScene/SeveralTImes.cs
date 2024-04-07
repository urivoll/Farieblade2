using Spine.Unity;
using UnityEngine;

public class SeveralTImes : MonoBehaviour
{
    private Unit _unit;
    [SerializeField] private AnimationReferenceAsset _attack2;
    [SerializeField] private AnimationReferenceAsset _attack3;

    void Start()
    {
        _unit = transform.parent.gameObject.transform.parent.GetComponent<Unit>();
        if (_unit.grade > 4 && _unit.grade < 10)
        {
            gameObject.GetComponent<UnitAnimation>()._attack = _attack2;
            gameObject.GetComponent<UnitProperties>().times = 2;
        }
        else if (_unit.grade == 10)
        {
            gameObject.GetComponent<UnitAnimation>()._attack = _attack3;
            gameObject.GetComponent<UnitProperties>().times = 3;
        }
    }
}
