using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace flexington.Tools
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform _pivot;

        private Vector3 _lastPosition;

        // Move    
        [SerializeField] private float _moveSpeed;
        private Vector3 _moveOffset;

        // Rotate
        [SerializeField] private float _rotateSpeed;
        private float _rotationOffset;

        // Zoom
        [SerializeField] private float _zoomSpeed;
        [SerializeField] private float _zoomMin;
        [SerializeField] private float _zoomMax;
        [SerializeField] private float _zoomCurrent;
        private float _zoomOffset;


        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            // Save position before manipulation
            _lastPosition = transform.position;
            // _timer += Time.deltaTime;

            // Move Camera
            HandleMovementInput();
            if (!_moveOffset.Equals(Vector3.zero))
            {
                // Get current position
                Vector3 currentPosition = transform.position;

                // Get current rotation
                Quaternion quaternion = transform.rotation;

                // Set direction to move to
                Vector3 direction = quaternion * _moveOffset;

                // Preserve hight
                float originalY = transform.position.y;

                // Set new position
                Vector3 newPosition = currentPosition + direction;

                // Reset hight
                newPosition.y = originalY;

                // Transition to new position
                transform.position = Vector3.Lerp(currentPosition, newPosition, Time.deltaTime * _moveSpeed);

                // Reset offset
                _moveOffset = Vector3.zero;

                // Update Pivot
                _pivot.position += (transform.position - _lastPosition);
            }

            // Rotate Camera
            HandleRotationInput();
            if (!_rotationOffset.Equals(0))
            {
                Vector3 currentPosition = transform.position - _pivot.position;

                Vector3 newPosition = Quaternion.Euler((Vector3.up) * _rotateSpeed * _rotationOffset * Time.deltaTime) * currentPosition;
                transform.position = Vector3.Lerp(currentPosition + _pivot.position, newPosition + _pivot.position, _rotateSpeed * Time.deltaTime);
                transform.rotation = Quaternion.LookRotation(_pivot.position - (newPosition + _pivot.position), Vector3.up).normalized;
                _rotationOffset = 0;
            }

            // Zoom Camera
            HandleZoomInput();
            if (!_zoomOffset.Equals(0))
            {
                // Get current position
                Vector3 currentPosition = transform.position;
                // Set new position
                Vector3 newPosition = (transform.localRotation * (Vector3.forward * _zoomOffset * _zoomSpeed)) + currentPosition;

                // Transition from current to new position
                if (newPosition.y < _zoomMin && newPosition.y > _zoomMax)
                {
                    transform.position = Vector3.Lerp(currentPosition, newPosition, _zoomSpeed * Time.deltaTime);
                    _zoomCurrent = Mathf.Clamp(transform.position.y, _zoomMax, _zoomMin);
                }
                // Reset offset
                _zoomOffset = 0;
            }
        }

        private void HandleMovementInput()
        {
            if (!Input.anyKey) return;

            if (Input.GetKey(KeyCode.W)) { _moveOffset += Vector3.forward; }
            else if (Input.GetKey(KeyCode.S)) { _moveOffset += -Vector3.forward; }

            if (Input.GetKey(KeyCode.D)) { _moveOffset += Vector3.right; }
            else if (Input.GetKey(KeyCode.A)) { _moveOffset += -Vector3.right; }
        }

        private void HandleRotationInput()
        {
            if (Input.anyKey)
            {
                if (Input.GetKey(KeyCode.Q)) { _rotationOffset = -1; }
                else if (Input.GetKey(KeyCode.E)) { _rotationOffset = 1; }
            }
        }

        private void HandleZoomInput()
        {
            // Keyboard
            if (Input.anyKey)
            {
                if (Input.GetKey(KeyCode.Z)) { _zoomOffset = 1; }
                else if (Input.GetKey(KeyCode.X)) { _zoomOffset = -1; }
            }
            // Mouse
            else if (Input.mouseScrollDelta.y != 0)
            {
                _zoomOffset = Input.mouseScrollDelta.y;
            }
        }
    }
}