using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�÷��̾��� ���� Ŭ���� ����
//�̵�, �ǰ�, ������ ����
public class PlayerController : MonoBehaviour
{
    private IMovement movement;//C# ������ ���� (������)
    private IWeaphone weapon;

    private void Awake()
    {
        if (TryGetComponent<IMovement>(out movement))
            Debug.Log("PlayerController.cs - Awake() - movement�� ������ �����ߴ�.");

        if (TryGetComponent<IWeaphone>(out weapon))
            Debug.Log("PlayerController.cs - Awake() - weapon ������ �����ߴ�.");
    }

    public void CustomUpdate(Vector2 moveDir)
    {
        //if(movement != null)
        //    movement.Move(moveDir);// nullptr

        movement?.Move(moveDir);//nullptr�� �ƴ� ��쿡�� ����

        weapon?.Fire();

    }
    public void StartGame()
    {
        movement?.SetEnable(true);
        weapon?.SetEnable(true);
    }

    public void StopGame()
    {
        movement?.SetEnable(false);
        weapon?.SetEnable(false);
    }

}
