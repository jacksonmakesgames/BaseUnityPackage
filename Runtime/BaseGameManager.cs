using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Jackson
{
   
    public class BaseGameManager : MonoBehaviour
    {

        public static bool playing = false;
        public static int score;

        [SerializeField]
        protected GameObject _mainMenu;
        protected static GameObject mainMenu = _mainMenu;

        [SerializeField]
        protected GameObject _playerPrefab;
        protected static GameObject playerPrefab = _playerPrefab;

        [SerializeField]
        protected Transform playerSpawn;

        

        [SerializeField]
        GameObject losePanel;

        // Start is called before the first frame update
        protected void Awake(){
            _mainMenu = MainMenu;
            ShowMainMenu();
            SpawnPlayer();

        }

        public virtual static int GetScore(){
            return score;
        }

        protected static virtual void ShowMainMenu(){
            _mainMenu.SetActive(true);
            if (mainMenu.GetComponent<Animator>()){
                mainMenu.GetComponent<Animator>().SetTrigger("Show");
            }
        }


        public virtual static void CloseMainMenu()
        {
            _mainMenu.SetActive(false);
            if (mainMenu.GetComponent<Animator>())
            {
                mainMenu.GetComponent<Animator>().SetTrigger("Hide");
            }
        }
        public virtual static void Lose(){
            playing = false;
            if (loseMenu){
                losePanel.SetActive(true);
                if (losePanel.GetComponent<Animator>()) {
                    losePanel.GetComponent<Animator>().SetTrigger("Show");
                }
            }
        }

        protected virtual static void SpawnPlayer(){
            Instantiate(playerPrefab, playerSpawn.position, playerSpawn.rotation, null);
        }

        public virtual static void ResetGame(){
            if (losePanel.GetComponent<Animator>()){
                losePanel.GetComponent<Animator>().SetTrigger("Hide");
            }
            SpawnPlayer();
            StartGame();
        }

        public static virtual void StartGame(){
            _mainMenu.SetActive(false);
            ResetScore();
            playing = true;
            scoreAnim.gameObject.SetActive(true);
            phaseManager.Restart();
        }

     

        public static virtual void AddScore(int amt){
            score += amt;
            UpdateScoreText();
        }



        protected virtual static void ResetScore(){
            score = 0;
            UpdateScoreText();
        }

        protected virtual static void UpdateScoreText()
        {
            if (scoreText)
                scoreText.text = score.ToString();
        }
       

    }
}