using UnityEngine;

namespace ThirdPersonTemplate
{
    /// <summary>
    /// Class used to replicate the view and the control of a third person camera
    /// </summary>
    public class ThirdPersonCamera : MonoBehaviour
    {
        ////////////////////////////////
        ////////// Attributes //////////
        ////////////////////////////////

        ///////////////////////////////////
        ////////// Configuration //////////

        [Header("Configuration")]
        [SerializeField] private VirtualJoystick _inputSource = null;
        [SerializeField] private Transform _subject = null;

        //////////////////////////////
        ////////// Settings //////////

        [Header("Settings")]
        [SerializeField, Range(2f, 9f)] private float _distanceFromSubject = 5.5f;
        [SerializeField] private bool _invertVerticalAxis = true;
        [SerializeField] private bool _invertHorizontalAxis = true;
        [SerializeField, Range(20f, 160f)] private float _rotationSpeed = 80f;
        [SerializeField] private Vector3 _subjectOffset = Vector3.zero;
        [SerializeField, Range(0f, 70f)] private float _verticalLimit = 35f;

        /////////////////////////////
        ////////// Methods //////////
        /////////////////////////////

        ////////////////////////////////////////////
        ////////// MonoBehaviour Callback //////////

        private void LateUpdate()
        {
            UpdateRotation();
            UpdatePosition();
        }

        //////////////////////////////
        ////////// Services //////////

        private void UpdatePosition()
        {
            transform.position = _subject.position + _subjectOffset - transform.forward * _distanceFromSubject;
        }

        private void UpdateRotation()
        {
            float horizontalInput = _invertHorizontalAxis ? -_inputSource.HorizontalInput : _inputSource.HorizontalInput;
            float verticalInput = _invertVerticalAxis ? -_inputSource.VerticalInput : _inputSource.VerticalInput;
            float deltaHorizontalRotation = horizontalInput * _rotationSpeed * Time.deltaTime;
            float deltaVerticalRotation = verticalInput * _rotationSpeed * Time.deltaTime;
            float clampedXLocalRotation = Mathf.Clamp(transform.localEulerAngles.x + deltaVerticalRotation, 0, _verticalLimit);

            transform.localEulerAngles = new Vector3(clampedXLocalRotation,
                transform.localEulerAngles.y + deltaHorizontalRotation,
                transform.localEulerAngles.z);
        }
    }
}
