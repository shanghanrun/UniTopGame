using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGenManager : MonoBehaviour
{
    ObjectGenPoint[] objGens;
    void Start()
    {
        objGens = GameObject.FindObjectsOfType<ObjectGenPoint>();
    }

    // Update is called once per frame
    void Update()
    {
        //ItemData 찾기
        ItemData[] items = GameObject.FindObjectsOfType<ItemData>();

        for (int i=0; i< items.Length; i++){
            ItemData item = items[i];
            if(item.type == ItemType.arrow){
                return; // 화살이 있으면 아무것도 하지 않는다.
            }
        }
        // 플레이어가 존재하는 지와 화살의 수 확인
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (ItemKeeper.hasArrows ==0 && player !=null){
            //배열의 개수 범위 안에서 난수 생성
            int index = Random.Range(0, objGens.Length);
            ObjectGenPoint objgen = objGens[index];
            objgen.ObjectCreate();
        }
    }
}
