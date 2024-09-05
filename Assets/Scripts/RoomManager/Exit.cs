using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ExitDirection
{
    right,
    left,
    down,
    up,
}

public class Exit : MonoBehaviour
{
    public string sceneName ="";
    public int doorNumber = 0;
    public ExitDirection direction = ExitDirection.down;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Player"){
            Debug.Log("플레이어가 Exit에 충돌");
            if(doorNumber ==100){
                //BGM정지
                SoundManager.soundManager.StopBgm();
                //SE재생
                SoundManager.soundManager.SEPlay(SEType.GameClear);
                //게임클리어
                GameObject.FindObjectOfType<UIManager>().GameClear();
            } else{
                string nowScene = PlayerPrefs.GetString("LastScene");
                SaveDataManager.SaveArrangeData(nowScene);
                RoomManager.ChangeScene(sceneName, doorNumber);
            }
        }
    }
}
