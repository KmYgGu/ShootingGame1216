using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SkillType
{
    ST_PowerUp,
    ST_DoubleShoot,
    ST_BoomCountAdd,
    ST_TripleShoot,
    ST_PowerUp2,
}

public enum EnchantState
{
    Learn,  // �̹� ��� ��ų
    Enable, // �䱸������ ���������� ������ ����
    Disable, // �䱸 ������ �ȵǰ� ���浵 ����
}


public class IncentBtn : MonoBehaviour
{
    [SerializeField] private Image activeImg;
    [SerializeField] private Image lockImg;
    [SerializeField] private IncentBtn prevBTN;
    [SerializeField] private IncentBtn nextBTN;
    [SerializeField] private int requireLevel; // �ش� ��ų ���� ���� �䱸����
    [SerializeField] private int price; // �ش� ��ų ���� ���� ���

    private EnchantState curState;
    public EnchantState STATE
    {
        get { return curState; }
        set { curState = value; }
    }

    [SerializeField] private GameObject toolTip;
    [SerializeField] private SkillType skillType;
    public SkillType TYPE
    {
        get { return skillType; }
    }


    public void InitBtn(bool isLearn, int playerLevel)
    {
        //Ȱ��ȭ ���� üũ
        lockImg.gameObject.SetActive(true);
        activeImg.gameObject.SetActive(true);
        STATE = EnchantState.Disable;

        if(isLearn)//�̹� ������ �Ϸ�� ��ų
        {
            STATE = EnchantState.Learn;
            lockImg.gameObject.SetActive(false);
            activeImg.gameObject.SetActive(false);
        }
        else // ���� ������� ���� ����
        {
            if(prevBTN != null)// ���ེų�� �ִ� ���
            {
               if(playerLevel >= requireLevel && prevBTN.STATE == EnchantState.Learn)
                {
                    lockImg.gameObject.SetActive(false);
                    STATE = EnchantState.Enable;// ���� �غ� �Ϸ�� ����
                }

            }
            else if(playerLevel >= requireLevel)
            {
                lockImg.gameObject.SetActive(false);
                STATE = EnchantState.Enable;
            }
        }
    }

    public void OnClick_Btn()//��ư ���� �ݹ�
    {
        toolTip.SetActive(true);
        toolTip.transform.parent = transform;
        toolTip.transform.localPosition = new Vector3(270.0f, 0f, 0f);

        //toolTip ���� �Լ� ���� �Ŀ� ȣ��
        if(toolTip.TryGetComponent<ToolTip>(out ToolTip tip))
        {
            tip.InitToolTip(this, price);
        }
    }

    // ��ų ���� ó��
    public void Upgrade()
    {
        PlayerPrefs.SetInt(TYPE.ToString(), 1);

        InitBtn(true, 99);
        if (nextBTN)
        {
            nextBTN.InitBtn(false, 99);
        }
    }

}

//�� 5������ ��ų�� Player
