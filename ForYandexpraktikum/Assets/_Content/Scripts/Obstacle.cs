using UnityEngine;
using Random = UnityEngine.Random;

namespace _Content
{
    public class Obstacle : MonoBehaviour
    {
        [SerializeField] private Transform _obstacleTransform;
        [SerializeField] private Vector2 _randomYMoveDistance;
        [SerializeField] private float _speed;
        private Vector3 _firstPoint;
        private Vector3 _secondPoint;
        private Vector3 _currentDestination;


        private void Awake()
        {
            var distance = Random.Range(_randomYMoveDistance.x, _randomYMoveDistance.y);
            _firstPoint = transform.position + Vector3.up * distance / 2f;
            _secondPoint = transform.position + Vector3.down * distance / 2f;
            _currentDestination = _firstPoint;
        }

        private void Update()
        {
            if (Vector3.Distance(_obstacleTransform.position, _currentDestination) <= 0.1f)
            {
                if (_currentDestination == _firstPoint)
                {
                    _currentDestination = _secondPoint;
                }
                else
                {
                    _currentDestination = _firstPoint;
                }
            }
            else
            {
                _obstacleTransform.position =
                    Vector3.MoveTowards(_obstacleTransform.position, _currentDestination, _speed * Time.deltaTime);
            }
        }
    }
}