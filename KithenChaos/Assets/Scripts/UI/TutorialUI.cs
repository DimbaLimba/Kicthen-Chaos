using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _keyMoveUpText;
    [SerializeField] private TextMeshProUGUI _keyMoveDownText;
    [SerializeField] private TextMeshProUGUI _keyMoveLeftText;
    [SerializeField] private TextMeshProUGUI _keyMoveRightText;
    [SerializeField] private TextMeshProUGUI _keyInteractText;
    [SerializeField] private TextMeshProUGUI _keyInteractAlternativeText;
    [SerializeField] private TextMeshProUGUI _keyPauseText;

    private void Start()
    {
        GameInput.Instance.OnBinbingRebind += GameInput_OnBinbingRebind;
        KichenGameManager.Instance.OnStateChanged += KichenGameManager_OnStateChanged;

        Show();

        UpdateVisual(); 
    }

    private void KichenGameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (KichenGameManager.Instance.IsCountdownToStartActive())
        {
            Hide();
        }
    }

    private void GameInput_OnBinbingRebind(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        _keyMoveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);
        _keyMoveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);
        _keyMoveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);
        _keyMoveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);
        _keyInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        _keyInteractAlternativeText.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlternative);
        _keyPauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
    }

    private void Hide()
    {
        gameObject.SetActive(false);  
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
}
