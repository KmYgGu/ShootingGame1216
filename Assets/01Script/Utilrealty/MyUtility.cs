using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyUtility : MonoBehaviour
{
    //유니티는 증손주 고손주를 찾지 못해서 따로 만듬
    public static Transform FindChildRescursive(Transform parent, string targetName)
    {// 깊이 우선탐색
        foreach(Transform child in parent)
        {
            if(child.name == targetName)
                return child;

            Transform findTrans = FindChildRescursive(child, targetName);// 재귀함수.

            if (findTrans != null)
            {
                return findTrans;
            }
        }
        return null;
    }
    //특정한 범위로 제약을 짓고나서 그 안에서 탐색을 할 수 있게 별도로 함수를 만들어서 사용함
}
