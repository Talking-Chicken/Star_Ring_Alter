using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class FallingMoonLander : MonoBehaviour
{

    [YarnCommand("change_to_roof_top")]
    public void changeToRoofTop() {
        StartCoroutine(waitToChangeScene());
    }

    IEnumerator waitToChangeScene() {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("Main 1");
    }
}
