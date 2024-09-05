using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemKeeper : MonoBehaviour
{
    public static int hasKeys =0;
    public static int hasArrows = 10;

    void Start()
    {
        //아이템 불러오기
        hasKeys = PlayerPrefs.GetInt("Keys");
        hasArrows = PlayerPrefs.GetInt("Arrows");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void SaveItem(){
        PlayerPrefs.SetInt("Keys", hasKeys);
        PlayerPrefs.SetInt("Arrows", hasArrows);
    }
}
