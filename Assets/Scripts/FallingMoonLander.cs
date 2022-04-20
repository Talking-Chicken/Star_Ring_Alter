using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class FallingMoonLander : MonoBehaviour
{

    [YarnCommand("Load_Scene")]
    public void changeToRoofTop(string scene) {
        StartCoroutine(waitToChangeScene(scene));
    }

    IEnumerator waitToChangeScene(string scene) {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene(scene);
    }
}
