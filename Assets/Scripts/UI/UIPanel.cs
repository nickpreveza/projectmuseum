using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public abstract class UIPanel : MonoBehaviour
{
    public bool fadeIn;
    public bool fadeOut;
    public bool enableDoF;
    [HideInInspector] public bool isActive;
    public bool startEnabled;
    [HideInInspector] public GameObject panelObject;
    [HideInInspector] public CanvasGroup canvasGroup;

    bool fadingIn;
    bool fadingOut;
    private void Awake()
    {
        panelObject = transform.GetChild(0).gameObject;
        canvasGroup = transform.GetChild(0).GetComponent<CanvasGroup>();
        if (!startEnabled)
        Disable();
    }
    public virtual void Activate()
    {
        if (enableDoF)
        {
            DepthOfField dof;
            if (GameManager.Instance.globalVolume.profile.TryGet<DepthOfField>(out dof))
            {
                dof.active = true;
            }

        }

        isActive = true;
        panelObject.SetActive(true);

        if (fadeIn)
        {
            canvasGroup.alpha = 0;
            fadingIn = true;
        }

    }

    private void Update()
    {
        if (fadingIn)
        {
            if (canvasGroup.alpha < 1)
            {
                canvasGroup.alpha += 0.1f;
            }
            else
            {
                fadingIn = false;
            }
        }

        if (fadingOut)
        {
            if (canvasGroup.alpha > 0)
            {
                canvasGroup.alpha -= 0.1f;
            }
            else
            {
                fadingOut = false;
                DisableAfterFade();
            }
        }
    }

    public virtual void UpdateData()
    {

    }

    public virtual void Setup()
    {

    }

    public virtual void Disable()
    {

        DepthOfField dof;
        if (GameManager.Instance.globalVolume.profile.TryGet<DepthOfField>(out dof))
        {
            if (dof != null)
                dof.active = false;
        }

        if (fadeOut)
        {
            canvasGroup.alpha = 1;
            fadingOut = true;
        }
        else
        {
            isActive = false;
            panelObject.SetActive(false);
        }
        
    }

    void DisableAfterFade()
    {
        isActive = false;
        panelObject.SetActive(false);
    }


}
