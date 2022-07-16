using System;
using Assets.Project.Snake.Input;
using UnityEngine.InputSystem;

namespace Snake.Player
{
    public sealed class PlayerMovement
    {
        private readonly InputSnake _input = new InputSnake();

        public PlayerMovement()
        {
            _input.Player.Left.started += AddActionTurnLeft;
            _input.Player.Right.started += AddActionTurnRight;
            _input.Player.Up.started += AddActionLookAtUp;
            _input.Player.Down.started += AddActionLookAtDown;
            _input.Player.ZoomIn.started += AddActionZoomIn;
            _input.Player.ZoomOut.started += AddActionZoomOut;

            _input.Player.Enable();
        }

        public event Action OnTurnedLeft = delegate { };
        public event Action OnTurnedRight = delegate { };
        public event Action OnLookedAtUp = delegate { };
        public event Action OnLookedAtDown = delegate { };
        public event Action OnZoomIn = delegate { };
        public event Action OnZoomOut = delegate { };

        ~PlayerMovement()
        {
            _input.Player.Left.started -= AddActionTurnLeft;
            _input.Player.Right.started -= AddActionTurnRight;
            _input.Player.Up.started -= AddActionLookAtUp;
            _input.Player.Down.started -= AddActionLookAtDown;
            _input.Player.ZoomIn.started -= AddActionZoomIn;
            _input.Player.ZoomOut.started -= AddActionZoomOut;

            _input.Player.Disable();
        }

        private void AddActionTurnRight(InputAction.CallbackContext _)
        {
            OnTurnedRight.Invoke();
        }

        private void AddActionTurnLeft(InputAction.CallbackContext _)
        {
            OnTurnedLeft.Invoke();
        }

        private void AddActionLookAtUp(InputAction.CallbackContext _)
        {
            OnLookedAtUp.Invoke();
        }

        private void AddActionLookAtDown(InputAction.CallbackContext _)
        {
            OnLookedAtDown.Invoke();
        }

        private void AddActionZoomIn(InputAction.CallbackContext _)
        {
            OnZoomIn.Invoke();
        }

        private void AddActionZoomOut(InputAction.CallbackContext _)
        {
            OnZoomOut.Invoke();
        }
    }
}