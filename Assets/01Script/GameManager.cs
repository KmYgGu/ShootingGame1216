using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ���� �������� �帧�� ����, (�̱� ��)
// 1. ������ ����, ����,����
// 2. ���� ������
// 3. �����ε� : ���� �ε�
// 4. �� ���� : �� ���� ����.
// 5. �Է½ý���

public class GameManager : SingleTon<GameManager>
{
    private PlayerController pc;
    private ScrollManager scrollManager;
    private IinputHandle inputHandle;
    

    private void Start()
    {
        LoadSenceInit();// �ӽ÷� �ۼ� : ���� ������ ������ �Ϸ��� ����
        StartCoroutine(GameStart());// �ӽ�...
    }


    //���� ������ �Ǿ� �ε��� ������ ��
    //�ѹ��� ȣ���� �ǵ���
    private void LoadSenceInit()
    {
        pc = FindAnyObjectByType<PlayerController>();
        scrollManager = FindAnyObjectByType<ScrollManager>();
        inputHandle = GetComponent<KeyBordInputHandle>();
    }

    private void Update()
    {
        if(inputHandle != null)
            pc?.CustomUpdate(inputHandle.Getinput());

    }

    //������ ������ �ɶ� ó���Ǿ���ϴ� �Ϸ��� ��������
    //������ ���缭 �������ִ� ����
    IEnumerator GameStart()
    {
        yield return null;
        Debug.Log("������ �ʱ�ȭ");
        Debug.Log("������� ���");
        yield return new WaitForSeconds(1f);
        pc?.StartGame();
        scrollManager?.SetScrollSpeed(2.5f);


        Debug.Log("���� ���� ����");
    }
}
