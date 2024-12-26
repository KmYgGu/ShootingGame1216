using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//다형성을 구현하기 위해
//추상 클래스 : 추상 메소드를 가지고 있는 클래스를 가르키는 용어
//단독으로 객체를 생성할 수 없음
//파생 클래스를 생성해서, 파생 클래스에서 추상메소드를 구현을 의무적으로 하기 위해서.

//공통 기능
//몬스터에서 드랍이 되어서 생성이 되면, 일정시간(3~4초) 한 방향으로 이동하다가.
//3~4초에 방향을 바꾸는 기능

//플레이어와 충돌하게 되면 플레이어에게 습득처리

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public abstract class FlyItemBase : MonoBehaviour, IMovement, IPickuped
{
    // 추상 메소드
    public abstract void ApplyEffect(GameObject target);//구현부가 없는 추상 메소드


    private bool isInit = false;
    private float flySpeed = 0.7f;
    private Vector2 flyDirection = Vector2.zero;
    private Vector3 flyTargetPos = Vector2.zero;

    private ScoreManager scoreManager;

    protected ScoreManager ScoreMGR
    {
        get
        {
            if (scoreManager == null)
                scoreManager = FindAnyObjectByType<ScoreManager>();
            return scoreManager;
        }
    }

    private void Awake()
    {
        if(TryGetComponent<Rigidbody2D>(out Rigidbody2D rig))
        {
            rig.gravityScale = 0f;
        }
        if (TryGetComponent<CircleCollider2D>(out CircleCollider2D col))
        {
            col.isTrigger = true;
            col.radius = 0.25f;
        }

        SetEnable(true);
    }

    void Update()
    {
        if (isInit)
        {
            Move(flyDirection);
        }
    }
    public void Move(Vector2 newDirection)
    {
        transform.Translate(newDirection * (flySpeed * Time.deltaTime));
    }

    public void OnPickUp(GameObject Picker)
    {
        ApplyEffect(Picker);
        Destroy(gameObject);
    }

    public void SetEnable(bool newEnable)
    {
        isInit = newEnable;

        if (newEnable)//비활성화 상태에 있다가, 활성화가 되는 시점이라면
        {
            StartCoroutine("ChangeFlyDirection");
        }
        else
        {
            StopCoroutine("ChangeFlyDirection");
        }
    }
    IEnumerator ChangeFlyDirection()
    {
        while (true)
        {
            flyTargetPos.x = Random.Range(-2f, 2f);
            flyTargetPos.y = Random.Range(-2f, 2f);
            flyTargetPos.z = 0;

            flyDirection = (flyTargetPos - transform.position).normalized;
            yield return new WaitForSeconds(4f);
        }
    }
}
