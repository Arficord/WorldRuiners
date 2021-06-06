using System;
using System.Collections;
using System.Collections.Generic;
using My.Base.Units;
using TMPro;
using UnityEngine;

namespace My.Base.Battle
{
    public class TimeFlowMark : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI timeEventNameText;
        private RectTransform rectTransform;
        private float maxPosition;
        private BattleUnit battleUnit;

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
        }
        
        public void UpdatePosition()
        {
            float timePosition = battleUnit.BattleActionTime;
            float newXPosition = maxPosition < timePosition ? maxPosition : timePosition;
            rectTransform.anchoredPosition = new Vector2(newXPosition, 0);
        }

        private void OnDestroy()
        {
            if (battleUnit!=null)
            {
                battleUnit.OnBattleActionTimeChanged -= UpdatePosition;
            }
        }
    }
}