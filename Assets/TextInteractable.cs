using UnityEngine;
using TMPro;

public class TextInteractable : Interactable
{
    [SerializeField] string gameParentContent;
    [SerializeField] string textContent;
    [SerializeField] TextMeshProUGUI textmeshOrigin;

    private void Start()
    {
        if (textmeshOrigin != null)
        {
            textContent = textmeshOrigin.text;
        }
    }
    public override void Interact(Transform _grabPointTransform)
    {
        //  UIManager.Instance.overlayPanel?.GetComponent<OverlayPanel>().DisablePrompt();

        UIManager.Instance.ShowTextInspect(textContent, gameParentContent);
        EventManager.Instance.OnItemGrabbed(this.gameObject);
        Debug.Log("Should see interact text");
    }


    public override void Drop()
    {
        UIManager.Instance.CloseTextInspect();
        EventManager.Instance.OnItemDropped(this.gameObject);
    }

}
