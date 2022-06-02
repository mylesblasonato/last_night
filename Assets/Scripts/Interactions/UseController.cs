using Fungus;
using MDI.Gamekit.Interactables;
using UnityEngine;

namespace Game.GameProductionOne{
    public class UseController : MonoBehaviour{
        #region SFIELDS
        [SerializeField] string playerCameraTag = "PlayerCamera";
        [SerializeField] Flowchart flowchart;
        [SerializeField] string flowUseBlockName;
        [SerializeField] string layerMask;
        [SerializeField] float useDistance = 1000f;
        [SerializeField] string useAxis = "Use";
        [SerializeField, Multiline]string tutorialText = "EXAMINE/USE [E/Y]";
        #endregion

        new Camera camera;
        Transform parent;
        Rigidbody rigidBody;
        Collider collider;
        InteractableController interactableController;

        // Interactables
        IUseable useInteractable;
        IGrabbable grabInteractable;

        void Awake(){
            GetReferences();
            InitialiseInteractables();
        }

        void Update()
        {
            UseOnInput(this.useAxis);
            ChangeMouseCursor();
        }

        #region ABSTRACTION
        void ChangeMouseCursor(){
            if (InteractableManager.Instance.ActiveInteractable != null){
                if (DetectInteractableAtDistance(useDistance))
                    CursorManager.Instance.ChangeMouseRadius(20f, 1f, name.ToUpper(), tutorialText);
                else{
                    if(InteractableManager.Instance.ActiveInteractable == parent)
                        CursorManager.Instance.ChangeMouseRadius(4f, 1f, parent.name.ToUpper(), tutorialText);
                }
            }
        }
        
        bool DetectInteractableAtDistance(float distance){
            if (InteractableManager.Instance.ActiveInteractable != null){
                RaycastHit hit;
                Ray ray = camera.ScreenPointToRay(InputManager.Instance.GetMousePosition());
                if (Physics.Raycast(ray, out hit, distance, ~(1 >> LayerMask.NameToLayer(layerMask))) && hit.transform == transform)
                    return true;
            }
            return false;
        }

        void UseOnInput(string useAxis){
            if (MDI.Gamekit.Input.OldUnityInput.Instance.GetButtonDown(useAxis) && DetectInteractableAtDistance(useDistance) && NotInteracting()){
                useInteractable.Use(flowUseBlockName);
            }
        }

        void GetReferences(){
            camera = Camera.main;
            parent = transform.parent;
            interactableController = parent.GetComponent<InteractableController>();
            collider = GetComponent<Collider>();
            rigidBody = GetComponent<Rigidbody>();
        }

        void InitialiseInteractables(){
            useInteractable = new UseInteractable(flowchart);
            grabInteractable = (GrabInteractable)interactableController.GetGrabInteractable();
        }

        void OnMouseOver(){
            if (InteractableManager.Instance.ActiveInteractable != null)
                CursorManager.Instance.ChangeMouseRadius(20f, 1f, name.ToUpper(), tutorialText);
        }

        void OnMouseExit(){
            if (InteractableManager.Instance.ActiveInteractable != null)
                CursorManager.Instance.ChangeMouseRadius(4f, 0f, name.ToUpper(), tutorialText);
        }

        static bool NotInteracting(){
            return FindObjectOfType<SayDialog>() == null;
        }
        #endregion
    }
}