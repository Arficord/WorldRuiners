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
        public Unit UnitModel { get; set; }
        public Team UnitTeam { get; set; }

        public float BattleActionTime
        {
            get=>battleActionTime;
            set
            {
                battleActionTime = value;
                OnBattleActionTimeChanged?.Invoke(battleActionTime);
            }
        }
        public Action<float> OnBattleActionTimeChanged;
        private float battleActionTime;

        public void Initialize(Unit unitModel, Team unitTeam)
        {
            UnitModel = unitModel;
            UnitTeam = unitTeam;
        }
        
        public void IncreaseTimePlaceByParameters()
        {
            BattleActionTime += UnitModel.CurrentAttributes.Speed;
        }
    }
}