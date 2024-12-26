using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyItemPowerUp : FlyItemBase
{
    //다형성 : 다양한 형태에 객체로 확장 가능하고, 유연하게 호출등의 작업이 이뤄질수 있게
    //코드를 작성하는 방식 객체지향프로그래밍
    public override void ApplyEffect(GameObject target)
    {
        ScoreMGR.PowerUp();
    }
}
