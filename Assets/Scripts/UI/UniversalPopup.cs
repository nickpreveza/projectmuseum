using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class UniversalPopup : UIPopup
{
    public bool dofOnShow;

    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI description;

    [SerializeField] Button confirm;
    [SerializeField] TextMeshProUGUI confirmText;
    [SerializeField] Button cancel;
    [SerializeField] TextMeshProUGUI cancelText;

    public void EnableDof()
    {
        DepthOfField dof;
        if (GameManager.Instance.globalVolume.profile.TryGet<DepthOfField>(out dof))
        {
            dof.active = true;
        }
    }

    public void DisableDof()
    {
        DepthOfField dof;
        if (GameManager.Instance.globalVolume.profile.TryGet<DepthOfField>(out dof))
        {
            dof.active = false;
        }
    }
    public void SetDataForReward(string newTitle, string newDescription, string option1name, string option2name)
    {
        cancel.gameObject.SetActive(true);
        confirm.gameObject.SetActive(true);
        title.text = newTitle;
        description.text = newDescription;
        confirm.onClick.RemoveAllListeners();
        cancel.onClick.RemoveAllListeners();
        confirm.interactable = true;

        confirmText.text = option1name;
        confirm.onClick.AddListener(() => UIManager.Instance.confirmAction());
        confirm.onClick.AddListener(() => CloseWithDelay());

        cancelText.text = option2name;
        cancel.onClick.AddListener(() => UIManager.Instance.additionalAction());
        cancel.onClick.AddListener(() => CloseWithDelay());
    }
    public void SetData(string newTitle, string newDescription, bool available, string option1name, string option2name, bool showButtons)
    {
        cancel.gameObject.SetActive(true);
        confirm.gameObject.SetActive(true);
        title.text = newTitle;
        description.text = newDescription;
        confirm.onClick.RemoveAllListeners();
        confirm.interactable = available;

        confirmText.text = option1name;
        cancelText.text = option2name;

        if (showButtons)
        {
            if (available)
            {
                confirm.onClick.AddListener(() => UIManager.Instance.confirmAction());
            }
            confirm.onClick.AddListener(() => CloseWithDelay());
          
        }
        else
        {
            confirm.gameObject.SetActive(false);
        }

        cancel.onClick.RemoveAllListeners();
        cancel.onClick.AddListener(() => CloseWithDelay());

    }

    public void SetDataTextInspect(string textContent, string gameParent)
    {
        title.text = gameParent;
        description.text = textContent;
        confirm.onClick.RemoveAllListeners();
        cancel.onClick.RemoveAllListeners();
        confirm.interactable = true;
        cancel.gameObject.SetActive(false);
        confirm.gameObject.SetActive(true);
        confirmText.text = "CLOSE";
        confirm.onClick.AddListener(() => CloseWithDelay());

        if (dofOnShow)
        {
            EnableDof();
        }
    }

    public void SetDataForMonument(string newTitle, string newDescription, string option1name)
    {
        title.text = newTitle;
        description.text = newDescription;
        confirm.onClick.RemoveAllListeners();
        cancel.onClick.RemoveAllListeners();
        confirm.interactable = true;
        cancel.gameObject.SetActive(false);
        confirm.gameObject.SetActive(true);
        confirmText.text = option1name;
        confirm.onClick.AddListener(() => UIManager.Instance.confirmAction());
        confirm.onClick.AddListener(() => CloseWithDelay());
    }

    public void CloseWithDelay()
    {
        StartCoroutine(CloseEnum());
    }
    
    IEnumerator CloseEnum()
    {
        yield return new WaitForSeconds(0.1f);
        AudioManager.Instance.ClickSound();
        UIManager.Instance.waitingForPopupReply = false;
        Close();
        
    }

    public override void Close()
    {
        DisableDof();
        base.Close();
    }


}
