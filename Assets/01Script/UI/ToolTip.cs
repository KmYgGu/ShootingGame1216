using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ToolTip : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI nameText;
    [SerializeField]
    private TextMeshProUGUI balanceText;
    [SerializeField]
    private TextMeshProUGUI priceText;
    [SerializeField]
    private GameObject upgradeBTNobj;

    private int upgradePrice; //비용이 얼마인지?
    private int balance; //유저 잔액

    private bool isAvailable;
    private IncentBtn enchantButton; //해당 툴팁을 연 스킬 버튼 누구인지?


    public void InitToolTip(IncentBtn enchant, int price)
    {
        enchantButton = enchant;
        upgradePrice = price;

        // 스킬 이름
        nameText.text = enchantButton.TYPE.ToString();
        // 필요 금액
        priceText.text = "/"+upgradePrice.ToString();
        // 유저 잔액
        balance = PlayerPrefs.GetInt(SAVE_Type.SAVE_GOLD.ToString(), 0);
        balanceText.text = balance.ToString();

        if(balance >= upgradePrice)
        {
            isAvailable = true;
            balanceText.color = Color.green;
        }
        else
        {
            isAvailable = false;
            balanceText.color = Color.red;
        }

        if(enchant.STATE == EnchantState.Enable)
            upgradeBTNobj.SetActive(true);
        else
            upgradeBTNobj.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetMouseButtonUp(0))// 빈공간 클릭 되었을 때 비활성화 
            gameObject.SetActive(false);
    }

    public void OnClickUpgradeBTN()
    {
        if(balance >= upgradePrice && enchantButton != null)
        {
            balance -= upgradePrice;
            PlayerPrefs.SetInt(SAVE_Type.SAVE_GOLD.ToString() , balance);
            enchantButton.Upgrade();
        }
    }
}
