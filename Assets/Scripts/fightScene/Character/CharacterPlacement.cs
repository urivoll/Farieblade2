using System.Collections.Generic;
using UnityEngine;

public class CharacterPlacement : MonoBehaviour
{
    public CircleProperties[,] CirclesMap => _circlesMap;
    public CircleProperties[] CircleAll => _circleAll;
    public List<UnitProperties> UnitAll => _unitAll;
    public List<UnitProperties> UnitLeft => _unitLeft;
    public List<UnitProperties> UnitRight => _unitRight;
    public List<UnitProperties> UnitEndLeft => _unitEndLeft;
    public List<UnitProperties> UnitEndRight => _unitEndRight;
    public List<UnitProperties> UnitEnemy => _unitEnemy;
    public List<UnitProperties> UnitOur => _unitOur;
    public int EnemySide => _enemySide;

    private List<UnitProperties> _unitAll = new();
    private List<UnitProperties> _unitLeft = new();
    private List<UnitProperties> _unitRight = new();
    private List<UnitProperties> _unitEndLeft = new();
    private List<UnitProperties> _unitEndRight = new();
    private List<UnitProperties> _unitEnemy;
    private List<UnitProperties> _unitOur;
    private CircleProperties[,] _circlesMap = new CircleProperties[2, 6];

    [SerializeField] private CircleProperties[] _circleAll;
    [SerializeField] private CircleProperties[] _circleLeftPrefub;
    [SerializeField] private CircleProperties[] _circleRightPrefub;
    private int _enemySide;
    public void Definition()
    {
        for (int i = 0; i < 6; i++)
        {
            _circlesMap[0, i] = _circleLeftPrefub[i];
            _circlesMap[1, i] = _circleRightPrefub[i];
        }
    }
    public void DefinitionSides(UnitProperties turnUnit)
    {
        _enemySide = (turnUnit.ParentCircle.Side == 1) ? 0 : 1;
        _unitEnemy = (turnUnit.ParentCircle.Side == 1) ? _unitLeft : _unitRight;
        _unitOur = (turnUnit.ParentCircle.Side == 1) ? _unitRight : _unitLeft;
        StartIni.turnEffect[turnUnit.ParentCircle.Side].SetActive(true);
    }
    public void InitializationCircles()
    {
        for (int i = 0; i < _circleAll.Length; i++)
            _circleAll[i].Init();
        StartIni.setViewTrops?.Invoke();
        //GetComponent<StartIni>().animatorShake.SetTrigger("start");
        _unitEndLeft.AddRange(_unitLeft);
        _unitEndRight.AddRange(_unitRight);
    }
    public void CheckTurnEnd()
    {
        int count = 0;
        for (int i = 0; i < _unitAll.Count; i++)
        {
            if (_unitAll[i].CharacterState.went == false)
            {
                count++;
                break;
            }
        }
        if (count == 0)
        {
            for (int i = 0; i < _unitAll.Count; i++)
            {
                _unitAll[i].CharacterState.MakeStep(false);
                _unitAll[i].transform.Find("UI/turn").gameObject.GetComponent<BarAnimation>().SetCaracterState("idle");
            }
            //numberTurn += 1;
            //_numberTurn.text = Convert.ToString(numberTurn);
        }
    }
    public void AddCharacter(int sideOnMap, UnitProperties newObject)
    {
        if (sideOnMap == 1)
            _unitRight.Add(newObject);
        else
            _unitLeft.Add(newObject);
        _unitAll.Add(newObject);
    }
    public void DeleteCharacter(UnitProperties unitProperties, int side)
    {
        _unitAll.Remove(unitProperties);
        if (side == 0) _unitLeft.Remove(unitProperties);
        else if (side == 1) _unitRight.Remove(unitProperties);
    }
}
