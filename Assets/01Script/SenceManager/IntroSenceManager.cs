using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public enum SAVE_Type
{
    SAVE_NickName,  // �������� �г���
    SAVE_SceneName, // �ε��� ����� ��
    SAVE_SFX,   // ���� ȿ����
    SAVE_BGM,   // �����
    SAVE_Level, // �÷��̾� ����
    SAVE_EXP,   // �÷��̾� ����ġ
    SAVE_GOLD,  // ���
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

    // ���� �����Ͱ� �ִ��� �˻�
    // ������ ȯ�� �޼���
    // ������ ������������
    private void InitIntroScene()
    {
        // ĳ�� ����⸦ �ϸ� ��� ������ �ʱ�ȭ
        // ��踦 �ٲٰ� �Ǹ� ��� ������ �ʱ�ȭ
        // PlayerPrefs << �ӽ� ������ ����
        // ����Ƽ ������ �����ϴ� ���� ������ �����

        //PlayerPrefs.SetInt("level", 5);
        //PlayerPrefs.GetInt("level");

        // �г����� ���� ���� 2���� �̸��̸� �����Ͱ� ���ٴ� ���
        if (PlayerPrefs.GetString(SAVE_Type.SAVE_NickName.ToString()).Length < 2)
        {
            welcomeText.gameObject.SetActive(false);// ȯ�� �޼��� ������ �ʰ�
            inputField.gameObject.SetActive(true);
            haveUserInfo = false;
        }
        else// ���� �����Ͱ� ������ ��
        {
            welcomeText.gameObject.SetActive(true);
            welcomeText.text = PlayerPrefs.GetString(SAVE_Type.SAVE_NickName.ToString()) + "�� ȯ���մϴ�.";
            //��Ʈ�� ������ ����ϸ� ���� �ڵ带 ȿ������ ���� �� ����

            inputField.gameObject.SetActive(false);
            haveUserInfo = true;          
        }
    }

    public void GameStartBTN()
    {
        if (!haveUserInfo) // ��ư ������ �� �����Ͱ� ���ٸ�,
        {
            if(inputField.text.Length > 2)
            {
                Debug.Log("���� ���� " + inputField.text);
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
