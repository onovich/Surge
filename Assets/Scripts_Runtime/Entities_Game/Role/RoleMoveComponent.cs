using UnityEngine;

namespace Surge {

    public struct RoleMoveComponent {

        public Vector2 pathOffset;
        public int curPathIndex;

        public Vector2Int lastPosInt;

        public void Reuse() {
            curPathIndex = 0;
        }

    }

}