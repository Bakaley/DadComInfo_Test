using System;
using System.Collections;
using ScriptableObjects;
using StateControllers;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace MovementControllers
{
    public class MonsterMovementController : MonoBehaviour, IMovementController
    {
        [SerializeField] private MonsterMovementSO _config;
        private MonsterStateController _monster;
        private NavMeshAgent _navMeshAgent;
        private float _maxDistanceSamplePos = 500;

        private bool _attacking;
        private bool _movementAllowed = false;
        private float _currentSpeedMultiplier;
        private float _currentSpeedMagnitude;

        private Coroutine _chasingCoroutine;
        
        public float CurrentSpeedMagnitude => _navMeshAgent.velocity.magnitude;

        public void Init(MonsterStateController monster)
        {
            _monster = monster;
        }
    
        public void SetMovementAllowed(bool newValue)
        {
            if (newValue != _movementAllowed)
            {
                _movementAllowed = newValue;
                if (_movementAllowed)
                {
                    _chasingCoroutine = StartCoroutine(RefreshDestination(_config.PathfindingRefreshingInterval));
                    _navMeshAgent.isStopped = false;
                }
                else
                {
                    StopCoroutine(_chasingCoroutine);
                    _navMeshAgent.isStopped = true;
                }
            }
        }

        public void SetAttackingMultiplier(bool newValue)
        {
            _attacking = newValue;
            _currentSpeedMultiplier = _attacking ? _config.SpeedWhileAttackingMultipler : 1;
            _navMeshAgent.speed = _config.BasicSpeed * _currentSpeedMultiplier;
        }

        public void Warp(Vector3 newPos)
        {
            NavMesh.SamplePosition(newPos, out NavMeshHit myNavHit, _maxDistanceSamplePos, -1);
            _navMeshAgent.Warp(myNavHit.position);
        }
        
        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _navMeshAgent.speed = _config.BasicSpeed;
        }

        private void Update()
        {
            if (_movementAllowed && _monster.PlayerToAttack) transform.LookAt(_monster.PlayerToAttack.transform.position);
        }

        private IEnumerator RefreshDestination(float interval)
        {
            WaitForSeconds wait = new WaitForSeconds(interval);
            while (true)
            {
                if(_movementAllowed && _monster.PlayerToAttack) _navMeshAgent.SetDestination(_monster.PlayerToAttack.transform.position);
                yield return wait;
            }
        }
    }
}
