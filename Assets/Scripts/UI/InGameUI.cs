using StateControllers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class InGameUI : GeneralWindow
    {
        [SerializeField] private Slider _hpSlider;
        [SerializeField] private TextMeshProUGUI _inGameScore;
        private string _scoreString = "Score: ";
        private PlayerStateController _player;
        private Level _level;

        public void Init(PlayerStateController player, Level level)
        {
            _player = player;
            _level = level;
        }
        
        private void RefreshScore(int newValue)
        {
            _inGameScore.text = _scoreString + newValue;
        }

        private void RefreshHPSlider(int newValue)
        {
            _hpSlider.value = 1.0f * newValue / _player.MaxHP;
        }
        
        public override void Open()
        {
            _level.OnScoreChanged += RefreshScore;
            RefreshScore(_level.CurrentScore);
            _player.OnHpChanged += RefreshHPSlider;
            RefreshHPSlider(_player.CurrentHP);
            base.Open();
        }

        public override void Close()
        {
            _level.OnScoreChanged -= RefreshScore;
            _player.OnHpChanged -= RefreshHPSlider;
            base.Close();
        }
    }
}
