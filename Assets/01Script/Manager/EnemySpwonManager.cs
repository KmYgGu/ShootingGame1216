using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpwonManager : MonoBehaviour
{
    [SerializeField] private ShootingGame gameDatas;
    private Dictionary<int, MonsterTable_Entity> monsterDatas = new Dictionary<int, MonsterTable_Entity>();

    [SerializeField] private Transform[] spawnTrans;
    [SerializeField] private GameObject[] spawnEnemyPrefabs;
    [SerializeField] private GameObject[] spawnBossPrefabs;

    private float spawnDelta = 1f;
    private int spawnLevel = 0;
    private int spawnCount = 0;
    private GameObject obj; //생성 오브젝트 참조용 변수

    private EnemyBoss bossAI;

    // 보스 등장을 알리는 델리게이트
    public delegate void SpawnFinish();
    public static event SpawnFinish OnSpawnFinsh;

    private void Awake()
    {
        for (int i = 0; i < gameDatas.MonsterTable.Count; i++)//몬스터 테이블에 있는 값만큼 반복
        {
            monsterDatas.Add(gameDatas.MonsterTable[i].MonsterID,//key값은 monsterID
                gameDatas.MonsterTable[i]);               
        }
        //if (monsterDatas.TryGetValue(10031, out MonsterTable_Entity data))
        //{
        //    Debug.Log("테이블 읽기 성공 " + data.MonsterName);
        //}
        //else
        //    Debug.Log("테이블 읽기 실패");
    }

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

    
    IEnumerator SpawnEnemy()
    {
        yield return null;

        while(spawnCount < GameState.WaveCount)
        {
            for(int i = 0; i < spawnTrans.Length; i++)
            {
                obj = Instantiate(spawnEnemyPrefabs[spawnLevel],    // 스폰 프리펩
                    spawnTrans[i].position,                         // 스폰 위치
                    Quaternion.identity);                           // 스폰 각도

                if(obj.TryGetComponent<Enemy>(out Enemy enemy))
                {
                    int tableID = 10000 + 10 * (spawnLevel/3 +1)
                        + 1 * (spawnLevel %3 + 1);
                    //0 10011
                    //1 10012
                    //2 10013
                    //3 10021

                    if(monsterDatas.TryGetValue(tableID, out MonsterTable_Entity data))
                    {
                        enemy.InitMonsterData(data);
                        Debug.Log("몬스터 생성 HP : " + data.MonsterHP);
                    } 
                    enemy.SetEnable(true);
                    
                }
                    
            }

            spawnCount++;
            yield return new WaitForSeconds(spawnDelta);
        }

        // 객체지향프로그래밍의 원칙(SOLID원칙)
        // 단일책임원칙 : 하나의 클래스는 하나의 책임(목적성)만 갖는다


        // 일반 몬스터 스폰 종료
        // 딜레이를 주면서 유저에게 경고하는 메세지를 띄워
        OnSpawnFinsh?.Invoke();

        yield return new WaitForSeconds(3f); //일반몬스터 스폰 완료후 3초 대기

        obj = Instantiate(spawnBossPrefabs[spawnLevel], new Vector3(0f, 8f, 0f), Quaternion.identity);

        if(obj.TryGetComponent<EnemyBoss>(out  bossAI))
        {
            IWeaphone[] weapons = new IWeaphone[]
            {new EnemyBossWepon1(), new EnemyBossWepon3(), new EnemyBossWepon2()};

            for (int i = 0;i < weapons.Length;i++)
            {
                weapons[i].SetOwner(obj);
            }

            bossAI.InitBoss("무지막지한 보스", 500, weapons);
            bossAI.OnBossDied += NextLevel;
           
        }

        // 보스 몬스터 등장..
    }

    // 보스처치할 때마다 호출(델리게이트 방식), 난이도 상승시키는 메소드
    public void NextLevel()
    {
        bossAI.OnBossDied -= NextLevel;

        //todo : 보스 작업 이후에
        spawnLevel++;
        if(spawnLevel >= 3)
        {
            spawnLevel = 0;
        }
        spawnCount = 0;
        StartCoroutine(SpawnEnemy());
    }


}
