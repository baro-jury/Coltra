using System;

public static class GameConstants
{
    public static string oneWayPlatform = "OneWayPlatform";
    public static string enemyBullet = "EnemyBullet";
    public static string playerBullet = "PlayerBullet";
}

public static class GameEvent
{
    public static Action OnPlayerDead;
    public static Action<CharacterColor> OnEnemyKill;
}
