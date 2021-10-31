using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public Inventory inventory;   
    public AttributesWindow attributes;
    public Player player;

    void Start()
    {
        Instance = this;
    }
}
