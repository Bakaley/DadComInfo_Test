using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "CameraConfig", menuName = "Configs/Camera config")]
    public class CameraControlSO : ScriptableObject
    {
        [SerializeField] private float _upAngleClamp = 340;
        [SerializeField] private float _downAngleClamp = 40;
        [SerializeField] private float _mouseSensitivity = 0.1f;

        public float UpAngleClamp => _upAngleClamp;
        public float DownAngleClamp => _downAngleClamp;
        public float MouseSensitivity => _mouseSensitivity;
    }
}
