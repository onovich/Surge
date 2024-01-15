#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Surge.Modifier {

    [ExecuteInEditMode]
    public class RoleEditorEntity : MonoBehaviour {

        public RoleSO so;
        public float delayTimer;

        SpriteRenderer sr;

        public void Update() {
            UpdateMeshRenderer();
        }

        void UpdateMeshRenderer() {
            if (so == null) return;
            if (so.tm == null) return;
            if (so.tm.spr == null) return;
            if (sr == null) sr = transform.GetChild(0).GetComponent<SpriteRenderer>();
            sr.sprite = so.tm.spr;
        }

    }

}
#endif