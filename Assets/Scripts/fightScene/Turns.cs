using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;
public class Turns : MonoBehaviour
{
    public static bool hitDone = false;
    public static bool finishEndEvent = false;
    public static Transform circlesTransform;
    public static List<UnitProperties> listAllowHit = new();
    public static List<Dictionary<string, int>> currentTryDeath = new();
    public static Action<UnitProperties> takeDamage;
    public static Action<UnitProperties, Dictionary<string, int>> tryDeath;
    public static Action<UnitProperties, UnitProperties, List<Dictionary<string, int>>> shooterPunch;
    public static Action<UnitProperties, UnitProperties, List<Dictionary<string, int>>> punch;
    public static Action<UnitProperties, UnitProperties> beforePunch;
    public static Action<GameObject, UnitProperties> getDebuff;
    public static Action<int, GameObject, char> getEnergy;
    public static UnitProperties unitChoose;
    public static UnitProperties turnUnit = null; 
    [HideInInspector] public bool endTurn = false;
    private bool win;
    private int gameCount = 0;
    [SerializeField] private EndFight endFightObject;
    [SerializeField] private GameObject _endPanel;
    [SerializeField] private GameObject _defendPrefub;
    [SerializeField] private GameObject[] defaulUnits;
    [SerializeField] private SideUnitUi sideUnitUI;
    public static bool aiMay = false;
    private AfterStep attackData;
    [SerializeField] private MapLocation _mapLocation;

    public int numberTurn = 1;
    
    [SerializeField] private TextMeshProUGUI _numberTurn;
    public Action<UnitProperties> TurnOver;
    [Inject] private CharacterPlacement _characterPlacement;
    [SerializeField] private CheckAllowHit _checkAllowHit;

    private void DefinisionStaticObjects()
    {
        if (PlayerData.defaultCards == null) 
            PlayerData.defaultCards = defaulUnits;
        _characterPlacement.Definition();
    }
    private IEnumerator GetAura()
    {
        yield return new WaitForSeconds(2);
        if (BattleNetwork.auraSend.Count == 0) yield break;
        //Аура
        for (int i = 0; i < BattleNetwork.auraSend.Count; i++)
        {
            UnitProperties unit = _characterPlacement.CirclesMap[BattleNetwork.auraSend[i]["side"], BattleNetwork.auraSend[i]["place"]].ChildCharacter;
            StartCoroutine(unit.Aura.GetAura(BattleNetwork.auraSend[i]));
            while (finishEndEvent == false) yield return null;
            finishEndEvent = false;
        }
        yield return new WaitForSeconds(1);
    }


    public IEnumerator Start2()
    {
        DefinisionStaticObjects();
        Coroutine cor;
        while (BattleNetwork.connected == false)
            yield return null;
        _mapLocation.SetBackground();
        _characterPlacement.InitializationCircles();
        cor = StartCoroutine(GetAura());
        yield return cor;
        cor = StartCoroutine(BeforeTurn());
        yield return cor;
        SetEndFight();
    }
    private IEnumerator BeforeTurn()
    {
        yield return new WaitForSeconds(0.1f);
        while (true)
        {
            gameCount++;
            while (BattleNetwork.doingQueue.Count < gameCount) yield return null;
            yield return new WaitForSeconds(0.2f);
            currentTryDeath = BattleNetwork.doingQueue[gameCount - 1].tryDeathSend;
            var currentDoing = BattleNetwork.doingQueue[gameCount - 1];
            if (currentDoing.array[0] == -444) break;
            turnUnit = _characterPlacement.CirclesMap[currentDoing.array[0], currentDoing.array[1]].ChildCharacter;
            StartIni.Bar();
            _characterPlacement.DefinitionSides(turnUnit);
            yield return StartCoroutine(PeriodicEffects());
            if (currentDoing.endDebuff.Count > 0)
            {
                for (int i = 0; i < currentDoing.endDebuff.Count; i++)
                {
                    for (int i2 = 0; i2 < turnUnit.DebuffList.Count; i2++)
                    {
                        if (turnUnit.DebuffList[i2].GetComponent<AbstractSpell>().id == currentDoing.endDebuff[i])
                            Destroy(turnUnit.DebuffList[i2]);
                    }
                }
            }
            if (turnUnit != null && !turnUnit.CharacterState.Paralize)
            {
                _checkAllowHit.TurnUnitEffect();
                if (turnUnit.ParentCircle.Side == BattleNetwork.sideOnBattle)
                {
                    sideUnitUI.SetSpells();
                    if (PlayerData.ai == 2) GetComponent<BattleAI>().AI0();
                }
            }
            aiMay = true;

            while (BattleNetwork.attackResultQueue.Count < gameCount) yield return null;
            yield return new WaitForSeconds(0.2f);
            aiMay = false;
            if (turnUnit != null)
            {
                if(turnUnit.ParentCircle.Side == BattleNetwork.sideOnBattle) sideUnitUI.Exit();
                StartIni.work = false;
                StartIni.playersBars[turnUnit.ParentCircle.Side].gameObject.SetActive(false);
            }
            yield return StartCoroutine(AfterTurn());
            DoDefaultValues();
            yield return StartCoroutine(AfterMoveEffects());
            yield return new WaitForSeconds(0.5f);
            _characterPlacement.CheckTurnEnd();
            hitDone = false;
        }
        win = (BattleNetwork.doingQueue[gameCount - 1].array[1] == BattleNetwork.sideOnBattle) ? true : false;
    }
    private IEnumerator AfterTurn()
    {
        attackData = BattleNetwork.attackResultQueue[gameCount - 1];
        currentTryDeath = attackData.tryDeathSend;
        if (turnUnit != null && turnUnit.Spells.modeIndex != attackData.mode)
        {
            turnUnit.Spells.SetState(attackData.mode);
            yield return new WaitForSeconds(0.5f);
        }
        if (attackData.spell != -20) turnUnit.Energy.SetEnergy(attackData.energy);
        //Удар
        _checkAllowHit.TurnOver();
        if (attackData.spell == -666)
        {
            turnUnit.Weapon.AttackedUnit(attackData.makeMove);
        }
        //Защита
        else if(attackData.spell == -10)
        {
            Instantiate(_defendPrefub, turnUnit.PathDebuffs);
            hitDone = true;
        }
        //Баш или смерть
        else if(attackData.spell == -20) hitDone = true;
        //Магия
        else turnUnit.Spells.UseActive(attackData.spell, attackData.makeMove);
        while (hitDone == false) yield return null;
        TurnOver?.Invoke(turnUnit);
    }


    public IEnumerator PeriodicEffects()
    {
        if (BattleNetwork.doingQueue[gameCount - 1].periodicDebuff.Count == 0) yield break;
        List<Dictionary<string, int>> tempList = BattleNetwork.doingQueue[gameCount - 1].periodicDebuff;
        for (int i = 0; i < tempList.Count; i++)
        {
            for (int i2 = 0; i2 < turnUnit.DebuffList.Count; i2++)
            {
                if (turnUnit.DebuffList[i2].GetComponent<AbstractSpell>().id != tempList[i]["id"])
                    continue;
                turnUnit.DebuffList[i2].GetComponent<AbstractSpell>().PeriodicMethod(tempList[i]);
                yield return new WaitForSeconds(0.6f);
                break;
            }
        }
    }
    public IEnumerator AfterMoveEffects()
    {
        if (attackData.afterStepDebuffs.Count == 0) yield break;
        yield return new WaitForSeconds(0.8f);
        for (int i = 0; i < attackData.afterStepDebuffs.Count; i++)
        {
            UnitProperties unit = _characterPlacement.CirclesMap[attackData.afterStepDebuffs[i]["side"], attackData.afterStepDebuffs[i]["place"]].ChildCharacter;
            List<GameObject> unitList = new();
            if (attackData.afterStepDebuffs[i]["type"] == 0) unitList = unit.DebuffList;
            else if (attackData.afterStepDebuffs[i]["type"] == 1) unitList = unit.Spells.SpellList;
            else unitList = unit.Spells.modeList;

            for (int i2 = 0; i2 < unitList.Count; i2++)
            {
                if (unitList[i2].GetComponent<AbstractSpell>().id == attackData.afterStepDebuffs[i]["debuffId"])
                {
                    StartCoroutine(unitList[i2].GetComponent<AbstractSpell>().AfterStep(attackData.afterStepDebuffs[i]));
                    break;
                }
            }
            while (finishEndEvent == false) yield return null;
            finishEndEvent = false;
        }
    }

    //Нажатие на кнопку сдаться
    public void Retret(bool win)
    {
        this.win = win;
        SetEndFight();
    }
    public void DoDefaultValues()
    {
        if (turnUnit != null)
        {
            turnUnit.transform.Find("UI/Turn").gameObject.GetComponent<BarAnimation>().SetCaracterState("death");
        }
        StartIni.turnEffect[0].SetActive(false);
        StartIni.turnEffect[1].SetActive(false);
        listAllowHit.Clear();
        if (turnUnit != null) turnUnit.ParentCircle.PathAnimation.SetCaracterState("idle");
    }
    //Проверка окончания хода

    public void SetEndFight()
    {
        StartCoroutine(endFightObject.EndScene(win));
        _endPanel.SetActive(true);
    }
}
