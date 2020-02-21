using UnityEngine;

namespace ThirdPersonTemplate
{
    /// <summary>
    /// Class used to enable player movement control 
    /// </summary>
    public class PlayerMovement : MonoBehaviour
    {
        ////////////////////////////////
        ////////// Attributes //////////
        ////////////////////////////////

        ////////////////////////////////
        ////////// Components //////////

        [Header("Components")]
        [SerializeField] private VirtualJoystick _inputSource = null;
        [SerializeField] private Transform _relativeMovementSource = null;

        /////////////////////////////////////////
        ////////// Computing Resources //////////

        private Vector2 _input = Vector2.zero;

        //////////////////////////////
        ////////// Settings //////////

        [Header("Setting")]
        [SerializeField, Range(2f, 16f)] private float _movementSpeed = 8f;

        /////////////////////////////
        ////////// Methods //////////
        /////////////////////////////

        /////////////////////////////////////////////
        ////////// MonoBehaviour Callbacks //////////

        private void FixedUpdate()
        {
            if (_input != Vector2.zero)
            {
                Vector3 direction = _relativeMovementSource.forward * _input.y + _relativeMovementSource.right * _input.x;

                direction.y = 0;
                UpdateRotation(in direction);
                UpdatePosition(in direction);
            }
        }

        private void Update()
        {
            _input = _inputSource.VirtualInput;
        }

        //////////////////////////////
        ////////// Services //////////

        private void UpdatePosition(in Vector3 direction)
        {
            transform.Translate(direction * _movementSpeed * Time.fixedDeltaTime, Space.World);
        }

        private void UpdateRotation(in Vector3 direction)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}
