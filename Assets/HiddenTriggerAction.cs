using UnityEngine;

public class HiddenTriggerAction : MonoBehaviour, IActionTrigger
{
    public bool canBeTriggeredMultipeTimes;
    public bool hasBeenTriggered;

    public bool setStateOnTrigger = false;
    public int source;
    public int target;
    public int sourceIndex
    {
        get
        {
            return source;
        }
        set
        {
            source = value;
        }
    }

    public int targetIndex
    {
        get
        {
            return target;
        }
        set
        {
            target = value;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (hasBeenTriggered && !canBeTriggeredMultipeTimes)
        {
            return;
        }

        if (col.gameObject.GetComponent<PlayerInteraction>() != null)
        {
            TriggerAction(setStateOnTrigger);
            hasBeenTriggered = true;
        }
    }

    public void TriggerAction(bool enabled)
    {
        EventManager.Instance.OnActionTriggered(sourceIndex, targetIndex, enabled);
    }
}
