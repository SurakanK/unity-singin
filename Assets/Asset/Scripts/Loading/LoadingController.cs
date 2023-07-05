using System.Collections;
using System.Collections.Generic;
using GameDevWare.Serialization;
using UnityEngine;

public class LoadingController : MonoBehaviour
{
    private ColyseusManager _colyseus;
    private ColyseusManager colyseus
    {
        get { return _colyseus; }
        set
        {
            _colyseus = value;
            if (_colyseus)
            {
                OnMessage();
            }
        }
    }

    private void Awake()
    {
        colyseus = FindObjectOfType<ColyseusManager>();
    }

    private void OnMessage()
    {
        colyseus.RoomGame.OnMessage<PlayersData>(E.EventState.LOADING_GAME_PROGRESS_RESPONSE, message =>
        {
            foreach (PlayerData player in message.players)
            {
                Debug.Log(player.sessionId); //
            }
        });
    }
}
