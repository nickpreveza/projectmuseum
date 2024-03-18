using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using TMPro;

public class PausePanel : UIPanel
{
    [Header("Subpanels")]
    [SerializeField] GameObject settingsPanel;

    [Header("Settings")]
    [SerializeField] Image audioImage;
    [SerializeField] Image musicImage;
    [SerializeField] Sprite audioOn;
    [SerializeField] Sprite audioOff;
    [SerializeField] Sprite musicOn;
    [SerializeField] Sprite musicOff;

    

    private void Start()
    {
        if (UIManager.Instance != null)
        {
            UIManager.Instance.pausePanel = this;
            UIManager.Instance.AddPanel(this);
        }

        HideSettingsPanel();

    }

    public override void Activate()
    {
        DepthOfField dof;
        if (GameManager.Instance.globalVolume.profile.TryGet<DepthOfField>(out dof))
        {
            dof.active = true;
        }
      
        base.Activate();
        HideSettingsPanel();
    }

    public override void Disable()
    {
        /*
        DepthOfField dof;
        if (globalVolume.profile.TryGet<DepthOfField>(out dof))
        {
            if (dof != null)
            dof.active = false;
        }*/
        base.Disable();
    }

    public void ActionResume()
    {
        //GameManager.Instance.SetPause = false;
    }

    public void RestartAction()
    {
        AudioManager.Instance.ClickSound();
        UIManager.Instance.OpenPopup(
                 "Return to title",
                 "Are you sure you want to exit?",
                 true,
                 "exit",
                 "cancel",
                 () => GameManager.Instance.ReloadScene(), true);

    }


    public void ExitAction()
    {
        AudioManager.Instance.ClickSound();
        UIManager.Instance.OpenPopup(
             "QUIT GAME",
             "Are you sure you want to exit?",
             true,
             "exit",
             "cancel",
             () => GameManager.Instance.ApplicationQuit(), true);
    }

    public void HideSettingsPanel()
    {
        settingsPanel.SetActive(false);
    }

    public void ActionExit()
    {
        UIManager.Instance.OpenMainMenu();
    }


    public void ToggleSettingsPanel()
    {
        settingsPanel.SetActive(!settingsPanel.activeSelf);
        AudioManager.Instance.ClickSound();
        if (settingsPanel.activeSelf)
        {
            if (AudioManager.Instance.musicOn)
            {
                musicImage.sprite = musicOn;
            }
            else
            {
                musicImage.sprite = musicOff;
            }

            if (AudioManager.Instance.audioOn)
            {
                audioImage.sprite = audioOn;
            }
            else
            {
                audioImage.sprite = audioOff;
            }
        }
    }

    public void ToggleMusic()
    {
        AudioManager.Instance.ClickSound();
        AudioManager.Instance.ToggleMusic();

        if (AudioManager.Instance.musicOn)
        {
            musicImage.sprite = musicOn;
        }
        else
        {
            musicImage.sprite = musicOff;
        }

    }

    public void ToggleAudio()
    {
        AudioManager.Instance.ClickSound();
        AudioManager.Instance.ToggleAudio();

        if (AudioManager.Instance.audioOn)
        {
            audioImage.sprite = audioOn;
        }
        else
        {
            audioImage.sprite = audioOff;
        }
    }


    public void CreditsLink(int index)
    {
        switch (index)
        {
            case 1:
                Application.OpenURL("https://nickpreveza.itch.io/");
                break;
            case 2:
                
                break;
            case 3:
               
                break;
            case 4:
              
                break;
            case 5:
           
                break;
        }
    }
}
