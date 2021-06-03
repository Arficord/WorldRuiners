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
        private void Start()
        {
            SpawnUnit(UnitTypes.TestMage, 17, battleField.GetEmptyPlace(Team.First));
            SpawnUnit(UnitTypes.TestWarrior, 727, battleField.GetEmptyPlace(Team.First));
            SpawnUnit(UnitTypes.TestTank, 737, battleField.GetEmptyPlace(Team.First));
            
            SpawnUnit(UnitTypes.TestMage, 17, battleField.GetEmptyPlace(Team.Second));
            SpawnUnit(UnitTypes.TestWarrior, 727, battleField.GetEmptyPlace(Team.Second));
            SpawnUnit(UnitTypes.TestTank, 737, battleField.GetEmptyPlace(Team.Second));
        }

        private void SpawnUnit(UnitTypes unitType, int level, Transform container)
        {
            SpawnUnit(UnitFactory.GetNewUnit(unitType, level), container);
        }

        private void SpawnUnit(Unit unit, Transform container)
        {
            BattleUnit loadedResource = Resources.Load<BattleUnit>($"BattleUnits/{unit.UnitType.ToString()}");
            BattleUnit battleUnit = Instantiate(loadedResource, container);
            battleUnit.UnitModel = unit;
        }
    }
}