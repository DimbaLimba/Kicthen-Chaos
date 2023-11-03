using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KichenGameManager : MonoBehaviour
{
    public static KichenGameManager Instance { get; private set; }

    public event EventHandler OnStateChanged;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnPaused;

    private enum Static
    {
        WaitingToStart,
        CountDownToStart,
        GamePlaying,
        GameOver
    }

    private Static _static;
    private float _waitingToStartTimer = 1f;
    private float _countDownToStartTimer = 3f;
    private float _gamePlayigTimer;
    private float _gamePlayigTimerMax = 100f;
    private bool _isGamePaused = false;

    private void Awake()
    {
        Instance = this;
        _static = Static.WaitingToStart;
    }

    private void Start()
    {
        GameInput.Instance.OnPauseActive += GameInput_OnPauseActive;
    }

    private void GameInput_OnPauseActive(object sender, EventArgs e)
    {
        TogglePauseGame();
    }

    private void Update()
    {
        switch (_static)
        {
            case Static.WaitingToStart:
                _waitingToStartTimer -= Time.deltaTime;
                if (_waitingToStartTimer < 0f)
                {
                    _static = Static.CountDownToStart;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case Static.CountDownToStart:
                _countDownToStartTimer -= Time.deltaTime;
                if (_countDownToStartTimer < 0f)
                {
                    _static = Static.GamePlaying;
                    _gamePlayigTimer = _gamePlayigTimerMax;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case Static.GamePlaying:
                _gamePlayigTimer -= Time.deltaTime;
                if (_gamePlayigTimer < 0f)
                {
                    _static = Static.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case Static.GameOver:
                break;
        }
    }

    public void TogglePauseGame()
    {
        _isGamePaused = !_isGamePaused;
        if (_isGamePaused)
        {
            Time.timeScale = 0f;

            OnGamePaused(this, EventArgs.Empty);
        }
        else
        {
            Time.timeScale = 1f;

            OnGameUnPaused(this, EventArgs.Empty);
        }
    }

    public bool IsGamePlaying()
    {
        return _static == Static.GamePlaying;
    }

    public bool IsCountdownToStartActive()
    {
        return _static == Static.CountDownToStart;
    }

    public float GetCountdownToStartTimer()
    {
        return _countDownToStartTimer;
    }

    public bool IsGameOver()
    {
        return _static == Static.GameOver;
    }

    public float GetGamePlayigTimerNormalize()
    {
        return 1 - (_gamePlayigTimer / _gamePlayigTimerMax);
    }
}
