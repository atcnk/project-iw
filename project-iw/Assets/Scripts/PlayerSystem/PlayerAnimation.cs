using System;
using UnityEngine;

namespace CodeNameIW.PlayerSystem
{
    public class PlayerAnimation : MonoBehaviour
    {
        [Header("Self-Components")]
        [SerializeField] private Animator _animator;
        [SerializeField] private PlayerStateController _playerStateController;
        
        [Header("Settings")]
        [SerializeField] private PlayerSettings _playerSettings;

        #region Static-Readonly & Contants

        private static readonly int PlayerIdleUnarmedHashName = Animator.StringToHash("Player_Idle_Unarmed");
        private static readonly int PlayerMoveForwardsHashName = Animator.StringToHash("Player_Move_Forwards");
        private static readonly int PlayerMoveBackwardsHashName = Animator.StringToHash("Player_Move_Backwards");
        private static readonly int PlayerMoveRightwardsHashName = Animator.StringToHash("Player_Move_Rightwards");
        private static readonly int PlayerMoveLeftwardsHashName = Animator.StringToHash("Player_Move_Leftwards");
        private static readonly int PlayerMoveRightForwardsHashName = Animator.StringToHash("Player_Move_RightForwards");
        private static readonly int PlayerMoveLeftForwardsHashName = Animator.StringToHash("Player_Move_LeftForwards");
        private static readonly int PlayerMoveRightBackwardsHashName = Animator.StringToHash("Player_Move_RightBackwards");
        private static readonly int PlayerMoveLeftBackwardsHashName = Animator.StringToHash("Player_Move_LeftBackwards");

        private const int FullBodyLayer = 0;
        private const int UpperBodyLayer = 1;
        private const int LowerBodyLayer = 2;
        
        private float _transitionTime;

        #endregion

        private void OnEnable()
        {
            SubscribeToOnPlayerStateChangedEvent();

            #region LocalMethods

            void SubscribeToOnPlayerStateChangedEvent()
            {
                _playerStateController.OnPlayerStateChanged += UpdatePlayerAnimation;
            }

            #endregion
        }

        private void OnDisable()
        {
            UnsubscribeFromOnPlayerStateChangedEvent();

            #region LocalMethods

            void UnsubscribeFromOnPlayerStateChangedEvent()
            {
                _playerStateController.OnPlayerStateChanged -= UpdatePlayerAnimation;
            }

            #endregion
        }

        private void UpdatePlayerAnimation(PlayerState oldState, PlayerState newState)
        {
            SetTransitionTimeByState();
            
            switch (newState)
            {
                case PlayerState.IdleUnarmed:
                    UpdateFullBodyAnimation(PlayerIdleUnarmedHashName);
                    return;
                case PlayerState.WalkDirectForwards:
                    UpdateFullBodyAnimation(PlayerMoveForwardsHashName);
                    return;
                case PlayerState.WalkDirectBackwards:
                    UpdateFullBodyAnimation(PlayerMoveBackwardsHashName);
                    return;
                case PlayerState.WalkDirectRightwards:
                    UpdateFullBodyAnimation(PlayerMoveRightwardsHashName);
                    return;
                case PlayerState.WalkDirectLeftwards:
                    UpdateFullBodyAnimation(PlayerMoveLeftwardsHashName);
                    return;
                case PlayerState.WalkRightForwards:
                    UpdateFullBodyAnimation(PlayerMoveRightForwardsHashName);
                    return;
                case PlayerState.WalkRightBackwards:
                    UpdateFullBodyAnimation(PlayerMoveRightBackwardsHashName);
                    return;
                case PlayerState.WalkLeftBackwards:
                    UpdateFullBodyAnimation(PlayerMoveLeftBackwardsHashName);
                    return;
                case PlayerState.WalkLeftForwards:
                    UpdateFullBodyAnimation(PlayerMoveLeftForwardsHashName);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            #region LocalMethods

            void SetTransitionTimeByState()
            {
                _transitionTime = (oldState == PlayerState.IdleUnarmed || newState == PlayerState.IdleUnarmed) ? 0f : _playerSettings.TransitionTime;
            }

            void UpdateFullBodyAnimation(int stateHashName)
            {
                _animator.CrossFadeInFixedTime(stateHashName, _transitionTime, FullBodyLayer);
            }

            void UpdateUpperAndLowerBodyLayersAnimation(int upperStateHashName, int lowerStateHashName)
            {
                _animator.CrossFadeInFixedTime(upperStateHashName, _transitionTime, UpperBodyLayer);
                _animator.CrossFadeInFixedTime(lowerStateHashName, _transitionTime, LowerBodyLayer);
            }

            #endregion
        }
    }
}