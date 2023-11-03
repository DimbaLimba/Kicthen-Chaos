using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI Instance { get; private set; }

    [SerializeField] private Button _soundEffectButton;
    [SerializeField] private Button _musicButton;
    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _moveUpButton;
    [SerializeField] private Button _moveDownButton;
    [SerializeField] private Button _moveRightButton;
    [SerializeField] private Button _moveLeftButton;
    [SerializeField] private Button _interactButton;
    [SerializeField] private Button _interactAlternativeButton;
    [SerializeField] private Button _pauseButton;

    [SerializeField] private TextMeshProUGUI _soundEffectText;
    [SerializeField] private TextMeshProUGUI _musicText;
    [SerializeField] private TextMeshProUGUI _moveUpText;
    [SerializeField] private TextMeshProUGUI _moveDownText;
    [SerializeField] private TextMeshProUGUI _moveRightText;
    [SerializeField] private TextMeshProUGUI _moveLeftText;
    [SerializeField] private TextMeshProUGUI _interactText;
    [SerializeField] private TextMeshProUGUI _interactAlternativeText;
    [SerializeField] private TextMeshProUGUI _pauseText;

    private void Awake()
    {
        Instance = this;

        _soundEffectButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.ChangedVolume();
            UpdateVisual();
        });

        _musicButton.onClick.AddListener(() =>
        {
            MusicManager.Instance.ChangedVolume();
            UpdateVisual();
        });

        _closeButton.onClick.AddListener(() =>
        {
            Hide();
        });
    }

    private void Start()
    {
        KichenGameManager.Instance.OnGameUnPaused += KichenGameManager_OnGameUnPaused;

        UpdateVisual();

        Hide();
    }

    private void KichenGameManager_OnGameUnPaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void UpdateVisual()
    {
        _soundEffectText.text = "Sount Effects: " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f);
        _musicText.text = "Music: " + Mathf.Round(MusicManager.Instance.GetVolume() * 10f);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
