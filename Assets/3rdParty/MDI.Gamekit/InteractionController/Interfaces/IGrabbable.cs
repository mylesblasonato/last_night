using UnityEngine;

namespace MDI.Gamekit.Interactables
{
    public interface IGrabbable
    {
        public bool IsGrabbing { get; set; }
        public void Grab(Transform transform, Vector3 offset);
        public void Release(Transform parent);
    }
}
