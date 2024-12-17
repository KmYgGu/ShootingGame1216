using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovement 
{

    void SetEnable(bool newEnable);// 이동 가능/ 불가능 set

    void Move(Vector2 newDirection);//해당하는 방향으로 이동하는지


}
