using System;
using System.Collections;
using System.Collections.Generic;
using My.Base.Unit;
using UnityEngine;

namespace My.Base.Battle
{
    public class BattleManager : MonoBehaviour
    {
        private void Start()
        {
            GameObject gm = new GameObject();
            BattleUnit bu = gm.AddComponent<BattleUnit>();
            bu.unit = UnitFactory.GetNewTestWarrior(10);

            Debug.Log($"cur health: {bu.unit.CurrentAttributes.Health}/{bu.unit.BaseAttributes.Health}");
            bu.unit.GetDamage(5);
            Debug.Log("health -5");
            Debug.Log($"cur health: {bu.unit.CurrentAttributes.Health}/{bu.unit.BaseAttributes.Health}");
        }
    }
}