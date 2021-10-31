using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityTimer;

public class DiceControl : MonoBehaviour
{
    public Sprite dice1, dice2, dice3, dice4, dice5, dice6;
    public GameObject dice;
    public GameObject dicePanel;
    public GameObject diceResult;
    public GameObject battlePanel;
    public GameObject attackButton;
    public GameObject inventoryButton;
    public GameObject runButton;
    public TMP_Text text;
    public int rangeInt;
    public BattleAttack battleAttack;

    [Space]
    public float rollTime;
    public float speed;

    public void StarRollDice()
    {
        attackButton.GetComponent<Button>().interactable = false;
        inventoryButton.GetComponent<Button>().interactable = false;
        runButton.GetComponent<Button>().interactable = false;
        dicePanel.SetActive(true);
        dice.SetActive(true);
        Timer roll = Timer.Register(rollTime, () =>
        {
            StopRoll();
        }, (o) =>
        {

        });
    }

    private void StopRoll()
    {
        rangeInt = Random.Range(1, 7);
        diceResult.SetActive(true);
        dice.SetActive(false);
        ShowDiceImage(rangeInt);

        if (rangeInt > 3)
        {
            battlePanel.SetActive(true);
            text.text = "Run Successful!";

            Timer.Register(2, () =>
            {
                attackButton.GetComponent<Button>().interactable = true;
                inventoryButton.GetComponent<Button>().interactable = true;
                runButton.GetComponent<Button>().interactable = true;
                diceResult.SetActive(false);
                dicePanel.SetActive(false);
                battlePanel.SetActive(false);

                GameObject.Find("Node Manager").GetComponent<BattleScence>().ReturnToMap();
            }, (s) =>
            {
            });
        }
        else
        {  
            battlePanel.SetActive(true);
            text.text = ("Failed to Run!");
            Timer.Register(2, () =>
            { 
                battleAttack.MonsterAttack();
                diceResult.SetActive(false);
                dicePanel.SetActive(false);
                battlePanel.SetActive(false);
            }, (s) =>
            {
            });
        }
    }

    public void ShowDiceImage(int num)
    {
        switch (num)
        {
            case 1:
                diceResult.GetComponent<Image>().sprite = dice1;
                break;
            case 2:
                diceResult.GetComponent<Image>().sprite = dice2;
                break;
            case 3:
                diceResult.GetComponent<Image>().sprite = dice3;
                break;
            case 4:
                diceResult.GetComponent<Image>().sprite = dice4;
                break;
            case 5:
                diceResult.GetComponent<Image>().sprite = dice5;
                break;
            case 6:
                diceResult.GetComponent<Image>().sprite = dice6;
                break;
        }
    }
}