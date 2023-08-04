public static class PlayerPreferences
{
    public static readonly string HIGH_SCORE = "highscore";
    public static readonly string CURRENT_SCORE = "currentScore";
}

public static class GameScenes
{
    public static readonly string MAIN_MENU = "Menu Start";
    public static readonly string MAIN_GAME = "Main Game";
    public static readonly string GAME_OVER = "Game Over";
}

public static class Tags
{
    public static readonly string SPAWN = "Spawn";
    public static readonly string WAYPOINT = "Waypoint";
    public static readonly string SHOOTABLE_ITEM = "ShootableItem";
    public static readonly string PLAYER = "Player";
    public static readonly string SHIELD = "Shield";
}

public enum HordeType
{
    WayPoints,
    RandomArea
}
