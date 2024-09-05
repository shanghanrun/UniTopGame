using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowShoot : MonoBehaviour
{
    public float shootSpeed = 12f;
    public float shootDelay = 0.25f;
    public GameObject bowPrefab;
    public GameObject arrowPrefab;

    bool inAttack = false;
    GameObject bowObj;

    void Start()
    {
        //활을 플레이어 캐릭터에 배치
        Vector3 pos = transform.position;
        bowObj = Instantiate(bowPrefab, pos, Quaternion.identity);
        bowObj.transform.SetParent(transform); //플레이어를 부모로 설정
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire3")){
            //공격키가 눌림
            Attack();
        }
        //활의 회전과 우선순위
        float bowZ = -1; // 활의 z값(캐릭터보다 앞으로 설정)
        PlayerController pControl = GetComponent<PlayerController>();
        if(pControl.angleZ > 30 && pControl.angleZ <150){
            //위 방향
            bowZ =1;  // 캐릭터보다 뒤로 설정
        }
        // 활의 회전
        bowObj.transform.rotation = Quaternion.Euler(0, 0, pControl.angleZ);
        //활의 우선순위
        bowObj.transform.position = new Vector3(transform.position.x,
                                                transform.position.y, bowZ);
    }

    //공격
    public void Attack(){
        //화살을 갖고 있음 & 공격중이 아님
        if(ItemKeeper.hasArrows >0 && inAttack == false){
            ItemKeeper.hasArrows -=1;
            inAttack = true;

            //화살발사
            PlayerController pCtrl = GetComponent<PlayerController>();
            float angleZ = pCtrl.angleZ; // 회전각도
            //화살의 게임오브젝트 만들기(진행 방향으로 회전)
            Quaternion q = Quaternion.Euler(0, 0, angleZ);
            GameObject arrowObj = Instantiate(arrowPrefab, transform.position, q);
            //화살을 발사할 벡터생성
            float x = Mathf.Cos(angleZ * Mathf.Deg2Rad);
            float y = Mathf.Sin(angleZ * Mathf.Deg2Rad);
            Vector3 v = new Vector3(x,y) *shootSpeed;
            //화살에 힘을 가하기
            Rigidbody2D rbody = arrowObj.GetComponent<Rigidbody2D>();
            rbody.AddForce(v, ForceMode2D.Impulse);

            //SE재생
            SoundManager.soundManager.SEPlay(SEType.Shoot);
            //공격중이 아님으로 설정
            Invoke("StopAttack", shootDelay);
        }
    }

    // 공격중지
    public void StopAttack(){
        inAttack = false;
    }
}
