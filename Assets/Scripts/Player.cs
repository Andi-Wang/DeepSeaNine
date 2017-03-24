using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets._2D {
    public class Player : MonoBehaviour {
        private Point startLocation;
        private Point location;
        private Vector3 facing;
        private LevelManager level;
        public int playerNumber { get; private set; }

        private float moveCounter = 0;

        // Use this for initialization
        void Start() {
            moveSprite(location);
        }

        // Update is called once per frame
        void Update() {

        }

        void FixedUpdate() {
            
        }

        public void Move(PlayerInput.Input input) {
            moveCounter += Time.deltaTime;
            facing = input.lastDirection;

            if(input.buildUpgradeHold) {
                if(level.Tiles[location].Type == "wall") {
                    //Build tower if there's no tower at location
                    if(true) {
                        TileScript currentTile = LevelManager.Instance.Tiles[location];
                        currentTile.TowerMenu(transform.position);
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
                //Interact with something if on an object that can be interacted with
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
                if(moveCounter > 0.1f) {
                    if (input.up || (input.upHold && facing == Vector3.up)) {
                        moveInDirection(facing);
                        moveCounter = 0;
                    }
                    else if (input.down || (input.downHold && facing == Vector3.down)) {
                        moveInDirection(facing);
                        moveCounter = 0;
                    }
                    else if (input.left || (input.leftHold && facing == Vector3.left)) {
                        moveInDirection(facing);
                        moveCounter = 0;
                    }
                    else if (input.right || (input.rightHold && facing == Vector3.right)) {
                        moveInDirection(facing);
                        moveCounter = 0;
                    }
                }
            }


        }

        private Point getNextPoint(Vector3 facing) {
            Point next = new Point(location.X + (int)facing.x, location.Y + (int)facing.y);
            return next;
        }

        private void moveInDirection(Vector3 facing) {
            if(facing != Vector3.zero) {
                Point next;
                if (facing == Vector3.up || facing == Vector3.down) {
                    next = getNextPoint(-facing);
                }
                else {
                    next = getNextPoint(facing);
                }                
                
                if (level.Tiles[next].Type != "water") {
                    location = next;
                    moveSprite(location);
                }
            }
        }

        private void moveSprite(Point point) {
            transform.position = new Vector3(level.worldStart.x + level.TileSize * point.X, level.worldStart.y - level.TileSize * point.Y, 0);
            float angle = Mathf.Atan2(facing.y, facing.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        public void createPlayerAt(int number, int x, int y) {
            playerNumber = number;
            level = GameObject.Find("LevelManager").GetComponent<LevelManager>();
            startLocation = new Point(x, y);
            location = startLocation;
            facing = Vector3.right;
            moveSprite(location);
        }
    }
}
