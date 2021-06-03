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
        public UnitInfoWindow unitInfoWindow;
        public BattleUnit battleUnitPrefab;

        [SerializeField] private Transform[] leftTeamPoints;
        [SerializeField] private Transform[] rightTeamPoints;
        
        private void Start()
        {
            SpawnUnit(UnitTypes.TestMage, 777, leftTeamPoints[0]);
            SpawnUnit(UnitTypes.TestTank, 11, leftTeamPoints[1]);
            SpawnUnit(UnitTypes.TestWarrior, 93, leftTeamPoints[2]);
            
            SpawnUnit(UnitTypes.TestMage, 77, rightTeamPoints[0]);
            SpawnUnit(UnitTypes.TestTank, 113, rightTeamPoints[1]);
            SpawnUnit(UnitTypes.TestWarrior, 3, rightTeamPoints[2]);
        }

        private void SpawnUnit(UnitTypes unitType, int level,Transform container)
        {
            SpawnUnit(UnitFactory.GetNewUnit(unitType, level), container);
        }

        private void SpawnUnit(Unit unit, Transform container)
        {
            BattleUnit loadedResource = Resources.Load<BattleUnit>(unit.UnitType.ToString());
            BattleUnit battleUnit = Instantiate(loadedResource, container);
            battleUnit.UnitModel = unit;
        }
    }
}