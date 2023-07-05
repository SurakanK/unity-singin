using Colyseus.Schema;

public partial class GameSchema : Schema
{
    [Type(0, "map", typeof(MapSchema<PlayerSchema>))]
    public MapSchema<PlayerSchema> players = new MapSchema<PlayerSchema>();
}

