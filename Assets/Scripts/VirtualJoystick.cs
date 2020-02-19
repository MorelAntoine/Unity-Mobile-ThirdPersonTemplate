using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace ThirdPersonTemplate
{
    /// <summary>
    /// Class used to simulate a joystick. 
    /// </summary>
    public sealed class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
    {
        ///////////////////////////////
        ////////// Attribute //////////
        ///////////////////////////////

        [SerializeField] private Vector2 _virtualInput = Vector2.zero;

        /////////////////////////////////
        ////////// UI Resource //////////

        private Image _joystickAreaImage = null;
        private Image _joystickControllerImage = null;

        //////////////////////////////
        ////////// Property //////////
        //////////////////////////////

        public Vector2 VirtualInput => _virtualInput;

        public float Horizontal => _virtualInput.x;

        public float Vertical => _virtualInput.y;

        ////////////////////////////
        ////////// Method //////////
        ////////////////////////////

        //////////////////////////////////////////////
        ////////// Interface Implementation //////////

        public void OnDrag(PointerEventData eventData)
        {
            Vector2 outLocalPoint;
            Vector2 inputVector;
            Vector2 inputVectorClamped;
            Vector2 joystickControllerPosition;

            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_joystickAreaImage.rectTransform,
                eventData.position, eventData.pressEventCamera, out outLocalPoint))
            {
                inputVector.x = outLocalPoint.x / _joystickAreaImage.rectTransform.sizeDelta.x;
                inputVector.y = outLocalPoint.y / _joystickAreaImage.rectTransform.sizeDelta.y;

                inputVectorClamped.x = inputVector.x * 2 - 1;
                inputVectorClamped.y = inputVector.y * 2 - 1;

                _virtualInput = inputVectorClamped.magnitude > 1.0f ? inputVectorClamped.normalized : inputVectorClamped;

                Debug.Log(_virtualInput);

                joystickControllerPosition.x = _virtualInput.x * (_joystickAreaImage.rectTransform.sizeDelta.x / 2.5f);
                joystickControllerPosition.y = _virtualInput.y * (_joystickAreaImage.rectTransform.sizeDelta.y / 2.5f);
                _joystickControllerImage.rectTransform.anchoredPosition = joystickControllerPosition;
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnDrag(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _virtualInput = Vector2.zero;

        }

        //////////////////////////////////////////////
        ////////// MonoBehaviour Callback ////////////

        private void Awake()
        {
            _joystickAreaImage = GetComponent<Image>();
            _joystickControllerImage = GetComponentInChildren<Image>();
        }
    }
}
