using UnityEngine;

namespace MDI.Gamekit.Core{
    public class Raycast{
        public RaycastHit RaycastFromCameraWithMask(Camera origin, Vector3 direction, float distance, string layerMask){
            RaycastHit hit;
            Ray ray = origin.ScreenPointToRay(direction);
            if (Physics.Raycast(ray, out hit, distance, ~(1 >> LayerMask.NameToLayer(layerMask))))
                return hit;
            else
                return new RaycastHit();

        }

        public RaycastHit RaycastFromCameraWithoutMask(Camera origin, Vector3 direction, float distance, string layerMask){
            RaycastHit hit;
            Ray ray = origin.ScreenPointToRay(direction);
            if (Physics.Raycast(ray, out hit, distance, (1 >> LayerMask.NameToLayer(layerMask))))
                return hit;
            else
                return new RaycastHit();

        }
    }
}