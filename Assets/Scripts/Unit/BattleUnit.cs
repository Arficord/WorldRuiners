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
        [SerializeField] private Transform bottomPoint;
        public event Action OnDestroyed;
        public event Action OnKilled;
            
        public Unit UnitModel { get; set; }

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

        public void Initialize(Unit unitModel)
        {
            UnitModel = unitModel;
            unitModel.OnDie += PlayDieAnimation;
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

        public Vector3 GetBottomMarkPosition()
        {
            return bottomPoint.position;
        }
        
        public void IncreaseTimePlaceByParameters()
        {
            BattleActionTime += UnitModel.CurrentAttributes.Speed;
        }
        
        public void PlayDieAnimation()
        {
            OnKilled?.Invoke();
            Remove();
        }

        public void Remove()
        {
            OnDestroyed?.Invoke();
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            UnitModel.OnDie -= PlayDieAnimation;
        }
    }
}