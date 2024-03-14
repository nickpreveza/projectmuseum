using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PickableInteractable : Interactable
{
    [SerializeField] Transform grabPointTransform;
    IEnumerator coroutine;

    private void Update()
    {
        if (grabPointTransform != null)
        {
            //Vector3 newPos = Vector3.Lerp(transform.position, grabPointTransform.position, Time.deltaTime * lerpSpeed);
            //rb.MovePosition(newPos);
            
            transform.position = Vector3.Lerp(transform.position, grabPointTransform.position, Time.deltaTime * lerpSpeed);
        }
    }

    public override void Interact(Transform _grabPointTransform)
    {
        UIManager.Instance.overlayPanel?.GetComponent<OverlayPanel>().DisablePrompt();

        if (grabPointTransform == null)
        {
            Grab(_grabPointTransform);
        }
    }

    private void Grab(Transform _grabPointTransform)
    {
        Debug.Log(gameObject.name + " was grabbed");
        grabPointTransform = _grabPointTransform;
        rb.isKinematic = true;
        rb.useGravity = false;
        rb.freezeRotation = true;
        isInteractable = false;
        DisableOutline();

        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }

        EventManager.Instance.OnItemGrabbed(this.gameObject);
    }

    public override void Drop()
    {
        Debug.Log(gameObject.name + " was dropped");
        EventManager.Instance.OnItemDropped(gameObject);

        isInteractable = false;
        grabPointTransform = null;
        rb.isKinematic = false;
        rb.freezeRotation = false;
        rb.useGravity = true;
        EnableOutline();

        if (destroyAfterInteraction)
        {
            coroutine = DestroyWithDelay();
        }
        else if (returnToOriginalPosition)
        {
            coroutine = ReturnToStartState();
        }
        else
        {
            coroutine = InteractableAgain();
        }
       
        StartCoroutine(coroutine);
    }


    IEnumerator InteractableAgain()
    {
        yield return new WaitForSeconds(delayBetweenInteractions);
        isInteractable = true;
    }

    IEnumerator DestroyWithDelay()
    {
        yield return new WaitForSeconds(destroyDelay);
        Destroy(this.gameObject);
    }

    IEnumerator ReturnToStartState()
    {
        yield return new WaitForSeconds(returnToStartDelay);

        transform.position = startPos;
        transform.rotation = startRot;
        isInteractable = true;
        rb.useGravity = false;
    }
}

public enum InteractionType
{
    INSPECT, //TODO
    GRAB,
    GRABRETURNABLE,
    COLLECT //TODO
}
