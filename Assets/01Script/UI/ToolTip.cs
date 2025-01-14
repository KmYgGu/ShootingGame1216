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

    private int upgradePrice; //����� ������?
    private int balance; //���� �ܾ�

    private bool isAvailable;
    private IncentBtn enchantButton; //�ش� ������ �� ��ų ��ư ��������?


    public void InitToolTip(IncentBtn enchant, int price)
    {
        enchantButton = enchant;
        upgradePrice = price;

        // ��ų �̸�
        nameText.text = enchantButton.TYPE.ToString();
        // �ʿ� �ݾ�
        priceText.text = "/"+upgradePrice.ToString();
        // ���� �ܾ�
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
        if(Input.GetMouseButtonUp(0))// ����� Ŭ�� �Ǿ��� �� ��Ȱ��ȭ 
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
