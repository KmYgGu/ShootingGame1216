using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�������� �����ϱ� ����
//�߻� Ŭ���� : �߻� �޼ҵ带 ������ �ִ� Ŭ������ ����Ű�� ���
//�ܵ����� ��ü�� ������ �� ����
//�Ļ� Ŭ������ �����ؼ�, �Ļ� Ŭ�������� �߻�޼ҵ带 ������ �ǹ������� �ϱ� ���ؼ�.

//���� ���
//���Ϳ��� ����� �Ǿ ������ �Ǹ�, �����ð�(3~4��) �� �������� �̵��ϴٰ�.
//3~4�ʿ� ������ �ٲٴ� ���

//�÷��̾�� �浹�ϰ� �Ǹ� �÷��̾�� ����ó��

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public abstract class FlyItemBase : MonoBehaviour, IMovement, IPickuped
{
    // �߻� �޼ҵ�
    public abstract void ApplyEffect(GameObject target);//�����ΰ� ���� �߻� �޼ҵ�


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

        if (newEnable)//��Ȱ��ȭ ���¿� �ִٰ�, Ȱ��ȭ�� �Ǵ� �����̶��
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
