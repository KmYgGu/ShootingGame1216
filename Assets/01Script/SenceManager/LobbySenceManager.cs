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
    }

    //게임 시작 버튼
    public void GameStart()
    {
        PlayerPrefs.SetString(SAVE_Type.SAVE_SceneName.ToString(), SCENE_NAME.BattleSence.ToString());
        SceneManager.LoadScene(SCENE_NAME.LoadingSence.ToString());
    }
}
