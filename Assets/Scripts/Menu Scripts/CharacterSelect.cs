using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelect : MonoBehaviour
{
    public int playerIndex;
    public int enemyIndex1;
    public int enemyIndex2;
    public int enemyIndex3;

    public List<Sprite> spriteList = new List<Sprite>();
    public List<AnimatorController> animatorList = new List<AnimatorController>();

    private GameObject player;
    private GameObject enemy1;
    private GameObject enemy2;
    private GameObject enemy3;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;

        playerIndex = 0;
        enemyIndex1 = 4;
        enemyIndex2 = 1;
        enemyIndex3 = 6;
    }

    // Cycles through the sprite list and assigns the image field the next sprite 
    // Activated when next button is clicked 
    public void NextLegend(Image playerSprite)
    {
        int i = 0;
        foreach (Sprite sprite in spriteList)
        {
            if (playerSprite.sprite == sprite)
            {
                i = spriteList.IndexOf(sprite);
            }
        }

        if (i + 1 == spriteList.Count) i = 0;
        else i++;
        playerSprite.sprite = spriteList[i];
    }

    // Cycles through the sprite list and assigns the image field the previous sprite 
    // Activated when previous button is clicked
    public void PreviousLegend(Image playerSprite)
    {
        int i = 0;
        foreach (Sprite sprite in spriteList)
        {
            if (playerSprite.sprite == sprite)
            {
                i = spriteList.IndexOf(sprite);
            }
        }

        if (i - 1 < 0) i = spriteList.Count - 1;
        else i--;
        playerSprite.sprite = spriteList[i];
    }

    // Increments an index so that the correct sprite carries over when game starts
    public void AssignNext(string keyword)
    {
        if(keyword == "player")
        {
            playerIndex++;
            if (playerIndex == 7) playerIndex = 0;
        }
        else if (keyword == "enemy1")
        {
            enemyIndex1++;
            if (enemyIndex1 == 7) enemyIndex1 = 0;
        }
        else if (keyword == "enemy2")
        {
            enemyIndex2++;
            if (enemyIndex2 == 7) enemyIndex2 = 0;
        }
        else if (keyword == "enemy3")
        {
            enemyIndex3++;
            if (enemyIndex3 == 7) enemyIndex3 = 0;
        }
    }

    // Decrements an index so that the correct sprite carries over when game starts
    public void AssignPrevious(string keyword)
    {
        if (keyword == "player")
        {
            playerIndex--;
            if (playerIndex == -1) playerIndex = 6;
        }
        else if (keyword == "enemy1")
        {
            enemyIndex1--;
            if (enemyIndex1 == -1) enemyIndex1 = 6;
        }
        else if (keyword == "enemy2")
        {
            enemyIndex2--;
            if (enemyIndex2 == -1) enemyIndex2 = 6;
        }
        else if (keyword == "enemy3")
        {
            enemyIndex3--;
            if (enemyIndex3 == -1) enemyIndex3 = 6;
        }
    }

    // Finds the game object for each character and loads correct sprite and animator based on index
    // Only will run once you enter the "Game" scene
    private void OnSceneLoaded(Scene Game, LoadSceneMode Single)
    {
        if (SceneManager.GetActiveScene().name == "Game")
        {
            player = GameObject.Find("Player");
            player.GetComponent<SpriteRenderer>().sprite = spriteList[playerIndex];
            player.GetComponent<Animator>().runtimeAnimatorController = animatorList[playerIndex];

            if (enemy1 = GameObject.Find("Enemy 1"))
            {
                enemy1.GetComponent<SpriteRenderer>().sprite = spriteList[enemyIndex1];
                enemy1.GetComponent<Animator>().runtimeAnimatorController = animatorList[enemyIndex1];
            }

            if (enemy2 = GameObject.Find("Enemy 2"))
            {
                enemy2.GetComponent<SpriteRenderer>().sprite = spriteList[enemyIndex2];
                enemy2.GetComponent<Animator>().runtimeAnimatorController = animatorList[enemyIndex2];
            }

            if (enemy3 = GameObject.Find("Enemy 3"))
            {
                enemy3.GetComponent<SpriteRenderer>().sprite = spriteList[enemyIndex3];
                enemy3.GetComponent<Animator>().runtimeAnimatorController = animatorList[enemyIndex3];
            }
        }        
    }
}
