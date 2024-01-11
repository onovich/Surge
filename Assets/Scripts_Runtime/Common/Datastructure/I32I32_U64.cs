using System.Runtime.InteropServices;

namespace Surge {

    [StructLayout(LayoutKind.Explicit)]
    public struct I32I32_U64 {

        [FieldOffset(0)]
        public int i32_1;
        [FieldOffset(4)]
        public int i32_2;
        [FieldOffset(0)]
        public ulong u64;

        public I32I32_U64(int i32_1, int i32_2) {
            this.u64 = 0;
            this.i32_1 = i32_1;
            this.i32_2 = i32_2;
        }

        public override bool Equals(object obj) {
            return obj is I32I32_U64 other && u64 == other.u64;
        }

        public override int GetHashCode() {
            int hashCode = 1861411795;
            hashCode = hashCode * -1521134295 + i32_1.GetHashCode();
            hashCode = hashCode * -1521134295 + i32_2.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(I32I32_U64 left, I32I32_U64 right) {
            return left.u64 == right.u64;
        }

        public static bool operator !=(I32I32_U64 left, I32I32_U64 right) {
            return !(left.u64 == right.u64);
        }

        public override string ToString() {
            return $"I32I32_U64({i32_1}, {i32_2})";
        }
    }

}