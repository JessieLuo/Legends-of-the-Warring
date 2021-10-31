using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityTimer;

public class BattleAttack : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;
    public DiceControl diceControl;
    public GameObject playerIcon;
    public GameObject enemyIcon;

    public float moveSpeed;
    public float durTime;

    public Button attackButton;
    public Button runButton;
    public Button inventory;
    public GameObject inventoryPanel;

    private Vector3 playerPos;
    private Vector3 monsterPos;
    private Animator playerAnimator;
    private Animator monsterAnimator;

    private bool InventoryShow = false;
    public static bool isPlayerAlive = true;
    public static bool isEnemyAlive = true;

    // Start is called before the first frame update
    private void Start()
    {
        isPlayerAlive = true;
        isEnemyAlive = true;
        playerIcon.GetComponent<Image>().sprite = player.GetComponent<SpriteRenderer>().sprite;
        enemyIcon.GetComponent<Image>().sprite = enemy.GetComponent<SpriteRenderer>().sprite;

        playerAnimator = player.GetComponent<Animator>();
        monsterAnimator = enemy.GetComponent<Animator>();

        attackButton.onClick.AddListener(AttackButton);
        inventory.onClick.AddListener(InventoryButton);
        runButton.onClick.AddListener(RunButton);

        playerPos = player.transform.position;
        monsterPos = enemy.transform.position;
    }

    private void RunButton()
    {
        diceControl.StarRollDice();
    }

    public void AttackButton()
    {
        diceControl.attackButton.GetComponent<Button>().interactable = false;
        diceControl.inventoryButton.GetComponent<Button>().interactable = false;
        diceControl.runButton.GetComponent<Button>().interactable = false;

        Timer attack = Timer.Register(durTime, (() =>
        {
            player.transform.localScale = new Vector3(-1.3f, 1.3f, 1.3f);

            // Monster Attack
            Timer returnPosition = Timer.Register(2, onComplete: (() =>
            {
                player.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
                MonsterAttack();
            }), onUpdate: (f =>
            {
                MoveGameObject(player, playerPos);
            }));
        }), (f =>
        {
            // Attack action
            MoveGameObject(player, enemy.transform.position);
        }));

        BattleScence.instance.PlayerAttack();
    }

    public void InventoryButton()
    {
        InventoryShow = !InventoryShow;
        inventoryPanel.SetActive(InventoryShow);
    }

    public void MonsterAttack()
    {
        if (!isEnemyAlive) { return; }

        Timer attack = Timer.Register(durTime, (() =>
        {
            enemy.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
            Timer returnPosition = Timer.Register(2, onComplete: (() =>
            {
                enemy.transform.localScale = new Vector3(-1.3f, 1.3f, 1.3f);
            }), onUpdate: (f =>
            {
                MoveGameObject(enemy, monsterPos);
            }));
        }), (f =>
        {
            // Attack action         
            MoveGameObject(enemy, player.transform.position);
        }));

        BattleScence.instance.EnemyAttack();
    }

    private void MoveGameObject(GameObject obj, Vector3 targetObj)
    {
        if (!isPlayerAlive)
        {
            playerAnimator.Play("Die");
        }
        else if (obj.name == player.name && targetObj == enemy.transform.position)
        {
            playerAnimator.Play("Attack");
        }
        else if (obj.name == player.name && targetObj == playerPos && obj.transform.position.x < 3f)
        {
            playerAnimator.Play("Walk");
        }
        else
        {
            playerAnimator.Play("Idle");
        }

        if (!isEnemyAlive)
        {
            monsterAnimator.Play("Die");
        }
        else if (obj.name == enemy.name && targetObj == player.transform.position)
        {
            monsterAnimator.Play("Attack");
        }
        else if (obj.name == enemy.name && targetObj == monsterPos && obj.transform.position.x < 3f)
        {
            monsterAnimator.Play("Walk");
        }
        else
        {
            monsterAnimator.Play("Idle");
        }

        obj.transform.position = Vector3.MoveTowards(obj.transform.position, targetObj, moveSpeed * Time.deltaTime);
    }
}