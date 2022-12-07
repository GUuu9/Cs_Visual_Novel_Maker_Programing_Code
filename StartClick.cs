using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; //Scene 전환을 위해



public class StartClick : MonoBehaviour
{   
    float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        if(SceneManager.GetActiveScene().name == "StartScene"){
            SoundManager.instance.StartOst();
        }
    }

    // Update is called once per frame
    void Update()
    {
    
        if(SceneManager.GetActiveScene().name == "PassedScene"){
            if (time < 4f){
            time += Time.deltaTime;
            }
            else{
                SceneManager.LoadScene("StartScene");
            }

        }
        if (Input.GetMouseButtonUp(0) || Input.anyKey) { // 마우스 클릭시 확인
            if(SceneManager.GetActiveScene().name == "StartScene")
                SceneManager.LoadScene("MainMenu");
        }
    }


}
