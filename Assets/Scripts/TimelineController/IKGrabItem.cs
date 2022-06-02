using UnityEngine;
using UnityEditor;
using UnityEngine.Animations.Rigging;
using MDI.Gamekit.Interactables;
using UnityEngine.Timeline;

namespace Game.GameProductionOne{
    public class IKGrabItem : MonoBehaviour{
        [SerializeField] string grabPointLeftHandTag;
        [SerializeField] string grabPointRightHandTag;
        [SerializeField] string leftHandRigTag;
        [SerializeField] string rightHandRigTag;

        Transform grabPointLeftHand;
        Transform grabPointRightHand;
        ChainIKConstraint leftHand;
        ChainIKConstraint rightHand;
        InteractableController interactableController;
        Rigidbody rigidBody;
        bool isTagsFound = false;

        void Awake(){
            if (string.Compare(grabPointLeftHandTag, "") == 0 ||
                string.Compare(grabPointRightHandTag, "") == 0 ||
                string.Compare(leftHandRigTag, "") == 0 ||
                string.Compare(rightHandRigTag, "") == 0){
                Debug.Log("NO TAG ENTERED...");
                return;
            }

            isTagsFound = true;
            grabPointLeftHand = GameObject.FindGameObjectWithTag(grabPointLeftHandTag).transform;
            grabPointRightHand = GameObject.FindGameObjectWithTag(grabPointRightHandTag).transform;
            leftHand = GameObject.FindGameObjectWithTag(leftHandRigTag).GetComponent<ChainIKConstraint>();
            rightHand = GameObject.FindGameObjectWithTag(rightHandRigTag).GetComponent<ChainIKConstraint>();
            interactableController = GetComponent<InteractableController>();
            rigidBody = GetComponent<Rigidbody>();
        }

        public void SetGrabLeftPoint(Transform grabPoint){
            if (!isTagsFound) return;
            grabPointLeftHand.position = grabPoint.position;
            grabPointLeftHand.rotation = grabPoint.rotation;
        }

        public void SetGrabRightPoint(Transform grabPoint){
            if (!isTagsFound) return;
            grabPointRightHand.position = grabPoint.position;
            grabPointRightHand.rotation = grabPoint.rotation;
        }

        public void GrabLeft(Transform leftHandPoint){
            if (!isTagsFound) return;
            interactableController.enabled = false;
            rigidBody.isKinematic = true;           
            transform.parent = leftHandPoint;
        }

        public void GrabRight(Transform rightHandPoint){
            if (!isTagsFound) return;
            interactableController.enabled = false;
            rigidBody.isKinematic = true;
            transform.parent = rightHandPoint;
        }

        public void Release(){
            if (!isTagsFound) return;
            rigidBody.isKinematic = false;
            interactableController.enabled = true;
            transform.parent = transform.parent;
        }

#if UNITY_EDITOR
        [MenuItem("Tools/Gamekit/Create Grab Item")]
        public static void CreateNewGrabItem(){
            var item = Instantiate(Resources.Load("Prefabs/InteractableItem"), Selection.activeGameObject.transform == null ? null : Selection.activeGameObject.transform);
            item.name = "New Grab Item";
        }
#endif
    }
}
