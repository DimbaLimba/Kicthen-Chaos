using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;
     
    [SerializeField] private KicthenObjectSO _plateKichenObjectSO;

    private float _spawnPlateTimer;
    private float _spawnPlateTimerMax = 4f;
    private int _platesSpawnAmount;
    private int _platesSpawnAmountMax = 4;

    private void Update()
    {
        _spawnPlateTimer += Time.deltaTime;

        if (_spawnPlateTimer > _spawnPlateTimerMax)
        {
            _spawnPlateTimer = 0f;

            if (_platesSpawnAmount < _platesSpawnAmountMax)
            {
                _platesSpawnAmount++;
                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(Player player)
    {
        if (!player.HasKichenObject())
        {
            if (_platesSpawnAmount > 0)
            {
                _platesSpawnAmount--;

                KichenObject.SpawnKichenObject(_plateKichenObjectSO, player);

                OnPlateRemoved?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
