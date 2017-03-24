using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets._2D {
    public class PlayerManager : Singleton<PlayerManager> {
        public GameObject[] playerPrefabs;
        public Player[] playerArray;


        // Use this for initialization
        void Start() {
            playerArray = new Player[4];
            createPlayer(1, 12, 12);

        }

        // Update is called once per frame
        void Update() {

        }


        private void createPlayer(int number, int x, int y) {
            Player player = Instantiate(playerPrefabs[number - 1]).GetComponent<Player>();
            player.createPlayerAt(number, x, y);
            playerArray[number - 1] = player;
        }
    }
}