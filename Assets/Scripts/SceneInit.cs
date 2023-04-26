using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneInit
{
    public const float TransitionActiveDelayLength = 0.5f;

    public static string EntryTransition { get; set; }

    public static float Delay { get; set; }

    public static bool OkToTransition() => SceneInit.Delay <= 0f;

    private static string _targetScene;

    public static void StartTransition(string targetScene, string targetTransition)
    {
        _targetScene = targetScene;
        SceneInit.EntryTransition = targetTransition;
        SceneInit.Delay = SceneInit.TransitionActiveDelayLength;
        UIManager.Instance.FadeToBlack();
    }

    public static void LoadTransitionScene(Action callback)
    {
        SceneManager.LoadScene(_targetScene);
        callback.Invoke();
    }
}
