using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//�������� Ÿ���� ���⸦ �����,
//�÷��̾� Ȥ�� ���Ͱ� Ȱ���ϱ� ���ؼ�,
//interface
public class PlayerWeapon : MonoBehaviour, IWeaphone
{
    [SerializeField]    private GameObject projectilePrefab; //���߿� ������Ʈ Ǯ�� �����ϸ鼭, ������ ����
    [SerializeField]    private Transform firePoint;
    [SerializeField]    private GameObject luncherBoombPrefab;

    
    private int numOfProjectTiles = 5; //����ü �߻�Ǵ� ����
    private float spreadAngle = 5f;//����ü �߻� ���� ����
    private float fireRate = 0.3f;//����ü �߻� ���� ����
    private float nextFireTime = 0f;
    private bool isFireing = false; //���Ⱑ �Ѿ��� �߻��ϰ� �ִ� ������.

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
        if (Time.time < nextFireTime)//�� �������̶� ���������� �ٷ� �۵�
            return;

        if(isFireing)
        {
            SoundManager.instance.PlaySFX(SFX_TYPE.SFX_Fire);

            nextFireTime = Time.time + fireRate;

            startAngle = -spreadAngle * (numOfProjectTiles -1)/2;

            for(int i = 0; i < numOfProjectTiles; i++)
            {
                //�ش� ������ ������Ÿ�� �߻� ���� ���
                angle = startAngle + spreadAngle * i;

                fireRotation = firePoint.rotation * Quaternion.Euler(0f, 0f, angle);
                fireDir = fireRotation * Vector2.up;

                //������Ʈ Ǯ�� �����ϰ�.
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
