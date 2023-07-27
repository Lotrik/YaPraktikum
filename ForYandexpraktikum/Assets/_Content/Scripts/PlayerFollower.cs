using UnityEngine;

namespace _Content
{
    public class PlayerFollower : MonoBehaviour
    {
        private Transform _player;
        [SerializeField] private float _xOffcet;

        private void LateUpdate()
        {
            if (_player == null)
                return;
            var cameraPosition = transform.position;
            cameraPosition.x = _player.position.x + _xOffcet;
            transform.position = cameraPosition;
        }

        public void SetPlayer(Transform playerTransform)
        {
            _player = playerTransform;
        }
    }
}