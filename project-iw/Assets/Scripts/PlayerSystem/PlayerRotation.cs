using UnityEngine;
using CodeNameIW.InputSystem;

namespace CodeNameIW.PlayerSystem
{
    public class PlayerRotation : MonoBehaviour
    {
        [Header("Self-Components")]
        [SerializeField] private Transform _playerTransform;

        [Header("Settings")]
        [SerializeField] private PlayerSettings _playerSettings;
        
        [Header("Input")]
        [SerializeField] private InputData _inputData;

        #region NonSerialized

        private Camera _camera;
        private Ray _ray;

        #endregion

        private void Awake()
        {
            CacheMainCamera();

            #region LocalMethods

            void CacheMainCamera()
            {
                _camera = Camera.main;
            }

            #endregion
        }

        private void OnEnable()
        {
            SubscribeToOnMousePositionChangedEvent();
            
            #region LocalMethods

            void SubscribeToOnMousePositionChangedEvent()
            {
                _inputData.OnMousePositionChanged += UpdateRotationWithMouseInput;
                _inputData.OnGamepadStickPositionChanged += UpdateRotationWithGamepadInput;
                _inputData.OnCurrentGamepadPlayerMovementChanged += UpdateRotationWithGamepadMovementInput;
            }
            
            #endregion
        }
        
        private void OnDisable()
        {
            UnsubscribeToOnMousePositionChangedEvent();
            
            #region LocalMethods

            void UnsubscribeToOnMousePositionChangedEvent()
            {
                _inputData.OnMousePositionChanged -= UpdateRotationWithMouseInput;
                _inputData.OnGamepadStickPositionChanged -= UpdateRotationWithGamepadInput;
                _inputData.OnCurrentGamepadPlayerMovementChanged -= UpdateRotationWithGamepadMovementInput;
            }
            
            #endregion
        }

        private void UpdateRotationWithMouseInput()
        {
            _ray = GetScreenPointRayWithMousePosition();

            if (Physics.Raycast(_ray, out var hit, 100))
            {
                _playerTransform.LookAt(hit.point); 
                    
                SetXZRotationsToZero();
            }

            #region LocalMethods

            Ray GetScreenPointRayWithMousePosition()
            {
                return _camera.ScreenPointToRay(_inputData.MousePosition);
            }

            void SetXZRotationsToZero()
            {
                var newRotation = _playerTransform.rotation;
                newRotation.x = 0f;
                newRotation.z = 0f;
                
                _playerTransform.rotation = newRotation;
            }

            #endregion
        }

        private void UpdateRotationWithGamepadInput()
        {
            _playerTransform.rotation = GetLookRotation();

            #region LocalMethods

            Quaternion GetLookRotation()
            {
                return Quaternion.LookRotation(_inputData.GamepadStickPosition.ToVector3XZ().ToIsometric(), Vector3.up);
            }

            #endregion
        }

        
        private void UpdateRotationWithGamepadMovementInput()
        {
            if ((_inputData.GamepadStickPosition != Vector2.zero) || _inputData.CurrentPlayerMovement == Vector3.zero)
            {
                return;
            }

            _playerTransform.rotation = GetTargetRotation();

            #region LocalMethods

            Quaternion GetTargetRotation()
            {
                return Quaternion.RotateTowards(_playerTransform.rotation, Quaternion.LookRotation(_inputData.CurrentPlayerMovement.ToIsometric(), Vector3.up), 180f);
            }

            #endregion
        }
    }
}