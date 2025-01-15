using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//플레이어의 메인 클래스 역할
//이동, 피격, 아이템 습득
public class PlayerController : MonoBehaviour
{
    private IMovement movement;//C# 참조형 변수 (포인터)
    private IWeaphone weapon;

    private void Awake()
    {
        TryGetComponent<IMovement>(out movement);
        //if (TryGetComponent<IMovement>(out movement))
        //Debug.Log("PlayerController.cs - Awake() - movement의 참조를 실패했다.");

        TryGetComponent<IWeaphone>(out weapon);
        //if (TryGetComponent<IWeaphone>(out weapon))
        //Debug.Log("PlayerController.cs - Awake() - weapon 참조를 실패했다.");
    }

    public void CustomUpdate(Vector2 moveDir)
    {
        //if(movement != null)
        //    movement.Move(moveDir);// nullptr

        movement?.Move(moveDir);//nullptr가 아닐 경우에만 실행
        weapon?.Fire();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            weapon?.LunchBomb();
        }

    }
    public void StartGame()
    {
        
        movement?.SetEnable(true);

        weapon?.SetOwner(gameObject);

        weapon?.SetEnable(true);
    }

    public void StopGame()
    {
        movement?.SetEnable(false);
        weapon?.SetEnable(false);
    }

}
