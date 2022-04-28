using Cinemachine;
using MDI.Gamekit.CharacterController;
using MDI.Gamekit.Input;
using System.Collections.Generic;
using UnityEngine;

namespace Game.ProjectZ
{
    public class Player : MonoBehaviour{
        float speedMultiplier = 0;

        protected virtual void MoveThePlayer(MoveableBehaviour playerMovement){
            MovementBehaviour movement = (MovementBehaviour)playerMovement;
            movement.Move(playerMovement.playerData.transform.forward, InputManager.Instance.GetAxis("Vertical"), speedMultiplier);
            movement.Move(playerMovement.playerData.transform.right, InputManager.Instance.GetAxis("Horizontal"), speedMultiplier);
        }

        protected virtual void DetermineIfPlayerIsRunning(MoveableBehaviour playerMovement){
            if (InputManager.Instance.GetButton("Run"))
                speedMultiplier = playerMovement.playerData.runMultiplier;
            else
                speedMultiplier = 1f;
        }

        protected virtual void RotatePlayerFSPCamera(MoveableBehaviour playerCamera){
            CameraBehaviour camera = (CameraBehaviour)playerCamera;
            transform.localEulerAngles = camera.RotateAround(playerCamera.playerData.transform.up);
        }

        protected virtual void ShakeCameraBasedOnPlayerMovement(MoveableBehaviour playerCamera){
            CameraBehaviour camera = (CameraBehaviour)playerCamera;
            var amplitudeRange = new Vector2(playerCamera.playerData.idleShakeAmplitude, playerCamera.playerData.moveShakeAmplitude);
            var movementAxis = new Vector2(InputManager.Instance.GetAxis("Vertical"), InputManager.Instance.GetAxis("Horizontal"));
            camera.Shake(amplitudeRange, movementAxis);
        }

        protected virtual void PlayPlayerIdleAndMoveAnimations(Animator animator){
            animator.SetFloat("Moving", Mathf.Abs(InputManager.Instance.GetAxis("Vertical")) + Mathf.Abs(InputManager.Instance.GetAxis("Horizontal")));
            animator.SetFloat("Running", InputManager.Instance.GetAxis("Run"));
        }

        public virtual void TurnCameraFollowOn(CameraBehaviour playerCamera){
            CameraBehaviour camera = (CameraBehaviour)playerCamera;
            camera.TurnOn(true);
        }

        public virtual void TurnCameraFollowOff(CameraBehaviour playerCamera){
            CameraBehaviour camera = (CameraBehaviour)playerCamera;
            camera.TurnOn(false);
        }
    }
}
