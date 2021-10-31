using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

public class CharacterAttributes : MonoBehaviour
{
    public static CharacterAttributes Instance;
    public string heroName;
    public int id;
    public int health;
    public int maxHealth;
    public float attack;
    public float attackRange;
    public float defense;
    public float movement;
    public float temporaryMovement;
    public int gold = 0;

    // Subscribe to onChange event
    public event Action onChange;

    // Hero birth
    private void HeroBirth(int id)
    {
        this.id = id + 1;
        //Debug.Log(heroName + " is created.");
    }

    public class HeroItem
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public string Attack { get; set; }
        public string AttackRange { get; set; }
        public string Defense { get; set; }
        public string HP { get; set; }
        public string Gold { get; set; }
    }
    public class Root
    {
        public List<HeroItem> Hero { get; set; }
    }
    public void GetJsonInfo()
    {
        StreamReader streamreader = new StreamReader(Application.dataPath + "/Resources/Attribute Data/HeroAttribute.json");
        JsonReader js = new JsonReader(streamreader);
        Root r = JsonMapper.ToObject<Root>(js);
        for (int i = 0; i < r.Hero.Count; i++)
        {
            if (id == int.Parse(r.Hero[i].ID))
            {
                heroName = r.Hero[i].Name;
                health = int.Parse(r.Hero[i].HP);
                attack = float.Parse(r.Hero[i].Attack);
                attackRange = float.Parse(r.Hero[i].AttackRange);
                defense = float.Parse(r.Hero[i].Defense);
                gold = int.Parse(r.Hero[i].Gold);
                maxHealth = health;
                break;
            }
        }
        //Debug.Log("Hero attributes as follow");
        //Debug.Log("name: " + heroName + ", id: " + id + ", attack: " + attack + ", defense: " + defense + ", attack range: " + attackRange + ", HP: " + health + ", max HP: " + maxHealth);
    }

    void Start()
    {
        Instance = this;
        HeroBirth(FindObjectOfType<CharacterSelect>().playerIndex);//This is used to generate the hero attribute. It needs the hero's name. When selecting a character, it needs to pass a parameter. The recommended parameter is the name.
        GetJsonInfo();
    }

    public float GetGold()
    {
        return gold;
    }

    // Updates gold based on amount, called from the shop after a purchase
    public void UpdateGold(int amount)
    {
        gold += amount;
        if (onChange != null)
        {
            onChange();
        }
    }
}
