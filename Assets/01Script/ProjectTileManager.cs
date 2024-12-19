using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//������ Ÿ���� ��û���� ��, 
public class ProjectTileManager : SingletonDestroy<ProjectTileManager>
{
    [SerializeField] private GameObject[] projecttilePrefabs;
    private Queue<ProjectTile>[] projecttilesQueue;

    private int poolSize = 10;  // ���� ����
    private GameObject obj;     // ������ ����
    private ProjectTile proj;


    protected override void Awake()
    {
        base.Awake();
        projecttilesQueue = new Queue<ProjectTile>[projecttilePrefabs.Length];

        for(int i = 0; i < projecttilePrefabs.Length; i++)
        {
            projecttilesQueue[i] = new Queue<ProjectTile>();
            //�ش� ť�� ���������� ������Ʈ ����
            AlloCate((ProjecttileType)i);//int���� enum�� ���� ����ȯ�� ������
        }
    }

    private void AlloCate(ProjecttileType type)
    {
        for(int i = 0;i < poolSize; i++)
        {
            obj = Instantiate(projecttilePrefabs[(int)type]);
            if(obj.TryGetComponent<ProjectTile>(out proj))
            {
                projecttilesQueue[(int)type].Enqueue(proj);//�ش� ť�� ��ũ��Ʈ ��ü�߰�
            }
            obj.SetActive(false);
        }
    }

    //Ǯ���� ������Ʈ �����ö�
    private ProjectTile GetProjectTileFromPool(ProjecttileType type)
    {
        if (projecttilesQueue[(int)type].Count < 1)
        {
            AlloCate(type);

        }
        return projecttilesQueue[(int)type].Dequeue();
    }

    
    //����ϴٰ�, ���̻� �ʿ����������, Ǯ�� ��ȯ�ϱ� ���� ȣ���ϱ� ���� �żҵ�
    public void ReturnProjectileToPool(ProjectTile returnProj, ProjecttileType type)
    {
        returnProj.gameObject.SetActive(false);
        projecttilesQueue[((int)type)].Enqueue(returnProj);
    }


    //�ܺο��� ��û�� �ö�, ������Ÿ�� �����ؼ� �߻�.
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
