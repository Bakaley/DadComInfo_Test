using ScriptableObjects;
using UnityEngine;

namespace MovementControllers
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovementController : MonoBehaviour, IMovementController
    {
        [SerializeField] private PlayerMovementSO _config;

        private Rigidbody _rigidbody;

        private bool _movementAllowed = true;
        private bool _attacking = false;
        private float _currentSpeedMultiplier = 1;
        private float _currentSpeedMagnitude;

        private Camera _mainCamera;
        private Vector3 _movementDirection;
    
        private PlayerInput _playerInput;

        public void SetMovementAllowed(bool newValue)
        {
            _movementAllowed = newValue;
        }

        public void SetAttackingMultiplier(bool newValue)
        {
            _attacking = newValue;
        }

        public float CurrentSpeedMagnitude => _currentSpeedMagnitude;
    
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _playerInput = new PlayerInput();
            _mainCamera = Camera.main;
        }

        private void Update()
        {
            ReadInput();
        }

        private void FixedUpdate()
        {
            if(_movementAllowed) Movement();
        }

        private void OnEnable()
        {
            _playerInput.Enable();
        }

        private void ReadInput()
        {
            var inputDir = _playerInput.KeyboardMouse.Movement.ReadValue<Vector2>();
            _movementDirection  = _mainCamera.transform.right * inputDir.x
                                  + _mainCamera.transform.forward * inputDir.y;
            _movementDirection.y = 0;

            _currentSpeedMagnitude = inputDir.magnitude;
        }

        private void Movement()
        {
            _currentSpeedMultiplier = _attacking ? _config.SpeedWhileAttackingMultipler : 1;
        
            Vector3 velocity = new Vector3(
                _movementDirection.x * _config.BasicSpeed * _currentSpeedMultiplier * Time.fixedDeltaTime,
                _rigidbody.velocity.y,
                _movementDirection.z * _config.BasicSpeed * _currentSpeedMultiplier * Time.fixedDeltaTime);
            _rigidbody.velocity = velocity;
        }

        private void OnDisable()
        {
            _playerInput.Disable();
        }
    }
}