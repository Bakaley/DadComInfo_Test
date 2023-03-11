using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class StartGameMenu : GeneralWindow
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _exitButton;

        public event Action PlayButtonPressed;
        public event Action ExitButtonPressed;

        public override void Open()
        {
            base.Open();
            _playButton.onClick.AddListener(PlayButtonHandle);
            _exitButton.onClick.AddListener(ExitButtonHandle);
        }

        public override void Close()
        {
            base.Close();
            _playButton.onClick.RemoveListener(PlayButtonHandle);
            _exitButton.onClick.RemoveListener(ExitButtonHandle);
        }

        private void PlayButtonHandle()
        {
            PlayButtonPressed?.Invoke();
        }
        
        private void ExitButtonHandle()
        {
            ExitButtonPressed?.Invoke();
        }
    }
}
