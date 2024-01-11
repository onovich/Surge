#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Surge.Modifier {

    [ExecuteInEditMode]
    public class MapEditorEntity : MonoBehaviour {

        [SerializeField] MapSO so;
        [SerializeField] Vector3 mapSize;
        [SerializeField] Vector3 tileSize;


        // [ContextMenu("Bake")]


    }

}
#endif