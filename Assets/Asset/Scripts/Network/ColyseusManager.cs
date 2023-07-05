using System.Threading.Tasks;
using Colyseus;
using UnityEngine;

public class ColyseusManager : MonoBehaviour
{
    public ENV env;
    private static ColyseusClient _client = null;
    private static ColyseusRoom<LobbySchema> _roomLobby = null;
    private static ColyseusRoom<GameSchema> _roomGame = null;
    private static ColyseusManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDisable()
    {
        if (_roomLobby != null)
        {
            RoomLobby.Leave();
        }

        if (_roomGame != null)
        {
            RoomGame.Leave();
        }
    }

    public void Initialize()
    {
        _client = new ColyseusClient(EvnConfig.Colyseus[this.env]);
    }

    public async Task JoinOrCreateLobby(string roomName)
    {
        _roomLobby = await Client.JoinOrCreate<LobbySchema>(roomName);
    }

    public async Task JoinById(string roomId)
    {
        _roomGame = await Client.JoinById<GameSchema>(roomId);
    }

    public async Task JoinOrCreateGame(string roomName)
    {
        _roomGame = await Client.JoinOrCreate<GameSchema>(roomName);
    }

    public async Task ConsumeSeatReservation(ColyseusMatchMakeResponse reservation)
    {
        _roomGame = await Client.ConsumeSeatReservation<GameSchema>(reservation);
    }

    public ColyseusClient Client
    {
        get
        {
            if (_client == null || !_client.Endpoint.Uri.ToString().Contains(EvnConfig.Colyseus[ENV.local]))
            {
                Initialize();
            }
            return _client;
        }
    }

    public ColyseusRoom<LobbySchema> RoomLobby
    {
        get
        {
            if (_roomLobby == null)
            {
                Debug.LogError("Room hasn't been initialized yet!");
            }
            return _roomLobby;
        }
    }

    public ColyseusRoom<GameSchema> RoomGame
    {
        get
        {
            if (_roomGame == null)
            {
                Debug.LogError("Room hasn't been initialized yet!");
            }
            return _roomGame;
        }
    }
}
