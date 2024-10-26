using System;
using BasicArchitecturalStructure;
using Unity.VisualScripting;
using UnityEngine;

namespace ThePrototype.Scripts.Managers
{
    public class DragAndDropManager : MonoBehaviour
    {
        private Vector2 _offset, _originalPosition;

        #region CashedData

        private Transform _transform;
        private Camera _mainCamera;

        #endregion

        private void Awake()
        {
            _transform = transform;
            _mainCamera = Camera.main;
        }

        private void Start()
        {
            _originalPosition = _transform.position;
        }

        private void OnMouseDown()
        {
            EventBus<SendFingerState>.Publish(new SendFingerState() { isPressing = true });

            _offset = GetMousePosition() - (Vector2)_transform.position;
        }

        private void OnMouseDrag()
        {
            var mousePosition = GetMousePosition();
            _transform.position = mousePosition - _offset;
        }

        private void OnMouseUp()
        {
            EventBus<SendFingerState>.Publish(new SendFingerState() { isPressing = false });
            _transform.position = _originalPosition;
        }

        private Vector2 GetMousePosition()
        {
            return _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        }

        public void SetOriginalPosition(Transform location)
        {
            _originalPosition = location.position;
        }
    }
}