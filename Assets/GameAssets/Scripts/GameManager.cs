using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Player;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        [Header("Datas")]
        public PlayerData playerData;
        [Header("Controllers")]
        public PlayerController playerController;
        public PullController pullController;


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

           
        }
    }
}

