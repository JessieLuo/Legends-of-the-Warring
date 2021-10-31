using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AiController : MonoBehaviour
{
    public List<GameObject> enemyList = new List<GameObject>();
    private GameObject enemy;
    public GameObject player;
    private Camera mainCamera;
    private GameObject turnEndingPanel;
    private GameObject turnTitlePanel;
    private GameObject actionPanel;
    private GameObject enemyStatePanel;
    private GameObject enemyHealthStatePanel;
    private GameObject enemyGoldStatePanel;
    private GameObject inventoryButton;
    private GameObject attributesButton;
    private GameObject endTurnButton;
    private GameObject diceButton;
    private Image iconImage;
    private GameObject dice;
    private Shop shop;
    private ShopUI shopUI;

    private string healthState = "Neutral";
    private string goldState = "Neutral";
    private string desiredItem = "Medicine";
    private bool needsHealing = false;
    private int enemyIndex = 0;
    public static bool hasPlayerMoved = false;
    public static bool inBattle = false;

    void Start()
    {
        enemyList.Add(GameObject.Find("Enemy 1"));
        enemyList.Add(GameObject.Find("Enemy 2"));
        enemyList.Add(GameObject.Find("Enemy 3"));
        enemy = enemyList[enemyIndex];
        mainCamera = Camera.main;
        player = GameObject.FindGameObjectWithTag("Player");

        turnEndingPanel = GameObject.Find("Turn Ending Panel");
        turnEndingPanel.SetActive(false);
        turnTitlePanel = GameObject.Find("Turn Title Panel");
        turnTitlePanel.SetActive(false);
        actionPanel = GameObject.Find("Enemy Action Panel");
        actionPanel.SetActive(false);

        enemyStatePanel = GameObject.Find("Enemy State Panel");
        enemyHealthStatePanel = GameObject.Find("Enemy Health State");
        enemyGoldStatePanel = GameObject.Find("Enemy Gold State");
        enemyStatePanel.SetActive(false);

        iconImage = GameObject.Find("Icon Image").GetComponent<Image>();
        inventoryButton = GameObject.Find("Inventory Button");
        attributesButton = GameObject.Find("Attributes Button");
        endTurnButton = GameObject.Find("End Turn Button");
        diceButton = GameObject.Find("Dice Button");
        dice = GameObject.Find("Player Dice");
        shop = GameObject.Find("AI Shop").GetComponent<Shop>();
        shopUI = GameObject.Find("Shop Canvas").GetComponent<ShopUI>();
        GameObject.Find("Battle UI").GetComponent<Canvas>().enabled = false;
    }

    void Update()
    {
        if (hasPlayerMoved)
        {
            diceButton.GetComponent<Button>().interactable = false;
        }
        else diceButton.GetComponent<Button>().interactable = true;

        // Trigger battle scene if two enemies are on same node
        if (player.GetComponent<RollDiceToMove>().currentNode == enemyList[2].GetComponent<RollDiceToMove>().currentNode)
        {           
            player.GetComponent<RollDiceToMove>().isMoving = false;
            player.GetComponent<RollDiceToMove>().currentNode = enemyList[2].GetComponent<RollDiceToMove>().currentNode;
            enemyList[2].GetComponent<RollDiceToMove>().isMoving = false;
            enemyList[2].GetComponent<RollDiceToMove>().currentNode = player.GetComponent<RollDiceToMove>().currentNode;
            if (!inBattle)
            {
                StartCoroutine(BattleScence.instance.BattleScenceInit(enemyList[2]));
                inBattle = true;
            }
        }
        else if (player.GetComponent<RollDiceToMove>().currentNode == enemyList[1].GetComponent<RollDiceToMove>().currentNode)
        {          
            player.GetComponent<RollDiceToMove>().isMoving = false;
            player.GetComponent<RollDiceToMove>().currentNode = enemyList[1].GetComponent<RollDiceToMove>().currentNode;
            enemyList[1].GetComponent<RollDiceToMove>().isMoving = false;
            enemyList[1].GetComponent<RollDiceToMove>().currentNode = player.GetComponent<RollDiceToMove>().currentNode;
            if (!inBattle)
            {
                StartCoroutine(BattleScence.instance.BattleScenceInit(enemyList[1]));
                inBattle = true;
            }
        }
        else if (player.GetComponent<RollDiceToMove>().currentNode == enemyList[0].GetComponent<RollDiceToMove>().currentNode)
        {           
            player.GetComponent<RollDiceToMove>().isMoving = false;
            player.GetComponent<RollDiceToMove>().currentNode = enemyList[0].GetComponent<RollDiceToMove>().currentNode;           
            enemyList[0].GetComponent<RollDiceToMove>().isMoving = false;
            enemyList[0].GetComponent<RollDiceToMove>().currentNode = player.GetComponent<RollDiceToMove>().currentNode;
            if (!inBattle)
            {
                StartCoroutine(BattleScence.instance.BattleScenceInit(enemyList[0]));
                inBattle = true;
            }
        }
    }

    // Called when END TURN button clicked by player
    public void StartComputerTurn()
    {
        StartCoroutine(Pattern());
    }

    // Changes the camera to focus on a new game object
    private void ChangeCameraFocus(GameObject cameraObject)
    {
        mainCamera.GetComponent<CameraMoving>().target = cameraObject.transform;
    }

    // Calls the Roll Dice function on Player
    public void RollForPlayer()
    {
        hasPlayerMoved = true;
        player.GetComponent<RollDiceToMove>().Click();
        GameObject.FindObjectOfType<MagicBar>().AddMagic(0.2f);
    }

    // Calls the Roll Dice function on enemy
    private void RollComputer()
    {
        enemy.GetComponent<RollDiceToMove>().Click();
    }

    // Updates UI with enemies data and updates panels/buttons
    private void UpdateUIEnemy()
    {
        hasPlayerMoved = true;

        ChangeCameraFocus(enemy);
        enemy.GetComponent<EnemyData>().UpdateHealthBar();
        enemy.GetComponent<EnemyData>().UpdateGold(0);
        iconImage.sprite = enemy.GetComponent<SpriteRenderer>().sprite;
        turnTitlePanel.GetComponentInChildren<TextMeshProUGUI>().text = $"Turn: {enemy.name}";
        enemyHealthStatePanel.GetComponentInChildren<TextMeshProUGUI>().text = $"{healthState}";
        enemyGoldStatePanel.GetComponentInChildren<TextMeshProUGUI>().text = $"{goldState}";

        enemyStatePanel.SetActive(true);
        turnTitlePanel.SetActive(true);
        endTurnButton.SetActive(false);
        inventoryButton.GetComponent<Button>().interactable = false;
        attributesButton.GetComponent<Button>().interactable = false;
        dice.GetComponent<ShowDiceResult>().HideResult();
    }

    // Checks the Enemy's variables to decide what state they will be in
    // The state will determine what actions they want to take
    private void StateMachine()
    {
        // Check health to determine enemies state
        if (enemy.GetComponent<EnemyData>().health < (enemy.GetComponent<EnemyData>().maxHealth / 2))
        {
            healthState = "Scared";
        }
        else if (enemy.GetComponent<EnemyData>().health >= (enemy.GetComponent<EnemyData>().maxHealth / 4 * 3))
        {
            healthState = "Aggressive";
        }
        else
        {
            healthState = "Neutral";
        }

        // Check gold to narrow down their state
        if (enemy.GetComponent<EnemyData>().currentGold <= 140)
        {
            goldState = "Broke";
        }
        else if (enemy.GetComponent<EnemyData>().currentGold <= 500)
        {
            goldState = "Poor";
        }
        else if (enemy.GetComponent<EnemyData>().currentGold >= 1500)
        {
            goldState = "Rich";
        }
        else
        {
            goldState = "Decent";
        }

        // Switch their desired item based on gold
        switch (goldState)
        {
            case "Rich":
                desiredItem = "Warlord's Armour";
                break;

            case "Decent":
                if (healthState == "Scared")
                {
                    desiredItem = "Medicine";
                }
                else desiredItem = "Long Sword";
                break;

            case "Poor":
                if (healthState == "Scared")
                {
                    desiredItem = "Medicine";
                }
                else if (healthState == "Neutral")
                {
                    desiredItem = null;
                }
                else desiredItem = "Long Sword";
                break;

            case "Broke":
                desiredItem = null;
                break;
        }
    }

    // Tells the enemy to buy an item (currently only medicine).
    // Pass in item name you want to buy and enemy will attempt to buy it
    private void BuyItem(string itemName)
    {
        if (itemName == "Medicine")
        {
            shop.EnemyBuy(shopUI.medicine, enemy);

            // Print only if buy is successful
            if (enemy.GetComponent<EnemyData>().itemList.Contains(shopUI.medicine))
            {
                actionPanel.GetComponentInChildren<TextMeshProUGUI>().text = $"{enemy.name} bought {shopUI.medicine.name}";
                actionPanel.SetActive(true);
            }
        }
        else if (itemName == "Warlord's Armour")
        {
            shop.EnemyBuy(shopUI.warlordsArmour, enemy);

            // Print only if buy is successful
            if (enemy.GetComponent<EnemyData>().itemList.Contains(shopUI.warlordsArmour))
            {
                actionPanel.GetComponentInChildren<TextMeshProUGUI>().text = $"{enemy.name} bought {shopUI.warlordsArmour.name}";
                actionPanel.SetActive(true);
            }
            else
            {
                itemName = "Cloth Armour";
            }
        }
        else if (itemName == "Long Sword")
        {
            shop.EnemyBuy(shopUI.longSword, enemy);

            // Print only if buy is successful
            if (enemy.GetComponent<EnemyData>().itemList.Contains(shopUI.longSword))
            {
                actionPanel.GetComponentInChildren<TextMeshProUGUI>().text = $"{enemy.name} bought {shopUI.longSword.name}";
                actionPanel.SetActive(true);
            }
        }
        else if (itemName == "Cloth Armour")
        {
            shop.EnemyBuy(shopUI.clothArmour, enemy);

            // Print only if buy is successful
            if (enemy.GetComponent<EnemyData>().itemList.Contains(shopUI.clothArmour))
            {
                actionPanel.GetComponentInChildren<TextMeshProUGUI>().text = $"{enemy.name} bought {shopUI.clothArmour.name}";
                actionPanel.SetActive(true);
            }
        }
    }

    // Tells the enemy to use an item (currently only medicine).
    // Searches the enemy inventory and only uses if it contains it.
    private void UseItem(string itemName)
    {
        if (itemName == "Medicine")
        {
            if (enemy.GetComponent<EnemyData>().itemList.Contains(shopUI.medicine))
            {
                enemy.GetComponent<EnemyData>().UpdateAttributes(shopUI.medicine.health, 0, 0, 0, 0, 0);
                enemy.GetComponent<EnemyData>().itemList.Remove(shopUI.medicine);

                actionPanel.GetComponentInChildren<TextMeshProUGUI>().text = $"{enemy.name} used {shopUI.medicine.name}!";
                actionPanel.SetActive(true);
            }
        }
        else if (itemName == "Warlord's Armour")
        {
            if (enemy.GetComponent<EnemyData>().itemList.Contains(shopUI.warlordsArmour))
            {
                enemy.GetComponent<EnemyData>().UpdateAttributes(shopUI.warlordsArmour.health, shopUI.warlordsArmour.maxHealth, 0, 0, 1, 0);
                enemy.GetComponent<EnemyData>().itemList.Remove(shopUI.warlordsArmour);

                actionPanel.GetComponentInChildren<TextMeshProUGUI>().text = $"{enemy.name} used {shopUI.warlordsArmour.name}!";
                actionPanel.SetActive(true);
            }
        }
        else if (itemName == "Cloth Armour")
        {
            if (enemy.GetComponent<EnemyData>().itemList.Contains(shopUI.clothArmour))
            {
                enemy.GetComponent<EnemyData>().UpdateAttributes(0, 0, 0, 0, 1, 0);
                enemy.GetComponent<EnemyData>().itemList.Remove(shopUI.warlordsArmour);

                actionPanel.GetComponentInChildren<TextMeshProUGUI>().text = $"{enemy.name} used {shopUI.warlordsArmour.name}!";
                actionPanel.SetActive(true);
            }
        }
        else if (itemName == "Long Sword")
        {
            if (enemy.GetComponent<EnemyData>().itemList.Contains(shopUI.longSword))
            {
                enemy.GetComponent<EnemyData>().UpdateAttributes(0, 0, 1, 0, 0, 0);
                enemy.GetComponent<EnemyData>().itemList.Remove(shopUI.longSword);

                actionPanel.GetComponentInChildren<TextMeshProUGUI>().text = $"{enemy.name} used {shopUI.longSword.name}!";
                actionPanel.SetActive(true);
            }
        }

        StateMachine();
        if (goldState != "Broke" && healthState == "Scared")
        {
            needsHealing = true;
        }
    }

    // Returns UI to player data and reactivates panels/buttons
    public void ReturnToPlayer()
    {
        hasPlayerMoved = false;
        enemyIndex = 0;
        enemy = enemyList[enemyIndex];

        ChangeCameraFocus(player);
        player.GetComponent<Player>().UpdateHealthBar();
        player.GetComponent<Player>().UpdateGold(0);
        iconImage.sprite = player.GetComponent<SpriteRenderer>().sprite;

        enemyStatePanel.SetActive(false);
        turnEndingPanel.SetActive(false);
        endTurnButton.SetActive(true);
        inventoryButton.GetComponent<Button>().interactable = true;
        attributesButton.GetComponent<Button>().interactable = true;
        dice.GetComponent<ShowDiceResult>().HideResult();
    }

    // Basic logic of computer AI pattern for its turn
    public IEnumerator Pattern()
    {
        StateMachine();
        UpdateUIEnemy();

        yield return new WaitForSeconds(1);

        if (desiredItem != null)
        {
            BuyItem(desiredItem);

            yield return new WaitForSeconds(1.5f);

            actionPanel.SetActive(false);

            yield return new WaitForSeconds(0.5f);

            UseItem(desiredItem);
            StateMachine();
            UpdateUIEnemy();

            yield return new WaitForSeconds(1.5f);

            actionPanel.SetActive(false);

            yield return new WaitForSeconds(0.5f);
        }

        if (needsHealing)
        {
            BuyItem("Medicine");

            yield return new WaitForSeconds(1.5f);

            actionPanel.SetActive(false);

            yield return new WaitForSeconds(0.5f);

            UseItem("Medicine");
            StateMachine();
            UpdateUIEnemy();

            yield return new WaitForSeconds(1.5f);

            actionPanel.SetActive(false);

            yield return new WaitForSeconds(0.5f);
            needsHealing = false;
        }

        RollComputer();  
        
        yield return new WaitForSeconds((int)enemy.GetComponent<RollDiceToMove>().diceAmount * 0.9f);        

        yield return new WaitUntil(() => inBattle == false);

        turnTitlePanel.SetActive(false);
        turnEndingPanel.SetActive(true);

        yield return new WaitForSeconds(2);

        turnEndingPanel.SetActive(false);
        if (enemyIndex == enemyList.Count - 1)
        {
            ReturnToPlayer();
        }
        else
        {
            enemyIndex++;
            enemy = enemyList[enemyIndex];
            StartComputerTurn();
        }     
    }
}