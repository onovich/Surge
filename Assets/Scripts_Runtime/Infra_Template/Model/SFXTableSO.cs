using UnityEngine;

namespace Surge {

    [CreateAssetMenu(menuName = "Surge/SFXSO")]
    public class SFXTableSO : ScriptableObject {

        // Role
        public AudioClip roleAttack;

        public AudioClip[] bgmLoop;

    }

}