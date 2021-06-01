using System;
using System.Collections;
using System.Collections.Generic;
using My.Base.Unit;
using My.UI.Windows;
using UnityEngine;

namespace My.Base.Battle
{
    //TODO: WIP. now this is a test code
    public class BattleManager : MonoBehaviour
    {
        public UnitInfoWindow unitInfoWindow;
        
        private BattleUnit bu;
        private void Start()
        {
            GameObject gm = new GameObject();
            bu = gm.AddComponent<BattleUnit>();
            bu.unit = UnitFactory.GetNewTestWarrior(10);
            bu.unit.CurrentAttributes.ManaRegeneration = 18.23213f;
            bu.unit.CurrentAttributes.Accuracy = 0.2121345f;
            bu.unit.CurrentAttributes.CriticalHitChance = 0.211233f;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                Debug.Log($"cur health: {bu.unit.CurrentAttributes.Health}/{bu.unit.BaseAttributes.Health}");
                bu.unit.GetDamage(5);
                Debug.Log("health -5");
                Debug.Log($"cur health: {bu.unit.CurrentAttributes.Health}/{bu.unit.BaseAttributes.Health}");
            }
            unitInfoWindow.UpdateView(bu.unit);
        }
    }
}