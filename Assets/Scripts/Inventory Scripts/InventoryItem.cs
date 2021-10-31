using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    private Item item;
    public TextMeshProUGUI nameText;
    public Image icon;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnUse);
    }

    public void Init(Item item) 
    {
        icon.sprite = item.GetIcon();
        nameText.text = item.GetDisplayName();
        this.item = item;
    }

    private void OnUse()
    {
        UIManager.Instance.player.UpdateAttributes(item.health, item.maxHealth, item.attackPower, item.attackRange, item.defense, item.movement);

        Transform birthPoint = UIManager.Instance.player.GetComponent<RollDiceToMove>().nodeList.nodeList[0];

        switch (item.GetItemID())
        {
            case "3"://Confusion Brew
                UIManager.Instance.player.GetComponent<RollDiceToMove>().changeDirection = true;
                break;

            case "4"://City Scroll
                UIManager.Instance.player.GetComponent<RollDiceToMove>().nodesCurrentIndex = 0;
                UIManager.Instance.player.GetComponent<RollDiceToMove>().transform.position = birthPoint.position;
                GameObject.Find("GameManager").GetComponent<UIControl>().inventoryClicked();
                break;

            case "5"://Jixing Pill
                CharacterAttributes.Instance.temporaryMovement = 2;
                break;

            case "6"://Forage
                GameObject.Find("Player Dice").GetComponent<ShowDiceResult>().HideResult();
                AiController.hasPlayerMoved = false;
                break;

            case "7"://Acacia                
                int range = UIManager.Instance.player.GetComponent<RollDiceToMove>().nodeList.nodeList.Count;
                int newNodeIndex = Random.Range(0, range);
                UIManager.Instance.player.GetComponent<RollDiceToMove>().nodesCurrentIndex = newNodeIndex;
                UIManager.Instance.player.GetComponent<RollDiceToMove>().transform.position = UIManager.Instance.player.GetComponent<RollDiceToMove>().nodeList.nodeList[newNodeIndex].position;
                GameObject.Find("GameManager").GetComponent<UIControl>().inventoryClicked();
                break;

            case "9"://Tiger Idol
                UIManager.Instance.player.UpdateAttributes(CharacterAttributes.Instance.maxHealth / 2, item.maxHealth, item.attackPower, item.attackRange, item.defense, item.movement);
                UIManager.Instance.player.GetComponent<RollDiceToMove>().nodesCurrentIndex = 0;
                UIManager.Instance.player.GetComponent<RollDiceToMove>().transform.position = birthPoint.position;
                GameObject.Find("GameManager").GetComponent<UIControl>().inventoryClicked();
                break;

            case "10"://Jade Seal
                UIManager.Instance.player.UpdateAttributes(CharacterAttributes.Instance.maxHealth, item.maxHealth, item.attackPower, item.attackRange, item.defense, item.movement);
                break;
        }

        GameObject.Destroy(gameObject);
    }
}
