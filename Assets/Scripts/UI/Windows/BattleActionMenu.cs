using System;
using System.Collections;
using System.Collections.Generic;
using My.Base.Battle;
using UnityEngine;
using UnityEngine.UI;

public class BattleActionMenu : MonoBehaviour
{
    [SerializeField] private Button skipTurnButton;
    private List<Action> unsubscribeActions = new List<Action>();
    
    public void Initialize(BattleManager battleManager)
    {
        skipTurnButton.onClick.AddListener(battleManager.SkipTurn);
        unsubscribeActions.Add( () => skipTurnButton.onClick.RemoveListener(battleManager.SkipTurn) );
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
    
    private void OnDestroy()
    {
        foreach (Action unsubscribe in unsubscribeActions)
        {
            unsubscribe();
        }
        unsubscribeActions.Clear();
    }
}
