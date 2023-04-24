using UnityEngine;

public class SwordController : MonoBehaviour
{
    public void AnimationDone()
    {
        PlayerController.Instance.SpinComplete();
    }
}
