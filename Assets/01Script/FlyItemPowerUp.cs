using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyItemPowerUp : FlyItemBase
{
    //������ : �پ��� ���¿� ��ü�� Ȯ�� �����ϰ�, �����ϰ� ȣ����� �۾��� �̷����� �ְ�
    //�ڵ带 �ۼ��ϴ� ��� ��ü�������α׷���
    public override void ApplyEffect(GameObject target)
    {
        ScoreMGR.PowerUp();
    }
}
