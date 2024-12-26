using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//C#에서는 다중상속을 허용하지 않는다.
//단 인터페이스에 한해서 다중적으로 허용
public class FlyItemBomb : FlyItemBase
{
    public override void ApplyEffect(GameObject target)
    {
        //효과
        ScoreMGR.ChangeBombCount(true);
    }
}
