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
        void FixedUpdate() {
            for (int i = 0; i < playerUIPanels.Length; i++) {
                GameObject panel = playerUIPanels[i];
                if (panel != null) {
                    Player player = playerArray[i];
                    panel.transform.FindChild("AmmoText").GetComponent<Text>().text = player.Ammo.ToString();
                    panel.transform.FindChild("GoldText").GetComponent<Text>().text = player.Gold.ToString();
                    panel.transform.FindChild("AmmoBarBackgroundImage").FindChild("AmmoBarImage").GetComponent<Image>().fillAmount = (float)player.AmmoInClip / (float)Player.clipSize;
                }
            }
        }


        private void createPlayer(int number, int x, int y) {
            Player player = Instantiate(playerPrefabs[number - 1]).GetComponent<Player>();
            player.createPlayerAt(number, x, y);
            playerArray[number - 1] = player;
            playerUIPanels[number - 1] = playerUICanvas.transform.FindChild("PlayerUIPanel" + number).gameObject;
            playerUIPanels[number - 1].SetActive(true);
        }
    }
}