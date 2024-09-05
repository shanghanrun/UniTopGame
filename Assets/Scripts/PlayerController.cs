using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //이동속도
    public float speed = 5f;
    //애니메이션 이름
    public string upAnime = "PlayerUp";
    public string downAnime ="PlayerDown";
    public string rightAnime ="PlayerRight";
    public string leftAnime = "PlayerLeft";
    public string deadAnime ="PlayerDead";

    //현재 에니메이션
    string nowAnimation ="";
    //이전 에니메이션
    string oldAnimation ="";

    float axisH;
    float axisV;
    public float angleZ = -90f; //회전각

    Rigidbody2D rbody;
    bool isMoving = false; //이동중인지 여부

    //데미지처리
    public static int hp =3;
    public static string gameState;
    bool inDamage = false;

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        oldAnimation = downAnime;
        gameState = "playing";
        hp = PlayerPrefs.GetInt("PlayerHP");
        //추가한 코드. 시작 위치
        transform.position = new Vector3(0,-6,0);
    }

    // Update is called once per frame
    void Update()
    {
        if(gameState !="playing" || inDamage){
            return;
        }
        if(!isMoving){
            axisH = Input.GetAxisRaw("Horizontal");
            axisV = Input.GetAxisRaw("Vertical");
        }

        // 키 입력으로 이동 각도 구하기
        Vector2 fromPoint = transform.position;
        Vector2 toPoint = new Vector2(fromPoint.x + axisH, fromPoint.y +axisV);
        angleZ = GetAngle(fromPoint, toPoint);

        // 이동 각도에서 방향과 에니메이션 변경
        if(angleZ >+ -45 && angleZ <45){
            //오른쪽
            nowAnimation = rightAnime;
        } else if(angleZ >=45 && angleZ <=135){
            //위쪽
            nowAnimation = upAnime;
        } else if(angleZ >=-135 && angleZ <= -45){
            //아래쪽
            nowAnimation = downAnime;
        } else{
            //왼쪽
            nowAnimation = leftAnime;
        }

        //에니메이션 변경하기
        if(nowAnimation != oldAnimation){
            oldAnimation = nowAnimation;
            GetComponent<Animator>().Play(nowAnimation);
        }
    }

    void FixedUpdate(){
        if(gameState != "playing"){
            return;
        }
        if(inDamage){
            //대미지를 받는 중에는 점멸시키기
            float val = Mathf.Sin(Time.time *50);
            Debug.Log(val);

            if(val >0){
                //스프라이트 표시
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
            } else{
                // 스프라이트 비표시
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
            return; // 대미지를 받는 중에는 조작할수 없게 하기
        }
        //이동 속도 변경하기
        rbody.velocity = new Vector2(axisH, axisV)*speed;
    }

    public void SetAxis(float h, float v){
        axisH = h;
        axisV = v;
        if(axisH ==0 && axisV ==0){
            isMoving = false;
        }else{
            isMoving = true;
        }
    }
    //p1에서 p2까지의 각도 계산
    float GetAngle(Vector2 p1, Vector2 p2){
        float angle;
        if(axisH !=0 || axisV !=0){
            //이동 중이면 각도를 변경
            float dx = p2.x -p1.x;
            float dy = p2.y -p1.y;

            //아크 탄젠트 함수로 각도(라디안)구하기
            float rad = Mathf.Atan2(dy, dx);
            //라디안을 각도로 변환하기
            angle = rad * Mathf.Rad2Deg;
        } else{ // 정지중이면 이전 각도를 유지
            angle = angleZ;
        }
        return angle;
    }
    //충돌처리
    private void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag == "Enemy"){
            GetDamage(other.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag =="Bullet")  // 충돌한 것이 Bullet일 때
        {
            // Player가 Bullet에 충돌 시 처리할 로직
            // 예: Player HP 감소, 게임 오버 처리 등
            Debug.Log("Player hit by bullet!");

            hp--;
        }
    }

    void GetDamage(GameObject enemy){
        if(gameState == "playing"){
            hp--;
            //HP 갱신하기
            PlayerPrefs.SetInt("PlayerHP", hp);
            
            if(hp>0){
                //이동중지
                rbody.velocity = new Vector2(0,0);
                //적 캐릭터의 반대방향으로 히트백
                Vector3 toPos = (transform.position - enemy.transform.position).normalized;
                rbody.AddForce(new Vector2(toPos.x*4, toPos.y*4), ForceMode2D.Impulse);
                //대미지를 받는 중으로 설정
                inDamage = true;
                Invoke("DamageEnd", 0.25f);
            } else{
                //게임오버
                GameOver();
            }
        }
    }

    // 대미지받기 끝
    void DamageEnd(){
        inDamage = false;
        // 스프라이트 보여주기
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }

    void GameOver(){
        Debug.Log("게임 오버!");
        gameState = "gameover";

        //================
        //게임오버 연출
        //=============

        //플레이어 충돌판정 비활성
        GetComponent<CircleCollider2D>().enabled = false;
        //이동중지
        rbody.velocity = new Vector2(0,0);
        //중력을 적용해 플레이어를 위로 튀어오르게 하는 연출
        rbody.gravityScale =1;
        rbody.AddForce(new Vector2(0,5), ForceMode2D.Impulse);
        //에니메이션 변경하기
        GetComponent<Animator>().Play(deadAnime);
        //1초 후에 플레이어 캐릭터 제거하기
        Destroy(gameObject, 1.0f);

        //BGM정지
        SoundManager.soundManager.StopBgm();
        //SE재생
        SoundManager.soundManager.SEPlay(SEType.GameOver);
    }

    
}
