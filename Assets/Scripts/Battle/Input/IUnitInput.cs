using System;
using System.Collections;
using System.Collections.Generic;
using My.Base.Battle;
using UnityEngine;

public interface IUnitInput
{
    event Action<BattleUnit> OnNeedInput;
    void PlayTurn(BattleManager battle, BattleUnit unit);
}
