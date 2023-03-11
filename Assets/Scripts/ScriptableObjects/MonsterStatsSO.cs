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

        public int OnHitDamage => _onHitDamage;
        public int HP => _hp;
        public int HPIncreasingPerDeath => _hpIncreasPerDeath;
        public float AttackDuration => _attackDuration;
        public float StaggeringDuration => _staggeringDuration;
    }
}
