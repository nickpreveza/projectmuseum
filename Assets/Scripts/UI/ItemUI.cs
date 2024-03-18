using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ItemUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int itemIndex;
    Button button;
    public Item data;
    GameObject handler;
   
    [SerializeField] Image targetGraphic;
    [SerializeField] GameObject emptyGraphic;
    [SerializeField] TextMeshProUGUI targetAmountText;

    bool isInteractable;
    bool isHovering;
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (data.isUnlocked)
        {
            isHovering = true;
            StartCoroutine(WaitForTooltip());
        }
    }

    IEnumerator WaitForTooltip()
    {
        yield return new WaitForSeconds(0.5f);
        UIManager.Instance.ShowTooltip(data.description, data.displayName);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopAllCoroutines();
        UIManager.Instance.HideTooltip();
    }

    public void UpdateData(GameObject _handler, bool showAmount)
    {
        button = GetComponent<Button>();
        handler = _handler;

        if (showAmount)
        {
           // targetAmountText.text = newItem.startingAmount.ToString();
        }
        else
        {
            targetAmountText.text = "";
        }

        button.onClick.RemoveAllListeners();
        isInteractable = false;
        button.interactable = false;
        emptyGraphic.SetActive(true);
    }
    public void UpdateData(Item newItem, GameObject _handler, bool showAmount)
    {
        button = GetComponent<Button>();
        handler = _handler;
        data = newItem;
        targetGraphic.sprite = data.icon;
        targetGraphic.gameObject.SetActive(true);

        if (showAmount)
        {
            //targetAmountText.text = newItem.startingAmount.ToString();
            targetAmountText.text = "";
        }
        else
        {
            targetAmountText.text = "";
        }
       
        button.onClick.RemoveAllListeners();

        if (newItem == null)
        {
            isInteractable = false;
            button.interactable = false;
            return;
        }

        if (newItem.isUnlocked)
        {
            emptyGraphic.SetActive(false);
        }
        else
        {
            emptyGraphic.SetActive(true);
        }

        if (isInteractable)
        {
            button.onClick.AddListener(() => Interact());
        } 
    }

    public void UpdateAmount()
    {
        /*
        if (!craftOnInteract && ItemManager.Instance.playerInventory.ContainsKey(data.key))
        {
            targetAmountText.text = ItemManager.Instance.playerInventory[data.key].ToString();
        }*/
        
    }

    public void Interact() //When is in inventory
    {
        /*
        switch (data.type)
        {
            case ItemType.Currency:
            case ItemType.Material:
                Debug.LogWarning("These items should not be interactable");
                isInteractable = false;
                button.interactable = false;
                break;
            case ItemType.Weapon:
                ItemManager.Instance.EquipItem(data.key);
                SetSelectedInWeaponsTab();
                break;
            case ItemType.Placeable:
                //ItemMaanager.Instance.TryToPlace();
            case ItemType.Consumable:
            case ItemType.Clothing:
                ItemManager.Instance.UseItem(data.key, 1, true);
                break;
        }*/
    }

    public void OnSelect()
    {
        GetComponent<Image>().color = UIManager.Instance.colors.uiSelected;
    }

    public void OnDeselect()
    {
        GetComponent<Image>().color = UIManager.Instance.colors.uiDeselected;
    }

    public void SetSelected()//When is in factory
    {
        //handler.GetComponent<FactoryUI>().SelectItem(this);
    }

    public void SetSelectedInWeaponsTab()
    {
       // handler.GetComponent<InventoryUI>().SelectedWeapon(this);
    }
}
