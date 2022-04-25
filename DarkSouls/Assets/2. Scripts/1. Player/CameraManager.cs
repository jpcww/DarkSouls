using UnityEngine;


namespace Hyobin
{
    public class CameraManager : MonoBehaviour
    {
        #region PUBLIC REGION
        public Transform transform_target; // Transform of Player
        public Transform transform_camera;
        public Transform transform_cameraPivot;

        public float horitzontalRotateSpeed = 0.1f;
        public float verticalRotateSpeed = 0.03f;
        public float followSpeed = 0.1f;

        public float minimumVerticalAngle = -35;
        public float maximumVerticalAngle = 35;

        // Singletone
        public static CameraManager instance;
        #endregion

        #region PRIVATE REGION
        private Transform transform_cameraHolder;
        private Vector3 positon_camera;
        private LayerMask ignoreLayers;

        private float defaultCameraDepth;
        private float horizontalAngle;
        private float verticalAngle;
        #endregion

        #region MONOBEHAVIOUR CALLBACKS
        private void Awake()
        {
            // Singleton
            instance = this;

            transform_cameraHolder = transform;
            defaultCameraDepth = transform_camera.localPosition.z;  // the distance between Camera and Player

            // Layers to be ignored
            ignoreLayers = ~(1 << 8 | 1 << 9 | 1 << 10);
        }
        #endregion

        #region PUBLIC METHODS
        public void FollowTarget(float delta)
        {
            // Calculate the target position
            Vector3 targetPosition = Vector3.Lerp(transform_cameraHolder.position, transform_target.position, delta/followSpeed);

            // Update Camera's Position
            transform_cameraHolder.position = targetPosition;
        }

        public void HandleCameraRotation(float delta, float cameraXInput, float cameraYInput)
        {
            // Calculate the rotation angles along with the camera input on x/y axis
            horizontalAngle += (cameraXInput * horitzontalRotateSpeed) / delta;
            verticalAngle -= (cameraYInput * verticalRotateSpeed) / delta;
            verticalAngle = Mathf.Clamp(verticalAngle, minimumVerticalAngle, maximumVerticalAngle); // Clamp the vertical rotation

            // Reset the camera's rotation
            Vector3 rotation = Vector3.zero;

            // Apply horizontal rotation
            rotation.y = horizontalAngle;
            Quaternion targetRotation = Quaternion.Euler(rotation);
            transform_cameraHolder.rotation = targetRotation;

            // Reset the camera's rotation
            rotation = Vector3.zero;

            // Apply vertical rotation
            rotation.x = verticalAngle;
            targetRotation = Quaternion.Euler(rotation);
            transform_cameraPivot.localRotation = targetRotation;
        }
        #endregion

        #region PRIVATE METHODS
        #endregion
    }
}
