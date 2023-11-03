using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [SerializeField] private AudioClipRefsSO _audioClipRefsSO;

    private float _volume = 1f;

    private void Awake()
    {
        Instance = this;

        _volume = PlayerPrefs.GetFloat("SoundEffectVolume", 1f);
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSecces += DeliveryManager_OnRecipeSecces;
        DeliveryManager.Instance.OnRecipeFalied += DeliveryManager_OnRecipeFalied;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        Player.Instance.OnPickedSomethink += Instance_OnPickedSomethink;
        BaseCounter.OnAnyObjectPlacedHere += BaseCounter_OnAnyObjectPlacedHere;
        TrashCounter.OnAnyObjectTrahed += TrashCounter_OnAnyObjectTrahed;
        PlateKichenObject.OnIngrediendSound += PlateKichenObject_OnIngrediendSound;
    }

    private void PlateKichenObject_OnIngrediendSound(object sender, System.EventArgs e)
    {
        PlaySound(_audioClipRefsSO.objectDrop, Player.Instance.transform.position);
    }

    private void TrashCounter_OnAnyObjectTrahed(object sender, System.EventArgs e)
    {
        TrashCounter trashCounter = sender as TrashCounter;
        PlaySound(_audioClipRefsSO.objectDrop, trashCounter.transform.position);
    }

    private void BaseCounter_OnAnyObjectPlacedHere(object sender, System.EventArgs e)
    {
        BaseCounter baseCounter = sender as BaseCounter;
        PlaySound(_audioClipRefsSO.objectDrop, baseCounter.transform.position);
    }

    private void Instance_OnPickedSomethink(object sender, System.EventArgs e)
    {
        PlaySound(_audioClipRefsSO.oblectPickup, Player.Instance.transform.position);
    }

    private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e)
    {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySound(_audioClipRefsSO.chop, cuttingCounter.transform.position);
    }

    private void DeliveryManager_OnRecipeFalied(object sender, System.EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(_audioClipRefsSO.deliveryFail, deliveryCounter.transform.position);
    }

    private void DeliveryManager_OnRecipeSecces(object sender, System.EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(_audioClipRefsSO.deliverySuccess, deliveryCounter.transform.position);
    }

    private void PlaySound(AudioClip[] audioClip, Vector3 position, float volume = 1f)
    {
        PlaySound(audioClip[Random.Range(0, audioClip.Length)], position, volume);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiplay = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volumeMultiplay * _volume);
    }

    public void PlayFootstepsSound(Vector3 position, float volume = 1f)
    {
        PlaySound(_audioClipRefsSO.footstep, position, volume);
    }

    public void ChangedVolume()
    {
        _volume += 0.1f;
        if (_volume > 1f)
        {
            _volume = 0f;
        }

        PlayerPrefs.SetFloat("SoundEffectVolume", _volume);
        PlayerPrefs.Save();
    }

    public float GetVolume()
    {
        return _volume;
    }
}
