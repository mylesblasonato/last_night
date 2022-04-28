using UnityEngine;

namespace MDI.Gamekit.Interactables
{
    public class GrabInteractable : IInteractable, IGrabbable
    {
        Rigidbody interactableRb;
        Transform interactableTransform;
        public bool IsGrabbing { get; set; } = false;

        public GrabInteractable(Rigidbody interactableRb, Transform interactableTransform)
        {
            this.interactableRb = interactableRb;
            this.interactableTransform = interactableTransform;
        }

        public void Grab(Transform transform, Vector3 offset)
        {
            if (IsGrabbing) return;
            IsGrabbing = true;
            this.interactableRb.useGravity = false;         
            this.interactableRb.isKinematic = false;
            this.interactableTransform.parent = transform;
            this.interactableTransform.rotation = Quaternion.identity;
            this.interactableTransform.localPosition = offset;
        }

        public void Release(Transform parent)
        {
            this.interactableRb.useGravity = true;
            //this.interactableRb.isKinematic = false;
            this.interactableTransform.parent = parent != null ? parent : null;
            IsGrabbing = false;
        }
    }
}