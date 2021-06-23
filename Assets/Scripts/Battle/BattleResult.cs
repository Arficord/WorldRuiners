using System.Collections;
using System.Collections.Generic;
using My.Base.Battle;
using My.Base.Units;
using UnityEngine;

public struct BattleResult
{
    public Team Winner;
    public bool IsFlee;
    
    public BattleResult(Team winner, bool isFlee)
    {
        Winner = winner;
        IsFlee = isFlee;
    }
}
