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

    private enum State
    {
        WaitingToStart,
        CountDownToStart,
        GamePlaying,
        GameOver
    }

    private State _state;
    private float _countDownToStartTimer = 3f;
    private float _gamePlayigTimer;
    private float _gamePlayigTimerMax = 60f;
    private bool _isGamePaused = false;

    private void Awake()
    {
        Instance = this;
        _state = State.WaitingToStart;
    }

    private void Start()
    {
        GameInput.Instance.OnPauseActive += GameInput_OnPauseActive;
        GameInput.Instance.OnInteractActive += GameInput_OnInteractActive;
    }

    private void GameInput_OnInteractActive(object sender, EventArgs e)
    {
        if (_state == State.WaitingToStart)
        {
            _state = State.CountDownToStart;
            OnStateChanged?.Invoke(this, new EventArgs()); 
        }
    }

    private void GameInput_OnPauseActive(object sender, EventArgs e)
    {
        TogglePauseGame();
    }

    private void Update()
    {
        switch (_state)
        {
            case State.CountDownToStart:
                _countDownToStartTimer -= Time.deltaTime;
                if (_countDownToStartTimer < 0f)
                {
                    _state = State.GamePlaying;
                    _gamePlayigTimer = _gamePlayigTimerMax;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GamePlaying:
                _gamePlayigTimer -= Time.deltaTime;
                if (_gamePlayigTimer < 0f)
                {
                    _state = State.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GameOver:
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
        return _state == State.GamePlaying;
    }

    public bool IsCountdownToStartActive()
    {
        return _state == State.CountDownToStart;
    }

    public float GetCountdownToStartTimer()
    {
        return _countDownToStartTimer;
    }

    public bool IsGameOver()
    {
        return _state == State.GameOver;
    }

    public float GetGamePlayigTimerNormalize()
    {
        return 1 - (_gamePlayigTimer / _gamePlayigTimerMax);
    }
}
