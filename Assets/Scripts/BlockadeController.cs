using UnityEngine;

public class BlockadeController : MonoBehaviour, ISwitchActivated
{
    private GameObject _onBlocks;
    private GameObject _offBlocks;

    // Start is called before the first frame update
    void Start()
    {
        _onBlocks = transform.Find("BlocksOn").gameObject;
        _offBlocks = transform.Find("BlocksOff").gameObject;
        Activate(true);
    }

    public void Activate(bool isActive)
    {
        _onBlocks.SetActive(isActive);
        _offBlocks.SetActive(!isActive);
    }
}
