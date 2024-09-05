using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    int hasKeys =0; // 열쇠 수
    int hasArrows =0; 
    int hp =0; // 플레이어 hp
    public GameObject arrowText;
    public GameObject keyText;
    public GameObject hpImage;
    public Sprite life3Image;
    public Sprite life2Image;
    public Sprite life1Image;
    public Sprite life0Image;
    public GameObject mainImage;
    public GameObject resetButton;
    public Sprite gameOverSpr;
    public Sprite gameClearSpr;
    public GameObject inputPanel;

    public string retrySceneName ="";

    void Start()
    {
        UpdateItemCount();
        UpdateHP();

        //이미지 숨기기
        Invoke("InactiveImage", 1f);
        resetButton.SetActive(false); // 버튼 숨기기
    }

    // Update is called once per frame
    void Update()
    {
        UpdateItemCount();
        UpdateHP();
    }

    //아이템 수 갱신
    void UpdateItemCount(){
        //화살
        if(hasArrows != ItemKeeper.hasArrows){
            arrowText.GetComponent<Text>().text = ItemKeeper.hasArrows.ToString();
            hasArrows = ItemKeeper.hasArrows;
        }
        //열쇠
        if(hasKeys != ItemKeeper.hasKeys){
            keyText.GetComponent<Text>().text = ItemKeeper.hasKeys.ToString();
            hasKeys = ItemKeeper.hasKeys;
        }
    }

    //HP갱신
    void UpdateHP(){
        //Player 가져오기
        if(PlayerController.gameState != "gameover"){
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if(player !=null){
                if(PlayerController.hp != hp){
                    hp = PlayerController.hp;
                    if(hp <=0){
                        hpImage.GetComponent<Image>().sprite = life0Image;

                        //플레이어 사망
                        resetButton.SetActive(true); // 버튼 표시
                        mainImage.SetActive(true); // 이미지 표시
                        mainImage.GetComponent<Image>().sprite = gameOverSpr;
                        inputPanel.SetActive(false); // 조작 UI 숨기기
                        PlayerController.gameState = "gameend";
                    } else if(hp ==1){
                        hpImage.GetComponent<Image>().sprite = life1Image;
                    } else if(hp ==2){
                        hpImage.GetComponent<Image>().sprite = life2Image;
                    } else{
                        hpImage.GetComponent<Image>().sprite = life3Image;
                    }
                }
            }
        }
    }

    //재시도
    public void Retry(){
        //HP 되돌리기
        PlayerPrefs.SetInt("PlayerHP", 3);

        //BGM초기화
        SoundManager.playingBGM = BGMType.None;
        // PlayerController.SetHP(3);
        SceneManager.LoadScene(retrySceneName);
    }

    //이미지 숨기기
    void InactiveImage(){
        mainImage.SetActive(false);
    }

    //게임클리어
    public void GameClear(){
        //화면표시
        mainImage.SetActive(true);
        mainImage.GetComponent<Image>().sprite = gameClearSpr;
        //조작 UI 숨기기
        inputPanel.SetActive(false);
        //게임클리어
        PlayerController.gameState ="gameclear";
        //3초뒤에 타이틀 화면으로 이동
        Invoke("GoToTitle", 3f);
    }
    void GoToTitle(){
        PlayerPrefs.DeleteKey("LastScene");
        SceneManager.LoadScene("Title");
    }
}
