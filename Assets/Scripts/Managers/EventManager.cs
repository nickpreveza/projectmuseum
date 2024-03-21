using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;

    public event Action onDataLoaded;
    public event Action onPlayerSpawned;

    public event Action<string, string> onPopUpRequested;
    public event Action<int> onExhibitSelected;

    public event Action<GameObject> onItemDropped;
    public event Action<GameObject> onItemGrabbed;

    public event Action<int,int, bool> onActionTriggered; //source, target, actionState
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void OnActionTriggered(int sourceIndex, int targetIndex, bool enabled)
    { 
        onActionTriggered?.Invoke(sourceIndex, targetIndex, enabled);
    }

    public void OnItemGrabbed(GameObject item)
    {
        onItemGrabbed?.Invoke(item);
    }
    public void OnItemDropped(GameObject item)
    {
        onItemDropped?.Invoke(item);
    }

    public void OnDataLoaded()
    {
        onDataLoaded?.Invoke();
    }

    public void OnPlayerSpawned()
    {
        onPlayerSpawned?.Invoke();
    }

    public void OnPopUpRequested(string header, string body)
    {
        onPopUpRequested?.Invoke(header, body);
    }

    public void OnExhibitSelected(int index)
    {
        onExhibitSelected?.Invoke(index);
    }
}
