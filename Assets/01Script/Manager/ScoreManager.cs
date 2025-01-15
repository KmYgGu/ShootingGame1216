using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    //C#에서 제공하는 문법 
    public delegate void ScoreChange(int score);
    public static event ScoreChange OnChangeScore;
    public static event ScoreChange OnChangeZemCount;
    public static event ScoreChange OnChangeHp;
    public static event ScoreChange OnChangeBomb;
    public static event ScoreChange OnChangePower;

    // 유니티 Action 
    public static event Action OnDiedPlayer;


    private int score;
    private int curHp;
    private int maxHp;
    private int zemCount;
    private int bombCount;
    private int powerLevel;

    public int Score => score; //읽기전용으로 외부한테 공개
    public int CurHp => curHp;
    public int MaxHp => maxHp;
    public int ZemCount => zemCount;
    public int BombCount => bombCount;


    private int SetScore
    {
        set
        {
            score = value;
            OnChangeScore?.Invoke(score);
        }
    }
    public void InitScoreReset()
    {
        SetScore = 0;
        curHp = maxHp = 5;
        OnChangeHp?.Invoke(curHp);

        powerLevel = 1;
        OnChangePower?.Invoke(powerLevel);

        zemCount = 0;
        OnChangeZemCount?.Invoke(zemCount);

        bombCount = 3;
        //OnChangeBomb?.Invoke(bombCount);

        if (PlayerPrefs.GetInt(SkillType.ST_BoomCountAdd.ToString()) > 0)
            bombCount = 5;
        OnChangeBomb?.Invoke(bombCount);
    }



    private void OnEnable()
    {
        //델리게이트의 구독
        Enemy.OnMonsterDied += HandleMonsterDied;
        //HandleMonsterDied 메소드를 통해 구독신청을 함
        Dropitem_Zem.OnPickupJam += HandldZempickup;
        PlayerHitBox.OnPlayerHPIncrease += PlayerHPChange;
    }

    private void OnDisable()
    {
        Enemy.OnMonsterDied -= HandleMonsterDied;
        //해제를 해주지 않게 되는 경우, 가비지 컬렉터 생성
        Dropitem_Zem.OnPickupJam -= HandldZempickup;
        PlayerHitBox.OnPlayerHPIncrease -= PlayerHPChange;
    }

    //델리게이트의 콜백 메소드
    private void HandleMonsterDied(Enemy enemyInfo)
    {
        Debug.Log($"{enemyInfo.gameObject.name}몬스터가 사망");
    }

    private void HandldZempickup()
    {
        zemCount++;
        OnChangeZemCount?.Invoke(zemCount++);
        SetScore = Score + 7;
    }

    public void PlayerHPChange(bool isincreased)
    {
        if (isincreased)
        {
            IncreaseHP();
        }
        else
        {
            DecreaseHp();
        }

        OnChangeHp?.Invoke(CurHp);
    }

    private void IncreaseHP()
    {
        curHp++;
        if(curHp > maxHp)
            curHp = maxHp;

        //ui갱신
    }

    private void DecreaseHp()
    {
        curHp--;
        //ui갱신
        if (curHp < 1)
        {
            curHp = 0;
            //사망처리
            OnDiedPlayer?.Invoke();
        }
    }

    public void ChangeBombCount(bool isIncrease)
    {
        if (isIncrease)
        {
            bombCount++;
        }
        else
        {
            bombCount--;
            //ui 갱신
        }
        OnChangeBomb?.Invoke(bombCount);
    }

    public void PowerUp()
    {
        powerLevel++;
        //플레이어 강화
        OnChangePower?.Invoke(powerLevel);
    }
}
