using MDI.Gamekit.Interactables;
using UnityEngine;
using Cinemachine;
using System.Collections;

namespace Game.GameProductionOne
{
    public class DoorController : MonoBehaviour
    {
        #region SFIELDS
        [SerializeField] string playerCameraTag = "PlayerCamera", playerCameraOpenTag = "PlayerCameraOpen";
        [SerializeField] Vector3 rotationAxis = new Vector3(0, 1, 0);
        [SerializeField] float degrees = 1f;
        [SerializeField] float amount = 1f;
        [SerializeField] float duration = 1f;
        [SerializeField] string layerMask;
        [SerializeField] float grabDistance = 1f;
        [SerializeField] string grabAxis = "Grab";
        [SerializeField] float distanceToBreakGrab;
        [SerializeField] bool isDrawer = false;
        [SerializeField, Multiline] string tutorialText = "OPEN [Y]";
        [SerializeField, Multiline] string tutorialText2 = "OPEN [Y/Q]";
        #endregion

        new Camera camera;
        bool isOpen = false;

        // Interactables
        IOpenable openInteractable;

        void Awake()
        {
            GetReferences();
            InitialiseInteractables();
        }

        // Update is called once per frame
        void Update()
        {
            GrabOpenableInteractable();
        }

        #region ABSTRACTION
        void GrabOpenableInteractable()
        {
            if (InputManager.Instance.GetButtonDown(grabAxis))
            {
                if (DetectInteractableAtDistance(grabDistance))
                {
                    OpenClose();
                }
            }
        }

        bool DetectInteractableAtDistance(float distance)
        {
            RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(InputManager.Instance.GetMousePosition());
            if (Physics.Raycast(ray, out hit, distance, ~(1 >> LayerMask.NameToLayer(layerMask))) && hit.transform == transform)
                return true;
            else
                return false;
        }

        void GetReferences()
        {
            camera = Camera.main;
        }

        void InitialiseInteractables()
        {
            openInteractable = new OpenInteractable("Mouse X", "Mouse Y");
        }

        void OpenClose()
        {
            if (isDrawer)
            {
                StartCoroutine(OpeningOrClosingDrawer(transform.localPosition.z == amount ? 0 : amount, duration, transform));
            }
            else
            {
                StartCoroutine(OpeningOrClosingDoor(degrees, duration, transform));
            }
        }

        IEnumerator OpeningOrClosingDrawer(float amount, float duration, Transform transform)
        {
            if (transform.localPosition.z != amount)
            {
                openInteractable.OpenDrawer(amount, duration, transform);
                yield return null;
            }
            else
            {
                transform.localPosition = new Vector3(0, 0, amount);
            }
        }

        IEnumerator OpeningOrClosingDoor(float degrees, float duration, Transform transform)
        {
            if (!isOpen)
                openInteractable.Open(degrees, duration, transform, rotationAxis);
            else
                openInteractable.Open(0, duration, transform, rotationAxis);

            isOpen = !isOpen;
            yield return null;
        }

        void OnMouseOver()
        {
            CursorManager.Instance.ChangeMouseRadius(20f, 1f, name.ToUpper(), tutorialText2);
        }

        void OnMouseExit()
        {
            CursorManager.Instance.ChangeMouseRadius(4f, 0f, name.ToUpper(), tutorialText2);
        }
        #endregion
    }
}