using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{
    public static int doorNumber=1;
    void Start()
    {
        //플레이어 캐릭터 위치
        //출입구를 배열로 얻기
        GameObject[] enters = GameObject.FindGameObjectsWithTag("Exit");
        for (int i=0; i<enters.Length; i++){
            GameObject doorObj = enters[i];
            Exit exit = doorObj.GetComponent<Exit>();
            if(doorNumber == exit.doorNumber){
                // 같은 문 번호
                // 플레이어 캐릭터를 출입구로 이동
                float x = doorObj.transform.position.x;
                float y = doorObj.transform.position.y;

                if (exit.direction == ExitDirection.up)
                {
                    y++;
                }
                else if (exit.direction == ExitDirection.right)
                {
                    x++;
                } else if (exit.direction == ExitDirection.down){
                    y--;
                } else if(exit.direction == ExitDirection.left){
                    x--;
                }
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                player.transform.position = new Vector3(x,y);
                break; // 반복문 빠져나오기
            }
            
        }
        //씬 이름 가져오기
        string scenename = PlayerPrefs.GetString("LastScene");
        if(scenename == "BossScene"){
            SoundManager.soundManager.PlayBgm(BGMType.InBoss);
        } else{
            SoundManager.soundManager.PlayBgm(BGMType.InGame);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 씬 이동
    public static void ChangeScene(string scenename, int doornum){
        doorNumber = doornum; // 문번호를 static 변수에 저장

        string nowScene = PlayerPrefs.GetString("LastScene");
        if (nowScene !=""){
            SaveDataManager.SaveArrangeData(nowScene);
        }
        PlayerPrefs.SetString("LastScene", scenename);
        PlayerPrefs.SetInt("LastDoor", doornum);
        ItemKeeper.SaveItem(); //아이템저장
        
        SceneManager.LoadScene(scenename);
    }
}
