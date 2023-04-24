public static class SceneInit
{
    public const float TransitionActiveDelayLength = 0.5f;

    public static string EntryTransition { get; set; }

    public static float Delay { get; set; }

    public static bool OkToTransition() => SceneInit.Delay <= 0f;

    public static void StartTransition(string targetTransition)
    {
        SceneInit.EntryTransition = targetTransition;
        SceneInit.Delay = SceneInit.TransitionActiveDelayLength;
    }
}
