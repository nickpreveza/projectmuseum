using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlaceInteractable : MonoBehaviour
{
    public int sourceIndexKey; //this is our key. 
    public string matchingKey; //this is the key for acceptable items

    public bool isEmpty = true;

  
    [SerializeField] GameObject previewObject;

    [SerializeField] Renderer feedbackRenderer;
    [SerializeField] Color inactiveColor;
    [SerializeField] Color activeColor;

    [SerializeField] PickableInteractable candidateInteractable;
    [SerializeField] GameObject placedObject;

    private void Start()
    {
        feedbackRenderer.sharedMaterial.SetColor("_EmissionColor", inactiveColor);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (candidateInteractable == null)
        {
            if (other.GetComponent<PickableInteractable>() != null && other.GetComponent<PickableInteractable>().itemKey == matchingKey)
            {
                candidateInteractable = other.GetComponent<PickableInteractable>();
            }
        }
       
    }

    private void OnTriggerStay(Collider other)
    {
        if (candidateInteractable != null)
        {
            if (!candidateInteractable.isHeld && isEmpty)
            {
                SetObjectInPosition();
            }
        }
       
    }

    private void OnTriggerExit(Collider other)
    {
        if (candidateInteractable != null && !isEmpty)
        {
            if (other.gameObject == candidateInteractable.gameObject)
            {
               candidateInteractable = null;
               ObjectRemoved();
            }
        }
       
      
    }

    public void SetObjectInPosition()
    {
        if (!isEmpty) { return; }

        isEmpty = false;
        previewObject.SetActive(false);
        placedObject = candidateInteractable.gameObject;
        candidateInteractable.placedParent = this;
        placedObject.transform.position = previewObject.transform.position;
        
        feedbackRenderer.sharedMaterial.SetColor("_EmissionColor", activeColor);

        EventManager.Instance.OnActionTriggered(sourceIndexKey, -1, true);
    }

    public void ObjectRemoved()
    {
        if (isEmpty) { return; } 

        isEmpty = true;
        previewObject.SetActive(true);
        placedObject = null;

        feedbackRenderer.sharedMaterial.SetColor("_EmissionColor", inactiveColor);
        EventManager.Instance.OnActionTriggered(sourceIndexKey, -1, false);
    }

}
