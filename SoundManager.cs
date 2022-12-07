using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//자동으로 AudioSource GetComponent 부착
[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    AudioSource myAudio;
    AudioSource myBGM;

    // botton Sound
    public AudioClip normalBtnSound;
    public AudioClip saveBtnSound;

    // Effect Sound
    //public AudioClip


    // BGM
    public AudioClip startOst;
    public AudioClip accidentBgm;
    public AudioClip isuHouse;
    public AudioClip thinkSound;


    private void Awake(){
        if(instance == null){
            instance=this;
        }
        //씬이 전환되어도 소리가 음악이 사라지지 않는다.
        DontDestroyOnLoad(transform.gameObject);
   
    }

    void Start()
    {
        myAudio = GetComponent<AudioSource>();
        myBGM = GetComponent<AudioSource>();
        myBGM.loop = true;
    }

// botton
    public void NormalBtnSound(){
        myAudio.PlayOneShot(normalBtnSound);
    }

    public void SaveBtnSound(){
        myAudio.PlayOneShot(saveBtnSound);
    }

// Effect
/*    public void LookBack_Dark(){
        myAudio.PlayOneShot(lookBack_Dark);
    }*/

// BGM
    public void PlayBGM(AudioClip clip){
        myBGM.Stop();
        myBGM.clip = clip;
        myBGM.time = 0;
        myBGM.Play();
    }

    public void StopBGM(){
        myBGM.Stop();
    }

    public void PauseBGM(){
        myBGM.Pause();
    }

    public void UnPauseBGM(){
        myBGM.UnPause();
    }

    public void StartOst(){
        PlayBGM(startOst);
    }

    public void AccidentBgm(){
        PlayBGM(accidentBgm);
    }

    public void IsuHouse(){
        PlayBGM(isuHouse);
    }

    public void ThinkSound(){
        PlayBGM(thinkSound);
    }
}
