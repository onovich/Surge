using UnityEngine;

namespace Surge {
    [CreateAssetMenu(fileName = "SO_Game", menuName = "Surge/GameSO")]
    public class GameSO : ScriptableObject {
        public GameConfig tm;
    }
}