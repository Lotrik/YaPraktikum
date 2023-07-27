using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace _Content
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private LevelGenerator _levelGenerator;
        [SerializeField] private Player _player;
        [SerializeField] private List<PlayerFollower> _playerFollowers;
        [SerializeField] private GameObject _startGameHint;
        [SerializeField] private TextMeshPro _scoreUI;
        private int _currentScores;
        private GameState _gameState;
        
        private void Start()
        {
            InitPlayerFollowers();
            InitLevelGenerator();
            NewGame();
        }

        private void Update()
        {
            switch (_gameState)
            {
                case GameState.WaitingForStartGame:
                    HandleWaitingForStartGame();
                    break;
                case GameState.GameInProgress:
                    HandleGameInProgress();
                    break;
            }
        }

        private void HandleGameInProgress()
        {
            if (_player.IsDead)
            {
                GameOver();
            }
        }

        private void HandleWaitingForStartGame()
        {
            if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
            {
                StartGame();
            }
        }

        private void StartGame()
        {
            SetVisibleStartGameHint(false);
            _player.EnableMovement();
            _levelGenerator.OnLevelStart();
            _gameState = GameState.GameInProgress;
        }

        private void InitLevelGenerator()
        {
            _levelGenerator.SetPlayer(_player.transform);
        }
        
        private void InitPlayerFollowers()
        {
            foreach (var playerFollower in _playerFollowers)
            {
                playerFollower.SetPlayer(_player.transform);
            }
        }

        private void NewGame()
        {
            _gameState = GameState.WaitingForStartGame;
            _player.ResetPlayer(Vector3.zero);
            _player.DisableMovement();
            SetVisibleStartGameHint(true);
            _currentScores = 0;
            UpdateUIScore();
            UpdateLevelGenerator();
            _levelGenerator.GenerateLevel();
        }

        private void SetVisibleStartGameHint(bool visible)
        {
            _startGameHint.SetActive(visible);
        }

        private void GameOver()
        {
            _levelGenerator.OnLevelEnd();
            NewGame();
        }
        
        public void AddScore(int value)
        {
            _currentScores += value;
            UpdateUIScore();
            UpdateLevelGenerator();
        }

        private void UpdateLevelGenerator()
        {
            _levelGenerator.SetCurrentLevelScores(_currentScores);
        }

        private void UpdateUIScore()
        {
            _scoreUI.text = _currentScores.ToString();
        }
    }
}