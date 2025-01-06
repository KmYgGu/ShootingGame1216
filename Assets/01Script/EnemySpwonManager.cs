using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpwonManager : MonoBehaviour
{
    [SerializeField]private Transform[] spawnTrans;
    [SerializeField] private GameObject[] spawnEnemyPrefabs;
    [SerializeField] private GameObject[] spawnBossPrefabs;

    private float spawnDelta = 1f;
    private int spawnLevel = 0;
    private int spawnCount = 0;
    private GameObject obj; //���� ������Ʈ ������ ����

    private EnemyBoss bossAI;

    // ���� ������ �˸��� ��������Ʈ
    public delegate void SpawnFinish();
    public static event SpawnFinish OnSpawnFinsh;



    public void InitSpawnManager()
    {
        spawnLevel = 0;
        spawnCount = 0;
        spawnDelta = 3f;
        StartCoroutine(SpawnEnemy());
    }
    public void StopSpawnManager()
    {
        StopAllCoroutines();//�ش� ��ü�� ���ؼ� ������ ��� �ڷ�ƾ ����.
    }

    
    IEnumerator SpawnEnemy()
    {
        yield return null;

        while(spawnCount < GameState.WaveCount)
        {
            for(int i = 0; i < spawnTrans.Length; i++)
            {
                obj = Instantiate(spawnEnemyPrefabs[spawnLevel],    // ���� ������
                    spawnTrans[i].position,                         // ���� ��ġ
                    Quaternion.identity);                           // ���� ����

                if(obj.TryGetComponent<Enemy>(out Enemy enemy))
                    enemy.SetEnable(true);
            }

            spawnCount++;
            yield return new WaitForSeconds(spawnDelta);
        }

        // ��ü�������α׷����� ��Ģ(SOLID��Ģ)
        // ����å�ӿ�Ģ : �ϳ��� Ŭ������ �ϳ��� å��(������)�� ���´�


        // �Ϲ� ���� ���� ����
        // �����̸� �ָ鼭 �������� ����ϴ� �޼����� ���
        OnSpawnFinsh?.Invoke();

        yield return new WaitForSeconds(3f); //�Ϲݸ��� ���� �Ϸ��� 3�� ���

        obj = Instantiate(spawnBossPrefabs[spawnLevel], new Vector3(0f, 8f, 0f), Quaternion.identity);

        if(obj.TryGetComponent<EnemyBoss>(out  bossAI))
        {
            IWeaphone[] weapons = new IWeaphone[]
            {new EnemyBossWepon3(), new EnemyBossWepon2(), new EnemyBossWepon1()};

            for (int i = 0;i < weapons.Length;i++)
            {
                weapons[i].SetOwner(obj);
            }

            bossAI.InitBoss("���������� ����", 500, weapons);
            bossAI.OnBossDied += NextLevel;
           
        }

        // ���� ���� ����..
    }

    // ����óġ�� ������ ȣ��(��������Ʈ ���), ���̵� ��½�Ű�� �޼ҵ�
    public void NextLevel()
    {
        bossAI.OnBossDied -= NextLevel;

        //todo : ���� �۾� ���Ŀ�
        spawnLevel++;
        if(spawnLevel >= 3)
        {
            spawnLevel = 0;
        }
        spawnCount = 0;
        StartCoroutine(SpawnEnemy());
    }


}
