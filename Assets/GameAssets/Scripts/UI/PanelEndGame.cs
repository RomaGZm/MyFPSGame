using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace Core.UI
{
    public class PanelEndGame : MonoBehaviour
    {
        public enum EndGameState
        {
            Lose, Win
        }
        [SerializeField]
        private TMP_Text text;

        public void Show(EndGameState gameState)
        {
            if (gameState == EndGameState.Win)
            {
                text.text = "Победа!";
            }
            else
            {
                text.text = "Поражение!";
            }
            gameObject.SetActive(true);
        }
        void Start()
        {
            text = transform.GetChild(0).GetComponent<TMP_Text>();
        }

        public void OnBtnExitClick()
        {
            Application.Quit();
        }
    }
}

