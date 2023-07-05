
public class E
{
    public class EventState
    {
        public const string MATCHMAKING_CLIENTS = "MATCHMAKING_CLIENTS";
        public const string CONSUME_SEAT_RESERVATION = "CONSUME_SEAT_RESERVATION";
        public const string LOADING_GAME_PROGRESS_RESPONSE = "LOADING_GAME_PROGRESS_RESPONSE";
        public const string BEFORE_START = "BEFORE_START";
        public const string UPDATE_BOARD = "UPDATE_BOARD";
        public const string UPDATE_SYMBOL = "UPDATE_SYMBOL";
        public const string SPAWN_SYMBOL = "SPAWN_SYMBOL";
        public const string SCORE_UPDATE = "SCORE_UPDATE";
    }

    public class EventAction
    {
        public const string CONFIRM_MATCH = "CONFIRM_MATCH";
        public const string READY = "READY";
        public const string ROTATE = "ROTATE";
        public const string MOVE = "MOVE";
        public const string DROP = "DROP";

    }
}
