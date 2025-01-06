using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeaponBase
{
    protected GameObject owner;
}

//부모의 포인터로 자식의 객체를 가르킬수 있다
public class EnemyBossWepon1 : BossWeaponBase, IWeaphone
{
    private Vector3 firePos;
    private int numOfProj = 5;
    float spreadAngle = 15f;

    private float angle;
    private Vector2 fireDir;

    public void Fire()
    {
        firePos = owner.transform.position;

        for(int i = 0; i < numOfProj; i++)
        {
            angle = spreadAngle * (i - (numOfProj - 1) /2f);
            fireDir = Quaternion.Euler(0f, 0f,angle) * Vector2.down;

            ProjectTileManager.instance.FireProjectile(ProjecttileType.Boss1, firePos, fireDir, owner, 1, 6f);
        }
    }

    public void LunchBomb()
    {
        
    }

    public void SetEnable(bool enable)
    {
        
    }

    public void SetOwner(GameObject newOwner)
    {
        owner = newOwner;
    }
}
public class EnemyBossWepon2 : BossWeaponBase, IWeaphone
{

    private Vector3 firePos;
    private int numOfProj = 36;
    private float angleDelta;
    private float startAngle; //매번 같은 방향으로 발사되는 걸 방지
    private int count = 0;

    private float spawnAngle;
    private Vector2 fireDir;

public void Fire()
    {
        firePos = owner.transform.position;
        angleDelta = 360 / numOfProj;

        startAngle = count++ * 1.1f;

        for(int i = 0;i < numOfProj; i++)
        {
            spawnAngle = i * angleDelta + startAngle;
            fireDir = Quaternion.Euler(0f,0f, spawnAngle) * Vector2.down;
            ProjectTileManager.instance.FireProjectile(ProjecttileType.Boss2, firePos, fireDir, owner, 1, 4f);
        }

    }

    public void LunchBomb()
    {
        
    }

    public void SetEnable(bool enable)
    {
        
    }

    public void SetOwner(GameObject newOwner)
    {
        owner = newOwner;
    }
}
public class EnemyBossWepon3 : BossWeaponBase, IWeaphone
{
    private Vector3 firePos;
    private int numOfProj = 48;
    //private float angleDelta;
    //private float startAngle; //매번 같은 방향으로 발사되는 걸 방지
    private int count = 0;

    private float spawnAngle;
    private Vector2 fireDir;


    public void Fire()
    {
        firePos = owner.transform.position;

        for (int i = 0; i < numOfProj; i++)
        {
            fireDir = Quaternion.Euler(i * 5, i * 5, 0f) * Vector2.down;
            ProjectTileManager.instance.FireProjectile(ProjecttileType.Boss3, firePos, fireDir, owner, 1, Mathf.Clamp(10 - i, 3, 8));
            fireDir = Quaternion.Euler(i * 5, i * 5, 180f) * Vector2.down;
            ProjectTileManager.instance.FireProjectile(ProjecttileType.Boss3,
                firePos, fireDir, owner, 1, Mathf.Clamp(10-i, 3, 8));
        }
    }

    public void LunchBomb()
    {
        
    }

    public void SetEnable(bool enable)
    {
        
    }

    public void SetOwner(GameObject newOwner)
    {
        owner = newOwner;
    }
}
