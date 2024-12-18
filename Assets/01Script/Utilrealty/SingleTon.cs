using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 2종류의 싱글톤 베이스..
//1. 씬이 변경이 되더라도 인스턴스가 유지가 되는 싱글톤
public class SingleTon<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        if (Instance == null)
        {
            Instance = this as T;
            DontDestroyOnLoad(gameObject);

            DoAwake();//인스턴스가 최초로 만들어 질때.
        }
        else
        {
            Destroy(gameObject);
        }
    }

    protected virtual void DoAwake()//파생 클래스에서 자신의 초기화에 필요한 로직을 작성해야할때.
    {

    }

}

//2. 씬이 변경이 되면, 인스턴스가 파괴되는 싱글톤

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
