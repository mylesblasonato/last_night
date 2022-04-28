using Cinemachine;
using UnityEngine;
using UltEvents;
using MDI.Gamekit.CharacterController;
using MDI.Gamekit.Input;

namespace Game.ProjectZ
{
    public class PlayerController : Player{
        [SerializeField] new CinemachineVirtualCamera camera;
        [SerializeField] Animator animator;
        [SerializeField] Rigidbody rb;
        [SerializeField] PlayerData playerData;

        public bool isCameraBehaviourOn = true;

        // Custom moveables
        MoveableBehaviour playerMovement;
        MoveableBehaviour playerCamera;

        void Start(){
            playerData.transform = transform;
            playerMovement = new MovementBehaviour(playerData, rb);
            playerCamera = new CameraBehaviour(playerData, camera);
        }

        void Update(){
            PlayPlayerIdleAndMoveAnimations(animator);
            DetermineIfPlayerIsRunning(playerMovement);
            ShakeCameraBasedOnPlayerMovement(playerCamera);      
        }

        void FixedUpdate(){ // Smoother movement in fixed update           
            RotatePlayerFSPCamera(playerCamera);
            if (!InputManager.Instance.isActive) return;
            MoveThePlayer(playerMovement);
        }

        public CameraBehaviour GetCameraBehaviour()
        {
            return (CameraBehaviour)playerCamera;
        }

        public void TurnCameraFollowOn()
        {
            Cursor.lockState = CursorLockMode.Locked;
            base.TurnCameraFollowOn((CameraBehaviour)playerCamera);
        }

        public void TurnCameraFollowOff()
        {
            Cursor.lockState = CursorLockMode.None;
            base.TurnCameraFollowOff((CameraBehaviour)playerCamera);
        }
    }
}