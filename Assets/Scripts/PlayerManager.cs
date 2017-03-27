using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UnityStandardAssets._2D {
    public class PlayerManager : Singleton<PlayerManager> {
        public GameObject[] playerPrefabs;
        public Player[] playerArray;
        public Canvas playerUICanvas;

        private GameObject[] playerUIPanels;


        // Use this for initialization
        void Start() {
            playerUIPanels = new GameObject[4];
            playerArray = new Player[4];
            createPlayer(1, 12, 11);
            createPlayer(2, 13, 12);
        }

        // Update is called once per frame
        void Update() {
            for(int i = 0; i < playerUIPanels.Length; i++) {
                if(playerUIPanels[i] != null) {
                    playerUIPanels[i].transform.FindChild("AmmoText").GetComponent<Text>().text = playerArray[i].Ammo.ToString();
                    playerUIPanels[i].transform.FindChild("GoldText").GetComponent<Text>().text = playerArray[i].Gold.ToString();
                    playerUIPanels[i].transform.FindChild("AmmoBarBackgroundImage").FindChild("AmmoBarImage").GetComponent<Image>().fillAmount = (float)playerArray[i].AmmoInClip/playerArray[i].ClipSize;
                }
            }
        }


        private void createPlayer(int number, int x, int y) {
            Player player = Instantiate(playerPrefabs[number - 1]).GetComponent<Player>();
            player.createPlayerAt(number, x, y);
            playerArray[number - 1] = player;
            playerUIPanels[number - 1] = playerUICanvas.transform.FindChild("HealthPanel").FindChild("PlayerUIPanel" + number).gameObject;
        }
    }
}