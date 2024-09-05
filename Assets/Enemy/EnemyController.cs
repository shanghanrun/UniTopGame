using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //HP
    public int hp=3;
    public float speed =5f;
    public float reactionDistance =6f;

    public string idleAnime ="EnemyIdle";
    public string upAnime ="EnemyUp";
    public string downAnime ="EnemyDown";
    public string rightAnime ="EnemyRight";
    public string leftAnime ="EnemyLeft";
    public string deadAnime ="EnemyDead";

    string nowAnimation ="";
    string oldAnimation ="";

    float axisH;  // -1 ~0 ~1
    float axisV;
    Rigidbody2D rbody;

    bool isActive =false;
    public int arrangeId =0;

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player !=null){
            if(isActive){
                // 플레이어와의 각도 구하기
                float dx = player.transform.position.x - transform.position.x;
                float dy = player.transform.position.y - transform.position.y;
                float rad = Mathf.Atan2(dy,dx);
                float angle = rad * Mathf.Rad2Deg;

                // 이동 각도에 따른 에니메이션 결정
                if(angle > -45f && angle <=45f){
                    nowAnimation = rightAnime;
                } else if (angle >45f && angle <=135f){
                    nowAnimation = upAnime;
                } else if (angle >= -135 && angle <= -45f){
                    nowAnimation = downAnime;
                } else {
                    nowAnimation = leftAnime;
                }

                // 이동할 벡터 만들기
                axisH = Mathf.Cos(rad) * speed;
                axisV = Mathf.Sin(rad) * speed;
            } else{
                // 플레이어와의 거리 확인
                float dist = Vector2.Distance(transform.position, player.transform.position);
                if(dist < reactionDistance){
                    isActive = true;
                }
            }
        } else if(isActive){ // player가 없을 경우
            isActive = false;
            rbody.velocity = Vector2.zero;
        }
    }

    void FixedUpdate(){
        if(isActive && hp>0){
            //이동
            rbody.velocity = new Vector2(axisH, axisV);
            if(nowAnimation != oldAnimation){
                oldAnimation = nowAnimation;
                Animator animator = GetComponent<Animator>();
                animator.Play(nowAnimation);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag =="Arrow"){
            hp--;
            if(hp <=0){
                //사망
                //사망연출========
                GetComponent<CircleCollider2D>().enabled =false;
                rbody.velocity = new Vector2(0,0); //이동정지
                Animator animator = GetComponent<Animator>();
                animator.Play(deadAnime);
                Destroy(gameObject, 0.5f);

                // 배치 Id저장
                SaveDataManager.SetArrangeId(arrangeId, gameObject.tag);
            }
        }
    }
}
