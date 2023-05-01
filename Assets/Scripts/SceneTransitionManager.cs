using System;
using System.Linq;
using UnityEngine;

public class SceneTransitionManager : MonoBehaviour
{
    void Start()
    {
        if (String.IsNullOrWhiteSpace(SceneInit.EntryTransition))
            return;

        var target = GetComponentsInChildren<SceneTransition>().FirstOrDefault(x => x.Name == SceneInit.EntryTransition);
        if (target == null)
        {
            Debug.LogError($"Unknown transition name encountered: '{SceneInit.EntryTransition}'");
        }

        PlayerController.Instance.transform.position = target.transform.position + target.EntryOffset;
        PlayerController.Instance.SceneStartPosition = PlayerController.Instance.transform.position;
        SceneInit.EntryTransition = null;
    }

    private void Update()
    {
        if (SceneInit.Delay > 0f)
        {
            SceneInit.Delay -= Time.deltaTime;
        }
    }
}
