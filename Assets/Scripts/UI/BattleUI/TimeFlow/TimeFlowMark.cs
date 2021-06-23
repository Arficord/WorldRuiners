using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using My.Base.Units;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace My.Base.Battle
{
    public class TimeFlowMark : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI timeEventNameText;
        [SerializeField] private Image plankImage;
        [SerializeField] private Color normalColor;
        [SerializeField] private Color currentColor;
        private RectTransform rectTransform;
        private float maxPosition;
        private BattleUnit battleUnit;
        private Tween moveTween;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        public void Initialize(BattleUnit unit, float moveRange)
        {
            battleUnit = unit;
            timeEventNameText.text = battleUnit.UnitModel.Name;
            maxPosition = moveRange;
            battleUnit.OnBattleActionTimeChanged += UpdatePosition;
            battleUnit.OnThisUnitSelectedView += MarkAsSelected;
            battleUnit.OnThisUnitUnselectedView += MarkAsUnselected;
            battleUnit.OnDestroyed += PlayDestroyAnimation;
        }

        private void UpdatePosition()
        {
            float timePosition = battleUnit.BattleActionTime;
            float newXPosition = maxPosition < timePosition ? maxPosition : timePosition;
            moveTween = rectTransform.DOAnchorPosX(newXPosition, BattleManager.BATTLE_TICK_TIME);
        }

        private void MarkAsSelected()
        {
            plankImage.color = currentColor;
            timeEventNameText.color = currentColor;
        }
        
        private void MarkAsUnselected()    
        {
            plankImage.color = normalColor;
            timeEventNameText.color = normalColor;
        }
        
        private void PlayDestroyAnimation()
        {
            Destroy(gameObject);
        }
        
        private void OnDestroy()
        {
            //if not instantiated
            if (battleUnit == null)
            {
                return;
            }

            moveTween.Kill();
            battleUnit.OnBattleActionTimeChanged -= UpdatePosition;
            battleUnit.OnThisUnitSelectedView -= MarkAsSelected;
            battleUnit.OnThisUnitUnselectedView -= MarkAsUnselected;
        }
    }
}