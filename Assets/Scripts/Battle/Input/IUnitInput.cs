using System;
using System.Collections;
using System.Collections.Generic;
using My.Base.Battle;
using UnityEngine;

namespace My.Base.Battle
{
    public interface IUnitInput
    {
        event Action<BattleUnit> OnNeedInput;
        void PlayTurn(BattleManager battle, BattleUnit unit);
    }
}