using System;
using System.Linq;
using UnityEngine;

public class SceneTransitionManager : MonoBehaviour
{
    public GameObject[] Transitions;

    void Start()
    {
        if (String.IsNullOrWhiteSpace(SceneInit.StartTransition))
            return;

        var target = this.Transitions.Select(x => x.GetComponent<SceneTransition>()).Single(x => x.Name == SceneInit.StartTransition);

        PlayerController.Instance.transform.position = target.transform.position;
        SceneInit.StartTransition = null;
    }
}
