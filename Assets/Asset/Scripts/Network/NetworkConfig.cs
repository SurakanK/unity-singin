using System.Collections.Generic;

public enum ENV
{
    local, prod,
}

public static class EvnConfig
{
    public static readonly Dictionary<ENV, string> Colyseus = new Dictionary<ENV, string>()
    {
        { ENV.local, "ws://localhost:8000" },
        { ENV.prod, "ws://3.104.77.1:8000" },
    };

     public static readonly Dictionary<ENV, string> Api = new Dictionary<ENV, string>()
    {
        { ENV.local, "http://localhost:3000" },
        { ENV.prod, "http://13.236.71.60:3000" },
    };
}