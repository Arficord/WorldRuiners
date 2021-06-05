using System;
using System.Collections;
using System.Collections.Generic;
using My.Base.Units;
using My.UI.Windows;
using UnityEngine;

namespace My.Base.Battle
{
    //TODO: Needs to be Refactored
    public class BattleManager : MonoBehaviour
    {
        [SerializeField] private BattleFieldManager battleField;
        private List<BattleUnit> unitsInBattle = new List<BattleUnit>();

        private BattleUnit currentTurnUnit = null;
        private bool isWaitingForUnitPlay = false;
        
        private const float BATTLE_ACTION_TIME_CUP = 1000;
        private const float BATTLE_TICK_TIME = 0.1f;

        private void Start()
        {
            SpawnUnit(UnitTypes.TestMage, 12, Team.First);
            SpawnUnit(UnitTypes.TestWarrior, 34, Team.First);
            SpawnUnit(UnitTypes.TestTank, 56, Team.First);
            
            SpawnUnit(UnitTypes.TestMage, 78, Team.Second);
            SpawnUnit(UnitTypes.TestWarrior, 30, Team.Second);
            SpawnUnit(UnitTypes.TestTank, 59, Team.Second);

            StartCoroutine(BattleCycle());
        }

        private void Update()
        {
            //imitate Computer's play / Player input
            if (Input.GetKeyDown(KeyCode.Space))
            {
                currentTurnUnit = null;
                isWaitingForUnitPlay = false;
            }
        }

        private IEnumerator BattleCycle()
        {
            while (true)
            {
                if (isWaitingForUnitPlay)
                {
                    yield return new WaitForEndOfFrame();
                    continue;
                }                
                if (TryToGiveTurnToUnit())
                {
                    yield return new WaitForEndOfFrame();
                    continue;
                }

                ProceedBattleTick();
                yield return new WaitForSeconds(BATTLE_TICK_TIME);
            }
        }
        
        private void ProceedBattleTick()
        {
            Debug.Log("Battle Tick");
            foreach (var unit in unitsInBattle)
            {
                unit.IncreaseTimePlaceByParameters();
            }
        }

        private bool TryToGiveTurnToUnit()
        {
            foreach (var unit in unitsInBattle)
            {
                if (unit.BattleActionTime >= BATTLE_ACTION_TIME_CUP)
                {
                    Debug.Log($"Currently moves {unit.UnitTeam.ToString()} - {unit.name} | {unit.UnitModel.CurrentAttributes.Speed}");
                    unit.BattleActionTime -= BATTLE_ACTION_TIME_CUP;
                    currentTurnUnit = unit;
                    isWaitingForUnitPlay = true;
                    return true;
                }
            }
            return false;
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
                Debug.Log($"Dont got place to spawn unit {unit.Name} in team {team}");
            }
            BattleUnit battleUnit = Instantiate(loadedResource, placeToSpawn);
            battleUnit.Initialize(unit, team);
            unitsInBattle.Add(battleUnit);
        }
    }
}