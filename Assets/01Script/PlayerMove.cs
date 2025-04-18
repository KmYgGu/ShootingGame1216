using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour, IMovement
{
    private bool isMoveing = false; // 참일때만 이동가능

    [SerializeField] private float moveSpeed = 5f;//기본값 5

    //캐릭터가 이동할 수 있는 영역
    private Vector2 minArea = new Vector2(-2f, -4.5f);
    private Vector2 maxArea = new Vector2(2f, 0f);

    //이동량 계산을 위한 백터
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
