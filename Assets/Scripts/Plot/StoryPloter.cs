using System;
using System.Collections;
using System.Collections.Generic;
using My.Base.Battle;
using My.Base.Units;
using UnityEngine;

namespace My.Base.Plot
{
    public class StoryPloter : MonoBehaviour
    {
        [SerializeField] private BattleManager battleManager;

        //test public method
        public void InitTestBattle()
        {
            List<Unit> participants = new List<Unit>();
            participants.Add(UnitFactory.GetNewUnit(UnitTypes.TestMage, 12, Team.First));
            participants.Add(UnitFactory.GetNewUnit(UnitTypes.TestWarrior, 34, Team.First));
            participants.Add(UnitFactory.GetNewUnit(UnitTypes.TestTank, 56, Team.First));
            participants.Add(UnitFactory.GetNewUnit(UnitTypes.TestMage, 78, Team.Second));
            participants.Add(UnitFactory.GetNewUnit(UnitTypes.TestWarrior, 30, Team.Second));
            participants.Add(UnitFactory.GetNewUnit(UnitTypes.TestTank, 59, Team.Second));
            InitBattle(participants);
        }
        
        private void InitBattle(List<Unit> participants)
        {
            battleManager.StartBattle(participants);
        }
    }
}