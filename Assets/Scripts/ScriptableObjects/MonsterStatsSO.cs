using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu (fileName = "MonsterStatConfig", menuName = "Configs/Monster stats config")]
    public class MonsterStatsSO : ScriptableObject
    {
        [SerializeField] private int _onHitDamage = 1;
        [SerializeField] private int _hp = 1;
        [SerializeField] private int _hpIncreasPerDeath = 1;
        [SerializeField] private float _attackDuration = 2f;
        [SerializeField] private float _staggeringDuration = .6f;
        [SerializeField] private float _respawnCooldown = 5;
        [SerializeField] private float _respawnRadius = 75;

        public int OnHitDamage => _onHitDamage;
        public int HP => _hp;
        public int HPIncreasingPerDeath => _hpIncreasPerDeath;
        public float AttackDuration => _attackDuration;
        public float StaggeringDuration => _staggeringDuration;
        public float RespawnCooldown => _respawnCooldown;
        public float RespawnRadius => _respawnRadius;
    }
}
