using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpwonManager : MonoBehaviour
{
    [SerializeField]private Transform[] spawnTrans;
    [SerializeField] private GameObject[] spawnEnemyPrefabs;
    [SerializeField] private GameObject[] sapwnBossPrefabs;

    private float spawnDelta = 1f;
    private int spawnLevel = 0;
    private int spawnCount = 0;
    private GameObject obj; //생성 오브젝트 참조용 변수

    public void InitSpawnManager()
    {
        spawnLevel = 0;
        spawnCount = 0;
        spawnDelta = 3f;
        StartCoroutine(SpawnEnemy());
    }
    public void StopSpawnManager()
    {
        StopAllCoroutines();//해당 객체를 통해서 생성한 모든 코루틴 정지.
    }

    // 보스처치할 때마다 호출(델리게이트 방식), 난이도 상승시키는 메소드
    public void NextLevel()
    {
        //todo : 보스 작업 이후에
        StartCoroutine(SpawnEnemy());
    }
    IEnumerator SpawnEnemy()
    {
        yield return null;

        while(spawnCount <10)
        {
            for(int i = 0; i < spawnTrans.Length; i++)
            {
                obj = Instantiate(spawnEnemyPrefabs[spawnLevel],    // 스폰 프리펩
                    spawnTrans[i].position,                         // 스폰 위치
                    Quaternion.identity);                           // 스폰 각도

                if(obj.TryGetComponent<Enemy>(out Enemy enemy))
                    enemy.SetEnable(true);
            }

            spawnCount++;
            yield return new WaitForSeconds(spawnDelta);
        }
    }

    
}
