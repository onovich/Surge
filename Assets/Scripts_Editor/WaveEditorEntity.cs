#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using TriInspector;

namespace Surge.Modifier {

    [ExecuteInEditMode]
    public class WaveEditorEntity : MonoBehaviour {

        [SerializeField] WaveSO so;
        [SerializeField] int typeID;
        [SerializeField] float delayTimer;
        [SerializeField] Vector2 mapSize;

        SpriteRenderer sr;

        public void Update() {
            // UpdateMeshRenderer();
        }

        [Button("Bake")]
        void Bake() {
            BakeBaseInfo();
            BakeMonsters();
            UnityEditor.EditorUtility.SetDirty(so);
            AssetDatabase.SaveAssets();
        }

        void BakeBaseInfo() {
            if (so == null) {
                SLog.LogWarning("SO is null");
            }
            so.tm.typeID = typeID;
            so.tm.delayTimer = delayTimer;
        }

        void BakeMonsters() {
            var monsters = transform.GetChild(0).GetComponentsInChildren<RoleEditorEntity>();
            so.tm.monsterTypeIDs = new int[monsters.Length];
            so.tm.monsterSpawnDelayTimers = new float[monsters.Length];
            so.tm.monsterSpawnPositions = new Vector2[monsters.Length];
            so.tm.monsterSpawnDirections = new float[monsters.Length];
            for (int i = 0; i < monsters.Length; i++) {
                so.tm.monsterTypeIDs[i] = monsters[i].so.tm.typeID;
                so.tm.monsterSpawnDelayTimers[i] = monsters[i].delayTimer;
                so.tm.monsterSpawnPositions[i] = monsters[i].transform.position;
                so.tm.monsterSpawnDirections[i] = monsters[i].transform.eulerAngles.z;
            }
        }

        void OnDrawGizmos() {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, mapSize);
        }

    }

}
#endif