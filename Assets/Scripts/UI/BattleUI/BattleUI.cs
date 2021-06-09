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
        [SerializeField] private BattleManager battleManager;
        [SerializeField] private Camera raycastCamera;
        [SerializeField] private UnitInfoWindow unitInfoWindow;
        [SerializeField] private BattleActionMenu battleActionMenu;
        [SerializeField] private TimeFlowPlank timeFlowPlank;
        private RectTransform unitInfoWindowTransform;
        private BattleUnit LookTarget
        {
            get => lookTarget;
            set
            {
                if (lookTarget != null)
                {
                    LookTarget.UnselectView();
                }
                if (value != null)
                {
                    value.SelectView();
                }
                lookTarget = value;
            }
        }
        private BattleUnit lookTarget;
        
        private void Start()
        {
            unitInfoWindowTransform = unitInfoWindow.GetComponent<RectTransform>();
            Initialize();
        }

        private void Update()
        {
            MouseRaycast();
        }

        public void ShowActionMenu(bool show)
        {
            if (show)
            {
                battleActionMenu.Show();
            }
            else
            {
                battleActionMenu.Hide();
            }
        }

        private void Initialize()
        {
            timeFlowPlank.Initialize(battleManager.UnitsInBattle);
            battleActionMenu.Initialize(battleManager);
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
                LookTarget = battleUnit;
            }
            else
            {
                unitInfoWindow.Hide();    
                LookTarget = null;
            }
        }
        
    }

    public interface IBattleUI
    {
        void BeginWaitUserInput();
    }
}