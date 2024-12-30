using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyUtility : MonoBehaviour
{
    //����Ƽ�� ������ ����ָ� ã�� ���ؼ� ���� ����
    public static Transform FindChildRescursive(Transform parent, string targetName)
    {// ���� �켱Ž��
        foreach(Transform child in parent)
        {
            if(child.name == targetName)
                return child;

            Transform findTrans = FindChildRescursive(child, targetName);// ����Լ�.

            if (findTrans != null)
            {
                return findTrans;
            }
        }
        return null;
    }
    //Ư���� ������ ������ ������ �� �ȿ��� Ž���� �� �� �ְ� ������ �Լ��� ���� �����
}
