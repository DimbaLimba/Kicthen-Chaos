using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStartCountdownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _countdownText;

    private const string NUMBER_POPUP = "NumberPopup";

    private Animator _animator;
    private int _previousCountdownNumber;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        KichenGameManager.Instance.OnStateChanged += Instance_OnStateChanged;

        Hide();
    }

    private void Instance_OnStateChanged(object sender, System.EventArgs e)
    {
        if (KichenGameManager.Instance.IsCountdownToStartActive())
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Update()
    {
        int countdownNumber = Mathf.CeilToInt(KichenGameManager.Instance.GetCountdownToStartTimer());
        _countdownText.text = countdownNumber.ToString();

        if (_previousCountdownNumber != countdownNumber)
        {
            _previousCountdownNumber = countdownNumber;
            _animator.SetTrigger(NUMBER_POPUP);
            SoundManager.Instance.PlayCountdownSound();
        }
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
