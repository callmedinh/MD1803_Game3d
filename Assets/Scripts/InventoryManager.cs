
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities.InputManager;

public class InventoryManager : Singleton<InventoryManager>
{
    public List<Item> Items = new List<Item>();
    public Transform ItemContent;
    public GameObject InventoryItem;

    public GameObject inventoryUI;
    private bool hasOpened = false;

    public void Add(Item item)
    {
        Items.Add(item);
    }
    public void Remove(Item item)
    {
        Items.Remove(item);
    }
    public void ListItems()
    {
        //clean content before open
        foreach (Transform item in ItemContent) {
            Destroy(item.gameObject);
        }
        foreach (var item in Items)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);
            var itemName = obj.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
            var itemIcon = obj.transform.Find("ItemIcon").GetComponent<Image>();

            itemName.text = item.itemName;
            itemIcon.sprite = item.icon;
        }
    }
    private void Update()
    {
        if (VirtualInputManager.Instance.Escape)
        {
            if (hasOpened == false)
            {
                inventoryUI.SetActive(true);
                hasOpened = true;
                ListItems();
            }
            else
            {
                inventoryUI.SetActive(false);
                hasOpened = false;
            }
        }
    }
}
