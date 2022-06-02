using Fungus;
using UnityEngine;

namespace Game.GameProductionOne
{
    public class HotspotController : MonoBehaviour
    {
        [SerializeField] GameObject interactable;
        [SerializeField] Flowchart flowchart;
        [SerializeField] string blockName = "Detection";
        [SerializeField] bool onlyPlayOnce = true;

        MeshRenderer meshRenderer;
        Collider collider;
        bool hasTriggered = false;

        void Awake()
        {
            meshRenderer = GetComponent<MeshRenderer>();
            collider = GetComponent<Collider>();
        }

        void Update()
        {
            if (InteractableManager.Instance.ActiveInteractable == null) return;
            if (InteractableManager.Instance.ActiveInteractable.gameObject != interactable) return;
            collider.enabled = true;
            hasTriggered = false;
            meshRenderer.enabled = true;
        }

        void OnTriggerStay(Collider other)
        {
            if (other.gameObject == interactable && !hasTriggered)
            {
                hasTriggered = true;
                collider.enabled = false;
                flowchart.transform.parent = transform.parent;
                flowchart.ExecuteBlock(blockName);
                if (onlyPlayOnce)
                {
                    gameObject.SetActive(false);
                }
                else
                    meshRenderer.enabled = false;
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.gameObject == interactable && !onlyPlayOnce)
            {
                if (InteractableManager.Instance.ActiveInteractable.gameObject == interactable)
                {
                    collider.enabled = true;
                    hasTriggered = false;
                    meshRenderer.enabled = true;
                }
            }
        }
    }
}