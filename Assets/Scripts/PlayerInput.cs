using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets._2D {
    [RequireComponent(typeof(Player))]
    public class PlayerInput : MonoBehaviour {
        public class Input {
            public char lastDirection;

            public bool up;
            public bool down;
            public bool left;
            public bool right;

            public bool interactDown;
            public bool interactHold;
            public bool fireDown;

            public bool buildUpgradeHold;
            public bool sellHold;

            public void resetButtonDown() {
                interactHold = false;
                buildUpgradeHold = false;
                sellHold = false;
            }
        }

        private Player player;
        private Input input;

        private void Awake() {
            player = GetComponent<Player>();
            input = new Input();
        }

        private void Update() {

            // Read button down inputs in Update so button presses aren't missed.
            if (!input.interactDown) {
                input.interactDown = CrossPlatformInputManager.GetButtonDown("Interact");
            }
            if (!input.fireDown) {
                input.fireDown = CrossPlatformInputManager.GetButtonDown("Fire");
            }
            if(CrossPlatformInputManager.GetButtonDown("Up")) {
                input.lastDirection = 'N';
            }
            if (CrossPlatformInputManager.GetButtonDown("Down")) {
                input.lastDirection = 'S';
            }
            if (CrossPlatformInputManager.GetButtonDown("Left")) {
                input.lastDirection = 'W';
            }
            if (CrossPlatformInputManager.GetButtonDown("Right")) {
                input.lastDirection = 'E';
            }
        }


        private void FixedUpdate() {
            input.up = CrossPlatformInputManager.GetButton("Up");
            input.down = CrossPlatformInputManager.GetButton("Down");
            input.left = CrossPlatformInputManager.GetButton("Left");
            input.right = CrossPlatformInputManager.GetButton("Right");

            input.interactHold = CrossPlatformInputManager.GetButton("Interact");
            input.buildUpgradeHold = CrossPlatformInputManager.GetButton("Build/Upgrade");
            input.sellHold = CrossPlatformInputManager.GetButton("Sell");

            player.Move(input);
            input.resetButtonDown();
        }
    }
}
