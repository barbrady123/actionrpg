using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string TargetScene;
    public string TargetTransition;
    public string Name;
    public Vector3 EntryOffset;

    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.tag != Global.Tags.Player)
            return;

        if (!SceneInit.OkToTransition())
            return;

        SceneInit.StartTransition(this.TargetTransition);
        SceneManager.LoadScene(this.TargetScene);
    }

    private void OnTriggerExit2D(Collider2D obj)
    {
        if (obj.tag != Global.Tags.Player)
            return;
    }
}
