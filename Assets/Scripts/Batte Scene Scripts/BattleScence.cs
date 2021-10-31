using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityTimer;

public class BattleScence : MonoBehaviour
{
    public static BattleScence instance;
    public DiceControl diceControl;
    public GameObject battlePanel;
    public GameObject tip;
    public Camera mainCamera;
    public GameObject player;
    public GameObject enemy;
    public GameObject playerBattleIcon;
    public GameObject enemyBattleIcon;

    private EnemyData enemyData;
    public GameObject playerDamaged;
    public GameObject enemyDamaged;
    public HealthBar playerHealthBar;
    public HealthBar enemyHealthBar;

    private GameObject enemyRef;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public IEnumerator BattleScenceInit(GameObject enemyReference)
    {
        enemyRef = enemyReference;
        battlePanel.SetActive(true);

        yield return new WaitForSeconds(2f);

        battlePanel.SetActive(false);
        GameObject.Find("Player UI").GetComponent<Canvas>().enabled = false;
        GameObject.Find("Battle UI").GetComponent<Canvas>().enabled = true;
        mainCamera.GetComponent<CameraMoving>().target = GameObject.Find("Camera Position").transform;

        LoadSprites(enemyReference);
    }

    public void LoadSprites(GameObject enemyReference)
    {
        Sprite playerSprite = player.GetComponent<SpriteRenderer>().sprite = GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>().sprite;
        Sprite enemySprite = enemy.GetComponent<SpriteRenderer>().sprite = enemyReference.GetComponent<SpriteRenderer>().sprite;
        player.GetComponent<Animator>().runtimeAnimatorController = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().runtimeAnimatorController;
        enemy.GetComponent<Animator>().runtimeAnimatorController = enemyReference.GetComponent<Animator>().runtimeAnimatorController;

        playerBattleIcon.GetComponent<Image>().sprite = playerSprite;
        enemyBattleIcon.GetComponent<Image>().sprite = enemySprite;

        // Initialise enemy's attributes data
        enemyData = enemyReference.GetComponent<EnemyData>();

        // Initialise damage text
        playerDamaged.SetActive(false);
        enemyDamaged.SetActive(false);

        // Initialise health bars
        playerHealthBar.SetValue((float)GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().health / (float)GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().maxHealth);
        enemyHealthBar.SetValue((float)enemyData.health / (float)enemyData.maxHealth);
    }

    public void ReturnToMap()
    {
        AiController.inBattle = false;
        GameObject.Find("Player UI").GetComponent<Canvas>().enabled = true;
        GameObject.Find("Battle UI").GetComponent<Canvas>().enabled = false;
        mainCamera.GetComponent<CameraMoving>().target = GameObject.Find("Player").transform;
        GameObject.Find("Player").GetComponent<RollDiceToMove>().ReturnToMap();
        enemyRef.GetComponent<RollDiceToMove>().ReturnToMap();
    }

    public void PlayerAttack()
    {       
        Invoke(nameof(DamageEnemy), 1.5f);
        Invoke(nameof(HideDamage), 4);
        Invoke(nameof(Killed), 4);
    }

    public void EnemyAttack()
    {
        Invoke(nameof(DamagePlayer), 1.5f);
        Invoke(nameof(HideDamage), 4);
        Invoke(nameof(ActivateButtons), 4);
        Invoke(nameof(Killed), 4);
    }

    public void HideDamage()
    {
        playerDamaged.SetActive(false);
        enemyDamaged.SetActive(false);
    }

    public void ActivateButtons()
    {
        diceControl.attackButton.GetComponent<Button>().interactable = true;
        diceControl.inventoryButton.GetComponent<Button>().interactable = true;
        diceControl.runButton.GetComponent<Button>().interactable = true;
    }

    public void DamageEnemy()
    {
        enemyDamaged.SetActive(true);

        int damage = (int)(GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().attackPower - (enemyData.defense/2));
        if (damage < 0) { damage = 0; }

        enemyData.UpdateAttributes(-damage, 0, 0, 0, 0, 0);
        enemyDamaged.GetComponent<TextMeshProUGUI>().text = "-" + damage;
        enemyHealthBar.SetValue((float)enemyData.health / (float)enemyData.maxHealth);

        if (enemyData.health <= 0)
        {
            BattleAttack.isEnemyAlive = false;
        }       

        if (enemyData.health == 0)
        {
            tip.SetActive(true);
            tip.GetComponentInChildren<TextMeshProUGUI>().text = "Enemy Defeated!";
            enemy.GetComponent<Animator>().Play("Die");
        }
    }

    public void DamagePlayer()
    {
        playerDamaged.SetActive(true);

        int damage = (int)(enemyData.attackPower - (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().defense/2));
        if (damage < 0) { damage = 0; }

        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().UpdateAttributes(-damage, 0, 0, 0, 0, 0);
        playerDamaged.GetComponent<TextMeshProUGUI>().text = "-" + damage;
        playerHealthBar.SetValue((float)GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().health / (float)GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().maxHealth);

        if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().health <= 0)
        {
            BattleAttack.isPlayerAlive = false;
        }

        if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().health == 0)
        {
            tip.SetActive(true);
            tip.GetComponentInChildren<TextMeshProUGUI>().text = "You Are Defeated!";
        }
    }

    public void Killed()
    {
        if (enemyData.health == 0)
        {         
            tip.SetActive(false);
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().killCount++;
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().killCount >= 3)
            {
                GameOver.Init(GameState.Win);
            }
            else ReturnToMap();
        }
        else if (GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().health == 0)
        {
            tip.SetActive(false);
            player.GetComponent<Animator>().Play("Die");
            BattleAttack.isPlayerAlive = false;
            GameOver.Init(GameState.Fail);
        }
    }
}