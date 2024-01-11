using System;
using System.Collections.Generic;
using UnityEngine;

namespace Surge {

    public static class PFMath {

        public static bool IsInRange(in Vector2 posA, in Vector2 posB, in float radius) {
            return Vector2.SqrMagnitude(posA - posB) <= (radius * radius);
        }

        public static Vector3Int RoundToInt(Vector3 pos) {
            int x = (int)Mathf.Round(pos.x);
            int y = (int)Mathf.Round(pos.y);
            int z = (int)Mathf.Round(pos.z);
            return new Vector3Int(x, y, z);
        }

        public static I32I32_U64 ToI32I32_U64(this Vector2Int pos) {
            return new I32I32_U64(pos.x, pos.y);
        }

        public static Vector2Int ToVector2Int(this I32I32_U64 u64) {
            return new Vector2Int(u64.i32_1, u64.i32_2);
        }

        public static Vector2Int ToVector2Int(this Vector2Byte pos) {
            return new Vector2Int(pos.x, pos.y);
        }

        public static Vector2 ToVector2(this Vector2Byte pos) {
            return new Vector2(pos.x, pos.y);
        }

        public static Vector2Int GetTilePos(Vector2 pos, float tileSize) {
            Vector2Int posInt = new Vector2Int((int)(pos.x), (int)(pos.y));
            float halfTileSize = tileSize / 2;
            if (pos.x > posInt.x + halfTileSize) {
                posInt.x++;
            } else if (pos.x < posInt.x - halfTileSize) {
                posInt.x--;
            }
            if (pos.y > posInt.y + halfTileSize) {
                posInt.y++;
            } else if (pos.y < posInt.y - halfTileSize) {
                posInt.y--;
            }
            return posInt;
        }

        public static Vector2Int ToVector2Int(this Vector2 vector2) {
            return new Vector2Int(Mathf.RoundToInt(vector2.x), Mathf.RoundToInt(vector2.y));
        }

        public static Vector2Int ToVector2Int(this Vector3 vector3) {
            return new Vector2Int(Mathf.RoundToInt(vector3.x), Mathf.RoundToInt(vector3.y));
        }

        public static Vector2 ToVector2(this Vector2Int vector2Int) {
            return new Vector2(vector2Int.x, vector2Int.y);
        }

        public static Vector2 ToVector2(this Vector3 vector3) {
            return new Vector2(vector3.x, vector3.y);
        }

        public static Vector3 ToVector3(this Vector2Int vector2Int) {
            return new Vector3(vector2Int.x, vector2Int.y);
        }

        public static Vector3 ToVector3(this Vector2 vector2) {
            return new Vector3(vector2.x, vector2.y);
        }

        public static TValue GetOrAdd<TKey, TValue>(this SortedList<TKey, TValue> sortedList, TKey key, Func<TValue> valueFactory) {
            if (!sortedList.TryGetValue(key, out TValue value)) {
                value = valueFactory();
                sortedList.Add(key, value);
            }
            return value;
        }

        public static TValue GetOrAdd<TKey, TValue>(this SortedDictionary<TKey, TValue> dictionary, TKey key, Func<TValue> valueFactory) {
            if (!dictionary.TryGetValue(key, out TValue value)) {
                value = valueFactory();
                dictionary.Add(key, value);
            }
            return value;
        }

        public static TValue GetOrAdd<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, Func<TValue> valueFactory) {
            if (!dictionary.TryGetValue(key, out TValue value)) {
                value = valueFactory();
                dictionary.Add(key, value);
            }
            return value;
        }

    }
}