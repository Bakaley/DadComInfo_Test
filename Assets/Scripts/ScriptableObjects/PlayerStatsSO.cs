using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu (fileName = "PlayerStatConfig", menuName = "Configs/Player stats config")]
    public class PlayerStatsSO : ScriptableObject
    {
        [SerializeField] private int _onHitDamage = 1;
        [SerializeField] private int _hp = 20;
        [SerializeField] private float _attackDuration = 2f;
        [SerializeField] private float _staggeringDuration = .5f;

        public int OnHitDamage => _onHitDamage;
        public int HP => _hp;
        public float AttackDuration => _attackDuration;
        public float StaggeringDuration => _staggeringDuration;
    }
}
