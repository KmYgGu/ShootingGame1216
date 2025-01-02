using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 2���� ���
// 1. �������� �޴� �Ϲ����� ������ ���
// 2. AI : �ΰ����� FSM ���ѻ��±��

public enum BossState
{
    BS_MoveToAppear, // �������( �������� �� ���� ��ġ�� �̵��ϰ� �ִ� ����)
    BS_Phase01, // ���ڸ����� ������ �ݺ��ϴ� ����,
    BS_Phase02, // �¿�� �̵� �ϸ鼭 ������ �ݺ��ϴ� ����.
}


// �������� �̿��ؼ� ���� ����� �����Ѵ�.
// �������� ���⸦ ������ �ְ�, ������ ������ ���� �ٸ� ����� ������ �����Ѵ�
// ���⸦ Ȱ��ȭ ���ִ� ĳ����

public class EnemyBoss : MonoBehaviour, IMovement, IDamaged
{

    [SerializeField] private float bossAppearPointY = 2.5f;

    #region _�����̺�_
    private BossState currentState = BossState.BS_MoveToAppear;//�� ���� ���������� ���� ������ ���ϴ� �ൿ�� �ٸ�����

    private IWeaphone[] wepons; //�ش� ������ ����� �� �ִ� ����
    private IWeaphone curWeaphon; // �� Ȱ������

    private Vector2 moveDir = Vector2.zero;

    private bool IsInit = false;
    private float moveSpeed = 3f;
    private string bossName;
    private int maxHp;
    private int curHp;
    #endregion

    public bool isDead { get => curHp <= 0; }
    // get => ���� ����� curHp <= 0; �񱳿�����

    public delegate void BossDiedEvent();
    public event BossDiedEvent OnBossDied;


    public void InitBoss(string name, int newHp, IWeaphone[] newweaphones)
    {
        bossName = name;
        curHp = maxHp = newHp;
        wepons = newweaphones;

        SetEnable(true);

        //AI ���� ��Ű��
    }

    // ��ó���� �ϳ��� ���� region, define, include
    #region _AICord_��ົ
    //������ �ִ� �ڵ���� ���� ���� �� �ִ�

    //���°��� ��������ִ� �޼ҵ�
    public void ChangeState(BossState newState)
    {
        StopCoroutine(currentState.ToString());//���� ���°��� �ڵ带 ���߰�
        currentState = newState;//���°��� ����
        StartCoroutine(currentState.ToString());//���ο� ���°��� �ڵ� ����
    }

    private IEnumerator BS_MoveToAppear()
    {
        moveDir = Vector2.down;

        while (true) 
        {
            if (transform.position.y <= bossAppearPointY)// ������ü �����ߴ���?
            {
                moveDir = Vector2.zero ;
                ChangeState(BossState.BS_Phase01);//appear���� phase01���� ���� ����
            }
            yield return null;//�� �������� ���� ���� üũ�� ���ڴ�
        }
        
    }

    //���ڸ����� ���� �۵���
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
            if(curHp > 0)//�پ�� ü���� 0���� ũ�ٸ�
            {//�ǰ�
                OnDamaged();
            }
            else
            {//���
                OnDied();
            }
        }
    }

    private void OnDamaged()
    {
        //�������� �޾Ҵ� ��, ���� ������Ʈ�� 1���̰� ü���� ���ݹ̸��� ���
        if(currentState == BossState.BS_Phase01 && (float) curHp/ maxHp < 0.5f)
        {
            ChangeState(BossState.BS_Phase02);//1�� ���Ͽ��� 2���������� �ѱ��
        }
    }

    private void OnDied()
    {
        OnBossDied?.Invoke();
        Destroy(gameObject);
    }

    
}
