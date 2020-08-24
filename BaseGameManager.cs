using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace jackson
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

        public virtual static void CloseMainMenu(){
            _mainMenu.SetActive(false);
        }

        [SerializeField]
        GameObject losePanel;

        protected PhaseManager phaseManager;

        // Start is called before the first frame update
        void Awake()
        {
            if (GameManager.Instance)
            {
                Destroy(this.gameObject);
                return;
            }
            else
            {
                GameManager.Instance = this;
            }
            _mainMenu = MainMenu;

            UpdateScreenEdges();

            ShowMainMenu();

            phaseManager = GetComponent<PhaseManager>();
            scoreAnim.gameObject.SetActive(false);
            SpawnPlayer();

        }

        public int GetScore()
        {
            return score;
        }
        void ShowMainMenu()
        {
            _mainMenu.SetActive(true);
        }


        void UpdateScreenEdges()
        {
            GameManager.screenEdges = new ScreenEdges();

            RaycastHit hit;

            Ray ray = Camera.main.ViewportPointToRay(new Vector3(.5f, 1.0f, 0));
            if (Physics.Raycast(ray, out hit))
                screenEdges.maxY = hit.point.y - hit.point.y / 4;

            ray = Camera.main.ViewportPointToRay(new Vector3(.5f, 0f, 0));
            if (Physics.Raycast(ray, out hit))
                screenEdges.minY = hit.point.y - hit.point.y / 4;

            ray = Camera.main.ViewportPointToRay(new Vector3(0.0f, .5f, 0));
            if (Physics.Raycast(ray, out hit))
                screenEdges.minX = hit.point.x - hit.point.x / 4;

            ray = Camera.main.ViewportPointToRay(new Vector3(1.0f, .5f, 0));
            if (Physics.Raycast(ray, out hit))
                screenEdges.maxX = hit.point.x - hit.point.x / 4;


            screenEdges.maxX = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
            screenEdges.minX = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
            screenEdges.maxY = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
            screenEdges.minY = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;

        }


        public static void Lose(){
            playing = false;
            if (loseMenu){
                losePanel.SetActive(true);
            }
        }

        protected virtual void SpawnPlayer(){
            Instantiate(playerPrefab, playerSpawn.position, playerSpawn.rotation, null);
        }

        public virtual void ResetGame()
        {
            losePanel.GetComponent<Animator>().SetTrigger("Hide");
            SpawnPlayer();
            RemoveAllEnemies();
            StartGame();
        }

        public static virtual void StartGame()
        {
            _mainMenu.SetActive(false);
            ResetScore();
            playing = true;
            scoreAnim.gameObject.SetActive(true);
            phaseManager.Restart();
        }

     

        public static virtual void AddScore(int amt)
        {
            score += amt;
            if (amt >= scoreAnimThreshold)
                scoreAnim.SetTrigger("AddScore");
        }



        protected static void ResetScore(){
            score = 0;
            scoreText.text = score.ToString();
        }

       

    }
}