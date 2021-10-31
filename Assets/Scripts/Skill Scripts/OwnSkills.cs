using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OwnSkills : MonoBehaviour
{
    public List<Item> ownSkillsList;
    private Item ownSkill;
    private Transform player;
    private bool acquireSkills;
    private bool isUsed;
    private bool toggle;
    private static GameObject SkillObj;
    private bool ismouseDown;

    private void Start()
    {
        acquireSkills = false;
        isUsed = false;
        toggle = true;
        ismouseDown = true;
        ownSkill = ownSkillsList[UnityEngine.Random.Range(0, ownSkillsList.Count)];
    }

    private void Update()
    {
        GetOwnSkill();
    }

    private void GetOwnSkill()
    {
        player = GameObject.FindObjectOfType<CameraMoving>().target;
        if (transform == player)
        {
            acquireSkills = true;
        }
        else
        {
            acquireSkills = false;
            toggle = true;
        }
        if (acquireSkills && isUsed && toggle && ismouseDown)
        {
            try
            {
                SkillObj = GameObject.FindObjectOfType<Inventory>().AddSkill(ownSkill);
                isUsed = false;
                toggle = false;
                ismouseDown = false;
            }
            catch (Exception msg)
            {
                print(msg);
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            ismouseDown = true;
            if (SkillObj == null)
            {
                isUsed = true;
            }
            else
            {
                toggle = false;
                isUsed = false;
            }
        }
    }
}
