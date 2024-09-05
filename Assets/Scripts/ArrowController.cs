using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public float deleteTime =2;
    void Start()
    {
        Destroy(gameObject, deleteTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //게임오브젝트 충돌처리
    private void OnCollisionEnter2D(Collision2D other){
        //접촉한 게임오브젝ㅌ의 자식으로 설정
        if(other.gameObject.tag != "Bullet" ){
            transform.SetParent(other.transform);
            //충돌판정을 비활성화
            GetComponent<CircleCollider2D>().enabled = false;
            //물리 시뮬레이션을 비활성화
            GetComponent<Rigidbody2D>().simulated = false;
        }
        
    }
}
