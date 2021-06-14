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
    [SerializeField] private SkillSelectItem skillSelectItemPrefab;
    [SerializeField] private Transform skillContainer;
    public event Action<Skill> OnNeedToSelectTarget;
    private BattleManager battleManager;
    
        
    public void Initialize(BattleManager battle)
    {
        battleManager = battle;
        battleManager.PlayerUnitInput.OnNeedInput += OnPlayerNeedsInput;
        skipTurnButton.onClick.AddListener(OnSkipTurnButtonPressed);
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
        //TODO: replace with recycling scroll
        foreach (Transform child in skillContainer.transform) {
            Destroy(child.gameObject);
        }

        foreach (var skill in unit.UnitModel.Skills)
        {
            SkillSelectItem skillItem = Instantiate(skillSelectItemPrefab, skillContainer);
            skillItem.Initialize(skill, OnSkillItemPressed);
        }
        Show();
    }

    private void OnSkipTurnButtonPressed()
    {
        Debug.Log("Pressed Skip turn battle");
        battleManager.SkipTurn();
        Hide();
    }
    
    private void OnSkillItemPressed(Skill skill)
    {
        Debug.Log("Pressed skill item");
        Hide();
        OnNeedToSelectTarget?.Invoke(skill);
    }

    private void OnDestroy()
    {
        battleManager.PlayerUnitInput.OnNeedInput -= OnPlayerNeedsInput;
        skipTurnButton.onClick.RemoveListener(OnSkipTurnButtonPressed);
    }
}
