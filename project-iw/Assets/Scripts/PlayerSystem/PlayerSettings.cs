using UnityEngine;

namespace CodeNameIW.PlayerSystem
{
    [CreateAssetMenu(fileName = "PlayerSettings", menuName = "ProjectI/PlayerSystem/Settings")]
    public class PlayerSettings : ScriptableObject
    {
        [Header("Movement")]
        [SerializeField] private float _walkingSpeed;

        [Header("Animation")]
        [SerializeField] private float _transitionTime;
        
        [Header("State Angles")]
        [SerializeField] private Vector2 _directForwardsAngles;
        [SerializeField] private Vector2 _rightForwardsAngles;
        [SerializeField] private Vector2 _directRightwardsAngles;
        [SerializeField] private Vector2 _rightBackwardsAngles;
        [SerializeField] private Vector2 _directBackwardsAngles;
        [SerializeField] private Vector2 _leftBackwardsAngles;
        [SerializeField] private Vector2 _directLeftwardsAngles;
        [SerializeField] private Vector2 _leftForwardsAngles;

        [Header("Interaction")]
        [SerializeField] private LayerMask _interactionLayerMask;
        [SerializeField] private Vector3 _interactionHalfExtents;

        public float WalkingSpeed => _walkingSpeed;
        public float TransitionTime => _transitionTime;
        public Vector2 DirectForwardsAngles => _directForwardsAngles;
        public Vector2 RightForwardsAngles => _rightForwardsAngles;
        public Vector2 DirectRightwardsAngles => _directRightwardsAngles;
        public Vector2 RightBackwardsAngles => _rightBackwardsAngles;
        public Vector2 DirectBackwardsAngles => _directBackwardsAngles;
        public Vector2 LeftBackwardsAngles => _leftBackwardsAngles;
        public Vector2 DirectLeftwardsAngles => _directLeftwardsAngles;
        public Vector2 LeftForwardsAngles => _leftForwardsAngles;
        public LayerMask InteractionLayerMask => _interactionLayerMask;
        public Vector3 InteractionHalfExtents => _interactionHalfExtents;
    }
}