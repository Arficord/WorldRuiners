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
        private enum UIState
        {
            Normal,
            UsingSkill,
            UsingItem,
        }
        [SerializeField] private BattleManager battleManager;
        [SerializeField] private Camera raycastCamera;
        [SerializeField] private UnitInfoWindow unitInfoWindow;
        [SerializeField] private BattleActionMenu battleActionMenu;
        [SerializeField] private TimeFlowPlank timeFlowPlank;
        [SerializeField] private SkillCastingUI skillCastingUI;
        
        public event Action<BattleUnit> OnClickOnUnit;
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
        private UIState state;
        
        private void Start()
        {
            unitInfoWindowTransform = unitInfoWindow.GetComponent<RectTransform>();
            Initialize();
        }

        private void Update()
        {
            MouseRaycastCheck();
            MouseButtonCheck();
        }

        public void ShowEnemyUnitTarget(Skill ability)
        {
            state = UIState.UsingSkill;
            BattleUnit currentUnit = battleManager.CurrentTurnUnit;
            foreach (var unit in battleManager.UnitsInBattle)
            {
                if (unit == currentUnit)
                {
                    continue;
                }
                if (unit.RealTeam != currentUnit.MindedTeam)
                {
                    unit.ShowTargetMark(true);
                }
            }

            skillCastingUI.Initialize(currentUnit.UnitModel, ability);
        }

        private void Initialize()
        {
            state = UIState.Normal;
            timeFlowPlank.Initialize(battleManager.UnitsInBattle);
            battleActionMenu.Initialize(battleManager);
            battleActionMenu.OnNeedToSelectTarget += ShowEnemyUnitTarget;
            skillCastingUI.OnSkillCasted += FinishUseSkillCastingUI;
        }

        private void FinishUseSkillCastingUI()
        {
            state = UIState.Normal;
            battleManager.SkipTurn();
        }

        private void MouseRaycastCheck()
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

        private void MouseButtonCheck()
        {
            if (Input.GetMouseButtonDown(0))
            {
                switch (state)
                {
                    case UIState.UsingSkill:
                    {
                        if (LookTarget != null)
                        {
                            skillCastingUI.ToggleUnitForSkill(lookTarget.UnitModel);
                        }
                        break;
                    }
                }
            }
        }

        private void OnDestroy()
        {
            battleActionMenu.OnNeedToSelectTarget -= ShowEnemyUnitTarget;
            skillCastingUI.OnSkillCasted -= FinishUseSkillCastingUI;
        }
    }
}