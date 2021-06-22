using System;
using System.Collections;
using System.Collections.Generic;
using My.Base.Battle;
using My.Base.Skills;
using My.Base.Units;
using My.UI;
using UnityEngine;
using Random = UnityEngine.Random;


namespace My.Base.Battle
{
    public class BattleUnitController : IUnitInput
    {
        public event Action<BattleUnit> OnNeedInput;

        public void PlayTurn(BattleManager battle, BattleUnit unit)
        {
            if (unit.UnitModel.MindedTeam == Team.First && unit.IsPlayerCanControl)
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
            //Tempo realization
            Debug.Log("Auto Play");
            Skill baseAttack = unit.UnitModel.Skills[0];
            List<Unit> targets = new List<Unit>();
            while (targets.Count < baseAttack.ManualTargetAmount)
            {
                int index = Random.Range(0, battle.UnitsInBattle.Count);
                Unit target = battle.UnitsInBattle[index].UnitModel;
                if (baseAttack.IsPossibleTarget(unit.UnitModel, target))
                {
                    targets.Add(target);
                }
            }

            baseAttack.Use(unit.UnitModel, targets);
            battle.SkipTurn();
        }

        private void NeedInput(BattleManager battle, BattleUnit unit)
        {
            Debug.Log("Need Input From User");
            OnNeedInput?.Invoke(unit);
        }
    }
}