using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeaphone  
{
    void SetOwner(GameObject newOwner);//무기의 소유자가 누구인지

    void Fire();

    void SetEnable(bool enable);
}
