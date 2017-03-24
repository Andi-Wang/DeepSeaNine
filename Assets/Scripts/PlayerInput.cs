using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets._2D {
    [RequireComponent(typeof(Player))]
    public class PlayerInput : MonoBehaviour {
        public class Input {
            public Vector3 lastDirection;

            public bool up;
            public bool down;
            public bool left;
            public bool right;

            public bool upHold;
            public bool downHold;
            public bool leftHold;
            public bool rightHold;

            public bool interactDown;
            public bool interactHold;
            public bool fireDown;

            public bool buildUpgradeHold;
            public bool sellHold;

            public void resetButtonDown() {
                up = false;
                down = false;
                left = false;
                right = false;

                interactDown = false;
                fireDown = false;
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
                input.interactDown = CrossPlatformInputManager.GetButtonDown("Player" + player.playerNumber + "Interact");
            }
            if (!input.fireDown) {
                input.fireDown = CrossPlatformInputManager.GetButtonDown("Player" + player.playerNumber + "Fire");
            }
            if(CrossPlatformInputManager.GetButtonDown("Player" + player.playerNumber + "Up")) {
                input.lastDirection = Vector3.up;
                input.up = true;
            }
            if (CrossPlatformInputManager.GetButtonDown("Player" + player.playerNumber + "Down")) {
                input.lastDirection = Vector3.down;
                input.down = true;
            }
            if (CrossPlatformInputManager.GetButtonDown("Player" + player.playerNumber + "Left")) {
                input.lastDirection = Vector3.left;
                input.left = true;
            }
            if (CrossPlatformInputManager.GetButtonDown("Player" + player.playerNumber + "Right")) {
                input.lastDirection = Vector3.right;
                input.right = true;
            }
        }


        private void FixedUpdate() {
            input.upHold = CrossPlatformInputManager.GetButton("Player" + player.playerNumber + "Up");
            input.downHold = CrossPlatformInputManager.GetButton("Player" + player.playerNumber + "Down");
            input.leftHold = CrossPlatformInputManager.GetButton("Player" + player.playerNumber + "Left");
            input.rightHold = CrossPlatformInputManager.GetButton("Player" + player.playerNumber + "Right");

            input.interactHold = CrossPlatformInputManager.GetButton("Player" + player.playerNumber + "Interact");
            input.buildUpgradeHold = CrossPlatformInputManager.GetButton("Player" + player.playerNumber + "Build/Upgrade");
            input.sellHold = CrossPlatformInputManager.GetButton("Player" + player.playerNumber + "Sell");

            player.Move(input);
            input.resetButtonDown();
        }
    }
}
