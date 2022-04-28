using UnityEngine;

namespace MDI.Gamekit.Interactables
{
    public class InspectInteractable : IInteractable, IInspectable
    {
        Vector2 posLastFrame;
        public bool IsInspecting { get; set; } = false;

        public void Inspect()
        {
            if (IsInspecting) return;
            IsInspecting = true;
        }

        public void Rotate(float speed, Transform transform, Vector2 mousePosition)
        {
            transform.GetChild(0).localPosition = Vector3.zero;
            transform.Rotate(-(mousePosition.x * speed * Time.deltaTime), 0, (mousePosition.y * speed * Time.deltaTime), Space.Self);
        }

        public void StopRotate(Transform offset, Transform parent)
        {
            offset.parent = parent;
        }

        public void Release()
        {
            if (!IsInspecting) return;
            IsInspecting = false;
        }
    }
}