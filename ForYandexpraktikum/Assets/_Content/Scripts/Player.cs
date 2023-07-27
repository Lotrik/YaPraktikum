using System;
using UnityEngine;

namespace _Content
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float _verticleForce = 5000f;
        [SerializeField] private float _horizontalSpeed = 5f;
        [SerializeField] private LayerMask _obstacleLayers;
        [SerializeField] private TrailRenderer _trail;
        private bool _moveEnabled = false;
        private bool _isDead;

        public bool IsDead => _isDead;

        private Rigidbody2D _rigidbody2D;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            if (_moveEnabled)
            {
                Move();
            }
        }

        private void Move()
        {
            transform.Translate(_horizontalSpeed * Time.deltaTime, 0f, 0f);

            if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
            {
                Jump();
            }
        }

        private void Jump()
        {
            _rigidbody2D.AddForce(Vector2.up * _verticleForce);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (_obstacleLayers.Contains(collision.gameObject.layer))
            {
                OnCollideWithObstacle(collision);
            }
        }

        private void OnCollideWithObstacle(Collision2D collision)
        {
            _isDead = true;
        }

        public void ResetPlayer(Vector3 initialPosition)
        {
            transform.position = initialPosition;
            _trail.Clear();
            _isDead = false;
        }

        public void DisableMovement()
        {
            _rigidbody2D.isKinematic = true;
            _rigidbody2D.velocity = Vector3.zero;
            _moveEnabled = false;
        }

        public void EnableMovement()
        {
            _rigidbody2D.isKinematic = false;
            _moveEnabled = true;
        }
        
    }
}