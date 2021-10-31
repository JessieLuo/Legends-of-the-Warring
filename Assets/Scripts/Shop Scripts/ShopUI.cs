using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


/// Updates the shop UI to fill in the price based on the item supplied in the field.
/// Also displays a messae when item is bought.
public class ShopUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI goldField;
    [SerializeField] Text afterBuyText;
    Player player = null;

    [SerializeField] public Item calmingDan;
    [SerializeField] TextMeshProUGUI calmingDanPrice;
    [SerializeField] public Item medicine;
    [SerializeField] TextMeshProUGUI medicinePrice;
    [SerializeField] public Item confusionBrew;
    [SerializeField] TextMeshProUGUI confusionBrewPrice;
    [SerializeField] public Item cityScroll;
    [SerializeField] TextMeshProUGUI cityScrollPrice;
    [SerializeField] public Item jixingPill;
    [SerializeField] TextMeshProUGUI jixingPillPrice;
    [SerializeField] public Item forage;
    [SerializeField] TextMeshProUGUI foragePrice;
    [SerializeField] public Item acacia;
    [SerializeField] TextMeshProUGUI acaciaPrice;
    [SerializeField] public Item unionSpell;
    [SerializeField] TextMeshProUGUI unionSpellPrice;
    [SerializeField] public Item tigerIdol;
    [SerializeField] TextMeshProUGUI tigerIdolPrice;
    [SerializeField] public Item jadeSeal;
    [SerializeField] TextMeshProUGUI jadeSealPrice;
    [SerializeField] public Item royalEdict;
    [SerializeField] TextMeshProUGUI royalEdictPrice;
    [Space]
    [SerializeField] public Item longSword;
    [SerializeField] TextMeshProUGUI longSwordPrice;
    [SerializeField] public Item spear;
    [SerializeField] TextMeshProUGUI spearPrice;
    [SerializeField] public Item vampireBlade;
    [SerializeField] TextMeshProUGUI vampireBladePrice;
    [SerializeField] public Item fortuneSabre;
    [SerializeField] TextMeshProUGUI fortuneSabrePrice;
    [SerializeField] public Item longbow;
    [SerializeField] TextMeshProUGUI longbowPrice;
    [SerializeField] public Item clothArmour;
    [SerializeField] TextMeshProUGUI clothArmourPrice;
    [SerializeField] public Item warlordsArmour;
    [SerializeField] TextMeshProUGUI warlordsArmourPrice;
    [SerializeField] public Item warHorse;
    [SerializeField] TextMeshProUGUI warHorsePrice;

    float timeToDisappear;

    void Update()
    {
        if (Time.time >= timeToDisappear)
        {
            afterBuyText.enabled = false;
        }
        goldField.text = $"{player.currentGold}";
    }

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        RefreshShopUI();
    }

    public void BuyMessage(Item item)
    {
        afterBuyText.text = $"{item.GetDisplayName()} Purchased!";
        afterBuyText.color = Color.white;
        afterBuyText.enabled = true;
        timeToDisappear = Time.time + 1.5f;
    }

    public void FailedMessage(Item item)
    {
        afterBuyText.text = $"Insufficient Gold!";
        afterBuyText.color = Color.red;
        afterBuyText.enabled = true;
        timeToDisappear = Time.time + 1.5f;
    }

    public void RefreshShopUI()
    {
        goldField.text = $"{player.currentGold}";

        calmingDanPrice.text = calmingDan.GetPrice().ToString();
        medicinePrice.text = medicine.GetPrice().ToString();
        confusionBrewPrice.text = confusionBrew.GetPrice().ToString();
        cityScrollPrice.text = cityScroll.GetPrice().ToString();
        jixingPillPrice.text = jixingPill.GetPrice().ToString();
        foragePrice.text = forage.GetPrice().ToString();
        acaciaPrice.text = acacia.GetPrice().ToString();
        unionSpellPrice.text = unionSpell.GetPrice().ToString();
        tigerIdolPrice.text = tigerIdol.GetPrice().ToString();
        jadeSealPrice.text = jadeSeal.GetPrice().ToString();
        royalEdictPrice.text = royalEdict.GetPrice().ToString();

        longSwordPrice.text = longSword.GetPrice().ToString();
        spearPrice.text = spear.GetPrice().ToString();
        vampireBladePrice.text = vampireBlade.GetPrice().ToString();
        fortuneSabrePrice.text = fortuneSabre.GetPrice().ToString();
        longbowPrice.text = longbow.GetPrice().ToString();
        clothArmourPrice.text = clothArmour.GetPrice().ToString();
        warlordsArmourPrice.text = warlordsArmour.GetPrice().ToString();
        warHorsePrice.text = warHorse.GetPrice().ToString();
    }
}
