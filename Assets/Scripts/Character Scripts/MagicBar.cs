using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicBar : MonoBehaviour
{

    public Image img;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Clear()
    {
        img.fillAmount = 0;
    }

    public void AddMagic(float pow)
    {
        img.fillAmount += pow;
    }
}
