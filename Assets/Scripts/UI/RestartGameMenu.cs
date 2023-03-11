using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class RestartGameMenu : GeneralWindow
    {
        [SerializeField] private TextMeshProUGUI _restartMenuScore;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _exitButton;

        private string _scoreString = "Score: ";
        private Level _level;

        public event Action RestartButtonPressed;
        public event Action ExitButtonPressed;
        
        public void Init(Level level)
        {
            _level = level;
        }
        
        public override void Open()
        {
            _level.OnScoreChanged += RefreshScore;
            RefreshScore(_level.CurrentScore);
            base.Open();
            _restartButton.onClick.AddListener(RestartButtonHandle);
            _exitButton.onClick.AddListener(ExitButtonHandle);
        }

        public override void Close()
        {
            _level.OnScoreChanged -= RefreshScore;
            base.Close();
            _restartButton.onClick.RemoveListener(RestartButtonHandle);
            _exitButton.onClick.RemoveListener(ExitButtonHandle);
        }

        private void RefreshScore(int newValue)
        {
            _restartMenuScore.text = _scoreString + newValue;
        }
        
        private void RestartButtonHandle()
        {
            RestartButtonPressed?.Invoke();
        }
        
        private void ExitButtonHandle()
        {
            ExitButtonPressed?.Invoke();
        }
    }
}
