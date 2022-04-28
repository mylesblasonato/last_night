using Fungus;
using MDI.Gamekit.Interactables;
using UnityEngine;
using MDI.Gamekit.Core;

namespace Game.ProjectZ
{
    public class InteractableController : MonoBehaviour
    {
        #region SFIELDS
        [SerializeField] string playerCameraTag = "PlayerCamera";
        [SerializeField] Flowchart flowchart;
        [SerializeField] string flowExamineBlockName;
        [SerializeField] string layerMask;
        [SerializeField] float examineDistance = 1000f;
        [SerializeField] string examineAxis = "Examine";
        [SerializeField] Vector3 grabViewOffset;
        [SerializeField] Vector3 grabViewRotationOffset;
        [SerializeField] float grabDistance = 1f;
        [SerializeField] string grabAxis = "Grab";
        [SerializeField] Transform grabOffset;
        [SerializeField] string inspectAxis = "Inspect";
        [SerializeField] float inspectDistance = 1f;
        [SerializeField] float inspectRotationSpeed = 1f;
        [SerializeField] Transform inspectOffset;
        [SerializeField] bool isDoor = false;
        [SerializeField] string defaultLayer = "Interactable";
        [SerializeField] string grabLayer = "GrabbedObject";
        [SerializeField, Multiline] string tutorial = "INTERACT [A]\nGRAB [X]";
        [SerializeField] SEvent OnGrab;
        [SerializeField] GameObject[] useObjects;
        [SerializeField] string floorObjectName = "Floor";
        #endregion

        new Camera camera;
        Transform parent;
        new Collider collider;
        Rigidbody rigidBody;
        Flowchart mainFlow;
        PlayerController playerController;

        // Interactables
        IExaminable examineInteractable;
        IGrabbable grabInteractable;
        IInspectable inspectInteractable;

        void Awake()
        {
            GetReferences();
            InitialiseInteractables();
        }

        void Update()
        {
            if (InteractableManager.Instance.ActiveOpenable != null) return;
            ExamineOnInput(this.examineAxis);
            GrabOnInput(this.grabAxis);
            InspectOnInput(this.inspectAxis);
        }

        #region ABSTRACTION
        bool DetectInteractableAtDistance(float distance)
        {
            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(InputManager.Instance.GetMousePosition());
            if (Physics.Raycast(ray, out hit, distance, ~(1 >> LayerMask.NameToLayer(layerMask))) && hit.transform == transform)
                return true;

            if (InteractableManager.Instance.ActiveInteractable == transform)
                return true;
            return false;
        }

        void ExamineOnInput(string examineAxis)
        {
            if (InputManager.Instance.GetButtonDown(examineAxis) && DetectInteractableAtDistance(examineDistance) && NotInteracting())
                examineInteractable.Examine(flowExamineBlockName);
        }

        void GrabOnInput(string grabAxis)
        {
            if (InputManager.Instance.GetButtonDown(grabAxis))
            {
                if (DetectInteractableAtDistance(grabDistance) && NotInteracting())
                {
                    if (!grabInteractable.IsGrabbing && !InteractableManager.Instance.ActiveInteractable)
                    {
                        if (rigidBody != null)
                        {
                            Grab();
                        }
                        else
                        {
                            CheckIfDoor();
                        }
                    }
                    else
                    {
                        foreach (var useObject in useObjects)
                        {
                            useObject.SetActive(false);
                        }

                        if (grabOffset.childCount > 0)
                        {
                            grabOffset.gameObject.SetLayer(9);
                            grabOffset.GetChild(0).gameObject.SetLayer(3);
                        }
                        grabInteractable.Release(parent);
                        collider.isTrigger = false;
                        CursorManager.Instance.ChangeMouseRadius(4f, 0f, name.ToUpper(), tutorial);
                        inspectInteractable.IsInspecting = false;
                        InteractableManager.Instance.SetActiveInteractable(null);
                        playerController.TurnCameraFollowOn(playerController.GetCameraBehaviour());
                    }
                }
            }
        }

        void CheckIfDoor()
        {
            if (isDoor)
                mainFlow.ExecuteBlock("DoorLocked");
            else
                mainFlow.ExecuteBlock("ToBigToGrab");
        }

        void InspectOnInput(string inspectAxis)
        {
            if (InputManager.Instance.GetButtonDown(inspectAxis))
            {
                
                if (!inspectInteractable.IsInspecting && grabInteractable.IsGrabbing)
                {
                    playerController.TurnCameraFollowOff(playerController.GetCameraBehaviour());
                    inspectInteractable.Inspect();
                }
                else
                {
                    if (grabInteractable.IsGrabbing)
                    {
                        CursorManager.Instance.ChangeMouseRadius(20f, 1f, name.ToUpper(), "INSPECT [R1/RMB]");
                        playerController.TurnCameraFollowOn(playerController.GetCameraBehaviour());
                        inspectInteractable.Release();
                    }
                }
            }

            if (inspectInteractable.IsInspecting && grabInteractable.IsGrabbing)
            {
                CursorManager.Instance.SetInteractableName("");
                CursorManager.Instance.ChangeMouseRadius(20f, 1f, "", "");
                playerController.TurnCameraFollowOff(playerController.GetCameraBehaviour());
                var pos = new Vector2(-InputManager.Instance.GetAxis("Mouse Y"), -InputManager.Instance.GetAxis("Mouse X"));
                inspectInteractable.Rotate(inspectRotationSpeed, transform, pos);
            }
        }

        void GetReferences()
        {
            camera = Camera.main;
            parent = transform.parent;
            collider = GetComponent<Collider>();
            rigidBody = GetComponent<Rigidbody>();
            mainFlow = GameObject.FindGameObjectWithTag("MainFlow").GetComponent<Flowchart>();
            playerController = FindObjectOfType<PlayerController>();
        }

        void InitialiseInteractables()
        {
            examineInteractable = new ExamineInteractable(flowchart);
            grabInteractable = new GrabInteractable(rigidBody, transform);
            inspectInteractable = new InspectInteractable();
        }

        void Grab()
        {

            rigidBody.velocity = Vector3.zero;
            grabInteractable.Grab(grabOffset, grabViewOffset);

            if (grabOffset.childCount > 0)
            {
                foreach (var useObject in useObjects)
                {
                    useObject.SetActive(true);
                }

                DoNotClip();

                CursorManager.Instance.ChangeMouseRadius(20f, 1f, name.ToUpper(), "INSPECT [R1/RMB]");
                grabOffset.GetChild(0).localEulerAngles = grabViewRotationOffset;
            }

            collider.isTrigger = true;
            InteractableManager.Instance.SetActiveInteractable(transform);
            OnGrab?.Raise();
        }

        void DoNotClip()
        {
            grabOffset.gameObject.SetLayer(9);
            grabOffset.GetChild(0).gameObject.SetLayer(9);
        }

        void OnCollisionEnter(UnityEngine.Collision collision)
        {
            if(string.Compare(collision.gameObject.name, floorObjectName) == 0)
            {
                transform.parent = collision.gameObject.transform;
            }
        }

        void OnMouseOver()
        {
            if (InteractableManager.Instance.ActiveInteractable != null) return;
            if (!grabInteractable.IsGrabbing && InteractableManager.Instance.ActiveOpenable == null)
                CursorManager.Instance.ChangeMouseRadius(20f, 1f, name.ToUpper(), tutorial);
        }

        void OnMouseExit()
        {
            if (InteractableManager.Instance.ActiveInteractable != null) return;
            if (!grabInteractable.IsGrabbing && InteractableManager.Instance.ActiveOpenable == null)
                CursorManager.Instance.ChangeMouseRadius(4f, 0f, name.ToUpper(), tutorial);
        }

        static bool NotInteracting() =>
             FindObjectOfType<SayDialog>() == null;

        public IGrabbable GetGrabInteractable() =>
            grabInteractable;
        #endregion
    }
}