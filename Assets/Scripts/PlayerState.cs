using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CA.PlayerController
{
    public class PlayerState : MonoBehaviour
    {
        [field: SerializeField] public PlayerMovementState CurrentPlayerMovementState {  get; private set; } = PlayerMovementState.Idling;
        public enum PlayerMovementState
        {
            Idling = 0,
            Walking = 1,
            Running = 2,
            Sprinting = 3,
            Jumpting = 4,
            Falling = 5,
            Strafing = 6,

        }
    }
}