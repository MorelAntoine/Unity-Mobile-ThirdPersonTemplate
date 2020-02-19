using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace ThirdPersonTemplate
{
    /// <summary>
    /// Class used to virtually simulate a joystick.
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
    {
        ////////////////////////////////
        ////////// Attributes //////////
        ////////////////////////////////

        ////////////////////////////////
        ////////// Components //////////

        private Image _backgroundImage = null;
        private Image _controllerImage = null;

        ///////////////////////////////////////////
        ////////// Generated Information //////////

        [Header("Generated Information")]
        [SerializeField] private Vector2 _virtualInput = Vector2.zero;

        //////////////////////////////
        ////////// Settings //////////

        [Header("Settings")]
        [SerializeField, Range(2f, 4f)] private float _controllerRange = 2.7f;

        ////////////////////////////////
        ////////// Properties //////////
        ////////////////////////////////

        public float HorizontalInput => _virtualInput.x;
        public float VerticalInput => _virtualInput.y;
        public Vector2 VirtualInput => _virtualInput;

        /////////////////////////////
        ////////// Methods //////////
        /////////////////////////////

        ///////////////////////////////////////////////
        ////////// Interfaces Implementation //////////

        public void OnDrag(PointerEventData eventData)
        {
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_backgroundImage.rectTransform,
                eventData.position, eventData.pressEventCamera, out Vector2 outLocalPoint))
            {
                CalculateVirtualInput(in outLocalPoint);
                UpdateControllerPosition();
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnDrag(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _virtualInput = Vector2.zero;
            _controllerImage.rectTransform.anchoredPosition = Vector2.zero;
        }

        /////////////////////////////////////////////
        ////////// MonoBehaviour Callbacks //////////

        private void Awake()
        {
            _backgroundImage = GetComponent<Image>();
            _controllerImage = transform.GetChild(0).GetComponent<Image>();
        }

        //////////////////////////////
        ////////// Services //////////

        void CalculateVirtualInput(in Vector2 localPoint)
        {
            Vector2 inputVector;
            Vector2 inputVectorClamped;

            inputVector.x = localPoint.x / _backgroundImage.rectTransform.sizeDelta.x;
            inputVector.y = localPoint.y / _backgroundImage.rectTransform.sizeDelta.y;
            inputVectorClamped.x = inputVector.x * 2 - 1;
            inputVectorClamped.y = inputVector.y * 2 - 1;
            _virtualInput = inputVectorClamped.magnitude > 1.0f ? inputVectorClamped.normalized : inputVectorClamped;
        }

        void UpdateControllerPosition()
        {
            Vector2 controllerPosition;

            controllerPosition.x = _virtualInput.x * (_backgroundImage.rectTransform.sizeDelta.x / _controllerRange);
            controllerPosition.y = _virtualInput.y * (_backgroundImage.rectTransform.sizeDelta.y / _controllerRange);
            _controllerImage.rectTransform.anchoredPosition = controllerPosition;
        }

    }
}
