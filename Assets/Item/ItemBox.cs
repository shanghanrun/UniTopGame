using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    public Sprite openImage;
    public GameObject itemPrefab;
    public bool isClosed = true;
    public int arrangeId =0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other){
        if(isClosed && other.gameObject.tag =="Player"){
            GetComponent<SpriteRenderer>().sprite = openImage;
            isClosed = false;
            if(itemPrefab !=null){
                Instantiate(itemPrefab, transform.position, Quaternion.identity);
            }
            // 배치 Id 저장
            SaveDataManager.SetArrangeId(arrangeId, gameObject.tag);
        } 
    }
}
