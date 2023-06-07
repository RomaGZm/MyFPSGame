using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Player;
using Core.Zombie;
using Core.UI;
namespace Core
{
    public class GameManager : MonoBehaviour
    {
        [Header("Datas")]
        public PlayerData playerData;
        [Header("Controllers")]
        public PlayerController playerController;
        public PullController pullController;
        [Header("Enemy")]
        [SerializeField]
        private List<ZombieController> zombies;
        [Header("Other")]
        public PanelEndGame panelEndGame;
        public static GameManager Instance { get; private set; }

        private void Awake()
        {
            

            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }

            StartCoroutine(WaitGameOver());
        }
        private bool ZombiesIsDie()
        {
            foreach(ZombieController zombie in zombies)
            {
                if (!zombie.isDie) return false; ;
            }
            return true;
        }
        IEnumerator WaitGameOver()
        {
           
            yield return new WaitUntil(() => ZombiesIsDie());

            yield return new WaitForSeconds(3);

            panelEndGame.Show(PanelEndGame.EndGameState.Win);
            Time.timeScale = 0;
        }
    }
}

