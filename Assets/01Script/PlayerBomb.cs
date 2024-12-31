using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBomb : MonoBehaviour
{
    private Animator anims;

    [SerializeField] private AnimationCurve curve;
    //시각적으로 보면서 그래프를 그릴수 있음

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

        //초기값 세팅
        startPos = transform.position;//GameObject.FindAnyObjectByType<PlayerWeapon>().transform.position;// 발사위치
        current = 0f;
        percent = 0f;


        while(percent < 1f)
        {
            current+= Time.deltaTime;//한프레임 그린 시간을 current를 더해줌
            percent = current / 1.5f;//출발지에서 목표지까지 1.5초로 이동시키기 위해 퍼센트로 계산

            //시작점에서 목표좌표까지의 선형 보간을 한다
            transform.position = Vector3.Lerp(startPos, endPos, curve.Evaluate(percent));

            yield return null;//한 프레임을 쉬게한다

        }

        anims.SetTrigger("Onfire");
    }
}
