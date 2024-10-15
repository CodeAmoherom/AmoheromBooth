using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CA.PlayerController
{
    public class PlayerAnimation : MonoBehaviour
    {
        [SerializeField] public Animator _animator;
        [SerializeField] private float locolotionBlendSpeed = 4f;

        private PlayerLocomotioninput _playerLocomotioninput;

        private static int inputXHash = Animator.StringToHash("InputX");
        private static int inputYHash = Animator.StringToHash("InputY");

        private Vector3 _currentBlendInput = Vector3.zero;

        private void Awake()
        {
            _playerLocomotioninput = GetComponent<PlayerLocomotioninput>();
        }

        private void Update()
        {
            UpdateAnimationState();
        }

        private void UpdateAnimationState()
        {
            Vector2 inputTarget = _playerLocomotioninput.MovementInput;
            _currentBlendInput = Vector3.Lerp(_currentBlendInput, inputTarget, locolotionBlendSpeed * Time.deltaTime);

            _animator.SetFloat(inputXHash, _currentBlendInput.x);
            _animator.SetFloat(inputYHash, _currentBlendInput.y);
        }
    }
}