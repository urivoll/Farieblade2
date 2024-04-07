using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class Turns : MonoBehaviour
{
    public static CircleProperties[,] circlesMap = new CircleProperties[2, 6];
    public static CircleProperties[] circleAll;
    public static CircleAnimation[] circleBig;
    public static int numberTurn = 1;
    public static bool hitDone = false;
    public static bool finishEndEvent = false;
    public static bool beforeAttackCheck = false;
    public static Transform circlesTransform;
    public static List<UnitProperties> listUnitAll = new();
    public static List<GameObject> eventEndCard = new();
    public static List<UnitProperties> listUnitLeft = new();
    public static List<UnitProperties> listUnitRight = new();
    public static List<UnitProperties> listUnitEndLeft = new();
    public static List<UnitProperties> listUnitEndRight = new();
    public static List<UnitProperties> listUnitEnemy;
    public static List<UnitProperties> listUnitOur;
    public static List<CircleProperties> listFinishRight = new();
    public static List<UnitProperties> listAllowHit = new();
    public static List<Dictionary<string, int>> currentTryDeath = new();

    public static Action<UnitProperties> turnNumberObject;
    public static Action<UnitProperties> takeDamage;
    public static Action<UnitProperties, Dictionary<string, int>> tryDeath;
    public static Action<UnitProperties> resurectAction;
    public static Action<UnitProperties, UnitProperties, List<Dictionary<string, int>>> shooterPunch;
    public static Action<UnitProperties, UnitProperties, List<Dictionary<string, int>>> punch;
    public static Action<UnitProperties, UnitProperties> beforePunch;
    public static Action<GameObject, UnitProperties> getDebuff;
    public static Action newTurn;
    public static Action endGame;
    public static Action beforeAttack;
    public static Action<int, GameObject, char> getEnergy;

    public static UnitProperties unitChoose;
    public static UnitProperties turnUnit = null;
    [SerializeField] private GameObject blockUI;

    public GameObject slideDamagePrefub;
    public GameObject lightning;
    public bool endGameBool = false;
    public AudioClip bodyFall;
    [HideInInspector] public bool endTurn = false;
    public static int enemySide;

    private bool win;
    private int gameCount = 0;
    [SerializeField] private EndFight endFightObject;
    [SerializeField] private AudioClip nextTurnEnemy;
    [SerializeField] private AudioClip nextTurnYou;
    [SerializeField] private GameObject startHelper;
    [SerializeField] private TextMeshProUGUI _numberTurn;
    [SerializeField] private GameObject _endPanel;
    [SerializeField] private Transform circlesPrefub;
    [SerializeField] private GameObject _defendPrefub;
    [SerializeField] private CircleProperties[] circleAllPrefub;
    [SerializeField] private CircleProperties[] circleLeftPrefub;
    [SerializeField] private CircleProperties[] circleRightPrefub;
    [SerializeField] private CircleAnimation[] circleBigPrefub;
    [SerializeField] private GameObject[] defaulUnits;
    [SerializeField] private SideUnitUi sideUnitUI;
    public static bool aiMay = false;
    private AfterStep attackData;
    private void DefinisionStaticObjects()
    {
        circlesTransform = circlesPrefub;
        circleAll = circleAllPrefub;
        for (int i = 0; i < 6; i++)
        {
            circlesMap[0, i] = circleLeftPrefub[i];
            circlesMap[1, i] = circleRightPrefub[i];
        }

        circleBig = circleBigPrefub;
        if (PlayerData.defaultCards == null) PlayerData.defaultCards = defaulUnits;
    }
    private IEnumerator GetAura()
    {
        yield return new WaitForSeconds(2);
        if (BattleNetwork.auraSend.Count == 0) yield break;
        //Аура
        for (int i = 0; i < BattleNetwork.auraSend.Count; i++)
        {
            UnitProperties unit = circlesMap[BattleNetwork.auraSend[i]["side"], BattleNetwork.auraSend[i]["place"]].newObject;
            StartCoroutine(unit.aura.GetAura(BattleNetwork.auraSend[i]));
            while (finishEndEvent == false) yield return null;
            finishEndEvent = false;
        }
        yield return new WaitForSeconds(1);
    }
    private void InitializationCircles()
    {
        for (int i = 0; i < circleAll.Length; i++) circleAll[i].InitializationCircle();
        StartIni.setViewTrops?.Invoke();
        GetComponent<StartIni>().animatorShake.SetTrigger("start");
        listUnitEndLeft.AddRange(listUnitLeft);
        listUnitEndRight.AddRange(listUnitRight);
    }

    public IEnumerator Start2()
    {
        DefinisionStaticObjects();
        Coroutine cor;
        while (BattleNetwork.connected == false)
            yield return null;
        GetComponent<MapLocation>().SetBackground();
        InitializationCircles();
        cor = StartCoroutine(GetAura());
        yield return cor;
        cor = StartCoroutine(BeforeTurn());
        yield return cor;
        SetEndFight();
    }
    private IEnumerator BeforeTurn()
    {
        Coroutine cor;
        yield return new WaitForSeconds(0.8f);
        while (true)
        {
            gameCount++;
            while (BattleNetwork.doingQueue.Count < gameCount) yield return null;
            yield return new WaitForSeconds(0.2f);
            print(gameCount);
            currentTryDeath = BattleNetwork.doingQueue[gameCount - 1].tryDeathSend;
            var currentDoing = BattleNetwork.doingQueue[gameCount - 1];
            if (currentDoing.array[0] == -444) break;
            turnUnit = circlesMap[currentDoing.array[0], currentDoing.array[1]].newObject;
            StartIni.playersBars[turnUnit.sideOnMap].gameObject.SetActive(true);
            StartIni.playersBars[turnUnit.sideOnMap].fillAmount = 1f;
            StartIni.work = true;
            DefinitionSides();
            cor = StartCoroutine(PeriodicEffects());
            yield return cor;
            if (currentDoing.endDebuff.Count > 0)
            {
                for (int i = 0; i < currentDoing.endDebuff.Count; i++)
                {
                    for (int i2 = 0; i2 < turnUnit.idDebuff.Count; i2++)
                    {
                        if (turnUnit.idDebuff[i2].GetComponent<AbstractSpell>().id == currentDoing.endDebuff[i])
                            Destroy(turnUnit.idDebuff[i2]);
                    }
                }
            }
            if (turnUnit != null && !turnUnit.paralize && turnUnit.pathParent.fraction != 9)
            {
                GetComponent<CheckAllowHit>().TurnUnitEffect();
                Debug.Log(turnUnit.sideOnMap + " Ходит! " + turnUnit.placeOnMap);
                if (turnUnit.sideOnMap == BattleNetwork.sideOnBattle)
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
                if(turnUnit.sideOnMap == BattleNetwork.sideOnBattle) sideUnitUI.Exit();
                StartIni.work = false;
                StartIni.playersBars[turnUnit.sideOnMap].gameObject.SetActive(false);
            }
            cor = StartCoroutine(AfterTurn());
            yield return cor;
            DoDefaultValues();
            cor = StartCoroutine(AfterMoveEffects());
            yield return cor;
            yield return new WaitForSeconds(0.5f);
            CheckTurnEnd();
            hitDone = false;
        }
        if (BattleNetwork.doingQueue[gameCount - 1].array[1] == BattleNetwork.sideOnBattle) win = true;
        else win = false;
    }
    private IEnumerator AfterTurn()
    {
        attackData = BattleNetwork.attackResultQueue[gameCount - 1];
        currentTryDeath = attackData.tryDeathSend;
        //print(turnUnit.PathSpells.StartModeIndex + " " + attackData.mode);
        if (turnUnit != null && turnUnit.pathSpells.modeIndex != attackData.mode)
        {
            turnUnit.pathSpells.SetState(attackData.mode);
            yield return new WaitForSeconds(0.5f);
        }
        if (attackData.spell != -20) turnUnit.pathEnergy.SetEnergy(attackData.energy);
        //Удар
        GetComponent<CheckAllowHit>().TurnOver();
        if (attackData.spell == -666)
        {
            turnUnit.AttackedUnit(circlesMap[attackData.makeMove[0].attackSend["sideTarget"], attackData.makeMove[0].attackSend["placeTarget"]].newObject, attackData.makeMove);
        }
        //Защита
        else if(attackData.spell == -10)
        {
            Instantiate(_defendPrefub, turnUnit.pathDebuffs);
            hitDone = true;
        }
        //Баш или смерть
        else if(attackData.spell == -20) hitDone = true;
        //Магия
        else turnUnit.pathSpells.UseActive(attackData.spell, attackData.makeMove);
        while (hitDone == false) yield return null;
        if (turnUnit != null && turnUnit.moved)
        {
            if (turnUnit.pushUnit != null)
            {
                turnUnit.pushUnit.transform.position = circlesMap[turnUnit.pushUnit.sideOnMap, turnUnit.pushUnit.placeOnMap].transform.position;
                turnUnit.pushUnit = null;
            }
            turnUnit.transform.position = circlesMap[turnUnit.sideOnMap, turnUnit.placeOnMap].transform.position;
            turnUnit.pathParent.transform.SetParent(turnUnit.pathCircle.transform);
            turnUnit.pathParent.transform.localScale = new Vector2(1, 1);
        }
    }
    public void Defend()
    {
        unitChoose = turnUnit;
        GetComponent<BattleNetwork>().AttackQuery(-10, turnUnit.pathSpells.modeIndex, BattleNetwork.ident, turnUnit.sideOnMap, turnUnit.placeOnMap);
    }
    //После хода восстанавливать дефолтные значения

    public void DefinitionSides()
    {
        if (turnUnit.sideOnMap == 1)
        {
            enemySide = 0;
            listUnitEnemy = listUnitLeft;
            listUnitOur = listUnitRight;
            StartIni.turnEffect[1].SetActive(true);
        }
        else
        {
            enemySide = 1;
            listUnitEnemy = listUnitRight;
            listUnitOur = listUnitLeft;
            StartIni.turnEffect[0].SetActive(true);
        }
    }
    public IEnumerator PeriodicEffects()
    {
        if (BattleNetwork.doingQueue[gameCount - 1].periodicDebuff.Count == 0) yield break;
        List<Dictionary<string, int>> tempList = BattleNetwork.doingQueue[gameCount - 1].periodicDebuff;
        for (int i = 0; i < tempList.Count; i++)
        {
            for (int i2 = 0; i2 < turnUnit.idDebuff.Count; i2++)
            {
                if (turnUnit.idDebuff[i2].GetComponent<AbstractSpell>().id == tempList[i]["id"])
                {
                    turnUnit.idDebuff[i2].GetComponent<AbstractSpell>().PeriodicMethod(tempList[i]);
                    yield return new WaitForSeconds(0.6f);
                    break;
                }
            }
        }
    }
    public IEnumerator AfterMoveEffects()
    {
        if (attackData.afterStepDebuffs.Count == 0) yield break;
        yield return new WaitForSeconds(0.8f);
        for (int i = 0; i < attackData.afterStepDebuffs.Count; i++)
        {
            UnitProperties unit = circlesMap[attackData.afterStepDebuffs[i]["side"], attackData.afterStepDebuffs[i]["place"]].newObject;
            List<GameObject> unitList = new();
            if (attackData.afterStepDebuffs[i]["type"] == 0) unitList = unit.idDebuff;
            else if (attackData.afterStepDebuffs[i]["type"] == 1) unitList = unit.pathSpells.SpellList;
            else unitList = unit.pathSpells.modeList;

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
        beforeAttackCheck = false;
        if (turnUnit != null)
        {
            turnUnit.paralize = false;
            turnUnit.went = true;
            turnUnit.pathParent.transform.Find("Fight/Canvas/turn").gameObject.GetComponent<BarAnimation>().SetCaracterState("death");
        }
        StartIni.turnEffect[0].SetActive(false);
        StartIni.turnEffect[1].SetActive(false);
        listAllowHit.Clear();
        if (turnUnit != null) turnUnit.pathCircle.PathAnimation.SetCaracterState("idle");
    }
    //Проверка окончания хода
    private void CheckTurnEnd()
    {
        int count = 0;
        for (int i = 0; i < listUnitAll.Count; i++)
        {
            if (listUnitAll[i].went == false)
            {
                count++;
                break;
            }
        }
        if (count == 0)
        {
            for (int i = 0; i < listUnitAll.Count; i++)
            {
                listUnitAll[i].went = false;
                listUnitAll[i].pathParent.transform.Find("Fight/Canvas/turn").gameObject.GetComponent<BarAnimation>().SetCaracterState("idle");
            }
            numberTurn += 1;
            _numberTurn.text = Convert.ToString(numberTurn);
            newTurn?.Invoke();
        }
    }
    public void SetEndFight()
    {
        StartCoroutine(endFightObject.EndScene(win));
        _endPanel.SetActive(true);
    }
}
