using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class CharacterInfo
{
    public GameObject obj;
    public string legendName;
    public string skillDescriptionTitle;
    public string skillDescription;
    public string attributesHealth;
    public string attributesAttack;
    public string attributesRange;
    public string attributesDefense;
    public string attributesGold;
    public Sprite skillSprite;
}

public class GameManager : MonoBehaviour
{
    [SerializeField]
    public List<CharacterInfo> playerList = new List<CharacterInfo>();

    public static GameManager Instance;
    public SkillInfoWin skillWin;
    private CharacterInfo currentHero;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        OnSelectHero(0);
    }

    public void OnSelectHero(int index)
    {
        if(currentHero != null)
        {
            currentHero.obj.SetActive(false);
        }

        currentHero = playerList[index];

        if (currentHero != null)
        {
            currentHero.obj.SetActive(true);
            skillWin.Initialise(currentHero);
        }       
    }

    public CharacterInfo GetCharacter(int index)
    {
        return playerList[index];
    }
}
