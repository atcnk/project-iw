using UnityEngine;
using UnityEngine.InputSystem;

namespace CodeNameIW.InputSystem
{
    public class InputController : MonoBehaviour
    {
        [SerializeField] private InputData _inputData;

        #region Non-Serialized

        private PlayerInputAction _playerInputAction;
        
        private InputAction _playerControlGamepadMovementInputAction;
        private InputAction _playerControlKeyboardMovementInputAction;
        private InputAction _playerControlGamepadRotationInputAction;
        private InputAction _playerControlMouseRotationInputAction;

        #endregion
        
        private void Awake()
        {
            InitializeNewPlayerInputAction();
            InitializePlayerControlInputActions();

            #region LocalMethods

            void InitializeNewPlayerInputAction()
            {
                _playerInputAction = new PlayerInputAction();
            }

            void InitializePlayerControlInputActions()
            {
                _playerControlGamepadMovementInputAction = _playerInputAction.PlayerControlActionMap.GamepadMovementAction;
                _playerControlKeyboardMovementInputAction = _playerInputAction.PlayerControlActionMap.KeyboardMovementAction;
                _playerControlGamepadRotationInputAction = _playerInputAction.PlayerControlActionMap.GamepadRotationAction;
                _playerControlMouseRotationInputAction = _playerInputAction.PlayerControlActionMap.MouseRotationAction;
            }

            #endregion
        }
        
        private void OnEnable()
        {
            EnablePlayerControlInputActions();
            
            SubscribeToPlayerControlMovementInputActionEvents();
            SubscribeToPlayerControlRotationInputActionEvents();

            #region LocalMethods
            
            void EnablePlayerControlInputActions()
            {
                _playerControlGamepadMovementInputAction.Enable();
                _playerControlKeyboardMovementInputAction.Enable();
                _playerControlGamepadRotationInputAction.Enable();
                _playerControlMouseRotationInputAction.Enable();
            }

            void SubscribeToPlayerControlMovementInputActionEvents()
            {
                _playerControlGamepadMovementInputAction.started += ProcessPlayerControlGamepadMovementInputActionToInputData;
                _playerControlGamepadMovementInputAction.performed += ProcessPlayerControlGamepadMovementInputActionToInputData;
                _playerControlGamepadMovementInputAction.canceled += ProcessPlayerControlGamepadMovementInputActionToInputData;

                _playerControlKeyboardMovementInputAction.started += ProcessPlayerControlKeyboardMovementInputActionToInputData;
                _playerControlKeyboardMovementInputAction.performed += ProcessPlayerControlKeyboardMovementInputActionToInputData;
                _playerControlKeyboardMovementInputAction.canceled += ProcessPlayerControlKeyboardMovementInputActionToInputData;
            }

            void SubscribeToPlayerControlRotationInputActionEvents()
            {
                _playerControlGamepadRotationInputAction.started += ProcessPlayerControlGamepadRotationInputActionToInputData;
                _playerControlGamepadRotationInputAction.performed += ProcessPlayerControlGamepadRotationInputActionToInputData;
                _playerControlGamepadRotationInputAction.canceled += ProcessPlayerControlGamepadRotationInputActionToInputData;

                _playerControlMouseRotationInputAction.started += ProcessPlayerControlMouseRotationInputActionToInputData;
                _playerControlMouseRotationInputAction.performed += ProcessPlayerControlMouseRotationInputActionToInputData;
                _playerControlMouseRotationInputAction.canceled += ProcessPlayerControlMouseRotationInputActionToInputData;
            }

            #endregion
        }

        private void OnDisable()
        {
            DisablePlayerControlInputActions();
            
            UnsubscribeFromPlayerControlMovementInputActionEvents();
            UnsubscribeFromPlayerControlRotationInputActionEvents();

            #region LocalMethods
            
            void DisablePlayerControlInputActions()
            {
                _playerControlGamepadMovementInputAction.Disable();
                _playerControlKeyboardMovementInputAction.Disable();
                //_playerControlRunningInputAction.Disable();
                _playerControlGamepadRotationInputAction.Disable();
                _playerControlMouseRotationInputAction.Disable();
            }

            void UnsubscribeFromPlayerControlMovementInputActionEvents()
            {
                _playerControlGamepadMovementInputAction.started -= ProcessPlayerControlGamepadMovementInputActionToInputData;
                _playerControlGamepadMovementInputAction.performed -= ProcessPlayerControlGamepadMovementInputActionToInputData;
                _playerControlGamepadMovementInputAction.canceled -= ProcessPlayerControlGamepadMovementInputActionToInputData;
                
                _playerControlKeyboardMovementInputAction.started -= ProcessPlayerControlKeyboardMovementInputActionToInputData;
                _playerControlKeyboardMovementInputAction.performed -= ProcessPlayerControlKeyboardMovementInputActionToInputData;
                _playerControlKeyboardMovementInputAction.canceled -= ProcessPlayerControlKeyboardMovementInputActionToInputData;
            }
            
            void UnsubscribeFromPlayerControlRunningInputActionEvents()
            {
                //_playerControlRunningInputAction.started -= ProcessPlayerControlRunningInputActionToInputData;
                //_playerControlRunningInputAction.canceled -= ProcessPlayerControlRunningInputActionToInputData;
            }
            
            void UnsubscribeFromPlayerControlRotationInputActionEvents()
            {
                _playerControlGamepadRotationInputAction.started -= ProcessPlayerControlGamepadRotationInputActionToInputData;
                _playerControlGamepadRotationInputAction.performed -= ProcessPlayerControlGamepadRotationInputActionToInputData;
                _playerControlGamepadRotationInputAction.canceled -= ProcessPlayerControlGamepadRotationInputActionToInputData;
                
                _playerControlMouseRotationInputAction.started -= ProcessPlayerControlMouseRotationInputActionToInputData;
                _playerControlMouseRotationInputAction.performed -= ProcessPlayerControlMouseRotationInputActionToInputData;
                _playerControlMouseRotationInputAction.canceled -= ProcessPlayerControlMouseRotationInputActionToInputData;
            }

            #endregion
        }

        private void ProcessPlayerControlGamepadMovementInputActionToInputData(InputAction.CallbackContext callbackContext)
        {
            ReadAndProcessCurrentPlayerMovement();

            #region LocalMethods

            void ReadAndProcessCurrentPlayerMovement()
            {
                _inputData.UpdateCurrentPlayerMovementWithGamepadInput(callbackContext.ReadValue<Vector2>().ToVector3XZ());
            }

            #endregion
        }

        private void ProcessPlayerControlKeyboardMovementInputActionToInputData(InputAction.CallbackContext callbackContext)
        {
            ReadAndProcessCurrentPlayerMovement();

            #region LocalMethods

            void ReadAndProcessCurrentPlayerMovement()
            {
                _inputData.UpdateCurrentPlayerMovementWithKeyboardInput(callbackContext.ReadValue<Vector2>().ToVector3XZ());
            }

            #endregion
        }

        private void ProcessPlayerControlGamepadRotationInputActionToInputData(InputAction.CallbackContext callbackContext)
        {
            ReadAndProcessToInputData();

            #region LocalMethods

            void ReadAndProcessToInputData()
            {
                _inputData.GamepadStickPosition = callbackContext.ReadValue<Vector2>();
            }

            #endregion
        }

        private void ProcessPlayerControlMouseRotationInputActionToInputData(InputAction.CallbackContext callbackContext)
        {
            ReadAndProcessToInputData();

            #region LocalMethods
            
            void ReadAndProcessToInputData()
            {
                _inputData.MousePosition = callbackContext.ReadValue<Vector2>();
            }

            #endregion
        }
    }
}