using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [SerializeField] LayerMask interactableLayer;

    public bool isInteractable = true;
    
    public bool interactableAgain; //can the interaction be repeated;
    public float delayBetweenInteractions = 0.1f;

    //One off
    public bool destroyAfterInteraction;
    public float destroyDelay;


    //Return to Start
    public bool returnToOriginalPosition;
    public float returnToStartDelay = 5;

    //Lerp
    public float lerpSpeed = 10f;

    //UI
    public float overlayOffset;
    public bool usePlacementOverlay;

    protected Outline outline;

    private void Awake()
    {
        //just to make sure you haven't forgot about these 
        gameObject.tag = "Interactable";

        outline = GetComponent<Outline>();
        if (!isInteractable)
        {
            outline.enabled = false;
        }
    }

    public bool TryInteract(Transform grabPointTransform)
    {
        if (!isInteractable)
        {
            return false;
        }

        Interact(grabPointTransform);
        return true;
    }

    public abstract void Highlight(bool enable);

    public abstract void Interact(Transform grabPointTransform);

    public abstract void Drop();

    public void EnableOutline()
    {
        if (GetComponent<Outline>().enabled == true)
        {
            return;
        }

        if (isInteractable)
        {
            GetComponent<Outline>().enabled = true;
        }
    }

    public void DisableOutline()
    {
        if (isInteractable)
        {
            GetComponent<Outline>().enabled = false;
        }
    }
}
