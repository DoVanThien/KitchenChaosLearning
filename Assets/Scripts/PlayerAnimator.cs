using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    //CONSTANTS
    private const string ANIM_PARAM_IS_WALKING = "IsWalking";
    
    private Animator _animator;
    private Player _player;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _player = GetComponentInParent<Player>();
    }

    private void Update()
    {
        _animator.SetBool(ANIM_PARAM_IS_WALKING, _player.IsWalking());
    }
}
