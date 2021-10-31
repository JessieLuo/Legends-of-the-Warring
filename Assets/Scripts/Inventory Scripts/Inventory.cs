using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public List<InventoryItem> itemList = new List<InventoryItem>();

    public InventoryItem itemPrefab;
    public Transform itemParent;

    public void AddItem(Item item) 
    {
        var itemClone = GameObject.Instantiate(itemPrefab, itemParent);
        itemClone.GetComponent<InventoryItem>().Init(item);
        itemList.Add(itemClone);
    }

    public GameObject AddSkill(Item item)
    {
        var itemClone = GameObject.Instantiate(itemPrefab, itemParent);
        itemClone.GetComponent<InventoryItem>().Init(item);
        itemList.Add(itemClone);
        return itemClone.gameObject;
    }
}
