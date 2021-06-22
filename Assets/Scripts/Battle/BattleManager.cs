using System;
using System.Collections;
using System.Collections.Generic;
using My.Base.Units;
using My.UI;
using My.UI.Windows;
using UnityEngine;

namespace My.Base.Battle
{
    public class BattleManager : MonoBehaviour
    {
        [SerializeField] private BattleUI battleUI;
        [SerializeField] private BattleFieldManager battleField;
        public IUnitInput PlayerUnitInput;
        public List<BattleUnit> UnitsInBattle { get; private set; } = new List<BattleUnit>();
        public BattleUnit CurrentTurnUnit { get; private set; } = null;
        private bool isWaitingForUnitPlay = false;
        private const float BATTLE_ACTION_TIME_CUP = 1000;
        private const float BATTLE_TICK_TIME = 0.1f;
        
        public void StartBattle(List<Unit> participants)
        {
            PlayerUnitInput = new BattleUnitController();
            foreach (var unit in participants)
            {
                SpawnUnit(unit);
            }
            
            StartCoroutine(BattleCycle());
            battleUI.Initialize(this);
        }

        public void SkipTurn()
        {
            Debug.Log("Skip Turn");
            EndTurn();    
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
            Debug.Log($"Currently moves {unit.UnitModel.RealTeam.ToString()} - {unit.name} | {unit.UnitModel.CurrentAttributes.Speed}");
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
        
        private void SpawnUnit(Unit unit)
        {
            BattleUnit loadedResource = Resources.Load<BattleUnit>($"BattleUnits/{unit.UnitType.ToString()}");
            Transform placeToSpawn = battleField.GetEmptyPlace(unit.RealTeam);
            if (placeToSpawn == null)
            {
                Debug.LogError($"Did not get place to spawn unit {unit.Name} in team {unit.RealTeam}");
                return;
            }

            BattleUnit battleUnit = Instantiate(loadedResource, placeToSpawn);
            battleUnit.Initialize(unit);
            battleUnit.PlayInput = PlayerUnitInput;
            UnitsInBattle.Add(battleUnit);
            battleUnit.UnitModel.OnDie += () => DestroyUnit(battleUnit);
        }

        private void DestroyUnit(BattleUnit battleUnit)
        {
            UnitsInBattle.Remove(battleUnit);
            battleUnit.PlayDieAnimation();
        }
    }
}