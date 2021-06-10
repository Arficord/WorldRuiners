using System;
using System.Collections;
using System.Collections.Generic;
using My.Base.Units;
using UnityEngine;
using UnityEngine.Serialization;

namespace My.Base.Battle
{
    public class BattleUnit : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private GameObject targetMark;
        
        public Unit UnitModel { get; set; }
        public Team RealTeam { get; set; }
        public Team MindedTeam { get; set; }
        
        public bool IsPlayerCanControl { get; set; }
        public IUnitInput PlayInput { get; set; }
        
        public float BattleActionTime
        {
            get=>battleActionTime;
            set
            {
                battleActionTime = value;
                OnBattleActionTimeChanged?.Invoke();
            }
        }
        public event Action OnBattleActionTimeChanged;
        public event Action OnThisUnitTurnStarted;
        public event Action OnThisUnitTurnEnded;
        public event Action OnThisUnitSelectedView;
        public event Action OnThisUnitUnselectedView;
        
        
        private float battleActionTime;

        public void Initialize(Unit unitModel, Team unitTeam)
        {
            UnitModel = unitModel;
            RealTeam = unitTeam;
            MindedTeam = RealTeam;
            //TODO: Must be calculated from parameters and relationship 
            IsPlayerCanControl = true;
        }

        public void PlayTurn(BattleManager battle)
        {
            OnThisUnitTurnStarted?.Invoke();
            PlayInput?.PlayTurn(battle, this);
        }

        public void EndTurn()
        {
            OnThisUnitTurnEnded?.Invoke();
        }

        public void SelectView()
        {
            OnThisUnitSelectedView?.Invoke();
        }
        
        public void UnselectView()
        {
            OnThisUnitUnselectedView?.Invoke();
        }

        public void ShowTargetMark(bool show)
        {
            targetMark.SetActive(show);
        }
        
        public void IncreaseTimePlaceByParameters()
        {
            BattleActionTime += UnitModel.CurrentAttributes.Speed;
        }
    }
}