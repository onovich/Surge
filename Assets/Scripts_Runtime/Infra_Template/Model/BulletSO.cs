using UnityEngine;

namespace Surge {
    [CreateAssetMenu(fileName = "SO_Bullet", menuName = "Surge/BulletSO")]
    public class BulletSO : ScriptableObject {
        public BulletTM tm;
    }
}