using System;
using System.Collections;
using System.Collections.Generic;
using My.Base.Battle;
using My.Base.Skills;
using My.UI.InfoBlocks;
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
        [SerializeField] private Camera raycastCamera;
        [SerializeField] private UnitInfoWindow unitInfoWindow;
        [SerializeField] private BattleActionMenu battleActionMenu;
        [SerializeField] private TimeFlowPlank timeFlowPlank;
        [SerializeField] private SkillCastingUI skillCastingUI;
        [SerializeField] private UnitHudManager unitHud;
        private BattleManager battleManager;
        
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
        
        private void Awake()
        {
            unitInfoWindowTransform = unitInfoWindow.GetComponent<RectTransform>();
        }

        private void Update()
        {
            MouseRaycastCheck();
            MouseButtonCheck();
        }

        public void Initialize(BattleManager battle)
        {
            gameObject.SetActive(true);
            battleManager = battle;
            state = UIState.Normal;
            timeFlowPlank.Initialize(battleManager.UnitsInBattle);
            battleActionMenu.Initialize(battleManager);
            battleActionMenu.OnNeedToSelectTarget += ShowEnemyUnitTarget;
            skillCastingUI.OnSkillCasted += FinishUseSkillCastingUI;
            unitHud.Initialize(battleManager.UnitsInBattle);
        }

        private void ShowEnemyUnitTarget(Skill ability)
        {
            state = UIState.UsingSkill;
            BattleUnit currentUnit = battleManager.CurrentTurnUnit;
            skillCastingUI.Initialize(currentUnit.UnitModel, ability);
            skillCastingUI.MarkValidTargets(battleManager.UnitsInBattle);
        }

        private void FinishUseSkillCastingUI()
        {
            state = UIState.Normal;
            skillCastingUI.ClearTargetMarks(battleManager.UnitsInBattle);
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