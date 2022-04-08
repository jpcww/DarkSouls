using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hyobin
{
    public class PlayerAnimatorManager : MonoBehaviour
    {
        #region PUBLIC FIELDS
        // References on Player
        public Animator animator;
        #endregion

        #region PRVATE FIELDS
        // Variables for parameters on the Animator
        int vertical;
        int horizontal;

        // Flags
        public bool canRotate;
        #endregion

        #region MONOBEHAVIOUR CALLBACKS
        #endregion

        #region PUBLIC METHODS
        // Reference and link variables and components
        public void Initialize()
        {
            // Referce the animator on Player
            animator = GetComponent<Animator>();

            // Link the parameters on the animator with the variables
            vertical = Animator.StringToHash("Vertical");
            horizontal = Animator.StringToHash("Horizontal");
        }

        // Update parameters in Animator
        public void UpdateAnimatorValues(float verticalMovement, float horizontalMovement)
        {
            #region Clamp Vertical movement value
            float v = 0;

            if (verticalMovement > 0 && verticalMovement < 0.55f)
                v = 0.5f;

            else if (verticalMovement > 0.55f)
                v = 1;

            else if (verticalMovement < 0 && verticalMovement > -0.55f)
                v = -0.5f;

            else if (verticalMovement < -0.55f)
                v = -1;

            else
                v = 0;
            #endregion

            #region Clamp Horizontal movement value
            float h = 0;

            if (horizontalMovement > 0 && horizontalMovement < 0.55f)
                h = 0.5f;

            else if (horizontalMovement > 0.55f)
                h = 1;

            else if (horizontalMovement < 0 && horizontalMovement > -0.55f)
                h = -0.5f;

            else if (horizontalMovement < -0.55f)
                h = -1;

            else
                h = 0;
            #endregion

            // Update the parmeters in the animator with the calculated movement values
            animator.SetFloat(vertical, v, 0.1f, Time.deltaTime);   // depending on Damptime, the change in the value can be controlled
            animator.SetFloat(horizontal, h, 0.1f, Time.deltaTime);
        }

        public void CanRotate()
        {
            canRotate = true;
        }

        public void StopRotation()
        {
            canRotate = false;
        }
        #endregion

        #region PRIVATE METHODS
        #endregion
    }
}