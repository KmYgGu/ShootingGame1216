using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    private Transform canvasTransform;//총괄 canvas trans
    private TextMeshProUGUI scoreText;//점수 Text
    private TextMeshProUGUI zemText;
    private TextMeshProUGUI bombText;
    [SerializeField] private Image[] heartImg;

    [SerializeField] private GameObject popupObj;

    private GameObject obj;
    private Transform tr;

    private TextMeshProUGUI ScoreText
    {
        get
        {
            if(scoreText == null)
            {
                tr = MyUtility.FindChildRescursive(canvasTransform, "Score_Text");
                if (tr != null) 
                { 
                    scoreText = tr.GetComponent<TextMeshProUGUI>();
                }
                else
                {
                    Debug.Log("UIManger.cs  - ScoreText - 참조실패");
                    return null;
                }

            }
            return scoreText;
        }
    }

    TextMeshProUGUI ZemText
    {
        get
        {
            if (zemText == null)
            {
                tr = MyUtility.FindChildRescursive(canvasTransform, "Zem_Text");
                if (tr != null)
                {
                    zemText = tr.GetComponent<TextMeshProUGUI>();
                }
                else
                {
                    Debug.Log("UIManger.cs  - zemText - 참조실패");
                    return null;
                }

            }
            return zemText;
        }
    }

    TextMeshProUGUI BombText
    {
        get
        {// 다이나믹 프로그래밍 DP : 캐싱해서 사용한다
            if (bombText == null)
            {
                tr = MyUtility.FindChildRescursive(canvasTransform, "Boom_Text");
                if (tr != null)
                {
                    bombText = tr.GetComponent<TextMeshProUGUI>();
                }
                else
                {
                    Debug.Log("UIManger.cs  - bombText - 참조실패");
                    return null;
                }

            }
            return bombText;
        }
    }

    private void Awake()
    {
        obj = GameObject.Find("Canvas");
        canvasTransform = obj.GetComponent<Transform>();
    }

    private void OnEnable()
    {
        ScoreManager.OnChangeScore += UpdateScoreText;
        ScoreManager.OnChangeBomb += UpdateBombText;
        ScoreManager.OnChangeHp += UpdatePlayerHP;
        ScoreManager.OnChangeZemCount += UpdateZemText;
        ScoreManager.OnDiedPlayer += OpenDiePopup;
    }
    private void OnDisable()
    {
        ScoreManager.OnChangeScore -= UpdateScoreText;
        ScoreManager.OnChangeBomb -= UpdateBombText;
        ScoreManager.OnChangeHp -= UpdatePlayerHP;
        ScoreManager.OnChangeZemCount -= UpdateZemText;
        ScoreManager.OnDiedPlayer -= OpenDiePopup;
    }

    private void OpenDiePopup()
    {
        popupObj.SetActive(true);
    }

    private void UpdatePlayerHP(int score)
    {
        for(int i = 0; i < 5; i++)
        {
            if(i < score)
            {
                heartImg[i].enabled = true;
            }
            else
                heartImg[i].enabled = false;
        }
    }

    private void UpdateZemText(int score)
    {
        ZemText.text = score.ToString();
    }

    private void UpdateBombText(int score)
    {
        BombText.text = "X : " + score.ToString();
    }

    private void UpdateScoreText(int score)
    {
        ScoreText.text = score.ToString();
    }

    public void LoadLobbyScene()
    {
        GameObject obj = GameObject.Find("ScoreManager");
        if(obj.TryGetComponent<ScoreManager>(out ScoreManager score))
        {
            int newGold = (int)(score.Score * 0.5f);
            newGold += PlayerPrefs.GetInt(SAVE_Type.SAVE_GOLD.ToString());
            PlayerPrefs.SetInt(SAVE_Type.SAVE_GOLD.ToString(), newGold);
        }
            


        PlayerPrefs.SetString(SAVE_Type.SAVE_SceneName.ToString(), SCENE_NAME.LobbySence.ToString());
        SceneManager.LoadScene(SCENE_NAME.LoadingSence.ToString());
    }
}
