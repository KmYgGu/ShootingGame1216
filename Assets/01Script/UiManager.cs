using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiManager : MonoBehaviour
{
    private Transform canvasTransform;//�Ѱ� canvas trans
    private TextMeshProUGUI scoreText;//���� Text
    private TextMeshProUGUI zemText;
    private TextMeshProUGUI bombText;
    [SerializeField] private Image[] heartImg;

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
                    Debug.Log("UIManger.cs  - ScoreText - ��������");
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
                    Debug.Log("UIManger.cs  - zemText - ��������");
                    return null;
                }

            }
            return zemText;
        }
    }

    TextMeshProUGUI BombText
    {
        get
        {// ���̳��� ���α׷��� DP : ĳ���ؼ� ����Ѵ�
            if (bombText == null)
            {
                tr = MyUtility.FindChildRescursive(canvasTransform, "Boom_Text");
                if (tr != null)
                {
                    bombText = tr.GetComponent<TextMeshProUGUI>();
                }
                else
                {
                    Debug.Log("UIManger.cs  - bombText - ��������");
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
}
