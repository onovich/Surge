using System;

namespace Surge.UI {

    public class UIEventCenter {

        public UIEventCenter() {

        }

        // Login
        public Action Login_OnStartGameClickHandle;
        public void Login_OnStartGameClick() {
            Login_OnStartGameClickHandle?.Invoke();
        }

        public Action Login_OnExitGameClickHandle;
        public void Login_OnExitGameClick() {
            Login_OnExitGameClickHandle?.Invoke();
        }

        public void Clear() {
            Login_OnStartGameClickHandle = null;
            Login_OnExitGameClickHandle = null;
        }

    }

}