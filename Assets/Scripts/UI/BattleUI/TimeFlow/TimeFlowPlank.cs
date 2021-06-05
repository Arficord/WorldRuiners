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
        [SerializeField] private float plankSize;

        public void Initialize(List<BattleUnit> units)
        {
            Transform transformCashed = transform;
            foreach (var unit in units)
            {
                TimeFlowMark mark = Instantiate(timeFlowMarkPrefab, transformCashed);
                mark.Initialize(unit, plankSize);
            }
        }
    }
}