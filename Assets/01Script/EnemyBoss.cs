using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 2가지 기능
// 1. 데미지를 받는 일반적인 적기의 기능
// 2. AI : 인공지능 FSM 유한상태기계

public enum BossState
{
    BS_MoveToAppear, // 증장과정( 전투시작 전 전투 위치로 이동하고 있는 상태)
    BS_Phase01, // 제자리에서 공격을 반복하는 상태,
    BS_Phase02, // 좌우로 이동 하면서 공격을 반복하는 상태.
}


// 다형성을 이용해서 공격 방식을 구현한다.
// 여러가지 무기를 가지고 있고, 각각의 웨폰이 서로 다른 방식의 공격을 구현한다
// 무기를 활성화 해주는 캐릭터

public class EnemyBoss : MonoBehaviour, IMovement, IDamaged
{

    [SerializeField] private float bossAppearPointY = 2.5f;

    #region _프라이빗_
    private BossState currentState = BossState.BS_MoveToAppear;//이 값이 무엇인지에 따라 보스가 취하는 행동을 다르게함

    private IWeaphone[] wepons; //해당 보스가 사용할 수 있는 무기
    private IWeaphone curWeaphon; // 현 활성무기

    private Vector2 moveDir = Vector2.zero;

    private bool IsInit = false;
    private float moveSpeed = 3f;
    private string bossName;
    private int maxHp;
    private int curHp;
    #endregion

    public bool isDead { get => curHp <= 0; }
    // get => 람다 연산식 curHp <= 0; 비교연산자

    public delegate void BossDiedEvent();
    public event BossDiedEvent OnBossDied;


    public void InitBoss(string name, int newHp, IWeaphone[] newweaphones)
    {
        bossName = name;
        curHp = maxHp = newHp;
        wepons = newweaphones;

        SetEnable(true);

        //AI 동작 시키기
    }

    // 전처리기 하나의 종류 region, define, include
    #region _AICord_요약본
    //연관이 있는 코드들을 묶어 놓을 수 있다

    //상태값을 변경시켜주는 메소드
    public void ChangeState(BossState newState)
    {
        StopCoroutine(currentState.ToString());//기존 상태값의 코드를 멈추고
        currentState = newState;//상태값을 변경
        StartCoroutine(currentState.ToString());//새로운 상태값의 코드 시작
    }

    private IEnumerator BS_MoveToAppear()
    {
        moveDir = Vector2.down;

        while (true) 
        {
            if (transform.position.y <= bossAppearPointY)// 전투위체 도달했는지?
            {
                moveDir = Vector2.zero ;
                ChangeState(BossState.BS_Phase01);//appear에서 phase01으로 상태 변경
            }
            yield return null;//한 프레임을 쉬고 나서 체크해 보겠다
        }
        
    }

    //제자리에서 무기 작동만
    private IEnumerator BS_Phase01()
    {
        curWeaphon = wepons[0];
        curWeaphon.SetEnable(true);
        while (true)
        {
            curWeaphon.Fire();
            yield return new WaitForSeconds(0.7f);
        }
    }

    private IEnumerator BS_Phase02()
    {
        curWeaphon.SetEnable(false);
        curWeaphon = wepons[1];
        curWeaphon.SetEnable(true);

        moveDir = Vector2.right;
        while (true)
        {
            curWeaphon.Fire();

            if(transform.position.x <= -2.5f
                || transform.position.x >= 2.5f)
            {
                moveDir *= -1f;
            }
            yield return new WaitForSeconds(0.4f);
        }
    }
    #endregion 

    private void Update()
    {
        if (IsInit)
        {
            Move(moveDir);
        }
    }

    public void Move(Vector2 newDirection)
    {
        transform.Translate(moveDir * (moveSpeed * Time.deltaTime));
    }

    public void SetEnable(bool newEnable)
    {
        IsInit = newEnable;
    }

    public void TakeDamage(GameObject attacker, int damage)
    {
        if (!isDead)
        {
            curHp -= damage;
            if(curHp > 0)//줄어든 체력이 0보다 크다면
            {//피격
                OnDamaged();
            }
            else
            {//사망
                OnDied();
            }
        }
    }

    private void OnDamaged()
    {
        //데미지를 받았는 데, 나의 스테이트가 1번이고 체력이 절반미만일 경우
        if(currentState == BossState.BS_Phase01 && (float) curHp/ maxHp < 0.5f)
        {
            ChangeState(BossState.BS_Phase02);//1번 패턴에서 2번패턴으로 넘기기
        }
    }

    private void OnDied()
    {
        OnBossDied?.Invoke();
        Destroy(gameObject);
    }

    
}
