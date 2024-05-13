using UnityEngine;
using Zenject;
public class CheckAllowHit : MonoBehaviour
{
    private int sideOur;
    private int sideEnemy;
    private bool spell;
    private int _type;
    [Inject] private CharacterPlacement _characterPlacement;

    public void CheckUnit(int type, bool spell, bool notMe = false)
    {
        _type = type;
        this.spell = spell;
        sideOur = Turns.turnUnit.ParentCircle.Side;
        sideEnemy = (sideOur == 1) ? 0 : 1;
        Clear();
        //БЛИЖНИК
        if (type == 0)
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
        else if (type == 1)
        {
            for (int i = 0; i < 6; i++) if (_characterPlacement.CirclesMap[sideEnemy, i].ChildCharacter != null) MayHit(i);
        }
        //БАФФ
        else if (type == 2)
        {
            for (int i = 0; i < 6; i++)
            {
                if (_characterPlacement.CirclesMap[sideOur, i].ChildCharacter == null) continue;
                if (notMe && _characterPlacement.CirclesMap[sideOur, i] != Turns.turnUnit) MayHit(i);
            }
        }
    }

    private void MayHit(int index)
    {
        CircleProperties enemyCircle = _characterPlacement.CirclesMap[sideEnemy, index];
        CircleProperties ourCircle = _characterPlacement.CirclesMap[sideOur, index];
        HighlightCircle(enemyCircle, ourCircle);
        print(_type);
        if (_type != 2)
        {
            enemyCircle.ChildCharacter.CharacterState.MakeStep(true);
            Turns.listAllowHit.Add(enemyCircle.ChildCharacter);
        }
        else
        {
            ourCircle.ChildCharacter.CharacterState.ChangeAllowHit(true);
            Turns.listAllowHit.Add(ourCircle.ChildCharacter);
        }
    }

    //Подсвечивание вражеских кругов которых можно поразить
    private void HighlightCircle(CircleProperties enemyCircle, CircleProperties ourCircle)
    {
        if (Turns.turnUnit.ParentCircle.Side == BattleNetwork.sideOnBattle && PlayerData.ai != 2)
        {
            if (spell == false && _type != 2)
            {
                enemyCircle.PathAnimation.SetCaracterState("circleEnemy");
            }
            else
            {
                if (_type != 2)
                {
                    enemyCircle.PathAnimation.SetCaracterState("circleSpell");
                }

                else if (ourCircle.ChildCharacter != Turns.turnUnit)
                {
                    ourCircle.PathAnimation.SetCaracterState("circleSpell");
                }
            }
        }
    }

    //Возврат анимации всех кругов в idle
    public void TurnOver()
    {
        for (int i = 0; i < _characterPlacement.UnitAll.Count; i++)
        {
            if (!_characterPlacement.UnitAll[i].CharacterState.AllowHit) continue;
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
