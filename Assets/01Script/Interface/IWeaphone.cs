using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeaphone  
{
    void SetOwner(GameObject newOwner);//������ �����ڰ� ��������

    void Fire();

    void SetEnable(bool enable);
}
