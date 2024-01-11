using System;
using System.Collections.Generic;

namespace Surge {

    public class Pool<T> {

        Stack<T> stack;

        Func<T> ctorHandle;

        public Pool(int capacity, Func<T> ctorHandle) {
            stack = new Stack<T>(capacity);
            this.ctorHandle = ctorHandle;
            for (int i = 0; i < capacity; i++) {
                stack.Push(ctorHandle());
            }
        }

        public T Take() {
            if (stack.Count == 0) {
                return ctorHandle();
            }
            return stack.Pop();
        }

        public void Return(T item) {
            stack.Push(item);
        }

    }

}