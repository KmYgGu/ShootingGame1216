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
        text.text = (newVolum*100.0f).ToString("N2");//�Ҽ��� �� �ڸ����� ǥ���ش޶�
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
        //�������� �ʿ��� ����ġ�� 300���� �����ϱ⶧���� 300�����⸦��

        SFX_ValueChange(PlayerPrefs.GetFloat(SAVE_Type.SAVE_SFX.ToString()));
        BGMValueChange(PlayerPrefs.GetFloat(SAVE_Type.SAVE_BGM.ToString()));

        // ���ü� ��ų ��ư ���� ����
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

    //���� ���� ��ư
    public void GameStart()
    {
        PlayerPrefs.SetString(SAVE_Type.SAVE_SceneName.ToString(), SCENE_NAME.BattleSence.ToString());
        SceneManager.LoadScene(SCENE_NAME.LoadingSence.ToString());
    }






    // �ϴ��� ��ư�� Ŭ������ �� �˾� �����ִ�

    private int activeMenu = 0;
    public int ActiveMenu
    {
        set
        {
            if (value < 1 || value > 5)
            {
                Debug.Log("���� 10010 : activeMenu�� ����");
                activeMenu = 0;// ��� â�� ���� ����
            }
            else
                activeMenu = value;
        }

    }
    [SerializeField] private List<GameObject> popupList;
    private Vector3 popupHidePos = new Vector3(0f, -1500.0f, 0f);
    public void OnClickBtn(int i)
    {
        if (activeMenu == i)//�� �����ִ� �˾� ��ư�� ������ > �˾��ݱ� 
        {
            LeanTween.moveLocalY(popupList[i], popupHidePos.y, 0.5f);
            //������ �ʴ� �ڷ�ƾ�� ����ؼ� lerp(��������)�� ����
            activeMenu = 0;//Ȱ��ȭ �˾��� ����
        }
        else// ����������, Ȥ�� ��ư�� �ٸ� �˾��� �������� �� ���ȴ�
        {// > ���� �˾� �ݰ�, �� �˾� ����
            if(activeMenu != 0)// ������ ��������
            {
                LeanTween.moveLocalY(popupList[activeMenu], popupHidePos.y, 0.5f);
            }

            activeMenu = i;
            LeanTween.moveLocalY(popupList[activeMenu], 0f, 0.5f);
        }
    }
}
