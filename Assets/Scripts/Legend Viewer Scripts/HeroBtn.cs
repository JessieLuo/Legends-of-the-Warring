using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroBtn : MonoBehaviour
{
    public int index;

    // Loads each hero into the index when it is selected.
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnSelectHero);
    }

    private void OnSelectHero()
    {
        GameManager.Instance.OnSelectHero(index);
    }
}