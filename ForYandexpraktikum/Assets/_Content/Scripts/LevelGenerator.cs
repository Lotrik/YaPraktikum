using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Content
{
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField] private float _xOffsetForComplexObstacle = 1f;
        [SerializeField] private Vector2 _minMaxY;
        [SerializeField] private Orb _orbPrefab;
        [SerializeField] private Obstacle _obstaclePrefab;
        [SerializeField] private Transform _startLevelPos;
        private Transform _playerTransform;

        private List<Transform> _currentObstacles = new List<Transform>();
        private List<Transform> _currentOrbs = new List<Transform>();
        private float _lastObstaclePositionX;
        private float _lastOrbPositionX;
        private bool _gameInProgress;

        private int _initialObstaclesCount = 6;
        private int _initialOrbsCount = 5;
        private int _obstacleInRow = 1;

        private void Update()
        {
            if (_gameInProgress)
            {
                HandleObstacles();
                HandleOrbs();
            }
        }

        private void HandleOrbs()
        {
            _currentOrbs.RemoveAll(o => o == null);
            foreach (var orb in _currentOrbs.ToList())
            {
                if (orb.position.x - _playerTransform.position.x < -15f)
                {
                    _currentOrbs.Remove(orb);
                    Destroy(orb.gameObject);
                }
            }

            var orbsToCreate = _initialOrbsCount - _currentOrbs.Count;

            for (int i = 0; i < orbsToCreate; i++)
            {
                var pos = _lastOrbPositionX + Random.Range(10f, 14f);
                CreateOrb(pos);
                _lastOrbPositionX = pos;
            }
        }

        private void HandleObstacles()
        {
            var destroyedObstacles = 0;
            foreach (var obstacle in _currentObstacles.ToList())
            {
                if (obstacle.position.x - _playerTransform.position.x < -15f)
                {
                    _currentObstacles.Remove(obstacle);
                    Destroy(obstacle.gameObject);
                    destroyedObstacles++;
                }
            }

            for (int i = 0; i < destroyedObstacles; i++)
            {
                var pos = _lastObstaclePositionX + Random.Range(8f, 12f);
                CreateObstacle(pos, _obstacleInRow);
                _lastObstaclePositionX = pos;
            }
        }

        public void OnLevelStart()
        {
            _gameInProgress = true;
        }

        public void OnLevelEnd()
        {
            _gameInProgress = false;
        }

        public void GenerateLevel()
        {
            foreach (var currentOrb in _currentOrbs)
            {
                if (currentOrb != null)
                    Destroy(currentOrb.gameObject);
            }

            _currentOrbs.Clear();

            foreach (var obstacle in _currentObstacles)
            {
                if (obstacle != null)
                    Destroy(obstacle.gameObject);
            }

            _currentObstacles.Clear();

            _lastObstaclePositionX = _startLevelPos.position.x;
            for (int i = 0; i < _initialObstaclesCount; i++)
            {
                var positionX = _lastObstaclePositionX;
                if (i > 0)
                {
                    positionX += Random.Range(8f, 12f);
                }

                CreateObstacle(positionX, 1);
                _lastObstaclePositionX = positionX;
            }

            _lastOrbPositionX = _startLevelPos.position.x;
            for (int i = 0; i < _initialOrbsCount; i++)
            {
                var positionX = _lastOrbPositionX;
                if (i > 0)
                {
                    positionX += Random.Range(10f, 14f);
                }

                CreateOrb(positionX);
                _lastOrbPositionX = positionX;
            }
        }

        private void CreateOrb(float positionX)
        {
            var position = new Vector3(positionX, Random.Range(_minMaxY.x, _minMaxY.y), 0f);
            var orb = Instantiate(_orbPrefab, position, Quaternion.identity);
            _currentOrbs.Add(orb.transform);
        }

        private void CreateObstacle(float positionX, int obstaclesCount)
        {
            var obstacleParent = new GameObject("Obstacle Parent");
            obstacleParent.transform.position = new Vector3(positionX, 0f, 0f);
            for (int i = 0; i < obstaclesCount; i++)
            {
                var position = new Vector3(positionX + i * _xOffsetForComplexObstacle,
                    Random.Range(_minMaxY.x, _minMaxY.y), 0f);
                var obstacle = Instantiate(_obstaclePrefab, position, Quaternion.identity);
                obstacle.transform.SetParent(obstacleParent.transform);
            }

            _currentObstacles.Add(obstacleParent.transform);
        }

        public void SetPlayer(Transform player)
        {
            _playerTransform = player;
        }

        public void SetCurrentLevelScores(int currentScores)
        {
            if (currentScores >= 12)
            {
                _obstacleInRow = 4;
            }
            else if (currentScores >= 8)
            {
                _obstacleInRow = 3;
            }
            else if (currentScores >= 4)
            {
                _obstacleInRow = 2;
            }
            else
            {
                _obstacleInRow = 1;
            }
        }
    }
}