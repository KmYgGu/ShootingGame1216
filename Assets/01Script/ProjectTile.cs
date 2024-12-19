using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//지정된 방향으로 지정된 속도, 지속적으로 이동하는 객체
//자신을 발사 시켜준 주체(Owner)와 다른 팀의 대상과 부딪혔을때, 상대방에게 데미지를 전달 하는 역할

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
    private string ownerTag;//피아 식별을 위한 태그

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


    // 투사체가 정상 작동하기 위해서 초기화 
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

    //해당 오브젝트가 비활성화가 될때 호출되는 이벤트
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
