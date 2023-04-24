using System.Linq;
using TMPro;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance;

    public GameObject DialogBox;
    public TMP_Text DialogText;

    public string[] DialogLines;
    private int _currentLine;

    private bool _justOpened;

    void Awake()
    {
        if ((Instance != null) && (Instance != this))
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void Start()
    {
        GameManager.Instance.DialogActive = false;
    }

    void Update()
    {
        if (this.DialogBox.activeInHierarchy)
        {
            if (Input.GetMouseButtonUp(Global.Inputs.LeftButton))
            {
                if (_justOpened)
                {
                    _justOpened = false;
                    return;
                }

                _currentLine++;

                if (_currentLine >= this.DialogLines.Length)
                {
                    this.DialogBox.SetActive(false);
                    GameManager.Instance.DialogActive = false;
                    /*
                    if (_questToMark != null)
                    {
                        QuestManager.instance.MarkQuestComplete(_questToMark, markQuestComplete);
                        _questToMark = null;
                    }
                    */
                }
                else
                {
                    SetText(this.DialogLines[_currentLine]);
                }
            }
        }
    }

    public void ShowDialog(
            string[] newLines)
            // string name,
            // string questToMark = null,
            // bool? markComplete = null)
    {
        this.DialogLines = newLines;
        // currentName = name;

        // _questToMark = questToMark;
        // markQuestComplete = markComplete ?? false;

        _currentLine = 0;

        SetText(this.DialogLines[_currentLine], true);
        this.DialogBox.SetActive(true);

        _justOpened = true;

        GameManager.Instance.DialogActive = true;
    }

    private void SetText(string line, bool resetSpeaker = false)
    {
        /*
        if (resetSpeaker)
        {
            nameText.color = GetColor();
            nameText.text = GetName();
        }
        */

        var parts = line.Split(':');

        // Flag for signs...
        bool showName = true;

        /*
        if (parts.Length > 1)
        {
            showName = parts[0] != Global.Labels.NoChat;
            nameBox.SetActive(showName);

            nameText.color = GetColor(parts[0]);
            nameText.text = GetName(parts[0]);
        }
        */

        this.DialogText.text = parts.Last();
        // this.DialogText.alignment = showName ? TextAnchor.UpperLeft : TextAnchor.MiddleCenter;

        this.DialogText.horizontalAlignment = showName ? HorizontalAlignmentOptions.Left : HorizontalAlignmentOptions.Center;
        this.DialogText.verticalAlignment = showName ? VerticalAlignmentOptions.Top : VerticalAlignmentOptions.Middle;
    }
}
