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
        private BattleUnit battleUnit;
        private RectTransform rectTransform;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        public void Initialize(BattleUnit unit)
        {
            battleUnit = unit;
            timeEventNameText.text = unit.UnitModel.Name;
        }

        //Do events
        public void UpdatePosition(float maxPosition)
        {
            float newXPosition = maxPosition < battleUnit.BattleActionTime ? maxPosition : battleUnit.BattleActionTime;
            rectTransform.anchoredPosition = new Vector2(newXPosition, 0);
        }
    }
}