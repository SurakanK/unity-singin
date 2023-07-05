using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Colyseus;

[Serializable]
public class PlayerData
{
    public string sessionId;
    public string name;
    public string loadingProgress;
    public int score;
}

[Serializable]
public class Vec2
{
    public int x;
    public int y;

    public Vec2(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}

[Serializable]
public class BoardDatas
{
    public Vec2 position;
    public int type;
}

public class PlayersData
{
    public PlayerData[] players;
}

public class SymbolData
{
    public string sessionId;
    public Vec2[] position;
    public int type;
    public Vec2[] ghostPosition;
}

public class BoardData
{
    public string sessionId;
    public BoardDatas[] boardData;
}

public class SpawnData
{
    public int nextSymbol;
}

