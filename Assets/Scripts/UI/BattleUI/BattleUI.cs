using System;
using System.Collections;
using System.Collections.Generic;
using My.Base.Battle;
using My.UI.Windows;
using UnityEngine;

namespace My.UI
{
    public class BattleUI : MonoBehaviour
    {
        [SerializeField] private Camera raycastCamera;
        [SerializeField] private UnitInfoWindow unitInfoWindow;
        [SerializeField] private TimeFlowPlank timeFlowPlank;
        private RectTransform unitInfoWindowTransform;

        private void Start()
        {
            unitInfoWindowTransform = unitInfoWindow.GetComponent<RectTransform>();
        }

        private void Update()
        {
            MouseRaycast();
        }

        public void Initialize(List<BattleUnit> units)
        {
            timeFlowPlank.Initialize(units);
        }

        private void MouseRaycast()
        {
            //if performance problems: do not update unitInfoWindow view every frame. Do events
            //Use something like Physics2D.GetRayIntersectionNonAlloc (will be deprecated in a future build)
            Vector2 mousePosition = Input.mousePosition;
            Ray raycastRay = raycastCamera.ScreenPointToRay(mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(raycastRay);
            
            if (hit.transform!=null)
            {
                BattleUnit battleUnit = hit.transform.GetComponent<BattleUnit>();
                if(battleUnit==null)
                    return;
                
                unitInfoWindowTransform.position = mousePosition;
                unitInfoWindow.UpdateView(battleUnit.UnitModel);
                unitInfoWindow.Show();
            }
            else
            {
                unitInfoWindow.Hide();
            }
        }
        
    }
}