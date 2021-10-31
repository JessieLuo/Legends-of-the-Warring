using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InstructionsMenu : MonoBehaviour
{
    public GameObject nextButton;
    public GameObject prevButton;   
    public TextMeshProUGUI instructionText;
    public Image instructionImage1;
    public Image instructionImage2;
    public Image instructionImage3;
    public Image instructionImage4;
    public Image instructionImage5;
    public Image instructionImage6;
    private int pageIndex = 0;

    private void Start()
    {
        nextButton.GetComponent<Button>().onClick.AddListener(NextButton);
        prevButton.GetComponent<Button>().onClick.AddListener(PrevButton);

        prevButton.SetActive(false);
        instructionImage2.enabled = false;
        instructionImage3.enabled = false;
        instructionImage4.enabled = false;
        instructionImage5.enabled = false;
        instructionImage6.enabled = false;
    }

    public void NextButton()
    {
        pageIndex++;       
        if (pageIndex == 1)
        {
            prevButton.SetActive(true);
            instructionImage2.enabled = true;
            instructionImage1.enabled = false;

            instructionText.text = "How To Play:\n\n" +
                "The Player and Enemies move around the map by rolling the dice each turn." +
                "You can only roll the dice once per turn.\n\n" +
                "When you end your turn, all the Enemies will have a turn each before it returns yo you.\n\n" +
                "To defeat your opponents you must move your character to their position on the map to enter battle.";
        }
        else if (pageIndex == 2)
        {
            instructionImage3.enabled = true;
            instructionImage2.enabled = false;

            instructionText.text = "Battle:\n\n" +
                "The Player and Enemy are transported to a 1 on 1 battle scene.\n\n" +
                "1. When in battle you have the option to attack using the attack button.\n\n" +
                "2. You also have the option to run using the run button.\n\n In order to successfully escape the battle, " +
                "you must roll a 4 or higher on the dice, which will return you to the map.";
        }
        else if (pageIndex == 3)
        {
            instructionImage4.enabled = true;
            instructionImage3.enabled = false;

            instructionText.text = "Enemies Turn:\n\n" +
                "On the Enemies turn, they can take the same actions as the player, including buying and using items, " +
                "and rolling the dice to move.\n\n" +
                "1. Messages will display on the screen to help the player understand what the Enemy is doing.\n\n" +
                "2. You will also have an Enemy State display in the to right. This will show the Enemy's Health State in red" +
                "and Gold State in yellow.\n\n" +
                "These States are indicators of how the Enemy will act on it's turn.";
        }
        else if (pageIndex == 4)
        {
            instructionImage5.enabled = true;
            instructionImage4.enabled = false;

            instructionText.text = "Shop:\n\n" +
                "1. There are 3 shops located around the map that the Player and Enemies can use.\n\n" +
                "2. Inside the shop are many different items that will power up your attributes or provide special effects.\n\n" +
                "Use your gold wisely to help get an advantage on your enemies to achieve victory.";
        }
        else if (pageIndex == 5)
        {
            instructionImage6.enabled = true;
            instructionImage5.enabled = false;
            nextButton.SetActive(false);

            instructionText.text = "How To Win:\n\n" +
                "In order to win, the Player must defeat all the other 3 Enemies in battle.\n\n" +
                "Be warned all 3 Enemies have teamed up to defeat you, so the odds are stacked against you.\n\n" +
                "Goodluck and have fun!";
        }
    }

    public void PrevButton()
    {
        pageIndex--;
        if (pageIndex == 0)
        {
            instructionImage1.enabled = true;
            instructionImage2.enabled = false;
            prevButton.SetActive(false);

            instructionText.text = "User Interface:\n\n1. The Health and Mana display is in the top left.\n\n" +
                "2. You can view your Gold in the bottom left, as well as the buttons for your Inventory and Attributes.\n\n" +
                "3. Each Character has a special skill, it's button is also in the bottom left.\n\n" +
                "4. The Roll Dice button is in the bottom right.\n\n" +
                "5. The End Turn button is in the top right.";
        }
        else if (pageIndex == 1)
        {
            instructionImage2.enabled = true;
            instructionImage3.enabled = false;

            instructionText.text = "How To Play:\n\n" +
                "The Player and Enemies move around the map by rolling the dice each turn." +
                "You can only roll the dice once per turn.\n\n" +
                "When you end your turn, all the Enemies will have a turn each before it returns yo you.\n\n" +
                "To defeat your opponents you must move your character to their position on the map to enter battle.";
        }
        else if (pageIndex == 2)
        {
            instructionImage3.enabled = true;
            instructionImage4.enabled = false;

            instructionText.text = "Battle:\n\n" +
                "The Player and Enemy are transported to a 1 on 1 battle scene.\n\n" +
                "1. When in battle you have the option to attack using the attack button.\n\n" +
                "2. You also have the option to run using the run button.\n\n In order to successfully escape the battle, " +
                "you must roll a 4 or higher on the dice, which will return you to the map.";
        }
        else if (pageIndex == 3)
        {
            instructionImage4.enabled = true;
            instructionImage5.enabled = false;

            instructionText.text = "Enemies Turn:\n\n" +
                "On the Enemies turn, they can take the same actions as the player, including buying and using items, " +
                "and rolling the dice to move.\n\n" +
                "1. Messages will display on the screen to help the player understand what the Enemy is doing.\n\n" +
                "2. You will also have an Enemy State display in the to right. This will show the Enemy's Health State in red" +
                "and Gold State in yellow.\n\n" +
                "These States are indicators of how the Enemy will act on it's turn.";
        }
        else if (pageIndex == 4)
        {
            instructionImage5.enabled = true;
            instructionImage6.enabled = false;

            nextButton.SetActive(true);

            instructionText.text = "Shop:\n\n" +
                "1. There are 3 shops located around the map that the Player and Enemies can use.\n\n" +
                "2. Inside the shop are many different items that will power up your attributes or provide special effects.\n\n" +
                "Use your gold wisely to help get an advantage on your enemies to achieve victory.";
        }
    }

    // Returns to main menu
    public void MenuReturn()
    {
        SceneManager.LoadScene("Menu");
    }
}
