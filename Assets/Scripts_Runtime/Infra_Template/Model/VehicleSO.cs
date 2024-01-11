using UnityEngine;

namespace Surge {
    [CreateAssetMenu(fileName = "SO_Vehicle", menuName = "Surge/VehicleSO")]
    public class VehicleSO : ScriptableObject {
        public VehicleTM tm;
    }
}