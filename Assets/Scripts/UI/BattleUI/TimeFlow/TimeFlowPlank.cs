using System;
using System.Collections;
using System.Collections.Generic;
using My.Base.Battle;
using UnityEngine;

namespace My.Base.Battle
{
    public class TimeFlowPlank : MonoBehaviour
    {
        [SerializeField] private TimeFlowMark timeFlowMarkPrefab;
        private RectTransform transformCashed;
        private float plankSize;
        private void Awake()
        {
            transformCashed = GetComponent<RectTransform>();
            plankSize = transformCashed.rect.width;
        }

        public void Initialize(List<BattleUnit> units)
        {
            foreach (var unit in units)
            {
                TimeFlowMark mark = Instantiate(timeFlowMarkPrefab, transformCashed);
                mark.Initialize(unit);
            }
        }

        //test method replace with events
        private void Update()
        {
            foreach (Transform child in transform)
            {
                child.GetComponent<TimeFlowMark>().UpdatePosition(plankSize);
            }
        }
    }
}