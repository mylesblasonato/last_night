using Cinemachine;
using UnityEngine;

namespace MDI.Gamekit.CharacterController{
    public class CameraBehaviour : MoveableBehaviour{
        CinemachineVirtualCamera camera;

        public CameraBehaviour(PlayerData playerData, CinemachineVirtualCamera camera){
            base.playerData = playerData;
            this.camera = camera;
        }

        public void TurnOn(bool turnOn){
            if (turnOn){
                camera.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_InputAxisName = "Mouse Y";
                camera.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_InputAxisName = "Mouse X";
            }
            else{
                camera.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_InputAxisName = "";
                camera.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_InputAxisName = "";
                camera.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.Reset();
                camera.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.Reset();
            }
        }

        public Vector3 RotateAround(Vector3 aroundAxis){
            camera.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed = base.playerData.sensitivity;
            camera.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed = base.playerData.sensitivity;
            return aroundAxis * camera.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.Value;
        }

        public void Shake(Vector2 amplitudeRange, Vector2 movementAxis){
            camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = amplitudeRange.y * Mathf.Clamp((Mathf.Abs(movementAxis.y) + Mathf.Abs(movementAxis.x)), amplitudeRange.x, 1);
            camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = amplitudeRange.y * Mathf.Clamp((Mathf.Abs(movementAxis.y) + Mathf.Abs(movementAxis.x)), amplitudeRange.x, 1);
        }
    }
}