using UnityEngine;

namespace CodeNameIW.InputSystem
{
    public class InputData : MonoBehaviour
    {
        public delegate void ValueChangedEventHandler();

        public event ValueChangedEventHandler OnCurrentPlayerMovementChanged;
        public event ValueChangedEventHandler OnCurrentGamepadPlayerMovementChanged;
        public event ValueChangedEventHandler OnMousePositionChanged;
        public event ValueChangedEventHandler OnGamepadStickPositionChanged;

        #region Non-Serialized

        private Vector3 _currentPlayerMovement;
        private Vector3 _lastPlayerMovement = Vector3.forward;
        private Vector2 _mousePosition;
        private Vector2 _gamepadStickPosition;

        #endregion

        public Vector3 LastPlayerMovement => _lastPlayerMovement;
        
        public Vector3 CurrentPlayerMovement
        {
            get => _currentPlayerMovement;
            
            private set
            {
                _currentPlayerMovement = value;
                
                if (value != Vector3.zero)
                {
                    _lastPlayerMovement = value;
                }
                
                OnCurrentPlayerMovementChanged?.Invoke();
            }
        }

        public void UpdateCurrentPlayerMovementWithKeyboardInput(Vector3 currentPlayerMovement)
        {
            CurrentPlayerMovement = currentPlayerMovement;
        }

        public void UpdateCurrentPlayerMovementWithGamepadInput(Vector3 currentPlayerMovement)
        {
            CurrentPlayerMovement = currentPlayerMovement;
            
            OnCurrentGamepadPlayerMovementChanged?.Invoke();
        }

        public Vector2 MousePosition
        {
            get => _mousePosition;
            set
            {
                _mousePosition = value;
                
                OnMousePositionChanged?.Invoke();
            }
        }

        public Vector2 GamepadStickPosition
        {
            get => _gamepadStickPosition;
            set
            {
                _gamepadStickPosition = value;

                if (value == Vector2.zero)
                {
                    OnCurrentGamepadPlayerMovementChanged?.Invoke();
                    return;
                }
                
                OnGamepadStickPositionChanged?.Invoke();
            }
        }
    }
}