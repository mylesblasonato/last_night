using UnityEngine;

namespace MDI.Gamekit.Interactables
{
    public interface IOpenable
    {
        public void Open(float force, Rigidbody rigidbody, Vector3 mouseLastFrame, bool isFront);
        public void Open(float speed, float duration, Transform transform, Vector3 axis);
        public void OpenDrawer(float force, Rigidbody rigidbody, Vector3 mouseLastFrame, bool isFront);
        public void OpenDrawer(float amount, float duration, Transform transform);
    }
}