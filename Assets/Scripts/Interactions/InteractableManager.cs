using UnityEngine;

namespace Game.ProjectZ{
    public class InteractableManager : MonoSingleton<InteractableManager>
    {
        void Awake()
        {
            CreateSingleton(this, () => Destroy(this.gameObject));
        }

        public Transform ActiveInteractable { get; private set; } = null;

        public void SetActiveInteractable(Transform newActiveInteractable)
        {
            ActiveInteractable = newActiveInteractable;
        }

        public Transform ActiveOpenable { get; private set; } = null;

        public void SetActiveOpenable(Transform newActiveOpenable)
        {
            ActiveOpenable = newActiveOpenable;
        }
    }
}
