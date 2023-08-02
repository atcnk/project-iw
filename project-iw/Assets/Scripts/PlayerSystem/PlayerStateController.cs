using UnityEngine;
using CodeNameIW.InputSystem;

namespace CodeNameIW.PlayerSystem
{
    public class PlayerStateController : MonoBehaviour
    {
        public delegate void ValueChangedEventHandler(PlayerState oldState, PlayerState newState);
        public event ValueChangedEventHandler OnPlayerStateChanged;

        [Header("Self-Components")]
        [SerializeField] private Transform _transform;
        
        [Header("Settings")]
        [SerializeField] private PlayerSettings _playerSettings;
        [SerializeField] private PlayerState _currentState;

        [Header("Input")]
        [SerializeField] private InputData _inputData;

        private float _angleDifference = 0f;
        
        public PlayerState CurrentState
        {
            get => _currentState;
            set
            {
                if (Equals(_currentState, value))
                {
                    return;
                }

                OnPlayerStateChanged?.Invoke(_currentState, value);
                
                _currentState = value;
            }
        }

        private void OnEnable()
        {
            SubscribeToOnCurrentPlayerMovementChanged();
            SubscribeToOnMousePositionChanged();
            SubscribeToOnGamepadStickPositionChanged();

            #region LocalMethods

            void SubscribeToOnCurrentPlayerMovementChanged()
            {
                _inputData.OnCurrentPlayerMovementChanged += UpdateCurrentState;
                _inputData.OnCurrentGamepadPlayerMovementChanged += UpdateCurrentState;
            }

            void SubscribeToOnMousePositionChanged()
            {
                _inputData.OnMousePositionChanged += UpdateCurrentState;
            }

            void SubscribeToOnGamepadStickPositionChanged()
            {
                _inputData.OnGamepadStickPositionChanged += UpdateCurrentState;
            }

            #endregion
        }

        private void OnDisable()
        {
            UnsubscribeFromOnCurrentPlayerMovementChanged();
            UnsubscribeFromOnMousePositionChanged();
            UnsubscribeFromOnGamepadStickPositionChanged();

            #region LocalMethods

            void UnsubscribeFromOnCurrentPlayerMovementChanged()
            {
                _inputData.OnCurrentPlayerMovementChanged -= UpdateCurrentState;
            }

            void UnsubscribeFromOnMousePositionChanged()
            {
                _inputData.OnMousePositionChanged -= UpdateCurrentState;
            }
            
            void UnsubscribeFromOnGamepadStickPositionChanged()
            {
                _inputData.OnGamepadStickPositionChanged -= UpdateCurrentState;
            }

            #endregion
        }

        private void UpdateCurrentState()
        {
            if (!IsPlayerMoving())
            {
                CurrentState = PlayerState.IdleUnarmed;
                return;
            }

            _angleDifference = GetAngleDifference();
            
            CurrentState = GetWalkStateByAngleDifference();

            #region LocalMethods

            bool IsPlayerMoving()
            {
                return _inputData.CurrentPlayerMovement != Vector3.zero;
            }

            float GetAngleDifference()
            {
               return Vector3.SignedAngle(_transform.forward.ToIsometric(), _inputData.LastPlayerMovement, Vector3.up);
            }

            PlayerState GetWalkStateByAngleDifference()
            {
                if (_angleDifference < _playerSettings.DirectForwardsAngles.x && _angleDifference > _playerSettings.DirectForwardsAngles.y)
                {
                    return PlayerState.WalkDirectForwards;
                }

                if (_angleDifference <= _playerSettings.RightForwardsAngles.x && _angleDifference >= _playerSettings.RightForwardsAngles.y)
                {
                    return PlayerState.WalkRightForwards;
                }

                if (_angleDifference <= _playerSettings.RightBackwardsAngles.x && _angleDifference >= _playerSettings.RightBackwardsAngles.y)
                {
                    return PlayerState.WalkRightBackwards;
                }

                if (_angleDifference < _playerSettings.DirectBackwardsAngles.x && _angleDifference > _playerSettings.DirectBackwardsAngles.y)
                {
                    return PlayerState.WalkDirectBackwards;
                }

                if (_angleDifference <= _playerSettings.LeftBackwardsAngles.x && _angleDifference >= _playerSettings.LeftBackwardsAngles.y)
                {
                    return PlayerState.WalkLeftBackwards;
                }

                if (_angleDifference <= _playerSettings.LeftForwardsAngles.x && _angleDifference >= _playerSettings.LeftForwardsAngles.y)
                {
                    return PlayerState.WalkLeftForwards;
                }
                
                if (_angleDifference <= _playerSettings.DirectLeftwardsAngles.x && _angleDifference >= _playerSettings.DirectLeftwardsAngles.y)
                {
                    return PlayerState.WalkDirectLeftwards;
                }

                if ((_angleDifference < _playerSettings.DirectRightwardsAngles.x && _angleDifference >= _playerSettings.DirectRightwardsAngles.x + _playerSettings.RightForwardsAngles.x) ||
                    (_angleDifference > _playerSettings.DirectRightwardsAngles.y && _angleDifference <= _playerSettings.DirectRightwardsAngles.y + _playerSettings.RightBackwardsAngles.y))
                {
                    return PlayerState.WalkDirectRightwards;
                }

                return PlayerState.IdleUnarmed;
            }

            #endregion
        }
    }
}