using UnityEngine;

public class DialogActivator : MonoBehaviour
{
    public string[] Lines;

    private bool _canActivate;

    // private NPCController ActivatingNPC;

    // public string questToMark = null;

    // public bool markQuestComplete;

    // Start is called before the first frame update
    void Start()
    {
        // activatingNPC = gameObject.GetComponent<NPCController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_canActivate && Input.GetMouseButtonDown(Global.Inputs.LeftButton) && !DialogManager.Instance.DialogBox.activeInHierarchy)
        {
            DialogManager.Instance.ShowDialog(this.Lines); //activatingNPC?.npcName, questToMark, markQuestComplete);
        }
    }

    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.tag == Global.Tags.Player)
        {
            _canActivate = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == Global.Tags.Player)
        {
            _canActivate = false;
        }
    }
}