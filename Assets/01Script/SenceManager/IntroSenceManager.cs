using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public enum SAVE_Type
{
    SAVE_NickName,  // 계정역할 닉네임
    SAVE_SceneName, // 로딩씬 사용할 때
    SAVE_SFX,   // 사운드 효과음
    SAVE_BGM,   // 배경음
    SAVE_Level, // 플레이어 레벨
    SAVE_EXP,   // 플레이어 경험치
    SAVE_GOLD,  // 골드
}

public enum SCENE_NAME
{
    IntroSence,
    LoadingSence,
    LobbySence,
    BattleSence
}

public class IntroSenceManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TextMeshProUGUI welcomeText;

    private bool haveUserInfo = false;


    private void Awake()
    {
        InitIntroScene();
    }

    // 유저 데이터가 있는지 검사
    // 있으면 환영 메세지
    // 없으면 계정생성로직
    private void InitIntroScene()
    {
        // 캐시 지우기를 하면 모든 데이터 초기화
        // 기계를 바꾸게 되면 모든 데이터 초기화
        // PlayerPrefs << 임시 데이터 저장
        // 유니티 엔진이 제공하는 로컬 데이터 저장소

        //PlayerPrefs.SetInt("level", 5);
        //PlayerPrefs.GetInt("level");

        // 닉네임의 글자 수가 2글자 미만이면 데이터가 없다는 취급
        if (PlayerPrefs.GetString(SAVE_Type.SAVE_NickName.ToString()).Length < 2)
        {
            welcomeText.gameObject.SetActive(false);// 환영 메세지 보이지 않게
            inputField.gameObject.SetActive(true);
            haveUserInfo = false;
        }
        else// 유저 데이터가 존재할 때
        {
            welcomeText.gameObject.SetActive(true);
            welcomeText.text = PlayerPrefs.GetString(SAVE_Type.SAVE_NickName.ToString()) + "님 환영합니다.";
            //스트링 빌더를 사용하면 위의 코드를 효율좋게 줄일 수 있음

            inputField.gameObject.SetActive(false);
            haveUserInfo = true;          
        }
    }

    public void GameStartBTN()
    {
        if (!haveUserInfo) // 버튼 눌렀을 때 데이터가 없다면,
        {
            if(inputField.text.Length > 2)
            {
                Debug.Log("계정 생성 " + inputField.text);
                CreateUserData(inputField.text);

                haveUserInfo = true;
            }
        }

        if(haveUserInfo)
        {
            PlayerPrefs.SetString(SAVE_Type.SAVE_SceneName.ToString(), SCENE_NAME.LobbySence.ToString());
            SceneManager.LoadScene(SCENE_NAME.LoadingSence.ToString());
        }
    }

    private void CreateUserData(string userNickName)
    {
        PlayerPrefs.SetString(SAVE_Type.SAVE_NickName.ToString(), userNickName);
        PlayerPrefs.SetInt(SAVE_Type.SAVE_Level.ToString(), 1);
        PlayerPrefs.SetInt(SAVE_Type.SAVE_EXP.ToString(), 0);

        PlayerPrefs.SetInt(SAVE_Type.SAVE_GOLD.ToString(), 20000);

        PlayerPrefs.SetFloat(SAVE_Type.SAVE_SFX.ToString(), 1.0f);
        PlayerPrefs.SetFloat(SAVE_Type.SAVE_BGM.ToString(), 1.0f);

        PlayerPrefs.SetInt(SkillType.ST_PowerUp.ToString(),         0);
        PlayerPrefs.SetInt(SkillType.ST_DoubleShoot.ToString(),     0);
        PlayerPrefs.SetInt(SkillType.ST_BoomCountAdd.ToString(),    0);
        PlayerPrefs.SetInt(SkillType.ST_PowerUp2.ToString(),        0);
        PlayerPrefs.SetInt(SkillType.ST_TripleShoot.ToString(),     0);

    }

    public void DeleteUserInfo()
    {
        PlayerPrefs.DeleteKey(SAVE_Type.SAVE_NickName.ToString());
        InitIntroScene();
    }
}
