using UnityEngine;

public class ItemHighlighter : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        if (col.transform.CompareTag("Interactable"))
        {
            if (col.transform.TryGetComponent(out Interactable interactable))
            {
                if (interactable.isInteractable)
                {
                    interactable.Highlight(true);
                }
            }
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.transform.CompareTag("Interactable"))
        {
            if (col.transform.TryGetComponent(out Interactable interactable))
            {
                if (interactable.isInteractable)
                {
                    interactable.Highlight(false);
                }
            }
        }
    }
}
