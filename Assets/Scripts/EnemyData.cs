using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyData : MonoBehaviour
{
    public List<Item> itemList = new List<Item>();

    public int health = 10;
    public int maxHealth = 10;
    public float attackPower = 1f;
    public float attackRange = 1f;
    public float defense = 1f;
    public float movement = 1f;
    public int startingGold = 500;
    public int currentGold;

    private HealthBar healthBar;

    void Start()
    {
        currentGold = startingGold;
        healthBar = GameObject.Find("Health Bar").GetComponent<HealthBar>();
    }

    public void UpdateAttributes(int newHealth, int newMaxHealth, float newAttackPower, float newAttackRange, float newDefense, float newMovement)
    {
        // Update EnemyData
        health += newHealth;
        maxHealth += newMaxHealth;
        attackPower += newAttackPower;
        attackRange += newAttackRange;
        defense += newDefense;
        movement += newMovement;

        if (health > maxHealth)
        {
            health = maxHealth;
        }
        else if (health < 0)
        {
            health = 0;
        }

        UpdateHealthBar();
    }

    public void UpdateHealthBar()
    {
        healthBar.SetValue((float)health / (float)maxHealth);
    }

    public void UpdateGold(int amount)
    {
        currentGold += amount;
        Text goldField = GameObject.Find("Gold Text").GetComponent<Text>();
        goldField.text = $"{currentGold}";
    }
}
