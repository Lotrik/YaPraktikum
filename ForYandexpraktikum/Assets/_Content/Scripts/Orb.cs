using UnityEngine;

namespace _Content
{
    public class Orb : MonoBehaviour
    {
        [SerializeField] private float _speed = 3f;
        [SerializeField] private LayerMask _playerMask;
        [SerializeField] private GameManager _gameManager;

        private Rigidbody2D _rigidbody2D;
        private Transform _playersTransform;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _gameManager = FindObjectOfType<GameManager>();
        }

        private void FixedUpdate()
        {
            if (_playersTransform != null)
            {
                var directionToPlayer = (_playersTransform.position - transform.position).normalized;
                _rigidbody2D.velocity = directionToPlayer * _speed;
                if (Vector2.Distance(_rigidbody2D.position, _playersTransform.position) < 0.5f)
                {
                    CollectOrb();
                }
            }
        }

        private void CollectOrb()
        {
            Debug.Log("Orb Collected");
            Destroy(gameObject);
            _gameManager.AddScore(1);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_playerMask.Contains(other.gameObject.layer))
            {
                StartMovement(other.transform);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (_playerMask.Contains(other.gameObject.layer))
            {
                FinishMovement();
            }
        }

        private void StartMovement(Transform playerTransform)
        {
            _playersTransform = playerTransform;
        }

        private void FinishMovement()
        {
            _playersTransform = null;
            _rigidbody2D.velocity = Vector2.zero;
        }


    }
}