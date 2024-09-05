using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData : MonoBehaviour
{
    public int arrangeId =0; 
    public string objTag ="";
}

[System.Serializable]
public class SaveDataList{
    public SaveData[] saveDatas;
}
