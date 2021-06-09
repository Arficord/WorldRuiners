using System;
using System.Collections;
using System.Collections.Generic;
using My.Base.Battle;
using UnityEngine;
using UnityEngine.UI;

public class BattleActionMenu : MonoBehaviour, IShowHide
{
    [SerializeField] private Button skipTurnButton;
    private List<Action> unsubscribeActions = new List<Action>();
    private BattleManager battleManager;
    public void Initialize(BattleManager battle)
    {
        battleManager = battle;
        battleManager.PlayerUnitInput.OnNeedInput += OnPlayerNeedsInput;
        unsubscribeActions.Add(() => battleManager.PlayerUnitInput.OnNeedInput -= OnPlayerNeedsInput);
        skipTurnButton.onClick.AddListener(OnSkipTurnButtonPressed);
        unsubscribeActions.Add( () =>skipTurnButton.onClick.RemoveListener(OnSkipTurnButtonPressed) );
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
    
    private void OnDestroy()
    {
        foreach (Action unsubscribe in unsubscribeActions)
        {
            unsubscribe();
        }
        unsubscribeActions.Clear();
    }
}
