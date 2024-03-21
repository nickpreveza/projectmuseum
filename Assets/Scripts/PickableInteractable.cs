using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PickableInteractable : Interactable
{
    public string itemKey;
    [SerializeField] Transform grabPointTransform;
    IEnumerator coroutine;
    Vector3 lerpTemp;
    Vector3 direction;
    public PlaceInteractable placedParent;
    FixedJoint joint;
    public bool isHeld
    {
        get
        {
            return (grabPointTransform != null);
        }
    }

    private void FixedUpdate()
    {
        /*
        if (grabPointTransform != null)
        {
            //if (joint != null && joint.connectedBody == null)
            //{
            //joint.connectedBody = GameManager.Instance.activePlayer.
            // }

           //lerpTemp = Vector3.Lerp(transform.position, grabPointTransform.position, Time.deltaTime * lerpSpeed);
            //rb.MovePosition(lerpTemp);
          // direction = transform.position - grabPointTransform.position;

            // Perform a raycast from object1 towards object2
            RaycastHit hit;
            if (Physics.Raycast(transform.position, direction, out hit, direction.magnitude))
            {
                
            }
            else
            {
               // rb.Move(lerpTemp, this.transform.rotation);
            }
           
           //transform.position = Vector3.Lerp(transform.position, grabPointTransform.position, Time.deltaTime * lerpSpeed);
        }*/
        
    }

    public override void Interact(Transform _grabPointTransform, FixedJoint targetJoint)
    {
        if (placedParent != null)
        {
            placedParent.ObjectRemoved();
            placedParent = null;
        }
        UIManager.Instance.overlayPanel?.GetComponent<OverlayPanel>().DisablePrompt();

        if (grabPointTransform == null)
        {
            Grab(_grabPointTransform, targetJoint);
        }
    }

    private void Grab(Transform _grabPointTransform, FixedJoint targetJoint)
    {
        Debug.Log(gameObject.name + " was grabbed");
        grabPointTransform = _grabPointTransform;
        joint = targetJoint;

        joint.connectedBody = rb;
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
        
        if (joint != null)
        {
            joint.connectedBody = null;
        }

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
