using ImpulseControl.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImpulseControl
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Input Reader")]
        [SerializeField] private GameInputReader gameInputReader;

        [Header("References")]
        [SerializeField] private Rigidbody2D rgb;

        private void OnEnable()
        {
            gameInputReader.Move += OnMove;
        }


        private void OnDisable()
        {
            gameInputReader.Move -= OnMove;
        }

        private void OnMove(Vector2 rawMoveementinput, bool contextStarted)
        {
            
        }


    }
}
