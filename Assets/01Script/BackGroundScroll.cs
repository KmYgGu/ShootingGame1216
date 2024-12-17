using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//스크롤 기능
//위치 리셋기능
//스크롤 속도를 변경하는 기능

public interface IBackGrondScroller
{
    void Scroll(float deltaTime);
    void ResetPositon();
    void SetScrollSpeed(float newSpeed);
}

//C#에는 기본적인 클래스의 다중상속을 허용하지 않는다.
//다만 interface클래스는 예외적으로 다중상속을 허용한다
public class BackGroundScroll : MonoBehaviour, IBackGrondScroller
{
    //컨트롤 .를 눌러 빠른 작업 리펙토링
    [SerializeField] private float scrollSpeed = 0f;
    private Vector3 startPos = new Vector3(0f, 12.75f, 0f);
    //스크롤이 완료가 되어서 화면밖으로 이동했을 때,
    //초기 위치로 돌아가기 위한 좌표

    private float resetPositonY = -12.75f;
    //화면을 벗어났다고 판정하기 위한 기준 높이
    
    public void ResetPositon()
    {
        transform.position = startPos;
    }

    public void Scroll(float deltaTime)
    {
        transform.position += Vector3.down * (scrollSpeed * deltaTime);
        if(transform.position.y < resetPositonY)
        {
            ResetPositon();
        }
    }

    public void SetScrollSpeed(float newSpeed)
    {

        scrollSpeed = Mathf.Clamp(newSpeed,0f,15f);
    }

}
