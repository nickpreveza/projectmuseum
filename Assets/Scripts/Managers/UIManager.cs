using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public UIPanel pausePanel;
    public UIPanel gamePanel;
    public UIPanel gameOverPanel;
    public UIPanel mainMenuPanel;


    public OverlayPanel overlayPanel;
    public UIPopup latestPopup;

    List<UIPanel> allPanelsList;

    public bool menuActive;
    public bool popupActive;
    public bool subPanelActive;

    [SerializeField] UIPanel currentSubpanel;
    [SerializeField] UniversalPopup universalPopup;

    public delegate void popupFunction();
    public popupFunction confirmAction;
    public popupFunction additionalAction;

    public bool waitingForPopupReply;

    public GameColors colors;
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

    private void Start()
    {
        ClosePopup();
    }

    public void OpenPopup(string title, string description, bool available, string option1name, string option2name, popupFunction newFunction, bool showButtons)
    {
        confirmAction = newFunction;
        additionalAction = null;

        universalPopup.gameObject.SetActive(true);
        universalPopup.SetData(title, description, available, option1name, option2name, showButtons);
    }

    public void OpenPopupInformative(string title, string description, string option1name)
    {
        confirmAction = null;
        additionalAction = null;

        waitingForPopupReply = true;


        universalPopup.gameObject.SetActive(true);

        universalPopup.SetDataForMonument(title, description, option1name);
    }
    public void OpenPopUpMonument(string title, string description, string option1name, popupFunction newFunction)
    {
        confirmAction = null;
        additionalAction = null;

        waitingForPopupReply = true;
        confirmAction = newFunction;

        universalPopup.gameObject.SetActive(true);

        universalPopup.SetDataForMonument(title, description, option1name);
    }

    public void OpenPopupReward(string title, string description, string option1name, popupFunction option1, string option2name, popupFunction option2)
    {
        confirmAction = null;
        additionalAction = null;

        waitingForPopupReply = true;
        confirmAction = option1;
        additionalAction = option2;

        universalPopup.gameObject.SetActive(true);
        universalPopup.SetDataForReward(title, description, option1name, option2name);
    }

    public void ClosePopup()
    {
        universalPopup.Close();
    }

    public void ToggleUIPanel(UIPanel targetPanel, bool state, bool fadeGamePanel = true, float delayAmount = 0.0f)
    {
        StartCoroutine(ToggleUIPanelEnum(targetPanel, state, fadeGamePanel, delayAmount));
    }

    public void HideInventoryPanel()
    {
        gamePanel.GetComponent<GamePanel>().HideInventoryPanel();
    }

    IEnumerator ToggleUIPanelEnum(UIPanel targetPanel, bool state, bool fadeGamePanel = true, float delayAmount = 0.0f)
    {
        yield return new WaitForSeconds(delayAmount);

        if (state)
        {
            GameManager.Instance.menuActive = true;
            currentSubpanel = targetPanel;
            if (fadeGamePanel)
            {
                gamePanel.canvasGroup.alpha = 0;
            }

            overlayPanel.DisablePrompt();
            subPanelActive = true;
            targetPanel.Activate();
        }
        else
        {
            gamePanel.canvasGroup.alpha = 1;
            subPanelActive = false;
            targetPanel.Disable();
            GameManager.Instance.menuActive = false;
            HideTooltip();
        }
    }


    public void CloseCurrentSubpanel()
    {
        if (currentSubpanel == null)
        {
            return;
        }
        currentSubpanel.Disable();
        subPanelActive = false;
        gamePanel.canvasGroup.alpha = 1;
        GameManager.Instance.menuActive = true;
        currentSubpanel = null;
    }

    public void AddPanel(UIPanel newPanel)
    {
        if (allPanelsList == null)
        {
            allPanelsList = new List<UIPanel>();
        }

        if (!allPanelsList.Contains(newPanel))
        {
            allPanelsList.Add(newPanel);
        }
    }

    public void StartTextbox(string characterName, List<string> dialogContent)
    {
        GameManager.Instance.menuActive = true;
        gamePanel.GetComponent<GamePanel>().textbox.StartTextbox(characterName, dialogContent);
    }


    public void EndTextbox()
    {
        GameManager.Instance.menuActive = false;
        gamePanel.GetComponent<GamePanel>().textbox.EndTextbox();
    }

    public void ClosePanels()
    {
        mainMenuPanel?.Disable();
        pausePanel?.Disable();
        gamePanel?.Disable();
        gameOverPanel?.Disable();
    }

    public void OpenGamePanel()
    {
        ClosePanels();
        gamePanel.GetComponent<GamePanel>().Setup();
        gamePanel.Activate();

        menuActive = false;
        GameManager.Instance.menuActive = false;

        UpdateHUD();
    }

    public void OpenMainMenu()
    {

        ClosePanels();

        menuActive = true;
        mainMenuPanel.Setup();
        mainMenuPanel.Activate();

        GameManager.Instance.menuActive = true;
    }

    public void GameOver(Player player)
    {
        gamePanel.Disable();
        pausePanel.Disable();

        gameOverPanel.Setup();
        gameOverPanel.Activate();
    }

    public void UpdateHUD()
    {
        GamePanel panel = gamePanel.GetComponent<GamePanel>();

        panel.UpdateCurrencies();
        panel.UpdateGUIButtons();

    }

    public void UpdateGUIButtons()
    {
        gamePanel.GetComponent<GamePanel>().UpdateGUIButtons();
    }

    public void AnimateMoney()
    {
        gamePanel.GetComponent<GamePanel>().MoneyChanged();
    }

    public void ShowTooltip(string body, string header = "")
    {
        //overlayPanel.tooltip.SetData(body, header);
        // overlayPanel.tooltip.gameObject.SetActive(true);
    }

    public void HideTooltip()
    {
        //overlayPanel.tooltip.gameObject.SetActive(false);
    }

    /// <summary>
    /// Used to close all the panels registered to the list
    /// </summary>
    public void CloseAllPanels()
    {
        if (allPanelsList == null)
        {
            Debug.LogWarning("CloseAllPanels failed. No panels registered");
            return;
        }
        foreach (UIPanel panel in allPanelsList)
        {
            panel.Disable();
        }
    }

    /// <summary>
    /// Called from GameManager when the Paused state is changed. Could be using an event here
    /// </summary>
    public void PauseChanged()
    {
        if (GameManager.Instance.isPaused)
        {
            gamePanel.canvasGroup.alpha = 0;
            pausePanel.Activate();
        }
        else
        {
            gamePanel.canvasGroup.alpha = 1;
            pausePanel.Disable();
        }
    }

    public void ActionOpenItchPage()
    {
        Application.OpenURL(GameManager.Instance.data.itchURL);
    }

    public void ActionOpenWebsite()
    {
        Application.OpenURL(GameManager.Instance.data.websiteURL);
    }

}

