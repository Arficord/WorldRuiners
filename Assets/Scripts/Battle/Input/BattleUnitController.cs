using System;
using System.Collections;
using System.Collections.Generic;
using My.Base.Battle;
using My.UI;
using UnityEngine;

public class BattleUnitController: IUnitInput
{
    public event Action<BattleUnit> OnNeedInput;
    public void PlayTurn(BattleManager battle, BattleUnit unit)
    {
        if (unit.MindedTeam == Team.First && unit.IsPlayerCanControl)
        {
            NeedInput(battle, unit);
        }
        else
        {
            AutoPlay(battle, unit);
        }
    }

    private void AutoPlay(BattleManager battle, BattleUnit unit)
    {
        Debug.Log("Auto Play");
        battle.SkipTurn();
    }

    private void NeedInput(BattleManager battle, BattleUnit unit)
    {
        Debug.Log("Need Input From User");
        OnNeedInput?.Invoke(unit);
    }
}
