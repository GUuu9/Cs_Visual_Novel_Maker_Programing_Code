using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChatController : MonoBehaviour
{   
    //Coroutine / PlayerPrefs
    public static bool isCall; // MainScene 에서 넘어오는지 확인
    public static bool textCheck;// 텍스트가 완성이 되었는지 확인.
    public static bool textfin;
    public static bool AutoOn;
    public string playerName;
    public string storyNow = null;
    public int isuScore = 0;
    public int gyonScore = 0;
    public int scoreCount = 0;
    public int flag = 0;
    public int bgNum = 0;
    public IEnumerator ieNow = null;

    //Image
    public GameObject chatBox;
    public Image emptyImageBG; // 기존에 있는 이미지; (BG)
    public Image emptyImageC; // 기존에 있는 이미지; (Center)
    public Image emptyImageR; // 기존에 있는 이미지; (Right)
    public Image emptyImageL; // 기존에 있는 이미지; (Left)

    public Sprite [] changeSpriteBG = new Sprite [25]; // 배경 이미지

    public Sprite [] changeSprite = new Sprite [8]; // 캐릭터 이미지

    //Text
    public Text ChatText; // 스토리 텍스트
    public Text CharacterName; // 캐릭터 이름

    public GameObject storyNowBox;
    public Text storyNowText; // 챕터 전환 텍스트
    public Text storyNowWhere; // 메뉴 위치 확인용

    public Text [] saveSlotText = new Text [8]; 
    public Text [] loadSlotText = new Text [8];

    //Button
    public GameObject b1; // 선택지 버튼
    public GameObject b2;
    public Text b1Text;
    public Text b2Text;


    public GameObject uiHide;   // UI 버튼
    public GameObject uiHideBack;
    public GameObject AutoPlay;
    public Text AutoText;

    public GameObject menuSet;  // 메뉴 버튼
    public GameObject menuCover;
    public GameObject resumebtn;
    public GameObject savebtn;
    public GameObject loadbtn;
    public GameObject returnHomebtn;

    public GameObject saveMenu; // 저장 메뉴
    public GameObject saveClose;
    public GameObject [] saveSlot = new GameObject [8];

    public GameObject loadMenu; // 불러오기 메뉴
    public GameObject loadClose;
    public GameObject [] loadSlot = new GameObject [8];


    public string writerText = ""; // 문자열을 한글자씩 나누기 위함

    void Awake(){
        Screen.sleepTimeout = (int)SleepTimeout.NeverSleep; // 화면 꺼짐 방지
    }

////// Start is called before the first frame update
    void Start()
    {   

        playerName = PlayerPrefs.GetString("Name");
        storyNowBox.SetActive(false);

        isuScore = 0;
        gyonScore = 0;
        scoreCount = 0;
        flag = 0;
        bgNum = 0;
        AutoOn = false;
        StartCoroutine(Text1_0());
        // 오브젝트 비활성화
        b1.SetActive(false); 
        b2.SetActive(false);
        uiHideBack.SetActive(false);


        // 로드 메뉴로 왔는지 데이터 받아서 불 함수로 변환`             
        string value = PlayerPrefs.GetString("isLoadCall", "false"); // MainScene 에서 넘어온 값 확인 if else문까지
        bool isCall = System.Convert.ToBoolean(value);

        // 로드 메뉴로 왔는지 판독
        if(isCall == true){
            menuCover.SetActive(true);
            SoundManager.instance.StopBGM();

        }
        else{
            menuCover.SetActive(false);
        }


        // 로드 메뉴 데이터 표시
        for(int i = 0; i <= 7; i++){
            saveSlotText[i].text = "플레이어 이름 : " + PlayerPrefs.GetString("playerName" + i) + "\n플레이 위치 : " + PlayerPrefs.GetString("playerData" + i) + "\n저장 시간 : " + PlayerPrefs.GetString("saveDate" + i, System.DateTime.Now.ToString());
            loadSlotText[i].text = "플레이어 이름 : " + PlayerPrefs.GetString("playerName" + i) + "\n플레이 위치 : " + PlayerPrefs.GetString("playerData" + i) + "\n저장 시간 : " + PlayerPrefs.GetString("saveDate" + i, System.DateTime.Now.ToString());
        }
    }


///////////////////////////////////////
//스토리 진행에 따른 버튼 //
    public void B1(){
        //AudioSource btnClickAudio = b1.GetComponent<AudioSource>();
        //btnClickAudio.Play();
        scoreCount++;
        switch(scoreCount){
            case 1 :
                StartCoroutine(Text2_2_1());
                break;
            case 2 :
                StartCoroutine(Text3_5_1());
                break;
            case 3 :
                StartCoroutine(Text5_2_1());
                flag = 0; 
                break;
            case 4 :
                isuScore++;
                StartCoroutine(Text7_3_1()); 
                break;
            case 5 :
                StartCoroutine(Text8_5_1()); 
                break;
            case 6 :
                StartCoroutine(Text10_5_1()); 
                break;
            case 7 :
                StartCoroutine(Text12_4_1()); 
                break;

            default :
                return;
        }
    
    }
    public void B2(){
        //AudioSource btnClickAudio = b2.GetComponent<AudioSource>();
        //btnClickAudio.Play();
        scoreCount++;
        switch(scoreCount){
            case 1 :
                gyonScore++;
                StartCoroutine(Text2_2_2());
                break;
            case 2 :
                isuScore++;
                StartCoroutine(Text3_5_2()); 
                break;
            case 3 :
                gyonScore++;
                StartCoroutine(Text5_2_2());
                flag = 1; 
                break;
            case 4 :
                StartCoroutine(Text7_3_1()); 
                break;
            case 5 :
                gyonScore++;
                StartCoroutine(Text8_5_2()); 
                break;
            case 6 :
                isuScore++;
                StartCoroutine(Text10_5_2()); 
                break;
            case 7 :
                StartCoroutine(Text12_4_2()); 
                break;

            default :
                return;
        }      
    }

    //////////////////////////////////////////////////
//////// 메뉴 버튼들
    public void UiHide(){ // 모든 오브젝트 숨기기
        //SoundManager.instance.NormalBtnSound();
        Time.timeScale = 0;
        uiHideBack.SetActive(true);
        uiHide.SetActive(false);
        menuSet.SetActive(false);
        chatBox.gameObject.SetActive(false);
    }
    
    public void UiHideBack(){ // 모든 오브젝트 다시 보이기
        //SoundManager.instance.NormalBtnSound();
        Time.timeScale = 1;
        uiHideBack.SetActive(false);
        uiHide.SetActive(true);
        menuSet.SetActive(true);
        chatBox.gameObject.SetActive(true);
    }

    public void MenuButtonSet(){ // 메뉴버튼
        //SoundManager.instance.NormalBtnSound();
        //SoundManager.instance.PauseBGM();
        menuCover.SetActive(true);
        saveMenu.SetActive(false);
        loadMenu.SetActive(false);
    }

    public void ResumeBtn(){ // 계속하기
        //SoundManager.instance.NormalBtnSound();
        //SoundManager.instance.UnPauseBGM();
        menuCover.SetActive(false);
    }

    public void SaveBtn(){
        //SoundManager.instance.NormalBtnSound();
        saveMenu.SetActive(true);
    }

    public void LoadBtn(){
        //SoundManager.instance.NormalBtnSound();
        loadMenu.SetActive(true);
    }

    public void ReturnHomebtn(){
        //SoundManager.instance.NormalBtnSound();
        SceneManager.LoadScene("MainMenu");
    }

    public void SaveClose(){
        //SoundManager.instance.NormalBtnSound();
        saveMenu.SetActive(false);
    }

    public void AutoOnClick(){
        //SoundManager.instance.NormalBtnSound();
        if(AutoOn == true){
            AutoOn = false;
            AutoText.text = "Auto";
        }
        else{
            AutoOn = true;
            AutoText.text = "off";
        } 
    }


////////////// 세이브 슬롯
    public void SaveSlot(int i){

        SoundManager.instance.SaveBtnSound();
        PlayerPrefs.SetString("playerName" + i, playerName);
        PlayerPrefs.SetString("playerData" + i, storyNow);
        PlayerPrefs.SetString("saveDate" + i, System.DateTime.Now.ToString());
        PlayerPrefs.SetInt("isuScore" + i, isuScore);
        PlayerPrefs.SetInt("gyonScore" + i, gyonScore);
        PlayerPrefs.SetInt("Score" + i, scoreCount);
        PlayerPrefs.SetInt("flag" + i, flag);
        saveSlotText[i].text = "플레이어 이름 : " + PlayerPrefs.GetString("playerName" + i) + "\n플레이 위치 : " + PlayerPrefs.GetString("playerData" + i) + "\n저장 시간 : " + PlayerPrefs.GetString("saveDate" + i, System.DateTime.Now.ToString());
        loadSlotText[i].text = "플레이어 이름 : " + PlayerPrefs.GetString("playerName" + i) + "\n플레이 위치 : " + PlayerPrefs.GetString("playerData" + i) + "\n저장 시간 : " + PlayerPrefs.GetString("saveDate" + i, System.DateTime.Now.ToString());
        PlayerPrefs.Save();
    }
////////////// 로드 닫기
    public void LoadClose(){
        SoundManager.instance.NormalBtnSound();
        string value = PlayerPrefs.GetString("isLoadCall", "false"); // MainScene 에서 넘어온 값 확인 if else문까지
        bool isCall = System.Convert.ToBoolean(value);

        if (isCall == true){
            SceneManager.LoadScene("MainMenu");
        }
        else{
            loadMenu.SetActive(false);
        }
    }

/////////////// 로드 슬롯 
    public void LoadSlot(int i){
        SoundManager.instance.NormalBtnSound();
        playerName = PlayerPrefs.GetString("playerName" + i);
        storyNow = PlayerPrefs.GetString("playerData" + i);
        isuScore = PlayerPrefs.GetInt("isuScore" + i);
        gyonScore = PlayerPrefs.GetInt("gyonScore" + i);
        scoreCount = PlayerPrefs.GetInt("Score" + i);
        flag = PlayerPrefs.GetInt("flag" + i);
        FindStory(storyNow);
    }

////////////////////////////////////////////////////////////////////////////////////////////////
// 저장 포인트 수정 지점
////////////////////////////////////////////////////////////////////////////////////////////////
    public void FindStory(string findNow){
        StopAllCoroutines();
        switch (findNow)
        {
            case "저장시점1":
                ieNow = Text1_0();
                break;
            case "저장시점2":
                ieNow = Text1_1();
                break;  

            default :
                return;
        }
        StartCoroutine(ieNow);
        menuCover.SetActive(false);
        isCall = false;
        PlayerPrefs.SetString("isLoadCall", isCall.ToString()); // 데이터 저장 후 씬 로드.
        PlayerPrefs.Save();
    }



// Update is called once per frame
    void Update(){
        if(Input.GetButtonDown("Cancel")){ 
            SoundManager.instance.NormalBtnSound();
            string value = PlayerPrefs.GetString("isLoadCall", "false"); // MainScene 에서 넘어온 값 확인 if else문까지
            bool isCall = System.Convert.ToBoolean(value);

            if (isCall == true){
                     SceneManager.LoadScene("MainMenu");
                }
            else{
                if(menuCover.activeSelf){
                   menuCover.SetActive(false);
                   SoundManager.instance.UnPauseBGM();
                   AutoOn = true;
                }
                else{
                    SoundManager.instance.PauseBGM();
                    AutoOn = false;
                    menuCover.SetActive(true);
                    saveMenu.SetActive(false);
                    loadMenu.SetActive(false);
                }
            }
        }
        
        if(Input.GetKeyUp(KeyCode.P)){
            SoundManager.instance.NormalBtnSound();
            if(uiHide.activeSelf){
                Time.timeScale = 0;
                uiHideBack.SetActive(true);
                uiHide.SetActive(false);
                menuSet.SetActive(false);
                chatBox.gameObject.SetActive(false);
            }
            else{
                Time.timeScale = 1;
                uiHideBack.SetActive(false);
                uiHide.SetActive(true);
                menuSet.SetActive(true);
                chatBox.gameObject.SetActive(true);
            }
        }

        if(Input.GetKeyUp(KeyCode.A)){
            SoundManager.instance.NormalBtnSound();
            if(AutoOn == true){
                AutoOn = false;
                AutoText.text = "Auto";
            }
            else{
                AutoOn = true;
                AutoText.text = "off";
            }
        }

        if(textfin == false){
            if (Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0)){
                textCheck = true;
            }
        }

        

        storyNowWhere.text = storyNow;
    }
// narrator = 캐릭터 이름 , narration = 대사 , BGImg = 배경 이미지 , character = 올라갈 캐릭터 이미지(C,R,L).
    // 타이핑 효과를 주는 코드
    IEnumerator NormalChat(string narrator, string narration, int BGImg, int characterC, int characterR, int characterL) {
        int a = 0;
        CharacterName.text = narrator;
        writerText = "";

// 배경 이미지 변경 
        if (BGImg == 0) {
            emptyImageBG.sprite = changeSpriteBG[BGImg];
        }
        if (BGImg == 1) {
            emptyImageBG.sprite = changeSpriteBG[BGImg];
        }
        if (BGImg == 2) {
            emptyImageBG.sprite = changeSpriteBG[BGImg];
        }
        if (BGImg == 3) {
            emptyImageBG.sprite = changeSpriteBG[BGImg];
        }
        if (BGImg == 4) {
            emptyImageBG.sprite = changeSpriteBG[BGImg];
        }
        if (BGImg == 5) {
            emptyImageBG.sprite = changeSpriteBG[BGImg];
        }
        if (BGImg == 6) {
            emptyImageBG.sprite = changeSpriteBG[BGImg];
        }
        if (BGImg == 7) {
            emptyImageBG.sprite = changeSpriteBG[BGImg];
        }
        if (BGImg == 8) {
            emptyImageBG.sprite = changeSpriteBG[BGImg];
        }
        if (BGImg == 9) {
            emptyImageBG.sprite = changeSpriteBG[BGImg];
        }
        if (BGImg == 10) {
            emptyImageBG.sprite = changeSpriteBG[BGImg];
        }
        if (BGImg == 11) {
            emptyImageBG.sprite = changeSpriteBG[BGImg];
        }
        if (BGImg == 12) {
            emptyImageBG.sprite = changeSpriteBG[BGImg];
        }
        if (BGImg == 13) {
            emptyImageBG.sprite = changeSpriteBG[BGImg];
        }
        if (BGImg == 14) {
            emptyImageBG.sprite = changeSpriteBG[BGImg];
        }
        if (BGImg == 15) {
            emptyImageBG.sprite = changeSpriteBG[BGImg];
        }
        if (BGImg == 16) {
            emptyImageBG.sprite = changeSpriteBG[BGImg];
        }
        if (BGImg == 17) {
            emptyImageBG.sprite = changeSpriteBG[BGImg];
        }
        if (BGImg == 18) {
            emptyImageBG.sprite = changeSpriteBG[BGImg];
        }
        if (BGImg == 19) {
            emptyImageBG.sprite = changeSpriteBG[BGImg];
        }
        if (BGImg == 20) {
            emptyImageBG.sprite = changeSpriteBG[BGImg];
        }
        if (BGImg == 21) {
            emptyImageBG.sprite = changeSpriteBG[BGImg];
        }
        if (BGImg == 22) {
            emptyImageBG.sprite = changeSpriteBG[BGImg];
        }
        if (BGImg == 23) {
            emptyImageBG.sprite = changeSpriteBG[BGImg];
        }
        if (BGImg == 24) {
            emptyImageBG.sprite = changeSpriteBG[BGImg];
        }
        
// 이미지 변경 
// 만약 해당 값이 1이라면 changeSprite로 변경
        if (characterC == 0) { // 투명 화면
            emptyImageC.sprite = changeSprite[characterC];
        }
        if (characterC == 1) { // Ame
            emptyImageC.sprite = changeSprite[characterC];
        }
        if (characterC == 2) { // Ina
            emptyImageC.sprite = changeSprite[characterC];
        }
/////////////////////////////////////////////////////////////////////////////////////
        if (characterR == 0) { // 투명 화면
            emptyImageR.sprite = changeSprite[characterR];
        }
        if (characterR == 1) { // Ame
            emptyImageR.sprite = changeSprite[characterR];
        }
        if (characterR == 2) { // Ina
            emptyImageR.sprite = changeSprite[characterR];
        }
/////////////////////////////////////////////////////////////////////////////////////
        if (characterL == 0) { // 투명 화면
            emptyImageL.sprite = changeSprite[characterL];
        }
        if (characterL == 1) { // Ame
            emptyImageL.sprite = changeSprite[characterL];
        }
        if (characterL == 2) { // Ina
            emptyImageL.sprite = changeSprite[characterL];
        }

        textfin = false;

        //텍스트 타이핑 효과
        for ( a = 0; a < narration.Length; a++){
            if (textCheck == false)
            {
                ChatText.text = narration.Substring(0, a);
                yield return new WaitForSeconds(0.02f);
            }
            else
            {
                ChatText.text = narration;
                textCheck = false;
                break;
            }
            yield return null;
        }

        textfin = true;
        yield return new WaitForSeconds(0.3f);

        if(AutoOn == true){
            yield return new WaitForSeconds(0.1f);
            yield break;
        }

        //다시 키를 누를때까지 무한 대기
        while (true) {
            if(Input.GetKeyUp(KeyCode.Space) && textfin == true){
                    break;
            }
            if(Input.GetMouseButtonUp(0) && textfin == true){
                if(!EventSystem.current.IsPointerOverGameObject()){
                    break;
                }
            }
            yield return null;
        }
    }

////
/*
        IEnumerator Text() {
        storyNow = "";
        SoundManager.instance.();
        b1.SetActive(false); 
        b2.SetActive(false);
        b1Text.text = "\"아니 왜 이러세요?\"";
        b2Text.text = "일단 시키는 대로 한다.";

        bgNum = ;

        yield return StartCoroutine(NormalChat("", "", bgNum,0,0,0));
        yield return StartCoroutine(NormalChat("", "", bgNum,0,0,0));
        yield return StartCoroutine(NormalChat("", "", bgNum,0,0,0));
        
        yield return StartCoroutine(Text());
    }*/
    
