using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour, IMovement
{
    private bool isMoveing = false; // ���϶��� �̵�����

    [SerializeField] private float moveSpeed = 5f;//�⺻�� 5

    //ĳ���Ͱ� �̵��� �� �ִ� ����
    private Vector2 minArea = new Vector2(-2f, -4.5f);
    private Vector2 maxArea = new Vector2(2f, 0f);

    //�̵��� ����� ���� ����
    private Vector3 moveDelta;

    public void Move(Vector2 newDirection)
    {
        if (isMoveing)
        {
            moveDelta.x = newDirection.x;
            moveDelta.y = newDirection.y;
            moveDelta.z = 0;

            moveDelta *= (moveSpeed * Time.deltaTime);

            moveDelta += transform.position;
            moveDelta.x = Mathf.Clamp(moveDelta.x, minArea.x, maxArea.x);
            moveDelta.y = Mathf.Clamp(moveDelta.y, minArea.y, maxArea.y);

            transform.position = moveDelta;
        }
    }

    public void SetEnable(bool newEnable)
    {
        isMoveing = newEnable;
    }
}
