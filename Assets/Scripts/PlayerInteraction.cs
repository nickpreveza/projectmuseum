using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private Transform grabPointTransform;
    [SerializeField] private float interactionDistance = 5f;
    [SerializeField] LayerMask interactionLayer;

    public bool holdsItem;
    public GameObject itemInHands;

    private void Start()
    {
        EventManager.Instance.onItemDropped += OnDrop;
        EventManager.Instance.onItemGrabbed += OnGrab;
    }

    private void OnDestroy()
    {
        EventManager.Instance.onItemDropped -= OnDrop;
        EventManager.Instance.onItemGrabbed -= OnGrab;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (holdsItem && itemInHands != null)
            {
                itemInHands.GetComponent<Interactable>().Drop();
            }
            else
            {
                if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, interactionDistance, interactionLayer))
                {
                    if (hit.transform.CompareTag("Interactable"))
                    {
                        if (hit.transform.TryGetComponent(out Interactable interactable))
                        {
                            interactable.TryInteract(grabPointTransform);
                        }
                    }

                }
            }
           
        }
    }

    void OnGrab(GameObject obj)
    {
        if (holdsItem)
        {
            Debug.LogError("Already holding item");
        }

        holdsItem = true;
        itemInHands = obj;

        //obj.transform.SetParent(this.gameObject.transform);
    }

    void OnDrop(GameObject obj)
    {
        if (holdsItem && obj == itemInHands)
        {
            holdsItem = false;
            itemInHands = null;
        }

        //obj.transform.SetParent(null);
    }
}
