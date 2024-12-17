using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//월드에 있는 특정 interface를 찾아서 탐색해오는 기능

public class InterFaceFinder : MonoBehaviour
{
    public static List<T> FindObjectOfInterface<T>() where T : class
    {
        //FindObjectsByType 서능이 좋고, 정렬기능을 제공
        //FindObjectsOfType 성능이 좋지 않고 정렬과 탐색 옵셥이 제공되지 않음
        MonoBehaviour[] allObjects = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None);
        
        List<T> interfaceObjects = new List<T>();
        //for문 : 시작조건, 반복조건, 변화조건.. 특정횟수만큼 반복문
        //foreach문 : 자료구조의 컨테이너의 모든 원소를 순환하는 반복문

        //제네릭 프로그래밍 문법
        foreach(var obj in allObjects)
        {
            if(obj is T interfaceObject)//obj를 T타입 캐스팅을 해서 성공을 하면, interfaceObject에 참조를 시킨다
            {
                interfaceObjects.Add(interfaceObject);
            }
        }

        return interfaceObjects;
    }
}
