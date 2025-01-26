using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ImpulseControl
{
    public class MainMenu : MonoBehaviour
    { 
        public bool keyPressed = false;
        public void PlayGame()
        {
            SceneManager.LoadScene(1);
        }

        public void OnQuitClicked()
        {
            UnityEngine.Application.Quit();
        }

        public void ChangeCanvas()
        {
            if (UnityEngine.Input.anyKey && !keyPressed)
            {
                keyPressed = true;
            }
        }
    }

}
