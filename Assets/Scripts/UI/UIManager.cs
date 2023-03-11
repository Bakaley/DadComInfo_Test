using StateControllers;
using UnityEngine;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Level _level;
        [SerializeField] private StartGameMenu _startGameMenu;
        [SerializeField] private InGameUI _inGameUI;
        [SerializeField] private RestartGameMenu _restartGameMenu;

        private PlayerStateController _player;
        private GeneralWindow _currentUI;

        private void Awake()
        {
            SwitchUI(_startGameMenu);
            _startGameMenu.PlayButtonPressed += StartGamePressedHandle;
            _startGameMenu.ExitButtonPressed += ExitGamePressedHandle;
            _restartGameMenu.RestartButtonPressed += RestartGamePressedHandle;
            _restartGameMenu.ExitButtonPressed += ExitGamePressedHandle;
        }

        private void SwitchUI(GeneralWindow ui)
        {
            if(_currentUI) _currentUI.Close();
            _currentUI = ui;
            _currentUI.Open();
        }

        private void StartGamePressedHandle()
        {
            StartGame();
        }

        private void ExitGamePressedHandle()
        {
            Debug.Log("Exit button pressed");
        }

        private void RestartGamePressedHandle()
        {
            _level.RestartGame();
            SwitchUI(_inGameUI);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        
        private void StartGame()
        {
            _player = _level.StartGame();
            _player.OnDie += PlayerDeadHandle;
            _inGameUI.Init(_player, _level);
            _restartGameMenu.Init(_level);
            SwitchUI(_inGameUI);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void PlayerDeadHandle(BattleCharacterStateController player)
        {
            SwitchUI(_restartGameMenu);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
