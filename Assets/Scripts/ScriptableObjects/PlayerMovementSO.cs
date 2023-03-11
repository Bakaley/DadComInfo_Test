using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "PlayerMovementConfig", menuName = "Configs/Player movement config")]
    public class PlayerMovementSO : ScriptableObject
    {
        [SerializeField] private float _basicSpeed = 25;
        [SerializeField] private float _speedWhileAttackingMultipler = .5f;

        public float BasicSpeed => _basicSpeed;
        public float SpeedWhileAttackingMultipler => _speedWhileAttackingMultipler;
    }
}
