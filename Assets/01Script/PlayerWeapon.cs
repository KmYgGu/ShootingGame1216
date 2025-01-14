using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//여러가지 타입의 무기를 만들고,
//플레이어 혹은 몬스터가 활용하기 위해서,
//interface
public class PlayerWeapon : MonoBehaviour, IWeaphone
{
    [SerializeField]    private GameObject projectilePrefab; //나중에 오브젝트 풀링 구현하면서, 수정할 예정
    [SerializeField]    private Transform firePoint;
    [SerializeField]    private GameObject luncherBoombPrefab;

    
    private int numOfProjectTiles = 5; //투사체 발사되는 갯수
    private float spreadAngle = 5f;//투사체 발사 각도 간격
    private float fireRate = 0.3f;//투사체 발사 사이 간격
    private float nextFireTime = 0f;
    private bool isFireing = false; //무기가 총알을 발사하고 있는 중인지.

    private float startAngle;
    private float angle;
    private Quaternion fireRotation;
    private Vector2 fireDir;
    private GameObject obj;

    public void InitWeapon(GameObject projectileType, float rate)
    {
        projectilePrefab = projectileType;
        fireRate = rate;
    }

    public void Fire()
    {
        if (Time.time < nextFireTime)//한 프레임이라도 증가했으면 바로 작동
            return;

        if(isFireing)
        {
            SoundManager.instance.PlaySFX(SFX_TYPE.SFX_Fire);

            nextFireTime = Time.time + fireRate;

            startAngle = -spreadAngle * (numOfProjectTiles -1)/2;

            for(int i = 0; i < numOfProjectTiles; i++)
            {
                //해당 순번의 프로젝타일 발사 각도 계산
                angle = startAngle + spreadAngle * i;

                fireRotation = firePoint.rotation * Quaternion.Euler(0f, 0f, angle);
                fireDir = fireRotation * Vector2.up;

                //오브젝트 풀링 구현하고.
                ProjectTileManager.instance.FireProjectile(ProjecttileType.Player01, firePoint.position,
                    fireDir, gameObject, 1, 10.0f);

            }
        }
    }

    public void LunchBomb()
    {
        if(GameManager.Instance.GetScoreManager.BombCount > 0)
        {
            GameManager.Instance.GetScoreManager.ChangeBombCount(false);

            obj = Instantiate(luncherBoombPrefab, transform.position, Quaternion.identity);
        }
    }

    public void SetEnable(bool enable)
    {
        isFireing = enable;
    }

    public void SetOwner(GameObject newOwner)
    {

    }

}
