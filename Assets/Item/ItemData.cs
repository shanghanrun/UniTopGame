using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//아이템 종류
public enum ItemType{
    arrow,
    key,
    life,
}

public class ItemData : MonoBehaviour
{
    public ItemType type;
    public int count =1; //아이템수
    public int arrangeId =0; // 식별을 위한 값
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //충돌
    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Player"){
            if(type == ItemType.key){
                ItemKeeper.hasKeys++;
            } else if(type == ItemType.arrow){
                ArrowShoot shoot = other.gameObject.GetComponent<ArrowShoot>();
                ItemKeeper.hasArrows += count;
            } else if(type == ItemType.life){
                if(PlayerController.hp <3){
                    //HP가 3이하면 추가
                    PlayerController.hp++;

                    //HP 갱신
                    PlayerPrefs.SetInt("PlayerHP", PlayerController.hp);
                }
            }

            //==== 아이템 획득 연출 =======
            // 충돌 판정 비활성
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            Rigidbody2D itemBody = GetComponent<Rigidbody2D>();
            itemBody.gravityScale = 2.5f;
            itemBody.AddForce(new Vector2(0,6), ForceMode2D.Impulse);
            // 0.5초 뒤에 제거
            Destroy(gameObject, 0.5f);

            //배치 Id 저장
            SaveDataManager.SetArrangeId(arrangeId, gameObject.tag);
        }
    }
}
