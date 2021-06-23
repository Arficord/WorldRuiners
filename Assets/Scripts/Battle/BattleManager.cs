using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        private Action<BattleResult> onBattleEndedCallback;
        private Coroutine battleCycleCoroutine;

        public void StartBattle(List<Unit> participants, Action<BattleResult> onBattleEnded)
        {
            onBattleEndedCallback += onBattleEnded;
            PlayerUnitInput = new BattleUnitController();
            foreach (var unit in participants)
            {
                SpawnUnit(unit);
            }
            battleCycleCoroutine = StartCoroutine(BattleCycle());
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
        
        private void CheckBattleResult()
        {
            if (UnitsInBattle == null || UnitsInBattle.Count == 0)
            {
                Debug.Log("All units is dead");
                DeclareBattleResult(Team.None, false);
                return;
            }
            if (IsAllUnitsFromOneTeam())
            {
                Team remainedTeam = UnitsInBattle[0].UnitModel.RealTeam;
                Debug.Log($"Only one team remains {remainedTeam}");
                DeclareBattleResult(remainedTeam, false);
            }
        }

        private void DeclareBattleResult(Team winner, bool isFlee)
        {
            BattleResult result = new BattleResult(winner, isFlee);
            battleUI.ShowBattleResult(result, onBattleEndedCallback);
            StopCoroutine(battleCycleCoroutine);
            ClearBattle();
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
            battleUnit.OnKilled += () => OnUnitDie(battleUnit);
        }

        private void OnUnitDie(BattleUnit battleUnit)
        {
            UnitsInBattle.Remove(battleUnit);
            CheckBattleResult();
        }

        private void RemoveUnit(BattleUnit battleUnit)
        {
            UnitsInBattle.Remove(battleUnit);
            battleUnit.Remove();
        }

        private void ClearBattle()
        {
            while (UnitsInBattle.Count > 0)
            {
                RemoveUnit(UnitsInBattle[0]);
            }
        }
        
        private bool IsAllUnitsFromOneTeam()
        {
            Team firstUnitTeam = UnitsInBattle[0].UnitModel.RealTeam;
            return UnitsInBattle.All(unit => firstUnitTeam == unit.UnitModel.RealTeam);
        }
    }
}