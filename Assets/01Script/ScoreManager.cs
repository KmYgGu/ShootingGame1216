using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

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
        }
    }
    public void InitScoreReset()
    {
        SetScore = 0;

        curHp = maxHp = 5;

        powerLevel = 0;
        zemCount = 0;
        bombCount = 3;
    }



    private void OnEnable()
    {
        //��������Ʈ�� ����
        Enemy.OnMonsterDied += HandleMonsterDied;
        //HandleMonsterDied �޼ҵ带 ���� ������û�� ��
    }

    private void OnDisable()
    {
        Enemy.OnMonsterDied -= HandleMonsterDied;
        //������ ������ �ʰ� �Ǵ� ���, ������ �÷��� ����
    }

    //��������Ʈ�� �ݹ� �޼ҵ�
    private void HandleMonsterDied(Enemy enemyInfo)
    {
        Debug.Log($"{enemyInfo.gameObject.name}���Ͱ� ���");
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
    }

    public void PowerUp()
    {
        powerLevel++;
        //�÷��̾� ��ȭ
    }
}
