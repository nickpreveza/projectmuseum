using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class GamePanel : UIPanel
{
    [SerializeField] TextMeshProUGUI areaName;

    [SerializeField] TextMeshProUGUI exhibitHeader;
    [SerializeField] TextMeshProUGUI exhibitBody;

    public TextMeshProUGUI devText;
    public Textbox textbox;

    [SerializeField] Image audioImage;
    [SerializeField] Image musicImage;
    [SerializeField] Sprite audioOn;
    [SerializeField] Sprite audioOff;
    [SerializeField] Sprite musicOn;
    [SerializeField] Sprite musicOff;

    [SerializeField] GameObject settingsPanel;
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject inventoryPanel;

    void Start()
    {
      
        if (UIManager.Instance != null)
        {
            UIManager.Instance.gamePanel = this.GetComponent<UIPanel>();
            UIManager.Instance.AddPanel(this);
        }

        HidePausePanel();
        HideSettingsPanel();
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

    public void HideSettingsPanel()
    {
        settingsPanel.SetActive(false);
    }

    public void SetPausePanelState(bool isPaused)
    {
        pausePanel.SetActive(isPaused);
        AudioManager.Instance.ClickSound();
        HideSettingsPanel();
    }

    public void HidePausePanel()
    {
        pausePanel.SetActive(true);
    }

    public void UpdateGUIButtons()
    {

    }

    public void MoneyChanged()
    {
        //getMoneyAnim.SetTrigger("GetMoney");
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

   
    public void ToggleInventoryPanel()
    {
        AudioManager.Instance.ClickSound();
        if (inventoryPanel.activeSelf)
        {
            inventoryPanel.SetActive(false);
        }
        else
        {
            //inventoryPanel.GetComponent<InventoryPanel?().FetchData();
            inventoryPanel.SetActive(true);
        }
    }

    public void HideInventoryPanel()
    {
        inventoryPanel.SetActive(false);
    }


    public void UpdateCurrencies()
    {
        
    }

   
    public override void Setup()
    {
        base.Setup();
    }

    public override void Activate()
    {
        base.Activate();
    }

    public override void Disable()
    {
        base.Disable();
    }
}
