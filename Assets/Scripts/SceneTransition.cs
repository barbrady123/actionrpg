using UnityEngine;

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

        SceneInit.StartTransition(this.TargetScene, this.TargetTransition);
    }

    private void OnTriggerExit2D(Collider2D obj)
    {
        if (obj.tag != Global.Tags.Player)
            return;
    }
}
