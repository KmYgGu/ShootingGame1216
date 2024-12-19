using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//������ �������� ������ �ӵ�, ���������� �̵��ϴ� ��ü
//�ڽ��� �߻� ������ ��ü(Owner)�� �ٸ� ���� ���� �ε�������, ���濡�� �������� ���� �ϴ� ����

public enum ProjecttileType
{
    Player01,
    Player02,
    Player03,
    Boss1,
    Boss2,
    Boss3
}
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class ProjectTile : MonoBehaviour, IMovement
{
    [SerializeField] private float moveSpeed = 10f;
    private int damage;
    private Vector2 moveDir;
    private GameObject owner;
    private string ownerTag;//�Ǿ� �ĺ��� ���� �±�

    private bool isInit = false;
    private ProjecttileType type;


    private void Awake()
    {
        if(TryGetComponent<Rigidbody2D>(out Rigidbody2D rig))
        {
            rig.gravityScale = 0;
        }
        if(TryGetComponent<CircleCollider2D>(out CircleCollider2D col))
        {
            col.isTrigger = true;
            col.radius = 0.1f;
        }
    }


    // ����ü�� ���� �۵��ϱ� ���ؼ� �ʱ�ȭ 
    public void InitProjecttile(ProjecttileType type, Vector2 newDir, GameObject newOwner,
        int newDamage, float newSpeed)
    {
        this.type = type;
        moveDir = newDir;
        moveSpeed = newSpeed;
        damage = newDamage;
        owner = newOwner;
        ownerTag = owner.tag;

        SetEnable(true);
    }

    //�ش� ������Ʈ�� ��Ȱ��ȭ�� �ɶ� ȣ��Ǵ� �̺�Ʈ
    private void OnDisable()
    {
        SetEnable(false);
    }

    private void Update()
    {
        if (isInit)
            Move(moveDir);
    }


    public void Move(Vector2 newDirection)
    {
        transform.Translate(newDirection * (moveSpeed * Time.deltaTime));
    }

    public void SetEnable(bool newEnable)
    {
        isInit = newEnable;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == owner)
            return;
        if(collision.CompareTag(ownerTag))
            return;

        if (collision.CompareTag("DestoryArea"))
        {
            ProjectTileManager.instance.ReturnProjectileToPool(this, type);
            return;
        }

        if(collision.TryGetComponent<IDamaged>(out IDamaged damaged))
        {
            damaged.TakeDamage(owner, damage);
            ProjectTileManager.instance.ReturnProjectileToPool(this, type);
        }
    }
}
