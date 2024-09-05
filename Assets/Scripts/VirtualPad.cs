using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VirtualPad : MonoBehaviour
{
    public float MaxLength =70; // 탭이 움직이는 최대거리
    public bool is4DPad = true;
    GameObject player;
    Vector2 defPos; // 탭의 초기 좌표
    Vector2 downPos; // 터치위치
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        defPos = GetComponent<RectTransform>().localPosition; // 탭의 초기좌표
    }

    // Update is called once per frame
    void Update()
    {        
    }

    //다운 이벤트
    public void PadDown(){
        // 마우스 포인트의 스크린 좌표
        downPos = Input.mousePosition;
    }
    // 드래그 이벤트
    public void PadDrag(){
        //마우스 포인트의 스크린 좌표
        Vector2 mousePosition = Input.mousePosition;
        // 새로운 탭 위치 구하기
        Vector2 newTabPos = mousePosition - downPos; // 마우스다운 위치에서의 이동거리
        if (is4DPad == false){
            newTabPos.y =0; // 횡스크롤일때는 Y값을 0으로
        } 
        // 이동 벡터 계산하기
        Vector2 axis = newTabPos.normalized;
        //두 점의 거리 구하기
        float len = Vector2.Distance(defPos, newTabPos);
        if(len > MaxLength){
            newTabPos.x = axis.x * MaxLength;
            newTabPos.y = axis.y * MaxLength;
        }

        // 탭 이동시키기
        GetComponent<RectTransform>().localPosition = newTabPos;
        //플레이어 캐릭터 이동시키기
        PlayerController pControl = player.GetComponent<PlayerController>();
        pControl.SetAxis(axis.x, axis.y);
    }

    //업 이벤트
    public void PadUp(){
        //탭 위치 초기화
        GetComponent<RectTransform>().localPosition = defPos;
        //플레이어 케릭터 정지시키기
        PlayerController pControl = player.GetComponent<PlayerController>();
        pControl.SetAxis(0,0);
    }

    // 공격
    public void Attack(){
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        ArrowShoot shoot = player.GetComponent<ArrowShoot>();
        shoot.Attack();
    }
    
}
