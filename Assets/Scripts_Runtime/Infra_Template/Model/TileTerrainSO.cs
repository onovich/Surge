using UnityEngine;

namespace Surge {
    [CreateAssetMenu(fileName = "SO_TileTerrain", menuName = "Surge/TileTerrainSO")]
    public class TileTerrainSO : ScriptableObject {
        public TileTerrainTM tm;
    }
}