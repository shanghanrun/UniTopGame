using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public int arrangeId = 0; //식별을 위한 값
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag == "Player"){
            //열쇠를 가지고 있으면
            if(ItemKeeper.hasKeys >0){
                ItemKeeper.hasKeys --; // 열쇠 하나 감소
                Destroy(this.gameObject); // 문없애서 열기
            }

            //배치 Id저장
            SaveDataManager.SetArrangeId(arrangeId, gameObject.tag);
        }
    }
}
