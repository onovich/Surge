using UnityEngine;

namespace Surge {
    [CreateAssetMenu(fileName = "SO_Chapter", menuName = "Surge/ChapterSO")]
    public class ChapterSO : ScriptableObject {
        public ChapterTM tm;
    }
}