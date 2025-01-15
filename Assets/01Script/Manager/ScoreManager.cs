using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    //C#���� �����ϴ� ���� 
    public delegate void ScoreChange(int score);
    public static event ScoreChange OnChangeScore;
    public static event ScoreChange OnChangeZemCount;
    public static event ScoreChange OnChangeHp;
    public static event ScoreChange OnChangeBomb;
    public static event ScoreChange OnChangePower;

    // ����Ƽ Action 
    public static event Action OnDiedPlayer;


    private int score;
    private int curHp;
    private int maxHp;
    private int zemCount;
    private int bombCount;
    private int powerLevel;

    public int Score => score; //�б��������� �ܺ����� ����
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
        //��������Ʈ�� ����
        Enemy.OnMonsterDied += HandleMonsterDied;
        //HandleMonsterDied �޼ҵ带 ���� ������û�� ��
        Dropitem_Zem.OnPickupJam += HandldZempickup;
        PlayerHitBox.OnPlayerHPIncrease += PlayerHPChange;
    }

    private void OnDisable()
    {
        Enemy.OnMonsterDied -= HandleMonsterDied;
        //������ ������ �ʰ� �Ǵ� ���, ������ �÷��� ����
        Dropitem_Zem.OnPickupJam -= HandldZempickup;
        PlayerHitBox.OnPlayerHPIncrease -= PlayerHPChange;
    }

    //��������Ʈ�� �ݹ� �޼ҵ�
    private void HandleMonsterDied(Enemy enemyInfo)
    {
        Debug.Log($"{enemyInfo.gameObject.name}���Ͱ� ���");
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

        //ui����
    }

    private void DecreaseHp()
    {
        curHp--;
        //ui����
        if (curHp < 1)
        {
            curHp = 0;
            //���ó��
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
            //ui ����
        }
        OnChangeBomb?.Invoke(bombCount);
    }

    public void PowerUp()
    {
        powerLevel++;
        //�÷��̾� ��ȭ
        OnChangePower?.Invoke(powerLevel);
    }
}
