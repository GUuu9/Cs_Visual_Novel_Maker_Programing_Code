using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScr : MonoBehaviour
{
    //GameObject
    public GameObject startGame; // 메뉴 버튼
    public GameObject loadGame;
    public GameObject exitGame;

    public static bool isCall; // 씬 전환간 데이터 유지를 위한 값

    void Start(){
        SoundManager.instance.StartOst();
    }

    public void StartGame(){
        isCall = false;
        PlayerPrefs.SetString("isLoadCall", isCall.ToString()); // 데이터 저장 후 씬 로드.
        PlayerPrefs.Save();
        SoundManager.instance.NormalBtnSound();
        SceneManager.LoadScene("NameScene");
    }

    public void LoadGame(){
        isCall = true;
        PlayerPrefs.SetString("isLoadCall", isCall.ToString()); // 데이터 저장 후 씬 로드.
        PlayerPrefs.Save();
        SoundManager.instance.NormalBtnSound();
        SceneManager.LoadScene("GameScene");   
    }

    public void ExitGame(){
        SoundManager.instance.NormalBtnSound();
        Application.Quit();
    }   

}
