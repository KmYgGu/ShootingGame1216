using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;



public enum MenuType
{
    Menu_Enchant = 1,
    Menu_Option = 5,
}
public class LobbySenceManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerLevelText;
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private Image expBar;

    [SerializeField] private AudioMixer audio_Master;

    [SerializeField] private TextMeshProUGUI sfx_Text;
    [SerializeField] private Slider sfx_slider;
    [SerializeField] private TextMeshProUGUI bgm_Text;
    [SerializeField] private Slider bgm_Slider;

    [SerializeField] private List<IncentBtn> enchantBTNs;

    private float valueF;

    public void SFX_ValueChange(float value)
    {
        PlayerPrefs.SetFloat(SAVE_Type.SAVE_SFX.ToString(), value);
        ChangeValum(sfx_Text, sfx_slider, SAVE_Type.SAVE_SFX, value);
    }
    public void BGMValueChange(float value)
    {
        PlayerPrefs.SetFloat(SAVE_Type.SAVE_BGM.ToString(), value);
        ChangeValum(bgm_Text, bgm_Slider, SAVE_Type.SAVE_BGM, value);
    }

    void ChangeValum(TextMeshProUGUI text, Slider slider, SAVE_Type type, float newVolum)
    {
        text.text = (newVolum*100.0f).ToString("N2");//소수점 두 자리까지 표현해달라
        slider.value = newVolum;
        valueF = newVolum * 30.0f - 30.0f;

        audio_Master.SetFloat(type.ToString(), valueF);
    }

    private void Awake()
    {
        InitLobbyScene();
    }

    private void InitLobbyScene()
    {
        playerLevelText.text = PlayerPrefs.GetInt(SAVE_Type.SAVE_Level.ToString()).ToString();
        goldText.text = PlayerPrefs.GetInt(SAVE_Type.SAVE_GOLD.ToString()).ToString();
        expBar.fillAmount = PlayerPrefs.GetInt(SAVE_Type.SAVE_EXP.ToString()) / 300.0f;
        //레벨업에 필요한 경험치를 300으로 고정하기때문에 300나누기를함

        SFX_ValueChange(PlayerPrefs.GetFloat(SAVE_Type.SAVE_SFX.ToString()));
        BGMValueChange(PlayerPrefs.GetFloat(SAVE_Type.SAVE_BGM.ToString()));

        // 제련소 스킬 버튼 정보 갱신
        for(int i = 0; i < 5;  i++)
        {
            SkillType type = (SkillType)i;
            if(0 < PlayerPrefs.GetInt(type.ToString()))
            {
                enchantBTNs[i].InitBtn(true, 99);
            }
            else
            {
                enchantBTNs[i].InitBtn(false, 99);
            }
        }
    }

    //게임 시작 버튼
    public void GameStart()
    {
        PlayerPrefs.SetString(SAVE_Type.SAVE_SceneName.ToString(), SCENE_NAME.BattleSence.ToString());
        SceneManager.LoadScene(SCENE_NAME.LoadingSence.ToString());
    }






    // 하단의 버튼을 클릭했을 때 팝업 열어주는

    private int activeMenu = 0;
    public int ActiveMenu
    {
        set
        {
            if (value < 1 || value > 5)
            {
                Debug.Log("에러 10010 : activeMenu값 오류");
                activeMenu = 0;// 모든 창이 닫힌 상태
            }
            else
                activeMenu = value;
        }

    }
    [SerializeField] private List<GameObject> popupList;
    private Vector3 popupHidePos = new Vector3(0f, -1500.0f, 0f);
    public void OnClickBtn(int i)
    {
        if (activeMenu == i)//현 열러있는 팝업 버튼이 눌렀다 > 팝업닫기 
        {
            LeanTween.moveLocalY(popupList[i], popupHidePos.y, 0.5f);
            //보이지 않는 코루틴을 사용해서 lerp(선형보간)을 해줌
            activeMenu = 0;//활성화 팝업은 없다
        }
        else// 닫혀있을때, 혹은 버튼과 다른 팝업이 열려있을 때 눌렸다
        {// > 기존 팝업 닫고, 새 팝업 열기
            if(activeMenu != 0)// 누군가 열린상태
            {
                LeanTween.moveLocalY(popupList[activeMenu], popupHidePos.y, 0.5f);
            }

            activeMenu = i;
            LeanTween.moveLocalY(popupList[activeMenu], 0f, 0.5f);
        }
    }
}
