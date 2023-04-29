using UnityEngine;

public class SwitchController : MonoBehaviour
{
    private GameObject _switchOn;
    private GameObject _switchOff;

    public bool CanReset = false;

    private bool _canInteract;

    private ISwitchActivated _switchActivated;

    public GameObject ActivatedObject;

    void Start()
    {
        _switchOn = transform.Find("Switch On").gameObject;
        _switchOff = transform.Find("Switch Off").gameObject;
        _switchActivated = this.ActivatedObject.GetComponent<BlockadeController>();
        SetSwitch(true, false);
    }

    private void Update()
    {
        if (!_canInteract)
            return;

        if (Input.GetMouseButtonDown(Global.Inputs.LeftButton))
        {
            SetSwitch(!_switchOn.activeInHierarchy);
        }
    }

    protected void PerformSwitchAction(bool isEnabled)
    {
        _switchActivated.Activate(isEnabled);
    }

    public void SetSwitch(bool isEnabled, bool performAction = true)
    {
        if (_switchOff.activeInHierarchy && (!this.CanReset))
            return;

        AudioManager.Instance.PlaySFX(SFX.Equip);
        _switchOn.SetActive(isEnabled);
        _switchOff.SetActive(!isEnabled);

        if (performAction)
        {
            PerformSwitchAction(isEnabled);
        }
    }

    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (!obj.tag.IsPlayerTag())
            return;

        _canInteract = true;
    }

    private void OnTriggerExit2D(Collider2D obj)
    {
        if (!obj.tag.IsPlayerTag())
            return;

        _canInteract = false;
    }
}
