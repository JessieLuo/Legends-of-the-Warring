using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public float attackPower;
    public float attackRange;
    public float defense;
    public float movement;
    public int currentGold;
    public int killCount;

    private HealthBar healthBar;

    void Start()
    {
        health = CharacterAttributes.Instance.maxHealth;
        maxHealth = CharacterAttributes.Instance.maxHealth;
        attackPower = CharacterAttributes.Instance.attack;
        attackRange = CharacterAttributes.Instance.attackRange;
        defense = CharacterAttributes.Instance.defense;
        currentGold = CharacterAttributes.Instance.gold;

        healthBar = GameObject.Find("Health Bar").GetComponent<HealthBar>();
        UpdateHealthBar();
        UpdateGold(0);
    }

    public void UpdateAttributes(int newHealth, int newMaxHealth, float newAttackPower, float newAttackRange, float newDefense, float newMovement)
    {    
        // Update Player
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
        
        // Update CharacterAttributes
        CharacterAttributes.Instance.health = health;
        CharacterAttributes.Instance.maxHealth = maxHealth;
        CharacterAttributes.Instance.attack = attackPower;
        CharacterAttributes.Instance.attackRange = attackRange;
        CharacterAttributes.Instance.defense = defense;
        CharacterAttributes.Instance.movement = movement;
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
