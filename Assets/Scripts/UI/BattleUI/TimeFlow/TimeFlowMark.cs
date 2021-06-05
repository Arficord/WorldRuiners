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

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        public void Initialize(BattleUnit unit, float moveRange)
        {
            timeEventNameText.text = unit.UnitModel.Name;
            maxPosition = moveRange;
            unit.OnBattleActionTimeChanged += UpdatePosition;
        }

        //Do events
        public void UpdatePosition(float timePosition)
        {
            float newXPosition = maxPosition < timePosition ? maxPosition : timePosition;
            rectTransform.anchoredPosition = new Vector2(newXPosition, 0);
        }
    }
}