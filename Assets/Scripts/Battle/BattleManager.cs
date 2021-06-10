using System;
using System.Collections;
using System.Collections.Generic;
using My.Base.Units;
using My.UI;
using My.UI.Windows;
using UnityEngine;

namespace My.Base.Battle
{
    //TODO: Needs to be Refactored
    public class BattleManager : MonoBehaviour
    {
        [SerializeField] private BattleFieldManager battleField;
        public IUnitInput PlayerUnitInput;
        public List<BattleUnit> UnitsInBattle { get; private set; } = new List<BattleUnit>();
        public BattleUnit CurrentTurnUnit { get; private set; } = null;
        private bool isWaitingForUnitPlay = false;
        private const float BATTLE_ACTION_TIME_CUP = 1000;
        private const float BATTLE_TICK_TIME = 0.1f;
        
        //TODO:REMOVE. Test tempo method
        private void Awake()
        {
            SpawnUnit(UnitTypes.TestMage, 12, Team.First);
            SpawnUnit(UnitTypes.TestWarrior, 34, Team.First);
            SpawnUnit(UnitTypes.TestTank, 56, Team.First);
            
            SpawnUnit(UnitTypes.TestMage, 78, Team.Second);
            SpawnUnit(UnitTypes.TestWarrior, 30, Team.Second);
            SpawnUnit(UnitTypes.TestTank, 59, Team.Second);

            PlayerUnitInput = new BattleUnitController();
            foreach (var unit in UnitsInBattle)
            {
                unit.PlayInput = PlayerUnitInput;
            }

            StartBattle();
        }

        public void SkipTurn()
        {
            Debug.Log("Skip Turn");
            EndTurn();    
        }
        
        private void StartBattle()
        {
            StartCoroutine(BattleCycle());
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
            foreach (var unit in UnitsInBattle)
            {
                unit.IncreaseTimePlaceByParameters();
            }
        }

        private bool TryToGiveTurnToUnit()
        {
            foreach (var unit in UnitsInBattle)
            {
                if (unit.BattleActionTime >= BATTLE_ACTION_TIME_CUP)
                {
                    StartTurn(unit);
                    return true;
                }
            }
            return false;
        }

        private void StartTurn(BattleUnit unit)
        {
            Debug.Log($"Currently moves {unit.RealTeam.ToString()} - {unit.name} | {unit.UnitModel.CurrentAttributes.Speed}");
            CurrentTurnUnit = unit;
            isWaitingForUnitPlay = true;
            CurrentTurnUnit.PlayTurn(this);
        }
        
        private void EndTurn()
        {
            if (CurrentTurnUnit == null)
            {
                Debug.LogError("Trying to end the turn. But current unit is missing!");
                return;
            }
            CurrentTurnUnit.BattleActionTime -= BATTLE_ACTION_TIME_CUP; 
            CurrentTurnUnit.EndTurn();
            CurrentTurnUnit = null;
            isWaitingForUnitPlay = false;
        }
        
        //Tempo unit spawn method 
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
            UnitsInBattle.Add(battleUnit);
        }
    }
}