using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Textbox : MonoBehaviour
{
    bool textboxActive;
    string characterName;
    [SerializeField] GameObject textbox;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI promptText;
    bool nextTextAvailable;
    List<string> dialogContent = new List<string>();


    private void Update()
    {
        if (textboxActive && !GameManager.Instance.isPaused)
        {
            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.F)))
            {
                if (nextTextAvailable)
                {
                    NextTextboxDialog();
                }
                else
                {
                    EndTextbox();
                }
            }
        }
    }
    public void StartTextbox(string _characterName, List<string> _dialogContent)
    {
        if (textboxActive) { return; }

        UIManager.Instance.menuActive = true;
    
        nameText.text = characterName;

        dialogContent = new List<string>(_dialogContent);

        if (dialogContent == null && dialogContent.Count < 1)
        {
            Debug.LogWarning("Textbox cannot be displayed as there is no dialog");
            return;
        }

        promptText.text = dialogContent[0];

        if (dialogContent.Count > 1)
        {
            nextTextAvailable = true;
        }

        textboxActive = true;

        textbox.SetActive(true);
    }

   
    public void EndTextbox()
     {
        UIManager.Instance.menuActive = false;
        textboxActive = false;
        textbox.SetActive(false);
    }

    void NextTextboxDialog()
    {
        dialogContent.RemoveAt(0);

        promptText.text = dialogContent[0];
        if (dialogContent.Count > 1)
        {
            nextTextAvailable = true;
        }
        else
        {
            nextTextAvailable = false;
        }
        textbox.SetActive(true);
    }
}

