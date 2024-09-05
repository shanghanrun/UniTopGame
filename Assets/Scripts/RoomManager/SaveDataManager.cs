using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SaveDataManager : MonoBehaviour
{
    public static SaveDataList arrangeDataList;
    void Start()
    {
        //SaveDataList 초기화
        arrangeDataList = new SaveDataList();
        arrangeDataList.saveDatas = new SaveData[]{};

        // 씬 이름 불러오기
        string stageName = PlayerPrefs.GetString("LastScene");
        // 씬 이름을 키로 해 저장 데이터 읽어오기
        string data = PlayerPrefs.GetString("stageName");

        if(data !=""){
            // 저장된 데이터가 존재할 경우
            // JSON에서 SaveDataList로 변환하기
            arrangeDataList = JsonUtility.FromJson<SaveDataList>(data);
            for (int i=0; i< arrangeDataList.saveDatas.Length; i++){
                SaveData saveData = arrangeDataList.saveDatas[i];  // 배열에서 가져오기
                // 태그로 게임 오브젝트 찾기
                string objTag = saveData.objTag;
                GameObject[] objects = GameObject.FindGameObjectsWithTag(objTag);
                for (int j=0; j< objects.Length; j++){
                    GameObject obj = objects[j];

                    // 게임오브젝트의 태그 확인하기
                    if(objTag == "Door"){
                        Door door = obj.GetComponent<Door>();
                        if(door.arrangeId == saveData.arrangeId){
                            Destroy(obj);  // arrangeId가 같으면 제거
                        }
                    } else if(objTag =="ItemBox"){
                        ItemBox box = obj.GetComponent<ItemBox>();
                        if(box.arrangeId == saveData.arrangeId){
                            box.isClosed = false; // 열기
                            box.GetComponent<SpriteRenderer>().sprite =box.openImage;
                        }
                    } else if(objTag =="Item"){
                        ItemData item = obj.GetComponent<ItemData>();
                        if(item.arrangeId == saveData.arrangeId){
                            Destroy(obj);
                        }
                    } else if(objTag =="Enemy"){
                        EnemyController enemy = obj.GetComponent<EnemyController>();
                        if (enemy.arrangeId == saveData.arrangeId){
                            Destroy(obj);
                        }
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //배치 Id 설정
    public static void SetArrangeId(int arrangeId, string objTag){
        if (arrangeId == 0 || objTag ==""){
            //기록하지 않음
            return;
        }
        //추가하기 위해 하나 더 SaveData 배열 만들기
        SaveData[] newSaveDatas = new SaveData[arrangeDataList.saveDatas.Length +1];
        //데이터 복사
        for (int i=0; i<arrangeDataList.saveDatas.Length; i++){
            newSaveDatas[i] = arrangeDataList.saveDatas[i];
        }
        //SaveDatas 만들기
        SaveData saveData = new SaveData();
        saveData.arrangeId = arrangeId;
        saveData.objTag = objTag;
        //SaveData 추가
        newSaveDatas[arrangeDataList.saveDatas.Length] = saveData; //마지막인덱스의 뒤인덱스에 추가
        arrangeDataList.saveDatas = newSaveDatas;
    }

    //기록된 데이터 저장
    public static void SaveArrangeData(string stageName){
        if (arrangeDataList.saveDatas != null && stageName !=""){
            //SaveDataList를 JSON데이터로 변환
            string saveJson = JsonUtility.ToJson(arrangeDataList);
            //씬 이름을 키로 해 저장
            PlayerPrefs.SetString(stageName, saveJson);
        }
    }

}
