using System;
using System.Collections;
using System.Collections.Generic;
using My.Base.Battle;
using My.Base.Units;
using UnityEngine;
using UnityEngine.UI;

public class BattleActionMenu : MonoBehaviour, IShowHide
{
    [SerializeField] private Button skipTurnButton;
    [SerializeField] private Button attackButton;
    [SerializeField] private Button skillButton;
    public event Action<Skill> OnNeedToSelectTarget;
    private BattleManager battleManager;
    
        
    public void Initialize(BattleManager battle)
    {
        battleManager = battle;
        battleManager.PlayerUnitInput.OnNeedInput += OnPlayerNeedsInput;
        skipTurnButton.onClick.AddListener(OnSkipTurnButtonPressed);
        attackButton.onClick.AddListener(OnAttackButtonPressed);
        skillButton.onClick.AddListener(OnSkillButtonPressed);
    }

    public void Hide()
    {
        //TODO: Closing animation
        gameObject.SetActive(false);
    }

    public void Show()
    {
        //TODO: Opening animation
        gameObject.SetActive(true);
    }
    
    private void OnPlayerNeedsInput(BattleUnit unit)
    {
        Show();
    }

    private void OnSkipTurnButtonPressed()
    {
        Debug.Log("Pressed Skip turn battle");
        battleManager.SkipTurn();
        Hide();
    }
    
    private void OnAttackButtonPressed()
    {
        Debug.Log("Pressed Attack");
        Hide();
        
        Skill simpleAttack = new PhysicalAttackSkill();
        OnNeedToSelectTarget?.Invoke(simpleAttack);
    }
    
    private void OnSkillButtonPressed()
    {
        Debug.Log("Pressed Skill");
    }
    
    private void OnDestroy()
    {
        battleManager.PlayerUnitInput.OnNeedInput -= OnPlayerNeedsInput;
        skipTurnButton.onClick.RemoveListener(OnSkipTurnButtonPressed);
        attackButton.onClick.RemoveListener(OnAttackButtonPressed);
        skillButton.onClick.RemoveListener(OnSkillButtonPressed);
    }
}
