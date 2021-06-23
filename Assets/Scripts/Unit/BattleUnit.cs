using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using My.Base.Units;
using UnityEngine;
using UnityEngine.Serialization;

namespace My.Base.Battle
{
    public class BattleUnit : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private GameObject targetMark;
        [SerializeField] private GameObject selectRing;
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
            unitModel.OnGetDamage += OnGetDamage;
            unitModel.OnGetHealth += OnGetHealth;
            //TODO: Must be calculated from parameters and relationship 
            IsPlayerCanControl = true;
        }

        public void PlayTurn(BattleManager battle)
        {
            selectRing.SetActive(true);
            OnThisUnitTurnStarted?.Invoke();
            PlayInput?.PlayTurn(battle, this);
        }

        public void EndTurn()
        {
            selectRing.SetActive(false);
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
            //TODO: replace sample animation
            spriteRenderer.DOColor(Color.black, 0.2f).OnComplete(OnDieAnimationEnded);
        }

        public void Remove()
        {
            OnDestroyed?.Invoke();
            Destroy(gameObject);
        }

        private void OnGetDamage(Damage damage)
        {
            //TODO: spawn floating damage text
            PlayGetDamageAnimation();
        }

        private void OnGetHealth(float health)
        {
            //TODO: spawn floating damage text
            PlayGetHealthAnimation();
        }
        
        private void PlayGetDamageAnimation()
        {
            //TODO: replace sample animation
            spriteRenderer.DOColor(Color.red, 0.1f).OnComplete(
                () => spriteRenderer.DOColor(Color.white, 0.1f));
        }

        private void PlayGetHealthAnimation()
        {
            //TODO: replace sample animation
            spriteRenderer.DOColor(Color.green, 0.1f).OnComplete(
                () => spriteRenderer.DOColor(Color.white, 0.1f));
        }

        
        private void OnDieAnimationEnded()
        {
            Remove();
        }
        
        private void OnDestroy()
        {
            UnitModel.OnDie -= PlayDieAnimation;
            UnitModel.OnGetDamage -= OnGetDamage;
            UnitModel.OnGetHealth -= OnGetHealth;
        }
    }
}