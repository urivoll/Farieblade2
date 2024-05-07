using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using PimDeWitte.UnityMainThreadDispatcher;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
public class BattleNetwork : MonoBehaviour
{
    public static List<BeforeStep> doingQueue = new();
    public static List<AfterStep> attackResultQueue = new();
    public HubConnection hubConnection;
    public Action<string, string> startEvent;
    public Action<int, int, int, int, int, int, int> pickEvent;
    public Action<int, string, int> multiplayerEvent;
    public Action<string> attackEvent;
    public Action<int> endEvent;
    public Action<string> gameReuslt;
    public Action<int, int> beforeTurnEvent;
    public static bool connected = false;
    public static string gameIndex;
    public static int sideOnBattle;
    public static string ident;
    [SerializeField] private GameObject multiplayer;
    [SerializeField] private MusicFight music;
    public static bool end = false;
    public static int[,] winners = new int[4, 6]
    {
        { -666, 0, 0, 0, 0, 0 },
        { -666, 0, 0, 0, 0, 0 },
        { -666, 0, 0, 0, 0, 0 },
        { -666, 0, 0, 0, 0, 0 }
    };
    public static int winnersCount;
    public static List<Dictionary<string, int>> auraSend = new();
    public IEnumerator Game(int state)
    {
        bool local = false;
        startEvent += StartAnswer;
        attackEvent += AttackAnswer;
        endEvent += EndAnswer;
        gameReuslt += GameResult;
        if (state == 0)
        hubConnection = new HubConnectionBuilder().WithUrl("http://localhost:5215/PickHub").Build();
        if (state == 1)
        {
            local = true;
            sideOnBattle = 0;
            hubConnection = new HubConnectionBuilder().WithUrl("http://localhost:5215/PickHub").Build();
        }
        if (Energy.mode == 1 || Energy.mode == 2)
        {
            pickEvent += PickAnswer;
            multiplayerEvent += MultipayerMethod;
            hubConnection.On("MultiplayerStage", (int side, string nick, int portrait) => multiplayerEvent?.Invoke(side, nick, portrait));
            hubConnection.On("PickStage", (int id, int side, int place, int level, int grade, int currentSkin, int pickTurn) => pickEvent?.Invoke(id, side, place, level, grade, currentSkin, pickTurn));
        }
        hubConnection.On("StartIni", (string stringStartData, string generalData) => startEvent?.Invoke(stringStartData, generalData));
        hubConnection.On("AttackQuery", (string stringAttack) => attackEvent?.Invoke(stringAttack));
        hubConnection.On("EndGame", (int loseSide) => endEvent?.Invoke(loseSide));
        if(!local)
        hubConnection.On("LevelHandler", (string stringResult) => gameReuslt?.Invoke(stringResult));
        yield return hubConnection.StartAsync().AsCoroutine();
        if (Energy.mode == 1) 
            yield return hubConnection.SendAsync("StartHandler", FirstStart.newProdID, gameIndex, sideOnBattle, ident).AsCoroutine();
        else if (Energy.mode == 2) 
            yield return hubConnection.SendAsync("StartWithBot", FirstStart.newProdID, gameIndex, sideOnBattle, ident).AsCoroutine();
        else
        {
            sideOnBattle = 0;
            yield return hubConnection.SendAsync("StartCampany", FirstStart.newProdID, FirstStart.newPassword, Campany.currentPlace -1).AsCoroutine();
        }
    }
    public void Retret()
    {
        hubConnection.SendAsync("AttackHandler", -666, -666, gameIndex, sideOnBattle, -666, -1);
    }
    //Œ“¬≈“ Õ¿ œ» 
    public void PickAnswer(int id, int side, int place, int level, int grade, int currentSkin, int pickTurn)
    {
        UnityMainThreadDispatcher.Instance().Enqueue(() => multiplayer.GetComponent<MultiplayerDraft>().SetUnit(id, side, place, level, grade, currentSkin, pickTurn));
    }
    public void EndAnswer(int loseSide)
    {
        UnityMainThreadDispatcher.Instance().Enqueue(() =>
        {
            end = true;
            if (connected)
            {
                if (loseSide == sideOnBattle) GetComponent<Turns>().Retret(false);
                else GetComponent<Turns>().Retret(true);
            }
            else
            {
                multiplayer.GetComponent<MultiplayerDraft>().GameStop();
                if(loseSide == sideOnBattle) multiplayer.transform.Find("youLeaved").gameObject.SetActive(true);
                else multiplayer.transform.Find("enemyLeaved").gameObject.SetActive(true);
            }
        });
    }
    public void Leave()
    {
        if (hubConnection == null) return;
        startEvent -= StartAnswer;
        endEvent -= EndAnswer;
        attackEvent -= AttackAnswer;
        pickEvent -= PickAnswer;
        multiplayerEvent -= MultipayerMethod;
        if (end == false) hubConnection.SendAsync("AttackHandler", -666, -666, gameIndex, sideOnBattle, -666);

        hubConnection.On("EndGame", null);
        hubConnection.On("StartIni", null);
        hubConnection.On("AttackQuery", null);
        hubConnection.On("MultiplayerStage", null);
        hubConnection.On("PickStage", null);
        hubConnection.StopAsync();
    }
    public void PickQuery(int id, int side, int place, string ident)
    {
        hubConnection.SendAsync("PickHandler", FirstStart.newProdID, id, side, place, gameIndex, ident);
    }
    //«¿œ–Œ— Õ¿ ¿“¿ ”   —≈–¬≈–”
    public void AttackQuery(AttackFormSubmitter attackFormSubmitter)
    {
        try
        {
            Debug.Log("—ÚÓÓÌ‡: " + attackFormSubmitter.Side + ", ÃÂÒÚÓ: " + attackFormSubmitter.Place + ", —ÔÓÒÓ·ÌÓÒÚ¸: " + attackFormSubmitter.Spell);
            hubConnection.SendAsync(
                "AttackHandler", 
                attackFormSubmitter.Side, 
                attackFormSubmitter.Place, 
                gameIndex, 
                sideOnBattle, 
                attackFormSubmitter.Spell, 
                attackFormSubmitter.ModeIndex, 
                attackFormSubmitter.Ident);
        }
        catch (Exception ex)
        {
            print(ex.ToString());
        }
    }

    private void MultipayerMethod(int side, string enemyNick, int enemyPortrait)
    {
        UnityMainThreadDispatcher.Instance().Enqueue(() =>
        {
            LoadingManager.LoadingScreenAfterObj.SetActive(false);
            sideOnBattle = side;
            Campany.enemyNick = enemyNick;
            Campany.enemyPortraitInt = enemyPortrait;
            multiplayer.SetActive(true);
        });
    }
    //Õ¿◊¿À‹Õ€… Œ“¬≈“
    private void StartAnswer(string json, string json2)
    {
        print(json);
        StartDataJson startData = JsonConvert.DeserializeObject<StartDataJson>(json);
        MapLocation.place = startData.background;
        gameIndex = startData.index;
        for (int i = 0; i < 6; i++)
        {
            MultiplayerDraft.units[0, i] = startData.leftSide[i]["id"];
            MultiplayerDraft.unitsLevel[0, i] = startData.leftSide[i]["level"];
            MultiplayerDraft.unitsGrade[0, i] = startData.leftSide[i]["grade"];
            MultiplayerDraft.unitsSkin[0, i] = startData.leftSide[i]["skin"];

            MultiplayerDraft.units[1, i] = startData.rightSide[i]["id"];
            MultiplayerDraft.unitsLevel[1, i] = startData.rightSide[i]["level"];
            MultiplayerDraft.unitsGrade[1, i] = startData.rightSide[i]["grade"];
            MultiplayerDraft.unitsSkin[1, i] = startData.rightSide[i]["skin"];
        }
        auraSend = startData.auraSend;
        GeneralMethod(json2);
        connected = true;
        if (Energy.mode == 0)
        {
            UnityMainThreadDispatcher.Instance().Enqueue(() =>
            {
                ident = startData.ident;
                LoadingManager.LoadingScreenAfterObj.SetActive(false);
                GetComponent<StartIni>().Start2();
                music.Start2();
            });
        }
    }
    //Œ“¬≈“  “Œ  Œ√Œ ¡‹≈“
    private void AttackAnswer(string json) => GeneralMethod(json);
    private void GeneralMethod(string json)
    {
        UnityMainThreadDispatcher.Instance().Enqueue(() =>
        {
            print(json);
            GeneralData generalData = JsonConvert.DeserializeObject<GeneralData>(json);
            if (generalData.beforeQueue.Count > 0) doingQueue.AddRange(generalData.beforeQueue);
            if (generalData.afterQueue.Count > 0) attackResultQueue.AddRange(generalData.afterQueue);
        });
    }
    private void GameResult(string json)
    {
        UnityMainThreadDispatcher.Instance().Enqueue(() =>
        {
            print(json);
            LevelHandler hand = JsonConvert.DeserializeObject<LevelHandler>(json);
            for (int i = 0; i < hand.id.Count; i++)
            {
                winners[i, 0] = hand.id[i];
                winners[i, 1] = hand.levels[i];
                winners[i, 2] = hand.exps[i];
                winners[i, 3] = hand.grades[i];
                winners[i, 4] = hand.expsPlus[i];
                winners[i, 5] = hand.lvlup[i];
            }
            winnersCount = hand.id.Count;
        });
    }
    public class GeneralData
    {
        public List<BeforeStep> beforeQueue = new();
        public List<AfterStep> afterQueue = new();
    }
}
public class LevelHandler
{
    public List<int> id = new();
    public List<int> grades = new();
    public List<int> levels = new();
    public List<int> exps = new();
    public List<int> expsPlus = new();
    public List<int> lvlup = new();
    public int accLevel;
    public int accLvlup;
    public int accExp;
}
public class StartDataJson
{
    public Dictionary<string, int>[] leftSide = new Dictionary<string, int>[6]
{
    new () { { "id", -666 }, { "level", 1 }, { "grade", 0 }, { "skin", 0 }, { "exp", 0 } },
    new () { { "id", -666 }, { "level", 1 }, { "grade", 0 }, { "skin", 0 }, { "exp", 0 } },
    new () { { "id", -666 }, { "level", 1 }, { "grade", 0 }, { "skin", 0 }, { "exp", 0 } },
    new () { { "id", -666 }, { "level", 1 }, { "grade", 0 }, { "skin", 0 }, { "exp", 0 } },
    new () { { "id", -666 }, { "level", 1 }, { "grade", 0 }, { "skin", 0 }, { "exp", 0 } },
    new () { { "id", -666 }, { "level", 1 }, { "grade", 0 }, { "skin", 0 }, { "exp", 0 } },
};
    public Dictionary<string, int>[] rightSide = new Dictionary<string, int>[6]
{
    new () { { "id", -666 }, { "level", 1 }, { "grade", 0 }, { "skin", 0 }, { "exp", 0 } },
    new () { { "id", -666 }, { "level", 1 }, { "grade", 0 }, { "skin", 0 }, { "exp", 0 } },
    new () { { "id", -666 }, { "level", 1 }, { "grade", 0 }, { "skin", 0 }, { "exp", 0 } },
    new () { { "id", -666 }, { "level", 1 }, { "grade", 0 }, { "skin", 0 }, { "exp", 0 } },
    new () { { "id", -666 }, { "level", 1 }, { "grade", 0 }, { "skin", 0 }, { "exp", 0 } },
    new () { { "id", -666 }, { "level", 1 }, { "grade", 0 }, { "skin", 0 }, { "exp", 0 } },
};
    public int background;
    public List<Dictionary<string, int>> auraSend = new();
    public List<BeforeStep> beforeQueue;
    public List<AfterStep> afterQueue;
    public string index;
    public string ident = "";
}
public class BeforeStep
{
    public int[] array = new int[2];
    public List<Dictionary<string, int>> periodicDebuff = new();
    public List<int> endDebuff = new();
    public List<Dictionary<string, int>> tryDeathSend = new();
    public string ________END_BEFORE_STEP________ = "";
}
public class AfterStep
{
    public int mode;
    public int energy;
    public int spell = -666;
    public int type;
    public List<MakeMove> makeMove = new();
    public List<Dictionary<string, int>> afterStepDebuffs = new();
    public List<Dictionary<string, int>> tryDeathSend = new();
}
public class MakeMove
{
    public Dictionary<string, int> attackSend = new();
    public Dictionary<string, int> hitEffectSend = new();
    public List<Dictionary<string, int>> beforePunchSend = new();
    public List<Dictionary<string, int>> takeDamageSend = new();
    public List<Dictionary<string, int>> punchSend = new();
    public List<Dictionary<string, int>> resurectActionSend = new();
    public List<Dictionary<string, int>> getEnergySend = new();
    public List<Dictionary<string, int>> shooterHitSend = new();

}
public static class TaskExtensions
{
    public static IEnumerator AsCoroutine(this Task task)
    {
        while (!task.IsCompleted)
        {
            yield return null;
        }

        if (task.IsFaulted)
        {
            Debug.LogError($"Coroutine failed: {task.Exception}");
        }
    }
}