using UnityEngine;

namespace Surge {
    [CreateAssetMenu(fileName = "SO_Buff", menuName = "Surge/BuffSO")]
    public class BuffSO : ScriptableObject {
        public BuffTM tm;
    }
}