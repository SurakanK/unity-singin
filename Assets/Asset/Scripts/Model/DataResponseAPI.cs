public class TileMapData
{
    public Vec2 position;
    public int tetromino;
}

public class JsonLevel
{
    public int level;
    public int coin;
    public int goal;
    public int move;
    public int time;
    public int missionType;
    public TileMapData[] tilemap;
}

public class JsonStatus
{
    public int code;
    public string message;
}

public class JsonResGetLevelMap
{
    public JsonStatus status;
    public JsonLevel data;
}