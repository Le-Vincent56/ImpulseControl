using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace ImpulseControl
{
    public class SceneChanging : MonoBehaviour
    {
        public void ChangeScenes(int sceneIndex)
        {
            SceneManager.LoadScene(sceneIndex);
        }
    }
}
