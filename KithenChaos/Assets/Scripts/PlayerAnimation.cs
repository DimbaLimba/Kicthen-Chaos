using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Player _player;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        _animator.SetBool("IsWalking", _player.IsWalking());
    }
}
