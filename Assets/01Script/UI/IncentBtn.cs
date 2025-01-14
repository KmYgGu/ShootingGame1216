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
    Learn,  // 이미 배운 스킬
    Enable, // 요구레벨은 충족하지만 습득은 아직
    Disable, // 요구 레벨도 안되고 습득도 안함
}


public class IncentBtn : MonoBehaviour
{
    [SerializeField] private Image activeImg;
    [SerializeField] private Image lockImg;
    [SerializeField] private IncentBtn prevBTN;
    [SerializeField] private IncentBtn nextBTN;
    [SerializeField] private int requireLevel; // 해당 스킬 배우기 위한 요구레벨
    [SerializeField] private int price; // 해당 스킬 배우기 위한 비용

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
        //활성화 여부 체크
        lockImg.gameObject.SetActive(true);
        activeImg.gameObject.SetActive(true);
        STATE = EnchantState.Disable;

        if(isLearn)//이미 습득이 완료된 스킬
        {
            STATE = EnchantState.Learn;
            lockImg.gameObject.SetActive(false);
            activeImg.gameObject.SetActive(false);
        }
        else // 아직 습득되지 않은 상태
        {
            if(prevBTN != null)// 선행스킬이 있는 경우
            {
               if(playerLevel >= requireLevel && prevBTN.STATE == EnchantState.Learn)
                {
                    lockImg.gameObject.SetActive(false);
                    STATE = EnchantState.Enable;// 습득 준비가 완료된 상태
                }

            }
            else if(playerLevel >= requireLevel)
            {
                lockImg.gameObject.SetActive(false);
                STATE = EnchantState.Enable;
            }
        }
    }

    public void OnClick_Btn()//버튼 눌림 콜백
    {
        toolTip.SetActive(true);
        toolTip.transform.parent = transform;
        toolTip.transform.localPosition = new Vector3(270.0f, 0f, 0f);

        //toolTip 툴팁 함수 구현 후에 호출
        if(toolTip.TryGetComponent<ToolTip>(out ToolTip tip))
        {
            tip.InitToolTip(this, price);
        }
    }

    // 스킬 습득 처리
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

//총 5종류의 스킬이 Player
