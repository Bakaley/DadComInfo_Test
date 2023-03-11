using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "LootGenConfig", menuName = "Configs/Loot generation config")]
    public class LootGenerationSO : ScriptableObject
    {
        [SerializeField] private float _hpSphereProbability = .25f;
        [SerializeField] private HPSphere _hpSphereSampler;

        public float HpSphereProbability => _hpSphereProbability;
        public HPSphere HpSphereSampler => _hpSphereSampler;
    }
}
