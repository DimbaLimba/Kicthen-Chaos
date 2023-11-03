using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    [SerializeField] private Button _mainMenuButton;
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _optionsButton;

    private void Awake()
    {
        _mainMenuButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.MainMenuScene);
        });

        _resumeButton.onClick.AddListener(() =>
        {
            KichenGameManager.Instance.TogglePauseGame();
        });

        _optionsButton.onClick.AddListener(() =>
        {
            OptionsUI.Instance.Show();
        });
    }

    private void Start()
    {
        KichenGameManager.Instance.OnGamePaused += KichenGameManager_OnGamePaused;
        KichenGameManager.Instance.OnGameUnPaused += KichenGameManager_OnGameUnPaused;

        Hide();
    }

    private void KichenGameManager_OnGameUnPaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void KichenGameManager_OnGamePaused(object sender, System.EventArgs e)
    {
        Show();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
