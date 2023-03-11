using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "HPSphereConfig", menuName = "Configs/Hp sphere config")]
    public class HPSphereSO : ScriptableObject
    {
        [SerializeField] private int _hpGain = 1;

        public int HpGain => _hpGain;
    }
}
