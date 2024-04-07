using UnityEngine;
public class CheckAllowHit : MonoBehaviour
{
    private int sideOur;
    private int sideEnemy;
    private bool spell;
    private int state;
    public void CheckUnit(int state, bool spell, bool notMe = false)
    {
        this.state = state;
        this.spell = spell;
        sideOur = Turns.turnUnit.sideOnMap;
        if (sideOur == 1) sideEnemy = 0;
        else sideEnemy = 1;
        Clear();
        //БЛИЖНИК
        if (state == 0)
        {
            //Проверка верхнего ближника
            if (Turns.turnUnit.pathCircle == Turns.circlesMap[sideOur, 0])
            {
                int count = 0;
                if (Turns.circlesMap[sideEnemy, 0].newObject != null)
                {
                    MayHit(0);
                    count++;
                }
                if (Turns.circlesMap[sideEnemy, 2].newObject != null)
                {
                    MayHit(2);
                    count++;
                }
                if (count == 0)
                {
                    if (Turns.circlesMap[sideEnemy, 4].newObject != null)
                    {
                        MayHit(4);
                    }
                    else
                    {
                        count = 0;
                        if (Turns.circlesMap[sideEnemy, 1].newObject != null)
                        {
                            MayHit(1);
                            count++;
                        }
                        if (Turns.circlesMap[sideEnemy, 3].newObject != null)
                        {
                            MayHit(3);
                            count++;
                        }
                        if (count == 0)
                        {
                            if (Turns.circlesMap[sideEnemy, 5].newObject != null)
                            {
                                MayHit(5);
                            }
                        }
                    }
                }
            }
            //Проверка нижнего ближника правого
            if (Turns.turnUnit.pathCircle == Turns.circlesMap[sideOur, 4])
            {
                int count = 0;
                if (Turns.circlesMap[sideEnemy, 4].newObject != null)
                {
                    MayHit(4);
                    count++;
                }
                if (Turns.circlesMap[sideEnemy, 2].newObject != null)
                {
                    MayHit(2);
                    count++;
                }
                if (count == 0)
                {
                    if (Turns.circlesMap[sideEnemy, 0].newObject != null)
                    {
                        MayHit(0);
                    }
                    else
                    {
                        count = 0;
                        if (Turns.circlesMap[sideEnemy, 5].newObject != null)
                        {
                            MayHit(5);
                            count++;
                        }
                        if (Turns.circlesMap[sideEnemy, 3].newObject != null)
                        {
                            MayHit(3);
                            count++;
                        }
                        if (count == 0)
                        {
                            if (Turns.circlesMap[sideEnemy, 1].newObject != null)
                            {
                                MayHit(1);
                            }
                        }
                    }
                }
            }
            //Проверка среднего ближника правого
            if (Turns.turnUnit.pathCircle == Turns.circlesMap[sideOur, 2])
            {
                int count = 0;
                if (Turns.circlesMap[sideEnemy, 0].newObject != null)
                {
                    MayHit(0);
                    count++;
                }
                if (Turns.circlesMap[sideEnemy, 2].newObject != null)
                {
                    MayHit(2);
                    count++;
                }
                if (Turns.circlesMap[sideEnemy, 4].newObject != null)
                {
                    MayHit(4);
                    count++;
                }
                if (count == 0)
                {
                    if (Turns.circlesMap[sideEnemy, 1].newObject != null)
                    {
                        MayHit(1);
                    }
                    if (Turns.circlesMap[sideEnemy, 3].newObject != null)
                    {
                        MayHit(3);
                    }
                    if (Turns.circlesMap[sideEnemy, 5].newObject != null)
                    {
                        MayHit(5);
                    }
                }
            }
        }
        //ДАЛЬНИК
        else if (state == 1)
        {
            for (int i = 0; i < 6; i++) if (Turns.circlesMap[sideEnemy, i].newObject != null) MayHit(i);
        }
        //БАФФ
        else if (state == 2)
        {
            for (int i = 0; i < 6; i++)
            {
                if (Turns.circlesMap[sideOur, i].newObject == null) continue;
                if (notMe && Turns.circlesMap[sideOur, i] != Turns.turnUnit) MayHit(i);
            }
        }
    }
    //Подсвечивание вражеских кругов которых можно поразить
    private void MayHit(int index)
    {
        if (Turns.turnUnit.sideOnMap == BattleNetwork.sideOnBattle && PlayerData.ai != 2)
        {
            if (spell == false && state != 2) Turns.circlesMap[sideEnemy, index].PathAnimation.SetCaracterState("circleEnemy");
            else
            {
                if(state != 2) Turns.circlesMap[sideEnemy, index].PathAnimation.SetCaracterState("circleSpell");
                else if(Turns.circlesMap[sideOur, index].newObject != Turns.turnUnit) Turns.circlesMap[sideOur, index].PathAnimation.SetCaracterState("circleSpell");
            }
        }
        if (state != 2)
        {
            Turns.circlesMap[sideEnemy, index].newObject.allowHit = true;
            Turns.listAllowHit.Add(Turns.circlesMap[sideEnemy, index].newObject);
        }
        else
        {
            Turns.circlesMap[sideOur, index].newObject.allowHit = true;
            Turns.listAllowHit.Add(Turns.circlesMap[sideOur, index].newObject);
        }
    }
    //Возврат анимации всех кругов в idle
    public void TurnOver()
    {
        for (int i = 0; i < Turns.listUnitAll.Count; i++)
        {
            if (!Turns.listUnitAll[i].allowHit) continue;
            Turns.listUnitAll[i].pathCircle.PathAnimation.SetCaracterState("idle");
            Turns.listUnitAll[i].allowHit = false;
        }
    }
    public void TurnUnitEffect()
    {
        if (Turns.turnUnit.sideOnMap == BattleNetwork.sideOnBattle) Turns.turnUnit.pathCircle.PathAnimation.SetCaracterState("circleOur");
        else Turns.turnUnit.pathCircle.PathAnimation.SetCaracterState("circleEnemyTurn");
    }
    public void Clear()
    {
        for (int i = 0; i < Turns.listUnitAll.Count; i++)
        {
            if (Turns.listUnitAll[i] == Turns.turnUnit) continue;
            Turns.listUnitAll[i].pathCircle.PathAnimation.SetCaracterState("idle");
            Turns.listUnitAll[i].allowHit = false;
        }
    }
}
