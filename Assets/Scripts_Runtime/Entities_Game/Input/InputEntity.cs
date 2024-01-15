using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Surge {

    public class InputEntity {

        public Vector2 moveAxis;
        public InputKeyEnum skillKeyDown;
        public InputKeyEnum skillKeyHold;

        InputKeybindingComponent keybindingCom;

        public void Ctor() {
            keybindingCom.Ctor();
        }

        public void ProcessInput(Camera camera, float dt) {

            // Move Axis
            if (keybindingCom.IsKeyPressing(InputKeyEnum.MoveLeft)) {
                moveAxis.x = -1;
            } else if (keybindingCom.IsKeyPressing(InputKeyEnum.MoveRight)) {
                moveAxis.x = 1;
            }

            if (keybindingCom.IsKeyPressing(InputKeyEnum.MoveForward)) {
                moveAxis.y = 1;
            } else if (keybindingCom.IsKeyPressing(InputKeyEnum.MoveBackward)) {
                moveAxis.y = -1;
            }

            // Skill
            InputKeyEnum skillKeyDown;
            if (keybindingCom.IsKeyDown(InputKeyEnum.Skill1)) {
                skillKeyDown = InputKeyEnum.Skill1;
            } else if (keybindingCom.IsKeyDown(InputKeyEnum.Skill2)) {
                skillKeyDown = InputKeyEnum.Skill2;
            } else if (keybindingCom.IsKeyDown(InputKeyEnum.Skill3)) {
                skillKeyDown = InputKeyEnum.Skill3;
            } else if (keybindingCom.IsKeyDown(InputKeyEnum.Skill4)) {
                skillKeyDown = InputKeyEnum.Skill4;
            } else {
                skillKeyDown = InputKeyEnum.None;
            }
            this.skillKeyDown = skillKeyDown;

            InputKeyEnum skillKeyHold;
            if (keybindingCom.IsKeyPressing(InputKeyEnum.Skill1)) {
                skillKeyHold = InputKeyEnum.Skill1;
            } else if (keybindingCom.IsKeyPressing(InputKeyEnum.Skill2)) {
                skillKeyHold = InputKeyEnum.Skill2;
            } else if (keybindingCom.IsKeyPressing(InputKeyEnum.Skill3)) {
                skillKeyHold = InputKeyEnum.Skill3;
            } else if (keybindingCom.IsKeyPressing(InputKeyEnum.Skill4)) {
                skillKeyHold = InputKeyEnum.Skill4;
            } else {
                skillKeyHold = InputKeyEnum.None;
            }
            this.skillKeyHold = skillKeyHold;

            // Camera Move

        }

        public void Keybinding_Set(InputKeyEnum key, KeyCode[] keyCodes) {
            keybindingCom.Bind(key, keyCodes);
        }

        public void Reset() {
            moveAxis = Vector2.zero;
        }

    }

}