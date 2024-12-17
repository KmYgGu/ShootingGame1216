using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//��ũ�� ���
//��ġ ���±��
//��ũ�� �ӵ��� �����ϴ� ���

public interface IBackGrondScroller
{
    void Scroll(float deltaTime);
    void ResetPositon();
    void SetScrollSpeed(float newSpeed);
}

//C#���� �⺻���� Ŭ������ ���߻���� ������� �ʴ´�.
//�ٸ� interfaceŬ������ ���������� ���߻���� ����Ѵ�
public class BackGroundScroll : MonoBehaviour, IBackGrondScroller
{
    //��Ʈ�� .�� ���� ���� �۾� �����丵
    [SerializeField] private float scrollSpeed = 0f;
    private Vector3 startPos = new Vector3(0f, 12.75f, 0f);
    //��ũ���� �Ϸᰡ �Ǿ ȭ������� �̵����� ��,
    //�ʱ� ��ġ�� ���ư��� ���� ��ǥ

    private float resetPositonY = -12.75f;
    //ȭ���� ����ٰ� �����ϱ� ���� ���� ����
    
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
