#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Surge.Modifier {

    [ExecuteInEditMode]
    public class RoleEditorEntity : MonoBehaviour {

        [SerializeField] RoleSO so;

        MeshFilter mf;
        MeshRenderer mr;

        public void Update() {
            UpdateMeshRenderer();
        }

        void UpdateMeshRenderer() {
            if (so == null) return;
            if (so.tm == null) return;
            if (mf == null) mf = transform.GetChild(0).GetComponent<MeshFilter>();
            if (mr == null) mr = transform.GetChild(0).GetComponent<MeshRenderer>();
        }

    }

}
#endif