using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowDiceResult : MonoBehaviour
{
    public RollDiceToMove rollDice;
    public Sprite dice1, dice2, dice3, dice4, dice5, dice6;
    Image diceImage;

    UIControl uIControl;

    // Start is called before the first frame update
    void Start()
    {
        diceImage = GameObject.Find("Dice Result").GetComponent<Image>();
        uIControl = GameObject.Find("GameManager").GetComponent<UIControl>();
    }

    public void ShowDiceImage(int num)
    {
        switch (num)
        {
            case 1:
                diceImage.sprite = dice1;
                break;
            case 2:
                diceImage.sprite = dice2;
                break;
            case 3:
                diceImage.sprite = dice3;
                break;
            case 4:
                diceImage.sprite = dice4;
                break;
            case 5:
                diceImage.sprite = dice5;
                break;
            case 6:
                diceImage.sprite = dice6;
                break;
        }
    }

    public void ShowResult()
    {
        diceImage.GetComponent<CanvasGroup>().alpha = 1;
        uIControl.diceHide();
    }

    public void HideResult()
    {
        diceImage.GetComponent<CanvasGroup>().alpha = 0;
        uIControl.diceShow();
    }
}
