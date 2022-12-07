using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NameScript : MonoBehaviour
{   
    public InputField inputName;

    // 이름 입력 받고 저장 페이지 이동
    public void Save() { 
        PlayerPrefs.SetString("Name", inputName.text);
        SceneManager.LoadScene("GameScene");
    }

    // Start is called before the first frame update
    void Start()
    {
        SoundManager.instance.StopBGM();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonUp("Submit")){
            PlayerPrefs.SetString("Name", inputName.text);
            SceneManager.LoadScene("GameScene");
        }
    }
}