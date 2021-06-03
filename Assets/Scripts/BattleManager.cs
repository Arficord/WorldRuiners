using System;
using System.Collections;
using System.Collections.Generic;
using My.Base.Units;
using My.UI.Windows;
using UnityEngine;

namespace My.Base.Battle
{
    //TODO: WIP. Currently this is a test code
    public class BattleManager : MonoBehaviour
    {
        [SerializeField] private BattleFieldManager battleField;
        private List<BattleUnit> unitsInBattle = new List<BattleUnit>();
        
        private void Start()
        {
            SpawnUnit(UnitTypes.TestMage, 12, Team.First);
            SpawnUnit(UnitTypes.TestWarrior, 34, Team.First);
            SpawnUnit(UnitTypes.TestTank, 56, Team.First);
            
            SpawnUnit(UnitTypes.TestMage, 78, Team.Second);
            SpawnUnit(UnitTypes.TestWarrior, 910, Team.Second);
            SpawnUnit(UnitTypes.TestTank, 112, Team.Second);
        }

        private void ProceedBattleTick()
        {
            foreach (var unit in unitsInBattle)
            {
                unit.IncreaseTimePlaceByParameters();
            }
        }
        
        private void SpawnUnit(UnitTypes unitType, int level, Team team)
        {
            SpawnUnit(UnitFactory.GetNewUnit(unitType, level), team);
        }

        private void SpawnUnit(Unit unit, Team team)
        {
            BattleUnit loadedResource = Resources.Load<BattleUnit>($"BattleUnits/{unit.UnitType.ToString()}");
            Transform placeToSpawn = battleField.GetEmptyPlace(team);
            if (placeToSpawn == null)
            {
                Debug.Log($"Dont gotten place to spawn unit {unit.Name} in team {team}");
            }
            BattleUnit battleUnit = Instantiate(loadedResource, placeToSpawn);
            battleUnit.Initialize(unit, team);
            unitsInBattle.Add(battleUnit);
        }
    }
}