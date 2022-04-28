using UnityEngine;

namespace MDI.Gamekit.CharacterController
{
    public class MovementBehaviour : MoveableBehaviour
    {
        Vector3 direction;
        Rigidbody rigidBody;

        public MovementBehaviour(PlayerData playerData, Rigidbody rigidBody)
        {
            base.playerData = playerData;
            this.rigidBody = rigidBody;
        }

        public void Move(Vector3 direction, float axis, float speedMultiplier)
        {
            var finalSpeed = (playerData.speed * (speedMultiplier < 1 ? 1 : speedMultiplier));
            this.rigidBody.AddForce(direction * axis * finalSpeed);
        }
    }
}
