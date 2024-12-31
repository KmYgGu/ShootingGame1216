using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBomb : MonoBehaviour
{
    private Animator anims;

    [SerializeField] private AnimationCurve curve;
    //�ð������� ���鼭 �׷����� �׸��� ����

    private Vector3 startPos;
    private Vector3 endPos = Vector3.zero;
    private float current;
    private float percent;


    private void Awake()
    {
        TryGetComponent<Animator>(out anims);

        StartCoroutine("MovetoCenter");
    }
    private IEnumerator MovetoCenter()
    {
        yield return null;

        //�ʱⰪ ����
        startPos = transform.position;//GameObject.FindAnyObjectByType<PlayerWeapon>().transform.position;// �߻���ġ
        current = 0f;
        percent = 0f;


        while(percent < 1f)
        {
            current+= Time.deltaTime;//�������� �׸� �ð��� current�� ������
            percent = current / 1.5f;//��������� ��ǥ������ 1.5�ʷ� �̵���Ű�� ���� �ۼ�Ʈ�� ���

            //���������� ��ǥ��ǥ������ ���� ������ �Ѵ�
            transform.position = Vector3.Lerp(startPos, endPos, curve.Evaluate(percent));

            yield return null;//�� �������� �����Ѵ�

        }

        anims.SetTrigger("Onfire");
    }
}
