using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//C#������ ���߻���� ������� �ʴ´�.
//�� �������̽��� ���ؼ� ���������� ���
public class FlyItemBomb : FlyItemBase
{
    public override void ApplyEffect(GameObject target)
    {
        //ȿ��
        ScoreMGR.ChangeBombCount(true);
    }
}
