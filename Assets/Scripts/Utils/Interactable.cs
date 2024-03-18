using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public abstract class Interactable : MonoBehaviour
{
    [Header("Interactable Base")]
    public bool isInteractable = true;
    public float delayBetweenInteractions = 0.1f;

    [Header("Ability: Destroy-After-Interaction")]
    public bool destroyAfterInteraction;
    public float destroyDelay = .1f;

    [Header("Ability: Return to Original Position")]
    public bool returnToOriginalPosition;
    public float returnToStartDelay = 5;

    [Header("Follow speed")]
    public float lerpSpeed = 10f;

    [Header("UI")]
    public float overlayOffset;
    public bool usePlacementOverlay;

    protected Outline outline;


    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public Collider col;
    [HideInInspector] public Vector3 startPos;
    [HideInInspector] public Quaternion startRot;

    private void Awake()
    {
        //just to make sure you haven't forgot about these 
        gameObject.tag = "Interactable";

        outline = GetComponent<Outline>();

        rb = this.GetComponent<Rigidbody>();
        col = this.GetComponent<Collider>();

        startPos = transform.position;
        startRot = transform.rotation;

        DisableOutline();
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

    public virtual void Highlight(bool enable)
    {
        //UIManager.Instance.overlayPanel?.GetComponent<OverlayPanel>().DisablePrompt();
        if (enable)
        {
            EnableOutline();
        }
        else
        {
            DisableOutline();
        }

    }

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
