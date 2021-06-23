using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleResultUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI winnerProclamationText;
    [SerializeField] private Button CloseButton;
    private Action onClose;
    private void Awake()
    {
        CloseButton.onClick.AddListener(Hide);
    }

    public void Show(BattleResult result, Action<BattleResult> onWindowClose)
    {
        winnerProclamationText.text = $"{result.Winner.ToString()} team is the winner!";
        CloseButton.onClick.AddListener(()=> onWindowClose(result));
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
        onClose?.Invoke();
    }
}
