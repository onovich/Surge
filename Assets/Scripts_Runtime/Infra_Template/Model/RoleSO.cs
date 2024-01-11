using UnityEngine;

namespace Surge {
    [CreateAssetMenu(fileName = "SO_Role", menuName = "Surge/RoleSO")]
    public class RoleSO : ScriptableObject {
        public RoleTM tm;
    }
}