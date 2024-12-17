using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���忡 �ִ� Ư�� interface�� ã�Ƽ� Ž���ؿ��� ���

public class InterFaceFinder : MonoBehaviour
{
    public static List<T> FindObjectOfInterface<T>() where T : class
    {
        //FindObjectsByType ������ ����, ���ı���� ����
        //FindObjectsOfType ������ ���� �ʰ� ���İ� Ž�� �ɼ��� �������� ����
        MonoBehaviour[] allObjects = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None);
        
        List<T> interfaceObjects = new List<T>();
        //for�� : ��������, �ݺ�����, ��ȭ����.. Ư��Ƚ����ŭ �ݺ���
        //foreach�� : �ڷᱸ���� �����̳��� ��� ���Ҹ� ��ȯ�ϴ� �ݺ���

        //���׸� ���α׷��� ����
        foreach(var obj in allObjects)
        {
            if(obj is T interfaceObject)//obj�� TŸ�� ĳ������ �ؼ� ������ �ϸ�, interfaceObject�� ������ ��Ų��
            {
                interfaceObjects.Add(interfaceObject);
            }
        }

        return interfaceObjects;
    }
}
