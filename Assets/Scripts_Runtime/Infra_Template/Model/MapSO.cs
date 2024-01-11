using UnityEngine;

namespace Hermit {
    [CreateAssetMenu(fileName = "SO_Map", menuName = "Hermit/MapSO")]
    public class MapSO : ScriptableObject {
        public MapSpawnTM tm;
    }
}