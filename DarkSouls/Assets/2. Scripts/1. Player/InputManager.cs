using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hyobin
{
    public class InputManager : MonoBehaviour
    {
        #region PUBLIC FIELDS
        [Header("Movement")]
        public float movementHorizontal;
        public float movementVertical;
        public float moveAmount;

        [Header("Camera")]
        public float cameraHorizontal;
        public float cameraVertical;
        #endregion

        #region PRIVATE FIELDS
        PlayerControls inputActions;

        Vector2 movementInput;
        Vector2 cameraInput;
        #endregion

        #region MONOBEHAVIOUR CALLBACKS
        public void OnEnable()
        {
            if (inputActions == null)
            {
                inputActions = new PlayerControls();

                // Subscribing events to be tirggered when input occurs
                inputActions.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();  // Subscribing Movement Input
                inputActions.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();  // Subscribing to Camera Input
            }

            inputActions.Enable();
        }

        private void OnDisable()
        {
            inputActions.Disable();
        }
        #endregion

        #region PUBLIC METHODS
        // ENCAPSULATION
        public void TickInput(float delta)
        {
            MoveInput(delta);
        }
        #endregion

        #region PRIVATE METHODS
        private void MoveInput(float delta)
        {
            // Divide Movement input into 2 for x/y axis
            movementHorizontal = movementInput.x;
            movementVertical = movementInput.y;

            // Calculate and clamp the movement amount between 0 and 1
            moveAmount = Mathf.Clamp01(Mathf.Abs(movementHorizontal) + Mathf.Abs(movementVertical));

            // Divide Camera input into 2 for x/y axis
            cameraHorizontal = cameraInput.x;
            cameraVertical = cameraInput.y;
        }
        #endregion
    }
}

