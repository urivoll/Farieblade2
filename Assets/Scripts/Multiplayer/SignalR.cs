using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using PimDeWitte.UnityMainThreadDispatcher;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using static BattleNetwork;
public class SignalR : MonoBehaviour
{
    public HubConnection hubConnection;
    public Action<int, int, string, string> MatchmakingStatus;
    public Energy energy;
    public void StartVoid(int i)
    {
        StartCoroutine(StartVoidAsync(i));
    }
    private IEnumerator StartVoidAsync(int i)
    {
        if(i == 1) yield return StartSearching();
        if (i == 0) yield return StopSearching();
    }
    public async Task StartSearching()
    {
        MatchmakingStatus += Set;
        hubConnection = new HubConnectionBuilder().WithUrl("http://46.8.21.206:5215/MatchmakingHub").Build();
        hubConnection.On("MatchmakingStatus", (int side, int mode, string index, string ident) => MatchmakingStatus?.Invoke(side, mode, index, ident));
        await hubConnection.StartAsync();
        Dictionary<string, string> form = new()
        { 
            { "id", FirstStart.newProdID.ToString() },
            { "password", FirstStart.newPassword },
            { "action", "1" } 
        };
        string form2 = JsonConvert.SerializeObject(form);
        await hubConnection.SendAsync("JoinMatchmakingQueue", form2);
    }
    public async Task StopSearching()
    {
        Dictionary<string, string> form = new()
        {
            { "id", FirstStart.newProdID.ToString() },
            { "password", FirstStart.newPassword },
            { "action", "0" }
        };
        string form2 = JsonConvert.SerializeObject(form);
        await hubConnection.SendAsync("JoinMatchmakingQueue", form2);
        Return();
    }
    private void Set(int side, int mode, string index, string ident)
    {
        UnityMainThreadDispatcher.Instance().Enqueue(() =>
        {
            try
            {
                sideOnBattle = side;
                gameIndex = index;
                BattleNetwork.ident = ident;
                Return();
                StartCoroutine(energy.PrepareToFight2(mode));
            }
            catch (Exception ex)
            {
                print(ex.ToString());
            }
        });
    }
    private void OnDestroy()
    {
        Return();
    }
    private void Return()
    {
        if (hubConnection != null)
        {
            hubConnection.StopAsync().Wait(); // Ожидание завершения асинхронной операции
            hubConnection.On("MatchmakingStatus", null); // Отписываемся от события
            MatchmakingStatus -= Set;
        }
    }
}
