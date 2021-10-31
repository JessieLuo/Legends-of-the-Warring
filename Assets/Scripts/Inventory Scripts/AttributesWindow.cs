using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttributesWindow : MonoBehaviour
{
    public Text healthText;
    public Text attackPowerText;
    public Text attackRangeText;
    public Text defenseText;
    public Text movementText;

    private Player player;

    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        Show(player);
    }

    public void Show(Player info)
    {
        gameObject.SetActive(true);
        healthText.text = $"{info.health.ToString()}/{info.maxHealth.ToString()}";
        attackPowerText.text = info.attackPower.ToString();
        attackRangeText.text = info.attackRange.ToString();
        defenseText.text = info.defense.ToString();
        movementText.text = info.movement.ToString();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}