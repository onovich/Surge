using UnityEngine;

namespace Surge {
    [CreateAssetMenu(fileName = "SO_Skill", menuName = "Surge/SkillSO")]
    public class SkillSO : ScriptableObject {
        public SkillTM tm;
    }
}