using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�÷��̾��� ���� Ŭ���� ����
//�̵�, �ǰ�, ������ ����
public class PlayerController : MonoBehaviour
{
    private IMovement movement;//C# ������ ���� (������)

    private void Awake()
    {
        if (TryGetComponent<IMovement>(out movement))
            Debug.Log("PlayerController.cs - Awake() - movement�� ������ �����ߴ�.");
    }

    public void CustomUpdate(Vector2 moveDir)
    {
        //if(movement != null)
        //    movement.Move(moveDir);// nullptr

        movement?.Move(moveDir);//nullptr�� �ƴ� ��쿡�� ����

    }
    public void StartGame()
    {
        movement?.SetEnable(true);
    }

    public void StopGame()
    {
        movement?.SetEnable(false);
    }

}
