using CodeNameIW.EnvironmentSystem;
using UnityEngine;

namespace CodeNameIW.PlayerSystem
{
    public class PlayerInteractor : MonoBehaviour
    {
        public bool IsGizmosOn = false;
        
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private Transform _interactionTransform;
        [SerializeField] private PlayerSettings _playerSettings;

        private int _lastOverlapNumber = 0;
        private int _overlapNumber = 0;
        private int _activeInteractionID;
        private IInteractable _lastInteractable;
        private Collider _activeInteractionCollider;
        
        private Collider[] _overlapColliders = new Collider[5];

        private void Update()
        {
            _overlapNumber = Physics.OverlapBoxNonAlloc(_interactionTransform.position, _playerSettings.InteractionHalfExtents, _overlapColliders, _playerTransform.rotation, _playerSettings.InteractionLayerMask);

            if (_overlapNumber == _lastOverlapNumber)
            {
                return;
            }

            _lastOverlapNumber = _overlapNumber;

            if (_overlapNumber == 0)
            {
                _activeInteractionCollider.gameObject.GetComponent<IInteractable>().Disconnect();
                _activeInteractionCollider = null;
                return;
            }

            if (_overlapColliders[0] == _activeInteractionCollider)
            {
                return;
            }


            if (_activeInteractionCollider != null)
            {
                _activeInteractionCollider.gameObject.GetComponent<IInteractable>().Disconnect();
            }
            
            _overlapColliders[0].gameObject.GetComponent<IInteractable>().Interact();
            _activeInteractionCollider = _overlapColliders[0];
        }

        private void OnDrawGizmos()
        {
            if (!IsGizmosOn)
            {
                return;
            }
            
            Gizmos.color = Color.red;
            Gizmos.DrawCube(_interactionTransform.position, _playerSettings.InteractionHalfExtents * 2);
        }
    }
}