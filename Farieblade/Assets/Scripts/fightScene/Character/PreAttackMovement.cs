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
        if (_turnUnit.Place == unitTarget.Place) 
            return;
        _moved = true;
        Transform newPosition;
        if (unitTarget.Place % 2 != 0) 
            newPosition = _characterPlacement.CirclesMap[enemySide, unitTarget.Place - 1].transform;
        else
        {
            if (_characterPlacement.CirclesMap[Turns.turnUnit.Side, unitTarget.Place].newObject != null) 
                newPosition = PushCharacter(unitTarget);
            else 
                newPosition = _characterPlacement.CirclesMap[Turns.turnUnit.Side, unitTarget.Place].transform;
        }
        turnUnit.transform.position = newPosition.position;
    }

    private Transform PushCharacter(UnitProperties unitTarget)
    {
        _pushUnit = _characterPlacement.CirclesMap[Turns.turnUnit.Side, unitTarget.Place].newObject;
        if (_pushUnit.Side == 1) _pushUnit.transform.localPosition += new Vector3(2f, 0, 0);
        else _pushUnit.transform.localPosition -= new Vector3(2f, 0, 0);

        _turnUnit.pathParent.transform.SetParent(_pushUnit.GetComponent<UnitProperties>().pathCircle.transform);
        _turnUnit.pathParent.transform.localScale = new Vector2(1, 1);
        return _pushUnit.pathCircle.transform;
    }

    private void TurnOver()
    {
        if (_turnUnit == null || !_moved) 
            return;
        if (_pushUnit != null)
        {
            _pushUnit.transform.position = _pushUnit.pathCircle.transform.position;
            _pushUnit = null;
        }
        _turnUnit.transform.position = _turnUnit.pathCircle.transform.position;
        _turnUnit.pathParent.transform.SetParent(_turnUnit.pathCircle.transform);
        _turnUnit.pathParent.transform.localScale = new Vector2(1, 1);
    }

    private void OnDestroy()
    {
        if(_turns != null) 
            _turns.TurnOver -= TurnOver;
    }
}
