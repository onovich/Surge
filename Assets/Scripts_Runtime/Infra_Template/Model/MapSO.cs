using UnityEngine;

namespace Surge {
    [CreateAssetMenu(fileName = "SO_Map", menuName = "Surge/MapSO")]
    public class MapSO : ScriptableObject {
        public MapSpawnTM tm;
    }
}