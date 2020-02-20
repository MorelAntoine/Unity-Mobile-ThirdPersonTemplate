using System.Collections;
using System.Collections.Generic;
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

        [SerializeField] private VirtualJoystick _inputSource = null;

        /////////////////////////////////////////
        ////////// Computing Resources //////////

        [SerializeField] private Vector2 _input = Vector2.zero;

        //////////////////////////////
        ////////// Settings //////////

        [SerializeField] private float _speed = 8f;

        /////////////////////////////
        ////////// Methods //////////
        /////////////////////////////

        /////////////////////////////////////////////
        ////////// MonoBehaviour Callbacks //////////

        private void FixedUpdate()
        {
            float deltaHorizontalSpeed = _input.x * _speed * Time.fixedDeltaTime;
            float deltaForwardSpeed = _input.y * _speed * Time.fixedDeltaTime;
            Vector3 translation = new Vector3(deltaHorizontalSpeed, 0, deltaForwardSpeed);

            translation = translation.magnitude > 1.0f ? translation.normalized : translation;
            transform.Translate(translation);
        }

        private void Update()
        {
            _input = _inputSource.VirtualInput;
        }
    }
}
