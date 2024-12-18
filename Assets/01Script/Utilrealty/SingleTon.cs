using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 2������ �̱��� ���̽�..
//1. ���� ������ �Ǵ��� �ν��Ͻ��� ������ �Ǵ� �̱���
public class SingleTon<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        if (Instance == null)
        {
            Instance = this as T;
            DontDestroyOnLoad(gameObject);

            DoAwake();//�ν��Ͻ��� ���ʷ� ����� ����.
        }
        else
        {
            Destroy(gameObject);
        }
    }

    protected virtual void DoAwake()//�Ļ� Ŭ�������� �ڽ��� �ʱ�ȭ�� �ʿ��� ������ �ۼ��ؾ��Ҷ�.
    {

    }

}

//2. ���� ������ �Ǹ�, �ν��Ͻ��� �ı��Ǵ� �̱���

public class SingletonDestroy<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T instance { get; private set; }

    protected virtual void Awake()
    {
        if(instance == null)
        {
            instance = this as T;
            DoAwake();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    protected virtual void DoAwake()
    {

    }
}
