using UnityEngine;

namespace MDI.Gamekit.CharacterController
{
    [System.Serializable, CreateAssetMenu(fileName = "New Player Data", menuName = "MDI/Player/Create New Player Data ")]
    public class PlayerData : ScriptableObject
    {
        public Transform transform;
        [Range(0, 50f)] public float speed;
        [Range(1, 3f)] public float runMultiplier;
        [Range(0, 300f)] public float sensitivity;
        [Range(0, 0.5f)] public float idleShakeAmplitude;
        [Range(0, 5f)] public float moveShakeAmplitude;
    }
}
