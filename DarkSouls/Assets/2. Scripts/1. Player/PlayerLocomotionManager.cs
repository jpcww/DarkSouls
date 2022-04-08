using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hyobin
{
    public class PlayerLocomotionManager : MonoBehaviour
    {
        #region PUBLIC FIELDS
        [HideInInspector]
        public Transform playerTransform;
        [HideInInspector]
        public PlayerAnimatorManager playerAnimatorManager;
        public Rigidbody playerRigidbody;
        public GameObject normalCamera; // camera that is used when not locking on
        #endregion

        #region PRVIATE FIELDS
        Transform cameraObject;
        Vector3 moveDirection;

        InputManager inputManger;

        Vector3 normalVector;
        Vector3 targetPosition;

        [Header("STATS")]
        [SerializeField]
        float movementSpeed = 5;
        [SerializeField]
        float rotationSpeed = 10;
        #endregion

        #region MONOBEHAVIOUR CALLBACKS
        private void Start()
        {
            // References on Player
            playerRigidbody = GetComponent<Rigidbody>();
            inputManger = GetComponent<InputManager>();
            playerAnimatorManager = GetComponentInChildren<PlayerAnimatorManager>();
            playerTransform = transform;

            // References outside of Player
            cameraObject = Camera.main.transform;

            playerAnimatorManager.Initialize();
        }

        private void Update()
        {
            float delta = Time.deltaTime;

            // Check any input every frame
            inputManger.TickInput(delta);

            // Process movement direction based on the main camera's forward direction
            moveDirection = cameraObject.forward * inputManger.movementVertical;
            moveDirection += cameraObject.right * inputManger.movementHorizontal;
            moveDirection.Normalize();
            moveDirection.y = 0;    // Keep Player on the ground

            // Apply movement speed onto the movement direction
            moveDirection *= movementSpeed;

            // Get the movement direction on the surface
            Vector3 projectedVelocity = Vector3.ProjectOnPlane(moveDirection, normalVector);
            playerRigidbody.velocity = projectedVelocity;

            Debug.DrawLine(moveDirection, normalVector, Color.blue);
            Debug.DrawLine(playerRigidbody.transform.position, projectedVelocity, Color.red);

            playerAnimatorManager.UpdateAnimatorValues(inputManger.moveAmount, 0);

            if(playerAnimatorManager.canRotate)
            {
                HandleRotation(delta);
            }
        }
        #endregion

        #region PUBLIC METHODS
        #endregion

        #region PRIVATE METHODS
        private void HandleRotation(float delta)
        {
            // Reset the target direction every frame
            Vector3 targetDirection = Vector3.zero;

            float moveOverride = inputManger.moveAmount;

            // Process the movement direction every frame
            targetDirection = cameraObject.forward * inputManger.movementVertical;
            targetDirection += cameraObject.right * inputManger.movementHorizontal;
            targetDirection.Normalize();
            targetDirection.y = 0;    // Keep Player on the ground

            // In case there is no rotation
            if (targetDirection == Vector3.forward)
            {
                targetDirection = playerTransform.forward;
            }

            // Generate the targetRotation in Quaternion from Vector3
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

            // Rotate the targetRotation onto Player smoothly
            Quaternion updatedTargetRotation = Quaternion.Slerp(playerTransform.rotation, targetRotation, rotationSpeed * delta);

            // Apply the rotation
            playerTransform.rotation = updatedTargetRotation;
        }
        #endregion
    }
}
