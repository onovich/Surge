using UnityEngine;
using System.Runtime.CompilerServices;

namespace Surge {

    public static class SLog {

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Log(object message) {
            UnityEngine.Debug.Log(message);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void LogWarning(object message) {
            UnityEngine.Debug.LogWarning(message);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void LogError(object message) {
            UnityEngine.Debug.LogError(message);
        }

    }

}