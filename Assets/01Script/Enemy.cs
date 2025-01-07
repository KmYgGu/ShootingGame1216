using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ��ġ�� �÷��̾� ����
// �̵� ���
// ������ �޴� ���
// ������� ��, �÷��̾� ������ ��� ( ��������Ʈ/�ݹ� )

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour, IMovement, IDamaged
{
    private Vector2 moveDir;
    private float moveSpeed = 3;
    private bool isInit;

    private int curHp = 10;
    private int maxHp = 10;

    public bool IsDead { get => curHp <= 0; }//���ٽ�
    //public bool IsDead { =>�� {return}������ ��
    //    get
    //    {
    //        return curHp <= 0;
    //    }
    //}

    //��������Ʈ
    public delegate void MonsterDiedEvent(Enemy enemyInfo);//��������Ʈ Ÿ�� ����

    public static event MonsterDiedEvent OnMonsterDied;//��������Ʈ ��ü(ü��) ����
    // event : public ����� ��������Ʈ ü�ο� �����Ͽ� ������ �� ������, ������ ���� X

    private void Awake()
    {
        if(TryGetComponent<CircleCollider2D>(out CircleCollider2D col))
        {
            col.isTrigger = true;
            col.radius = 0.25f;
        }

        if (TryGetComponent<Rigidbody2D>(out Rigidbody2D rig))
        {
            rig.gravityScale = 0f;
        }
    }
    void Update()
    {
        if (isInit)
        {
            Move(Vector2.down);
        }
    }

    public void InitMonsterData(MonsterTable_Entity data)
    {
        maxHp = data.MonsterHP;
    }

    public void Move(Vector2 newDirection)
    {
        transform.Translate(newDirection * (moveSpeed * Time.deltaTime));
    }

    public void SetEnable(bool newEnable)
    {
        isInit = newEnable;
    }

    public void TakeDamage(GameObject attacker, int damage)
    {
        
        if (!IsDead)
        {
            curHp -= damage;
            if(curHp < 1)
            {
                OnDie();
            }
            else
            {
                OnDamaged();
            }
        }
        
    }

    private void OnDamaged()
    {
        //Debug.Log("���� �ǰ�");
    }

    private void OnDie()
    {
        //Debug.Log("���� ���");
        OnMonsterDied?.Invoke(this);//�����ڰ� �Ѹ��̶� ������ ��������Ʈ�� �ߵ�

        Destroy(gameObject);
    }

    
}
