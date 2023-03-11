using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "MonsterMovementConfig", menuName = "Configs/Monster movement config")]
    public class MonsterMovementSO : ScriptableObject
    {
        [SerializeField] private float _basicSpeed = 25;
        [SerializeField] private float _speedWhileAttackingMultipler = .5f;
        [SerializeField] private float _pathfindingRefreshingInterval = 0.1f;

        public float BasicSpeed => _basicSpeed;
        public float SpeedWhileAttackingMultipler => _speedWhileAttackingMultipler;

        public float PathfindingRefreshingInterval => _pathfindingRefreshingInterval;
    }
}
