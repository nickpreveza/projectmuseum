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


    [SerializeField] GameObject inventoryPanel;

    void Start()
    {
      
        if (UIManager.Instance != null)
        {
            UIManager.Instance.gamePanel = this.GetComponent<UIPanel>();
            UIManager.Instance.AddPanel(this);
        }
    }

    public void UpdateGUIButtons()
    {

    }

    public void MoneyChanged()
    {
        //getMoneyAnim.SetTrigger("GetMoney");
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
