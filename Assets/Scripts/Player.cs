using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets._2D {
    public class Player : MonoBehaviour {
        Point location;
        char facing;
        int playerNumber;
        public LevelManager level;

        // Use this for initialization
        void Start() {

        }

        // Update is called once per frame
        void Update() {

        }

        void FixedUpdate() {

        }

        public void Move(PlayerInput.Input input) {
            facing = input.lastDirection;

            if(input.buildUpgradeHold) {
                if(level.Tiles[location].Type == "wall") {
                    //Build tower if there's no tower at location
                    if(true) {

                    }
                    //Upgrade tower if there is a tower at location
                    else {

                    }
                }
            }
            else if(input.sellHold) {
                //Sell tower if there is a tower at location
                if(true) {

                }
            }
            else if(input.interactDown) {
                Point next = getNextPoint(facing);
                //Do something if facing a point that can be interacted with
                if(true) {

                }
            }
            else if(input.fireDown) {
                //Fire weapon in facing direction if there is ammo left
                if(true) {

                }
            }
            //Move the player if the next tile isn't water
            else {
                if(input.up && facing == 'N') {
                    moveInDirection(facing);
                }
                else if(input.down && facing == 'S') {
                    moveInDirection(facing);
                }
                else if(input.left && facing == 'W') {
                    moveInDirection(facing);
                }
                else if(input.right && facing == 'E') {
                    moveInDirection(facing);
                }

            }


        }

        private Point getNextPoint(char facing) {
            Point next = new Point(-1, -1);

            if(facing == 'N') {
                next = new Point(location.X, location.Y + 1);
            }
            else if(facing == 'S') {
                next = new Point(location.X, location.Y - 1);
            }
            else if(facing == 'W') {
                next = new Point(location.X - 1, location.Y);
            }
            else if(facing == 'E') {
                next = new Point(location.X + 1, location.Y);
            }

            return next;
        }

        private void moveInDirection(char facing) {
            Point next = getNextPoint(facing);
            if(next.X != -1 && next.Y != -1 && level.Tiles[next].Type != "water") {
                location = next;
            }
        }
    }
}
