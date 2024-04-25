using System.Collections.Generic;
using UnityEngine;
using Zenject;
public class CheckAllowHit : MonoBehaviour
{
    private int sideOur;
    private int sideEnemy;
    private bool spell;
    private int state;
    private CharacterPlacement _characterPlacement;

    [Inject]
    private void Construct(CharacterPlacement characterPlacement)
    {
        _characterPlacement = characterPlacement;
    }
    public void CheckUnit(int state, bool spell, bool notMe = false)
    {
        this.state = state;
        this.spell = spell;
        sideOur = Turns.turnUnit.ParentCircle.Side;
        if (sideOur == 1) sideEnemy = 0;
        else sideEnemy = 1;
        Clear();
        //БЛИЖНИК
        if (state == 0)
        {
            //Проверка верхнего ближника
            if (Turns.turnUnit.ParentCircle == _characterPlacement.CirclesMap[sideOur, 0])
            {
                int count = 0;
                if (_characterPlacement.CirclesMap[sideEnemy, 0].ChildCharacter != null)
                {
                    MayHit(0);
                    count++;
                }
                if (_characterPlacement.CirclesMap[sideEnemy, 2].ChildCharacter != null)
                {
                    MayHit(2);
                    count++;
                }
                if (count == 0)
                {
                    if (_characterPlacement.CirclesMap[sideEnemy, 4].ChildCharacter != null)
                    {
                        MayHit(4);
                    }
                    else
                    {
                        count = 0;
                        if (_characterPlacement.CirclesMap[sideEnemy, 1].ChildCharacter != null)
                        {
                            MayHit(1);
                            count++;
                        }
                        if (_characterPlacement.CirclesMap[sideEnemy, 3].ChildCharacter != null)
                        {
                            MayHit(3);
                            count++;
                        }
                        if (count == 0)
                        {
                            if (_characterPlacement.CirclesMap[sideEnemy, 5].ChildCharacter != null)
                            {
                                MayHit(5);
                            }
                        }
                    }
                }
            }
            //Проверка нижнего ближника правого
            if (Turns.turnUnit.ParentCircle == _characterPlacement.CirclesMap[sideOur, 4])
            {
                int count = 0;
                if (_characterPlacement.CirclesMap[sideEnemy, 4].ChildCharacter != null)
                {
                    MayHit(4);
                    count++;
                }
                if (_characterPlacement.CirclesMap[sideEnemy, 2].ChildCharacter != null)
                {
                    MayHit(2);
                    count++;
                }
                if (count == 0)
                {
                    if (_characterPlacement.CirclesMap[sideEnemy, 0].ChildCharacter != null)
                    {
                        MayHit(0);
                    }
                    else
                    {
                        count = 0;
                        if (_characterPlacement.CirclesMap[sideEnemy, 5].ChildCharacter != null)
                        {
                            MayHit(5);
                            count++;
                        }
                        if (_characterPlacement.CirclesMap[sideEnemy, 3].ChildCharacter != null)
                        {
                            MayHit(3);
                            count++;
                        }
                        if (count == 0)
                        {
                            if (_characterPlacement.CirclesMap[sideEnemy, 1].ChildCharacter != null)
                            {
                                MayHit(1);
                            }
                        }
                    }
                }
            }
            //Проверка среднего ближника правого
            if (Turns.turnUnit.ParentCircle == _characterPlacement.CirclesMap[sideOur, 2])
            {
                int count = 0;
                if (_characterPlacement.CirclesMap[sideEnemy, 0].ChildCharacter != null)
                {
                    MayHit(0);
                    count++;
                }
                if (_characterPlacement.CirclesMap[sideEnemy, 2].ChildCharacter != null)
                {
                    MayHit(2);
                    count++;
                }
                if (_characterPlacement.CirclesMap[sideEnemy, 4].ChildCharacter != null)
                {
                    MayHit(4);
                    count++;
                }
                if (count == 0)
                {
                    if (_characterPlacement.CirclesMap[sideEnemy, 1].ChildCharacter != null)
                    {
                        MayHit(1);
                    }
                    if (_characterPlacement.CirclesMap[sideEnemy, 3].ChildCharacter != null)
                    {
                        MayHit(3);
                    }
                    if (_characterPlacement.CirclesMap[sideEnemy, 5].ChildCharacter != null)
                    {
                        MayHit(5);
                    }
                }
            }
        }
        //ДАЛЬНИК
        else if (state == 1)
        {
            for (int i = 0; i < 6; i++) if (_characterPlacement.CirclesMap[sideEnemy, i].ChildCharacter != null) MayHit(i);
        }
        //БАФФ
        else if (state == 2)
        {
            for (int i = 0; i < 6; i++)
            {
                if (_characterPlacement.CirclesMap[sideOur, i].ChildCharacter == null) continue;
                if (notMe && _characterPlacement.CirclesMap[sideOur, i] != Turns.turnUnit) MayHit(i);
            }
        }
    }
    //Подсвечивание вражеских кругов которых можно поразить
    private void MayHit(int index)
    {
        if (Turns.turnUnit.ParentCircle.Side == BattleNetwork.sideOnBattle && PlayerData.ai != 2)
        {
            if (spell == false && state != 2) _characterPlacement.CirclesMap[sideEnemy, index].PathAnimation.SetCaracterState("circleEnemy");
            else
            {
                if(state != 2) _characterPlacement.CirclesMap[sideEnemy, index].PathAnimation.SetCaracterState("circleSpell");
                else if(_characterPlacement.CirclesMap[sideOur, index].ChildCharacter != Turns.turnUnit) _characterPlacement.CirclesMap[sideOur, index].PathAnimation.SetCaracterState("circleSpell");
            }
        }
        if (state != 2)
        {
            _characterPlacement.CirclesMap[sideEnemy, index].ChildCharacter.CharacterState.MakeStep(true);
            Turns.listAllowHit.Add(_characterPlacement.CirclesMap[sideEnemy, index].ChildCharacter);
        }
        else
        {
            _characterPlacement.CirclesMap[sideOur, index].ChildCharacter.CharacterState.ChangeAllowHit(true);
            Turns.listAllowHit.Add(_characterPlacement.CirclesMap[sideOur, index].ChildCharacter);
        }
    }
    //Возврат анимации всех кругов в idle
    public void TurnOver()
    {
        for (int i = 0; i < _characterPlacement.UnitAll.Count; i++)
        {
            if (!_characterPlacement.UnitAll[i].CharacterState.allowHit) continue;
            _characterPlacement.UnitAll[i].ParentCircle.PathAnimation.SetCaracterState("idle");
            _characterPlacement.UnitAll[i].CharacterState.ChangeAllowHit(false);
        }
    }
    public void TurnUnitEffect()
    {
        if (Turns.turnUnit.ParentCircle.Side == BattleNetwork.sideOnBattle) Turns.turnUnit.ParentCircle.PathAnimation.SetCaracterState("circleOur");
        else Turns.turnUnit.ParentCircle.PathAnimation.SetCaracterState("circleEnemyTurn");
    }
    public void Clear()
    {
        for (int i = 0; i < _characterPlacement.UnitAll.Count; i++)
        {
            if (_characterPlacement.UnitAll[i] == Turns.turnUnit) continue;
            _characterPlacement.UnitAll[i].ParentCircle.PathAnimation.SetCaracterState("idle");
            _characterPlacement.UnitAll[i].CharacterState.ChangeAllowHit(false);
        }
    }
}
