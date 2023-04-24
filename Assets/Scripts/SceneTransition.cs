using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string TargetScene;
    public string TargetTransition;

    public string Name;

    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.tag != Global.Tags.Player)
            return;

        SceneInit.StartTransition = this.TargetTransition;
        SceneManager.LoadScene(this.TargetScene);
    }
}
