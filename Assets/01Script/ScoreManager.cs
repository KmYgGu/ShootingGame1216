using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private void OnEnable()
    {
        //델리게이트의 구독
        Enemy.OnMonsterDied += HandleMonsterDied;
        //HandleMonsterDied 메소드를 통해 구독신청을 함
    }

    private void OnDisable()
    {
        Enemy.OnMonsterDied -= HandleMonsterDied;
        //해제를 해주지 않게 되는 경우, 가비지 컬렉터 생성
    }

    //델리게이트의 콜백 메소드
    private void HandleMonsterDied(Enemy enemyInfo)
    {
        Debug.Log($"{enemyInfo.gameObject.name}몬스터가 사망");
    }
}
