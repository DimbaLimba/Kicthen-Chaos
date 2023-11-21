using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter, IHasProgress
{
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;

    public class OnStateChangedEventArgs : EventArgs
    {
        public State state;
    }

    public enum State
    {
        Idle,
        Frying,
        Fried,
        Burned
    }

    [SerializeField] private FlyingRecipeSO[] _flyingRecipeArray;
    [SerializeField] private BurningRecipeSO[] _burningRecipeArray;

    private State _state;
    private float _fryingTimer;
    private float _burnedTimer;
    private FlyingRecipeSO _flyingRecipeSO;
    private BurningRecipeSO _burningRecipeSO;

    private void Start()
    {
        _state = State.Idle;
    }

    private void Update()
    {
        if (HasKichenObject())
        {
            switch (_state)
            {
                case State.Idle:

                    break;
                case State.Frying:
                    _fryingTimer += Time.deltaTime;

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = _fryingTimer / _flyingRecipeSO.timerFlyingMax
                    });

                    if (_fryingTimer > _flyingRecipeSO.timerFlyingMax)
                    {
                        _fryingTimer = 0f;

                        GetKichenObject().DestroySelf();

                        KichenObject.SpawnKichenObject(_flyingRecipeSO.output, this);

                        _state = State.Fried;
                        _burnedTimer = 0f;
                        _burningRecipeSO = GetBurningRecipeSOWithInput(GetKichenObject().GetKicthenObjectSO());

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            state = _state
                        });
                    }
                    break;
                case State.Fried:
                    _burnedTimer += Time.deltaTime;

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = _burnedTimer / _burningRecipeSO.burningTimerMax
                    });

                    if (_burnedTimer > _burningRecipeSO.burningTimerMax)
                    {
                        GetKichenObject().DestroySelf();

                        KichenObject.SpawnKichenObject(_burningRecipeSO.output, this);

                        _state = State.Burned;

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            state = _state
                        });
                    }
                    break;
                case State.Burned:
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = 0f
                    });
                    break;


            }
        }
    }

    public override void Interact(Player player)
    {
        if (!HasKichenObject())
        {
            if (player.HasKichenObject())
            {
                if (HasRecipeWithInput(player.GetKichenObject().GetKicthenObjectSO()))
                {
                    player.GetKichenObject().SetKichenObjectParent(this);
                    _flyingRecipeSO = GetFlyingRecipeSOWithInput(GetKichenObject().GetKicthenObjectSO());

                    _fryingTimer = 0;
                    _state = State.Frying;
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                    {
                        state = _state
                    });

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = _fryingTimer / _flyingRecipeSO.timerFlyingMax
                    });

                }
            }
        }
        else
        {
            if (player.HasKichenObject())
            {
                if (player.GetKichenObject().TryGetPlate(out PlateKichenObject plateKichenObjec))
                {
                    if (plateKichenObjec.TryAddIngrediend(GetKichenObject().GetKicthenObjectSO()))
                    {
                        GetKichenObject().DestroySelf();


                        _state = State.Idle;

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            state = _state
                        });

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                        {
                            progressNormalized = 0f
                        });
                    }

                }
            }
            else
            {
                GetKichenObject().SetKichenObjectParent(player);
                _state = State.Idle;

                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                {
                    state = _state
                });

                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                {
                    progressNormalized = 0f
                });
            }
        }
    }

    private bool HasRecipeWithInput(KicthenObjectSO inputKicthenObjectSO)
    {
        foreach (FlyingRecipeSO flyingReicipeSO in _flyingRecipeArray)
        {
            if (flyingReicipeSO.input == inputKicthenObjectSO)
            {
                return true;
            }
        }
        return false;
    }

    private KicthenObjectSO GetOutputForInput(KicthenObjectSO inputKicthenObjectSO)
    {
        FlyingRecipeSO flyingReicipeSO = GetFlyingRecipeSOWithInput(inputKicthenObjectSO);

        if (flyingReicipeSO != null)
        {
            return flyingReicipeSO.output;
        }
        else
        {
            return null;
        }
    }

    private FlyingRecipeSO GetFlyingRecipeSOWithInput(KicthenObjectSO inputKicthenObjectSO)
    {
        foreach (FlyingRecipeSO flyingReicipeSO in _flyingRecipeArray)
        {
            if (flyingReicipeSO.input == inputKicthenObjectSO)
            {
                return flyingReicipeSO;
            }
        }
        return null;
    }

    private BurningRecipeSO GetBurningRecipeSOWithInput(KicthenObjectSO inputKicthenObjectSO)
    {
        foreach (BurningRecipeSO burningReicipeSO in _burningRecipeArray)
        {
            if (burningReicipeSO.input == inputKicthenObjectSO)
            {
                return burningReicipeSO;
            }
        }
        return null;
    }

    public bool IsFried()
    {
        return _state == State.Fried;
    }
}
