using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 배치된 플레이어 방해
// 이동 기능
// 데미지 받는 기능
// 사망했을 때, 플레이어 리워드 기능 ( 델리게이트/콜백 )

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour, IMovement, IDamaged
{
    private Vector2 moveDir;
    private float moveSpeed = 3;
    private bool isInit;

    private int curHp = 10;
    private int maxHp = 10;

    public bool IsDead { get => curHp <= 0; }//람다식
    //public bool IsDead { =>가 {return}역할을 함
    //    get
    //    {
    //        return curHp <= 0;
    //    }
    //}

    //델리게이트
    public delegate void MonsterDiedEvent(Enemy enemyInfo);//델리게이트 타입 선언

    public static event MonsterDiedEvent OnMonsterDied;//델리게이트 객체(체인) 선언
    // event : public 선언된 델리게이트 체인에 접근하여 구독할 수 있지만, 데이터 변형 X

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
        //Debug.Log("몬스터 피격");
    }

    private void OnDie()
    {
        //Debug.Log("몬스터 사망");
        OnMonsterDied?.Invoke(this);//구독자가 한명이라도 있으면 델리게이트를 발동

        Destroy(gameObject);
    }

    
}
