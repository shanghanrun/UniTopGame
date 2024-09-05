using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public GameObject startButton;
    public GameObject continueButton;
    public string firstSceneName;
    // Start is called before the first frame update
    void Start()
    {
        string sceneName =  PlayerPrefs.GetString("LastScene");
        // 자꾸 다른 신으로 가서 잠시 주석처리
        sceneName ="WorldMap"; // 이걸로 시작
        if (sceneName == ""){
            continueButton.GetComponent<Button>().interactable = false;// 비활성화
        } else{
            continueButton.GetComponent<Button>().interactable = true;
        }
        //타이틀 BGM재생
        SoundManager.soundManager.PlayBgm(BGMType.Title);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartButtonClicked(){
        //저장 데이터를 지움
        PlayerPrefs.DeleteAll();
        //HP초기화
        PlayerPrefs.SetInt("PlayerHP", 3);
        //저장된 스테이지 정보를 지움
        PlayerPrefs.SetString("LastScene", firstSceneName);
        RoomManager.doorNumber =1;

        SceneManager.LoadScene(firstSceneName);
    }
    public void ContinueButtonClicked(){
        string sceneName = PlayerPrefs.GetString("LastScene", "");//기본값 빈 문자열
        if(string.IsNullOrEmpty(sceneName)){
            Debug.Log("No Scene found in PlayerPrefs");
            return;
        }
        RoomManager.doorNumber = PlayerPrefs.GetInt("LastDoor", 1);
        SceneManager.LoadScene(sceneName);
    }
}
