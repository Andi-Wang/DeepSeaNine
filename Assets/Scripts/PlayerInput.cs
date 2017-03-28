using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets._2D {
    [RequireComponent(typeof(Player))]
    public class PlayerInput : MonoBehaviour {

        public class Input {
            public Vector3 lastDirection;

            public bool prevUpHold = false;
            public bool prevDownHold = false;
            public bool prevLeftHold = false;
            public bool prevRightHold = false;
            
            public bool upHold;
            public bool downHold;
            public bool leftHold;
            public bool rightHold;

            public bool interactDown;
            public bool interactHold;
            public bool fireDown;
            public bool fireHold;
            public bool buildUpgradeDown;
            public bool buildUpgradeHold;
            public bool buildUpgradeUp;
            public bool sellDown;
            public bool sellHold;
            public bool sellUp;
            public bool cancelDown;


            public void resetButtonDown() {
                prevUpHold = upHold;
                prevDownHold = downHold;
                prevLeftHold = leftHold;
                prevRightHold = rightHold;

                upHold = false;
                downHold = false;
                leftHold = false;
                rightHold = false;

                interactDown = false;
                fireDown = false;
                buildUpgradeDown = false;
                buildUpgradeUp = false;
                sellDown = false;
                sellUp = false;
                cancelDown = false;
            }
        }

        private Player player;
        private Input input;

        private void Awake() {
            player = GetComponent<Player>();
            input = new Input();
            input.lastDirection = new Vector3(1.0f, 0f, 0f);
        }

        private void Update() {

            // Read button down inputs in Update so button presses aren't missed.
            if (!input.interactDown) {
                input.interactDown = CrossPlatformInputManager.GetButtonDown("Player" + player.playerNumber + "Interact");
            }
            if (!input.fireDown) {
                input.fireDown = CrossPlatformInputManager.GetButtonDown("Player" + player.playerNumber + "Fire");
			}
            if (!input.buildUpgradeDown) {
                input.buildUpgradeDown = CrossPlatformInputManager.GetButtonDown("Player" + player.playerNumber + "Build/Upgrade");
            }
            if (!input.buildUpgradeUp) {
                input.buildUpgradeUp = CrossPlatformInputManager.GetButtonUp("Player" + player.playerNumber + "Build/Upgrade");
            }
            if (!input.sellDown) {
                input.sellDown = CrossPlatformInputManager.GetButtonDown("Player" + player.playerNumber + "Sell");
            }
            if (!input.sellUp) {
                input.sellUp = CrossPlatformInputManager.GetButtonUp("Player" + player.playerNumber + "Sell");
            }
            if (!input.cancelDown) {
                input.cancelDown = CrossPlatformInputManager.GetButtonDown("Player" + player.playerNumber + "Cancel");
            }


            if (CrossPlatformInputManager.GetAxis("Player" + player.playerNumber + "Vertical") > 0) {
                input.lastDirection = Vector3.up;
                input.upHold = true;
            }
            if (CrossPlatformInputManager.GetAxis("Player" + player.playerNumber + "Vertical") < 0) {
                input.lastDirection = Vector3.down;
                input.downHold = true;
            }
            if (CrossPlatformInputManager.GetAxis("Player" + player.playerNumber + "Horizontal") < 0) {
                input.lastDirection = Vector3.left;
                input.leftHold = true;
            }
            if (CrossPlatformInputManager.GetAxis("Player" + player.playerNumber + "Horizontal") > 0) {
                input.lastDirection = Vector3.right;
                input.rightHold = true;
            }
        }


        private void FixedUpdate() {
            input.fireHold = CrossPlatformInputManager.GetButton("Player" + player.playerNumber + "Fire");
            input.interactHold = CrossPlatformInputManager.GetButton("Player" + player.playerNumber + "Interact");
            input.buildUpgradeHold = CrossPlatformInputManager.GetButton("Player" + player.playerNumber + "Build/Upgrade");
            input.sellHold = CrossPlatformInputManager.GetButton("Player" + player.playerNumber + "Sell");



            player.Move(input);
            input.resetButtonDown();
        }
    }
}
