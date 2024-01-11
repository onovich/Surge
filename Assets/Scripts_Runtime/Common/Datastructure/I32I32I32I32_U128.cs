using System.Runtime.InteropServices;

namespace Surge {

    [StructLayout(LayoutKind.Explicit)]
    public struct I32I32I32I32_U128 {

        [FieldOffset(0)]
        public int i32_1;
        [FieldOffset(4)]
        public int i32_2;
        [FieldOffset(8)]
        public int i32_3;
        [FieldOffset(12)]
        public int i32_4;

        [FieldOffset(0)]
        public decimal u128;

        public I32I32I32I32_U128(int i32_1, int i32_2, int i32_3, int i32_4) {
            this.u128 = 0;
            this.i32_1 = i32_1;
            this.i32_2 = i32_2;
            this.i32_3 = i32_3;
            this.i32_4 = i32_4;
        }

        public static bool operator ==(I32I32I32I32_U128 left, I32I32I32I32_U128 right) {
            return left.u128 == right.u128;
        }

        public static bool operator !=(I32I32I32I32_U128 left, I32I32I32I32_U128 right) {
            return !(left.u128 == right.u128);
        }

        public override bool Equals(object obj) {
            return obj is I32I32I32I32_U128 other && u128 == other.u128;
        }

        public override int GetHashCode() {
            int hashCode = 1861411795;
            hashCode = hashCode * -1521134295 + i32_1.GetHashCode();
            hashCode = hashCode * -1521134295 + i32_2.GetHashCode();
            hashCode = hashCode * -1521134295 + i32_3.GetHashCode();
            hashCode = hashCode * -1521134295 + i32_4.GetHashCode();
            return hashCode;
        }

    }

}