using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyItemHealth : FlyItemBase
{
    public override void ApplyEffect(GameObject target)
    {
        ScoreMGR.PlayerHPChange(true);
    }
}
