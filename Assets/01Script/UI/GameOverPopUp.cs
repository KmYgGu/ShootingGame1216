using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPopUp : MonoBehaviour
{
    [SerializeField] GameObject title;
    [SerializeField] GameObject popUpObj;
    [SerializeField] GameObject lobbyBTN;
    [SerializeField] GameObject killCounter;
    [SerializeField] GameObject bossCounter;

    [SerializeField] GameObject star01;
    [SerializeField] GameObject star02;
    [SerializeField] GameObject star03;

    private void Awake()
    {
        ScoreManager.OnDiedPlayer += StartPopup;
    }


    //LearnTween : ������ �����ϴ� �۾�
    public void StartPopup()
    {
        ScoreManager.OnDiedPlayer -= StartPopup;
        LeanTween.scale(title, new Vector3(1.5f, 1.5f, 1.5f), 2f)//2�ʿ����ļ� 1.5f��ŭ Ŀ����
            .setEase(LeanTweenType.easeOutElastic)
            .setOnComplete(LevelComplete);// �ش� ������ ��������, ���� ����Ǵ� �۾��� ����
        LeanTween.moveLocal(title, new Vector3(0.0f, 450.0f, 0f), 0.5f)
            .setDelay(1.5f).setEase(LeanTweenType.easeInOutCubic);
    }


    void LevelComplete()
    {
        LeanTween.moveLocal(popUpObj, Vector3.zero, 0.7f)
            .setDelay(0.5f).setEase(LeanTweenType.easeInCirc)
            .setOnComplete(StarOn);

        LeanTween.scale(lobbyBTN, Vector3.one, 2f)
            .setDelay(1.5f).setEase(LeanTweenType.easeInOutElastic);

        LeanTween.scale(killCounter, Vector3.one, 2f)
            .setDelay(1.2f).setEase(LeanTweenType.easeInOutElastic);

        LeanTween.scale(bossCounter, Vector3.one, 2f)
            .setDelay(1.4f).setEase(LeanTweenType.easeInOutElastic);
    }

    void StarOn()
    {
        LeanTween.scale(star02, Vector3.one, 2f)
            .setDelay(0.1f).setEase(LeanTweenType.easeInOutElastic);
        LeanTween.scale(star03, Vector3.one, 2f)
            .setDelay(0.2f).setEase(LeanTweenType.easeInOutElastic);
        LeanTween.scale(star01, Vector3.one*1.5f, 2f)
            .setDelay(0.3f).setEase(LeanTweenType.easeInOutElastic);
    }




}
