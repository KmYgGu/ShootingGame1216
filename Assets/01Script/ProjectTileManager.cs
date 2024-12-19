using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//프로젝 타일을 요청했을 때, 
public class ProjectTileManager : SingletonDestroy<ProjectTileManager>
{
    [SerializeField] private GameObject[] projecttilePrefabs;
    private Queue<ProjectTile>[] projecttilesQueue;

    private int poolSize = 10;  // 값형 변수
    private GameObject obj;     // 참조형 변수
    private ProjectTile proj;


    protected override void Awake()
    {
        base.Awake();
        projecttilesQueue = new Queue<ProjectTile>[projecttilePrefabs.Length];

        for(int i = 0; i < projecttilePrefabs.Length; i++)
        {
            projecttilesQueue[i] = new Queue<ProjectTile>();
            //해당 큐에 일정갯수의 오브젝트 생성
            AlloCate((ProjecttileType)i);//int값과 enum은 서로 형변환이 가능함
        }
    }

    private void AlloCate(ProjecttileType type)
    {
        for(int i = 0;i < poolSize; i++)
        {
            obj = Instantiate(projecttilePrefabs[(int)type]);
            if(obj.TryGetComponent<ProjectTile>(out proj))
            {
                projecttilesQueue[(int)type].Enqueue(proj);//해당 큐에 스크립트 객체추가
            }
            obj.SetActive(false);
        }
    }

    //풀에서 오브젝트 꺼내올때
    private ProjectTile GetProjectTileFromPool(ProjecttileType type)
    {
        if (projecttilesQueue[(int)type].Count < 1)
        {
            AlloCate(type);

        }
        return projecttilesQueue[(int)type].Dequeue();
    }

    
    //사용하다가, 더이상 필요없어졌을때, 풀에 반환하기 위해 호출하기 위한 매소드
    public void ReturnProjectileToPool(ProjectTile returnProj, ProjecttileType type)
    {
        returnProj.gameObject.SetActive(false);
        projecttilesQueue[((int)type)].Enqueue(returnProj);
    }


    //외부에서 요청이 올때, 프로젝타일 생성해서 발사.
    public void FireProjectile(ProjecttileType type, Vector3 spawnPos, Vector2 newDir,
        GameObject newOwner, int damage, float newSpeed)
    {
        proj = GetProjectTileFromPool(type);

        if(proj != null)
        {
            proj.transform.position = spawnPos;
            proj.gameObject.SetActive(true);
            proj.InitProjecttile(type,newDir, newOwner, damage, newSpeed);
        }
    }
}
