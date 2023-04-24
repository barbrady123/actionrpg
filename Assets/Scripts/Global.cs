using UnityEngine;

public static class Global
{
    public static class Scenes
    {
        public const string MainMenu = "MainMenu";

        public const string Village = "Village";
    }

    public static class Inputs
    {
        public const string AxisHorizontal = "Horizontal";

        public const string AxisVertical = "Vertical";

        public const int LeftButton = 0;

        public const int RightButton = 1;

        public const string Fire1 = "Fire1";
    }

    public static class Tags
    {
        public const string Player = "Player";

        public const string Enemy = "Enemy";
    }

    public static class Parameters
    {
        public const string Speed = "Speed";

        public const string DirX = "dirX";

        public const string DirY = "dirY";

        public static class Triggers
        {
            public const string Attack = "Attack";

            public const string Spin = "Spin";
        }
    }

    public static bool Success(int percentChance) => (Random.Range(0, 100) < percentChance);
}
