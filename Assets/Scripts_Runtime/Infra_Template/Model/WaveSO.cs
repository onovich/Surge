using UnityEngine;

namespace Surge {
    [CreateAssetMenu(fileName = "SO_Wave", menuName = "Surge/WaveSO")]
    public class WaveSO : ScriptableObject {
        public WaveTM tm;
    }
}