using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// Deducts gold from the player based on which item they buy.
/// Displays a message saying they received item if successfully bought.
public class Shop : MonoBehaviour
{
    Player player;
    ShopUI shopUI = null;
    Inventory inventory = null;

    public void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        shopUI = GameObject.Find("Shop Canvas").GetComponent<ShopUI>();
        inventory = GameObject.Find("Player UI").GetComponentInChildren<Inventory>(true);
    }

    public void Buy(Item item)
    {
        if (player.currentGold < item.GetPrice())
        {
            shopUI.FailedMessage(item);
        }
        else
        {
            player.UpdateGold(-item.GetPrice());
            shopUI.RefreshShopUI();
            shopUI.BuyMessage(item);
            inventory.AddItem(item);
        }    
    }

    public void EnemyBuy(Item item, GameObject buyer)
    {
        EnemyData enemy = buyer.GetComponent<EnemyData>();
        if (enemy.currentGold > item.GetPrice())
        {
            enemy.UpdateGold(-item.GetPrice());

            var itemClone = item;
            enemy.itemList.Add(itemClone);
        }
        else print(buyer.name + " does not have enough gold.");
    }
}
