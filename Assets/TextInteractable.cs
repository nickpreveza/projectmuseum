using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TextInteractable : Interactable
{
    public string gameKey;

    [SerializeField] Image gameCover;

    [SerializeField] TextMeshProUGUI titleText;
    [SerializeField] TextMeshProUGUI descriptionText;

    [SerializeField] TextMeshProUGUI releaseDateText;
    [SerializeField] TextMeshProUGUI developerText;
    [SerializeField] TextMeshProUGUI publisherText;
    private void Start()
    {
        GameExhibitData gameData = GameManager.Instance.TryFindGameWithKey(gameKey);

        if (gameData == null)
        {
            isInteractable = false;
            titleText.text = "KEY DOES NOT EXITS";
            return;
        }

        titleText.text = gameData.name;
        descriptionText.text = gameData.description;
        releaseDateText.text = gameData.releaseDate;
        developerText.text = gameData.developer;
        publisherText.text = gameData.publisher;
        gameCover.sprite = gameData.coverImage;
    }


    public override void Interact(Transform _grabPointTransform)
    {
        //  UIManager.Instance.overlayPanel?.GetComponent<OverlayPanel>().DisablePrompt();

        UIManager.Instance.ShowTextInspect(gameKey);
        EventManager.Instance.OnItemGrabbed(this.gameObject);
        Debug.Log("Should see interact text");
    }


    public override void Drop()
    {
        UIManager.Instance.CloseTextInspect();
        EventManager.Instance.OnItemDropped(this.gameObject);
    }

}
