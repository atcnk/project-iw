using UnityEngine;
using CodeNameIW.InputSystem;

namespace CodeNameIW.PlayerSystem
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Self-Components")]
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private Rigidbody _playerRigidbody;

        [Header("Settings")]
        [SerializeField] private PlayerSettings _playerSettings;
        
        [Header("Input")]
        [SerializeField] private InputData _inputData;

        #region NonSerialized

        private float _movementSpeed;

        #endregion
        
        
        private void Awake()
        {
            InitializeMovementSpeed();
            
            #region LocalMethods

            void InitializeMovementSpeed()
            {
                _movementSpeed = _playerSettings.WalkingSpeed;
            }

            #endregion
        }

        private void FixedUpdate()
        {
            if (!IsPlayerMoving())
            {
                return;
            }
            
            Move();

            #region LocalMethods

            bool IsPlayerMoving()
            {
                return _inputData.CurrentPlayerMovement != Vector3.zero;
            }

            #endregion
        }

        private void Move()
        {
            _playerRigidbody.MovePosition(GetTargetPosition());

            #region LocalMethods

            Vector3 GetTargetPosition()
            {
                return _playerTransform.position + (_inputData.CurrentPlayerMovement.ToIsometric().normalized * (_movementSpeed * Time.deltaTime));
            }

            #endregion
        }
    }
}