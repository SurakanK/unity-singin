using Colyseus.Schema;

public partial class LobbySchema : Schema
{
    [Type(0, "map", typeof(MapSchema<PlayerSchema>))]
    public MapSchema<PlayerSchema> players = new MapSchema<PlayerSchema>();


    [Type(1, "[GroupSchema]", typeof(ArraySchema<GroupSchema>))]
    public ArraySchema<GroupSchema> groups = new ArraySchema<GroupSchema>();
}

public partial class GroupSchema : Schema
{
    [Type(0, "string")]
    public string groupId = default(string);

    [Type(1, "map", typeof(MapSchema<PlayerSchema>))]
    public MapSchema<PlayerSchema> players = new MapSchema<PlayerSchema>();
}
