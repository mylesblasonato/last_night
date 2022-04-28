using UnityEngine;

namespace MDI.Gamekit.Interactables
{
    public interface IInspectable
    {
        public bool IsInspecting {get; set;}

        public void Inspect();
        public void Rotate(float speed, Transform transform, Vector2 mousePosition);
        public void StopRotate(Transform offset, Transform parent);
        public void Release();
    }
}