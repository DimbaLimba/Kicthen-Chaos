using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryResultUI : MonoBehaviour
{
    private const string POPUP = "Popup";

    [SerializeField] private Image _backgroundImage;
    [SerializeField] private Image _iconImage;
    [SerializeField] private TextMeshProUGUI _messageText;
    [SerializeField] private Color _successColor;
    [SerializeField] private Color _falingColor;
    [SerializeField] private Sprite _successSprite;
    [SerializeField] private Sprite _falingSprite;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSecces += DeliveryManager_OnRecipeSecces;
        DeliveryManager.Instance.OnRecipeFalied += DeliveryManager_OnRecipeFalied; 
        gameObject.gameObject.SetActive(false);
    }

    private void DeliveryManager_OnRecipeFalied(object sender, System.EventArgs e)
    {
        gameObject.SetActive(true);
        _animator.SetTrigger(POPUP);
        _backgroundImage.color = _falingColor;
        _iconImage.sprite = _falingSprite;
        _messageText.text = "DELIVERY\nFAILED";
    }

    private void DeliveryManager_OnRecipeSecces(object sender, System.EventArgs e)
    {
        gameObject.SetActive(true);
        _animator.SetTrigger(POPUP);
        _backgroundImage.color = _successColor;
        _iconImage.sprite = _successSprite;
        _messageText.text = "DELIVERY\nSUCCESS";
    }
}
