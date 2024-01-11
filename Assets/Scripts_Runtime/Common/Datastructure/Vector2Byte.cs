using UnityEngine;

namespace Surge {

    [System.Serializable]
    public struct Vector2Byte {
        public byte x;
        public byte y;

        public static Vector2Byte operator +(Vector2Byte a, Vector2Byte b) {
            return new Vector2Byte() {
                x = (byte)(a.x + b.x),
                y = (byte)(a.y + b.y),
            };
        }

        public static Vector2Byte operator -(Vector2Byte a, Vector2Byte b) {
            return new Vector2Byte() {
                x = (byte)(a.x - b.x),
                y = (byte)(a.y - b.y),
            };
        }

        public static Vector2Byte operator *(Vector2Byte a, int b) {
            return new Vector2Byte() {
                x = (byte)(a.x * b),
                y = (byte)(a.y * b),
            };
        }

        public static Vector2Byte operator /(Vector2Byte a, int b) {
            return new Vector2Byte() {
                x = (byte)(a.x / b),
                y = (byte)(a.y / b),
            };
        }

        public static implicit operator Vector2Int(Vector2Byte a) {
            return new Vector2Int(a.x, a.y);
        }

        public static implicit operator Vector2(Vector2Byte a) {
            return new Vector2(a.x, a.y);
        }

        public static implicit operator Vector2Byte(Vector2Int a) {
            return new Vector2Byte() {
                x = (byte)a.x,
                y = (byte)a.y,
            };
        }
    }
}