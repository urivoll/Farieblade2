using UnityEngine;
using Zenject;

public class PreAttackMovement : MonoBehaviour
{
    private bool _moved = false;
    private UnitProperties _pushUnit;
    private UnitProperties _turnUnit;
    private Turns _turns;
    private CharacterPlacement _characterPlacement;
    public int enemySide;
    [Inject]
    private void Construct(Turns turns, CharacterPlacement characterPlacement)
    {
        _turns = turns;
        _characterPlacement = characterPlacement;
        _turns.TurnOver += TurnOver;
    }

    public void TryMove(UnitProperties turnUnit, UnitProperties unitTarget)
    {
        _turnUnit = turnUnit;
        if (_turnUnit.ParentCircle.Place == unitTarget.ParentCircle.Place) 
            return;
        _moved = true;
        Transform newPosition;
        if (unitTarget.ParentCircle.Place % 2 != 0) 
            newPosition = _characterPlacement.CirclesMap[enemySide, unitTarget.ParentCircle.Place - 1].transform;
        else
        {
            if (_characterPlacement.CirclesMap[Turns.turnUnit.ParentCircle.Side, unitTarget.ParentCircle.Place].ChildCharacter != null) 
                newPosition = PushCharacter(unitTarget);
            else 
                newPosition = _characterPlacement.CirclesMap[Turns.turnUnit.ParentCircle.Side, unitTarget.ParentCircle.Place].transform;
        }
        turnUnit.transform.position = newPosition.position;
    }

    private Transform PushCharacter(UnitProperties unitTarget)
    {
        _pushUnit = _characterPlacement.CirclesMap[Turns.turnUnit.ParentCircle.Side, unitTarget.ParentCircle.Place].ChildCharacter;
        if (_pushUnit.ParentCircle.Side == 1) _pushUnit.transform.localPosition += new Vector3(2f, 0, 0);
        else _pushUnit.transform.localPosition -= new Vector3(2f, 0, 0);

        _turnUnit.pathParent.transform.SetParent(_pushUnit.GetComponent<UnitProperties>().ParentCircle.transform);
        _turnUnit.pathParent.transform.localScale = new Vector2(1, 1);
        return _pushUnit.ParentCircle.transform;
    }

    private void TurnOver(UnitProperties unitProperties)
    {
        if (_turnUnit == null || !_moved) 
            return;
        if (_pushUnit != null)
        {
            _pushUnit.transform.position = _pushUnit.ParentCircle.transform.position;
            _pushUnit = null;
        }
        _turnUnit.transform.position = _turnUnit.ParentCircle.transform.position;
        _turnUnit.pathParent.transform.SetParent(_turnUnit.ParentCircle.transform);
        _turnUnit.pathParent.transform.localScale = new Vector2(1, 1);
    }

    private void OnDestroy()
    {
        if(_turns != null) 
            _turns.TurnOver -= TurnOver;
    }
}
