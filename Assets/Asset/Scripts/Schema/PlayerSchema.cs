using Colyseus.Schema;

public partial class PlayerSchema : Schema
{
    [Type(0, "string")]
    public string sessionId = default(string);

    [Type(1, "boolean")]
    public bool isReady = default(bool);

    [Type(2, "string")]
    public string name = default(string);

    [Type(3, "number")]
    public float loadingProgress = default(float);
}

