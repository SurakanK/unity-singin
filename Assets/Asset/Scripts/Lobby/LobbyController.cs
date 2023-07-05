using System.Collections;
using System.Collections.Generic;
using GameDevWare.Serialization;
using UnityEngine;
using UnityEngine.UI;
using Colyseus;
using Newtonsoft.Json;

public class LobbyController : MonoBehaviour
{
    [SerializeField]
    private Button ButtonPlay;

    private ColyseusManager colyseus;
    private MatchMakeing matchMakeing;
    private FindMatch findMatch;

    private void Awake()
    {
        colyseus = FindObjectOfType<ColyseusManager>();
        matchMakeing = FindObjectOfType<MatchMakeing>();
        findMatch = FindObjectOfType<FindMatch>();
    }

    void Start()
    {
        ButtonPlay.onClick.AddListener(OnClickPlay);
    }

    private void OnClickPlay()
    {
        FindMatch();
    }

    private async void FindMatch()
    {
        await colyseus.JoinOrCreateLobby("lobby");
        OnMessgae();

        findMatch.activeFindMatch(true);
        ButtonPlay.gameObject.SetActive(false);
    }

    public void CancelFindMatch()
    {
        colyseus.RoomLobby.Leave();
        ButtonPlay.gameObject.SetActive(true);
    }

    private void OnMessgae()
    {
        colyseus.RoomLobby.OnMessage<PlayersData>(E.EventState.MATCHMAKING_CLIENTS, message =>
        {
            matchMakeing.SetMatchDetail(message.players);
            matchMakeing.ActiveMatchmakeing(true);
            findMatch.activeFindMatch(false);
        });

        colyseus.RoomLobby.OnMessage<ColyseusMatchMakeResponse>(E.EventState.CONSUME_SEAT_RESERVATION, async reservation =>
        {
            await colyseus.ConsumeSeatReservation(reservation);
        });
    }
}
