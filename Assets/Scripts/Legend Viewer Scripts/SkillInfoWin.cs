using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

// Class for the information displayed for each individual hero, loaded in when clicked.
public class SkillInfoWin : MonoBehaviour
{
    public TextMeshProUGUI skillDescriptionTitle;
    public TextMeshProUGUI skillDescription;
    public TextMeshProUGUI attributesHealth;
    public TextMeshProUGUI attributesAttack;
    public TextMeshProUGUI attributesRange;
    public TextMeshProUGUI attributesDefense;
    public TextMeshProUGUI attributesGold;
    public TextMeshProUGUI legendName;
    public Image skillImg;
    private CharacterInfo info;

    public void Initialise(CharacterInfo hero)
    {
        skillImg.sprite = hero.skillSprite;
        skillDescriptionTitle.text = hero.skillDescriptionTitle;
        skillDescription.text = hero.skillDescription;
        attributesHealth.text = "Health: " + hero.attributesHealth;
        attributesAttack.text = "Attack Power: " + hero.attributesAttack;
        attributesRange.text = "Attack Range: " + hero.attributesRange;
        attributesDefense.text = "Defense: " + hero.attributesDefense;
        attributesGold.text = "Starting Gold: " + hero.attributesGold;
        legendName.text = hero.legendName;
    }
}