using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    public List<Item> skillList;
    // public List<GameObject> m_enemy;
    // private List<Item> m_enemySkill;
    private bool isCoolDown = true;

    [HideInInspector]
    public Item skill;
    private Transform currentHero;
    private bool isActive;
    private bool isUsed;
    private Slider slider;
    private bool isToggle;
    private bool temp;
    private int playerIndex;
    public Role role = Role.one;

    [Tooltip("技能几个回合使用一次")]
    //private int[] frequency = new int[]{5,5,5,5,8,5,3};
    private int[] frequency = new int[] { 2, 2, 2, 2, 2, 2, 2 };
    private int index = 0;
    private int indexAvatar = 0;
    private bool isClick;

    private Role preRole;

    public Image skillImg;
    public List<Sprite> imageList;

    private void Start()
    {
        skill = skillList[UnityEngine.Random.Range(0, skillList.Count)];
        //imageList = new List<Sprite>();
        Init();
        //InitizeImageList(imageList);
        GetComponent<Button>().onClick.AddListener(() =>
        {
            ButtonDown(skill);
        });
        var playerIndex = FindObjectOfType<CharacterSelect>().playerIndex;

        //skillImg.sprite = GameManager.Instance.GetCharacter(playerIndex).skillSprite;    
        switch (playerIndex)
        {
            case 0:
                skillImg.sprite = imageList[0];
                break;
            case 1:
                skillImg.sprite = imageList[1];
                break;
            case 2:
                skillImg.sprite = imageList[2];
                break;
            case 3:
                skillImg.sprite = imageList[3];
                break;
            case 4:
                skillImg.sprite = imageList[4];
                break;
            case 5:
                skillImg.sprite = imageList[5];
                break;
            case 6:
                skillImg.sprite = imageList[6];
                break;
        }
    }

    private void Init()
    {
        isClick = false;
        index = 5;
        preRole = role;
        SetActive(false);
        isUsed = true;
        isCoolDown = true;
        isToggle = true;
        temp = true;
        slider = transform.Find("Slider").GetComponent<Slider>();
        RenewButton(skill.GetIcon(), skill.GetDisplayName());
    }

    private void Update()
    {
        IsClick();
        Test();
        getSkill(role);
        //print(index);
    }

    private void Test()
    {
        try
        {
            int a = GameObject.FindObjectOfType<CharacterSelect>().playerIndex;
            playerIndex = a;
        }
        catch (System.Exception msg)
        {
            playerIndex = 0;
            print(msg);
        }
        finally
        {
            role = (Role)playerIndex;
        }
    }

    private IEnumerator CoolDown()
    {
        while (slider.value < slider.maxValue)
        {
            yield return new WaitForSeconds(0.1f);
            slider.value += 4f;
          
        }
        isCoolDown = false;
        isUsed = false;
        SetActive(true);
    }

    private bool isTemp = true;

    private void UpdateInit()
    {
        currentHero = GameObject.FindObjectOfType<CameraMoving>().target;
        if (currentHero.tag == "Player" && isTemp)
        {
            isActive = true;
            // HideGame(false);
            //index = indexAvatar;
            isTemp = false;
            isToggle = true;
        }
        else if (currentHero.tag != "Player")
        {
            // if (index > m_frequency[(int)m_role])
            // {
            //     index = 0;
            //     indexAvatar = 0;
            // }
            isToggle = false;
            HideGame(true);
            isTemp = true;
            if (isCoolDown)
            {
                slider.value = slider.minValue;
            }
        }
       
        if (isUsed && isCoolDown && isActive && isToggle && index == 5)
        {
            slider.value = slider.minValue;
            StartCoroutine(CoolDown());
            SetActive(false);
            isToggle = false;
            index = indexAvatar;
        }
    }

    private void RenewButton(Sprite sprite, string title)
    {
        gameObject.transform.GetChild(0).GetComponent<Image>().sprite = sprite;
        gameObject.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = title;
    }

    private void IsClick()
    {
        if (GameObject.Find("Player").GetComponent<RollDiceToMove>().clicked && !isClick)
        {
            index++;
            Debug.Log("index" + index);
            isClick = true;
            indexAvatar = index;
        }
        else if (!GameObject.Find("Player").GetComponent<RollDiceToMove>().clicked)
        {
            isClick = false;
        }
    }

    private void ButtonDown(Item item)
    {
        if (isActive && !isUsed && !isCoolDown)
        {
            if (item.GetPrice() > 0)
            {
                int addGold = item.GetPrice();
                UIManager.Instance.player.UpdateGold(addGold);
            }
            UIManager.Instance.player.UpdateAttributes(item.health, item.maxHealth, item.attackPower, item.attackRange, item.defense, item.movement);
            isActive = false;
            isUsed = true;
            isCoolDown = true;
            isToggle = true;
            SetActive(false);
            HideGame(true);
            index = indexAvatar = 0;
            GameObject.FindObjectOfType<MagicBar>().Clear();
        }
    }

    private void SetActive(bool isActive)
    {
        gameObject.GetComponent<Button>().interactable = isActive;
    }

    private bool istemporary=true;

    private void HideGame(bool isHide)
    {
        if (isHide)
        {
            gameObject.GetComponent<Image>().color = new Color(255, 255, 255, 100);
            //transform.GetChild(0).gameObject.SetActive(false);
            //transform.GetChild(1).gameObject.SetActive(false);
            istemporary=true;
        }
        else
        {
            gameObject.GetComponent<Image>().color = new Color(255, 255, 255, 255);
            transform.GetChild(0).gameObject.SetActive(true);
            //transform.GetChild(1).gameObject.SetActive(true);
            if(istemporary)
            {
                
                isUsed = isCoolDown = isActive = isToggle=true;
                istemporary = false;
            }
        }
    }

    public void getSkill(Role role)
    {
        switch (role)
        {
            case Role.one:
                skill = skillList[0];
                break;
            case Role.two:
                skill = skillList[1];
                break;
            case Role.three:
                skill = skillList[2];
                break;
            case Role.four:
                skill = skillList[3];
                break;
            case Role.five:
                skill = skillList[4];
                break;
            case Role.six:
                skill = skillList[5];
                break;
            case Role.seven:
                skill = skillList[6];
                break;
            default:
                print("unknown error");
                break;
        }
        if (preRole != role)
        {
            Init();
        }
        if (index >= frequency[(int)role])
        {
            HideGame(false);
        }
        else
        {
            HideGame(true);
        }
        UpdateInit();
    }
    public void InitizeImageList(List<Sprite> imageList)
    {
        imageList.Add(Resources.Load("Skill Image/FortuneSabre") as Sprite);
        imageList.Add(Resources.Load("Skill Image/GoldCoin") as Sprite);
        imageList.Add(Resources.Load("Skill Image/Acacia") as Sprite);
        imageList.Add(Resources.Load("Skill Image/Longbow") as Sprite);
        imageList.Add(Resources.Load("Skill Image/VampireBlade") as Sprite);
        imageList.Add(Resources.Load("Skill Image/WarlordsArmour") as Sprite);
        imageList.Add(Resources.Load("Skill Image/Calming Dan") as Sprite);
        Debug.Log(imageList.Count);
    }
}

public enum Role
{
    one,
    two,
    three,
    four,
    five,
    six,
    seven
}