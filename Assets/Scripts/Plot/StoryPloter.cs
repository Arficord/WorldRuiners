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
        //TODO: tempo remove
        [SerializeField] private GameObject battleStartUI;

        //test public method
        public void InitTestBattle()
        {
            List<Unit> participants = new List<Unit>();
            participants.Add(UnitFactory.GetNewUnit(UnitTypes.TestMage, 500, Team.First));
            participants.Add(UnitFactory.GetNewUnit(UnitTypes.TestWarrior, 520, Team.First));
            participants.Add(UnitFactory.GetNewUnit(UnitTypes.TestTank, 560, Team.First));
            participants.Add(UnitFactory.GetNewUnit(UnitTypes.TestMage, 2, Team.Second));
            participants.Add(UnitFactory.GetNewUnit(UnitTypes.TestWarrior, 30, Team.Second));
            participants.Add(UnitFactory.GetNewUnit(UnitTypes.TestTank, 80, Team.Second));
            InitBattle(participants);
        }

        private void InitBattle(List<Unit> participants)
        {
            battleManager.StartBattle(participants, ProceedBattleResults);
        }

        private void ProceedBattleResults(BattleResult result)
        {
            Debug.Log($"Story Ploter received battle result. The winner is {result.Winner} team");
            battleStartUI.gameObject.SetActive(true);
        }
    }
}