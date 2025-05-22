using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Bardent
{
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField]
        private string nameOfLevel;
        [SerializeField]
        private GameObject menuPanel;
        [SerializeField]
        private GameObject optionPanel;

        public void Play()
        {
            SceneManager.LoadScene("Main");
        }

        public void OpenOption()
        {
            menuPanel.SetActive(false);
            optionPanel.SetActive(true);
        }

        public void CloseOption()
        {
            optionPanel.SetActive(false);
            menuPanel.SetActive(true);
        }

        public void ExitGame()
        {
            Application.Quit();
        }
    }
}
