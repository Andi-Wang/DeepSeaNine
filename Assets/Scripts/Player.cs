﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets._2D {
    public class Player : MonoBehaviour {
        private Point startLocation;
        private Point location;
        private Vector3 facing;
        public int playerNumber { get; private set; }

        [SerializeField]
        private GameObject tower_projectile;

        private float moveCounter = 0;
        private float sellCounter = 0;
        private float fireCounter = 0;
        private float reloadCounter = 0;
        private float buildUpgradeCounter = 0;
        private float interactCounter = 0;
        private bool selecting = false;
        private bool working = false;
        private int projectileSpeed = 20;
        private int ammoInClip = 8;
        private int ammo = 80;
        private int gold = 100;

        //Tower selection menu
        private int selectedOption = 0;
        private int optionsPerRow = 3;  //temporary; will use an accessor later
        private int numOptions = 9;     //temporary; will use an accessor later

        private const int clipSize = 8;
        private const float reloadTime = 2;
        private const float buildUpgradeTime = 2;
        private const float sellTime = 2;
        private const float interactTime = 0.5f;
        private const float moveCooldown = 0.1f;
        private const float fireCooldown = 0.2f;

        // Use this for initialization
        void Start() {
            moveSprite(startLocation);
        }

        // Update is called once per frame
        void Update() {
        }

        void FixedUpdate() {
        }

        //Takes input from PlayerInput to handle all player actions
        public void Move(PlayerInput.Input input) {
            facing = input.lastDirection;
            TileScript currentTile = LevelManager.Instance.Tiles[location];

            //These are used to throttle their associated actions
            moveCounter += Time.deltaTime;
            fireCounter += Time.deltaTime;

            //These are used as a required channel time for their associated actions
            reloadCounter += Time.deltaTime;
            if(!input.sellHold) { sellCounter = 0; }
            if (!input.interactHold) { interactCounter = 0; }

            //Automatically reload weapon if not in use for a certain period or if clip is empty
            if (reloadCounter > reloadTime) {
                int missingAmmo = clipSize - ammoInClip;

                //If there's enough ammo left to reload the entire clip, do so
                if (ammo - missingAmmo > 0) {
                    ammo -= missingAmmo;
                    ammoInClip += missingAmmo;
                }
                //Otherwise, just reload bullets equal to the remaining ammo
                else {
                    ammoInClip += ammo;
                    ammo = 0;
                }
            }

            //If the player is selecting something from a menu
            if(selecting) {
                //If the player cancels the tower selection menu
                if(input.cancelDown) {
                    selecting = false;
                    LevelManager.Instance.TowerMenu.SetActive(false);
                }
                //If the player releases the build button, begin building the last selected tower
                else if(input.buildUpgradeUp) {
                    working = true;
                    selecting = false;
                    LevelManager.Instance.TowerMenu.SetActive(false);
                }
                //Otherwise, the player can alter the selected tower with movement keys
                else {
                    if(input.up)            { selectedOption -= optionsPerRow; }    //Move up one row
                    else if(input.down)     { selectedOption += optionsPerRow; }    //Move down one row
                    else if(input.right)    { selectedOption++; }                   //Move right one space
                    else if(input.left)     { selectedOption--; }                   //Move left one space

                    //All actions that can alter the selected option wrap back around
                    if (selectedOption >= numOptions) {
                        selectedOption %= numOptions;
                    }
                    while (selectedOption <= 0) {
                        selectedOption += numOptions;
                    }
                }
            }
            //If the player is in the process of building something
            else if(working) {
                transform.Rotate(new Vector3(0, 0, 90));//temporary "working" animation
                buildUpgradeCounter += Time.deltaTime;

                //If the player cancels the build command
                if(input.cancelDown) {
                    buildUpgradeCounter = 0;
                    working = false;
                    moveSprite(location);//temporary to correct facing after random rotation
                }
                //If the build command completes
                if(buildUpgradeCounter >= buildUpgradeTime) {
                    //build the selected tower on this line
                    buildUpgradeCounter = 0;
                    working = false;
                    moveSprite(location);//temporary to correct facing after random rotation
                }
            }
            //If the player starts building something
            else if (input.buildUpgradeDown) {
                //Can only build on walls
                if (currentTile.Type == "wall") {
                    selecting = true;
                    //selectedOption = 0; //(uncomment this line if saving past selection is undesired)
                    //Build tower if there's no tower at location (open tower selection menu)
                    if (!currentTile.IsTower) {
                        currentTile.TowerMenu(transform.position);
                    }
                    //Upgrade tower if there is a tower at location (open tower upgrade selection menu)
                    else {
                    }
                }
            }
            //If the player is selling something
            else if(input.sellHold) {
                //Sell tower if there is a tower at location
                if (currentTile.IsTower) {
                    transform.Rotate(new Vector3(0, 0, 90));//temporary "working" animation
                    sellCounter += Time.deltaTime;
                    if(sellCounter > sellTime) {
                        sellCounter = 0;
                        //sell the tower on this line
                        moveSprite(location);//temporary to correct facing after random rotation
                    }
                }
            }
            //If the player is interacting with something
            else if(input.interactDown) {
                TileScript next = LevelManager.Instance.Tiles[getNextPoint(facing)];

                //Interact with something if facing an object that can be interacted with
                if(true) {
                    interactCounter += Time.deltaTime;
                    if(interactCounter > interactTime) {
                        interactCounter = 0;
                        //interact with object on this line
                    }
                }
            }
            //If the player is firing their weapon (weapons are fully automatic)
            else if(input.fireDown || input.fireHold) {
                //Fire weapon in facing direction if there is ammo left in the clip
                if(ammoInClip > 0 && fireCounter > fireCooldown) {
                    ammoInClip--;
                    fireCounter = 0;
                    reloadCounter = 0;
                    //fire bullet on this line
                    GameObject bullet = Instantiate(tower_projectile, transform.position, transform.rotation) as GameObject;
                    bullet.GetComponent<Rigidbody2D>().velocity = facing * projectileSpeed;
                }
            }
            //If the player is moving (can move anywhere except water)
            else {
                if(moveCounter > moveCooldown) {
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

        //Get the next point in the given direction
        private Point getNextPoint(Vector3 facing) {
            Point next = new Point(location.X + (int)facing.x, location.Y + (int)facing.y);
            return next;
        }

        //Move the player in the given direction if the new space is not on water (illegal); call moveSprite() to update the player's facing direction
        private void moveInDirection(Vector3 facing) {            
            if (facing != Vector3.zero) {
                Point next;
                if (facing == Vector3.up || facing == Vector3.down) {
                    next = getNextPoint(-facing);
                }
                else {
                    next = getNextPoint(facing);
                }                
                
                if (LevelManager.Instance.Tiles[next].Type != "water") {
                    location = next;
                }
                moveSprite(location);
            }
        }

        //Put the player on the given point and update facing direction
        private void moveSprite(Point point) {
            transform.position = new Vector3(LevelManager.Instance.worldStart.x + LevelManager.Instance.TileSize * point.X, LevelManager.Instance.worldStart.y - LevelManager.Instance.TileSize * point.Y, 0);
            float angle = Mathf.Atan2(facing.y, facing.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        //Creates the player at coordinates x and y with the given player number; sets wherever they start to be the default start location
        public void createPlayerAt(int number, int x, int y) {
            playerNumber = number;
            startLocation = new Point(x, y);
            location = startLocation;
            facing = Vector3.right;
            moveSprite(location);
        }
    }
}
