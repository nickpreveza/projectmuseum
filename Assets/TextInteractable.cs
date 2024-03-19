using UnityEngine;
using TMPro;

public class TextInteractable : Interactable
{
    [SerializeField] string gameParentContent;
    [SerializeField] string textContent;
    [SerializeField] TextMeshProUGUI textmeshOrigin;
    bool isBeingSeen;

    private void Start()
    {
        if (textmeshOrigin != null)
        {
            textContent = textmeshOrigin.text;
        }
    }
    public override void Interact(Transform _grabPointTransform)
    {
        UIManager.Instance.overlayPanel?.GetComponent<OverlayPanel>().DisablePrompt();

        if (!isBeingSeen)
        {
            UIManager.Instance.ShowTextInspect(textContent, gameParentContent);
            isBeingSeen = true;
        }
        else
        {
            isBeingSeen = false;
            Drop();
        }
       
        Debug.Log("Should see interact text");
    }


    public override void Drop()
    {
        UIManager.Instance.CloseTextInspect();
    }

}
