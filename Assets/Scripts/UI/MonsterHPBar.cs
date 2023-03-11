using StateControllers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Canvas))]
    [RequireComponent(typeof(CanvasGroup))]
    public class MonsterHPBar : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        private Canvas _canvas;
        private CanvasGroup _canvasGroup;

        private MonsterStateController _monster;
        private bool _active = false;
        private Camera _camera;

        public void Init(MonsterStateController monster)
        {
            _monster = monster;
            _camera = Camera.main;
            _canvas.worldCamera = _camera;
            _monster.OnHpChanged += RefreshHPBar;
        }
    
        public void Show()
        {
            _active = true;
            RefreshHPBar(_monster.CurrentHP);
            _canvasGroup.alpha = 1;
        }

        public void Hide()
        {
            _active = false;
            _canvasGroup.alpha = 0;
        }

        private void Awake()
        {
            _canvas = GetComponent<Canvas>();
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        private void Update()
        {
            if (_active) transform.LookAt(_camera.transform);
        }

        private void RefreshHPBar(int newValue)
        {
            _slider.value = 1.0f * newValue / _monster.MaxHP;
        }
    }
}
