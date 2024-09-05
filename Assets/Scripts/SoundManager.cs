using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//BGM종류
public enum BGMType{
    None,
    Title,
    InGame,
    InBoss,
}

//SE 종류
public enum SEType{
    GameClear,
    GameOver,
    Shoot,
}

public class SoundManager : MonoBehaviour
{
    public AudioClip bgmInTitle;
    public AudioClip bgmInGame;
    public AudioClip bgmInBoss;

    public AudioClip meGameClear;
    public AudioClip meGameOver;
    public AudioClip seShoot;

    public static SoundManager soundManager;
    public static BGMType playingBGM = BGMType.None;

    void Awake(){
        //BGM재생
        if(soundManager == null){
            soundManager = this; // static변수에 자기 자신을 저장
            DontDestroyOnLoad(gameObject); //씬 바뀌어도 파괴안함
        } else{
            Destroy(gameObject);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //BGM설정
    public void PlayBgm(BGMType type){
        if(type != playingBGM){
            playingBGM = type;
            AudioSource audio = GetComponent<AudioSource>();
            if (type == BGMType.Title){
                audio.clip = bgmInTitle;
            } else if(type == BGMType.InGame){
                audio.clip = bgmInGame;
            } else if(type == BGMType.InBoss){
                audio.clip = bgmInBoss;
            }
            audio.Play();
        }
    }

    //BGM 정지
    public void StopBgm(){
        GetComponent<AudioSource>().Stop();
        playingBGM = BGMType.None;
    }

    //SE재생
    public void SEPlay(SEType type){
        if(type == SEType.GameClear){
            GetComponent<AudioSource>().PlayOneShot(meGameClear);
        } else if(type == SEType.GameOver){
            GetComponent<AudioSource>().PlayOneShot(meGameOver);
        } else if(type == SEType.Shoot){
            GetComponent<AudioSource>().PlayOneShot(seShoot);
        }
    }
}
