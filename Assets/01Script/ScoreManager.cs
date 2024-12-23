using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
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
}
