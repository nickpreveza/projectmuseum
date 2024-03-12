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
