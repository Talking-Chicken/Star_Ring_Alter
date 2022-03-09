using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using NaughtyAttributes;

public class DialogueFunctions : MonoBehaviour
{
    [SerializeField, Foldout("dialogue")] private DialogueRunner runner;

    NeuroImplantDevice playerNeuroDevice;
    PlayerBackpack playerBackpack;

    [SerializeField, Foldout("view")] private GameObject moonLanderCamera;
    void Start()
    {
        playerNeuroDevice = FindObjectOfType<PlayerControl>().GetComponent<NeuroImplantDevice>();
        playerBackpack = FindObjectOfType<PlayerBackpack>();

        runner.AddFunction("searchAppInDownload", 1, delegate (Yarn.Value[] parameters)
        {
            return searchDownloadedApp(parameters[0].AsString);
        });

        runner.AddFunction("backpackContains", 1, delegate (Yarn.Value[] parameters) {
            return playerBackpack.contains(parameters[0].AsString);
        });

        runner.AddFunction("getAccessCardLevel", 0, delegate (Yarn.Value[] parameters)
        {
            return getAccessCardLevel();
        });
    }

    public bool searchDownloadedApp(string appName)
    {
        if (appName.Equals(null))
        {
            Debug.LogWarning("app name for seach downloaded app cannot be null");
            return false;
        }

        return playerNeuroDevice.search(playerNeuroDevice.downloadedApps, appName);
    }

    public int getAccessCardLevel()
    {
        if (playerBackpack.getItem("access card") != null)
            return playerBackpack.getItem("access card").GetComponent<AccessCard>().level;

        return 0;
    }

    [YarnCommand("set_moon_lander_camera_active")]
    public void setMoonLanderCameraActive() {
        moonLanderCamera.SetActive(true);
    }

    [YarnCommand("set_moon_lander_camera_inactive")]
    public void setMoonLanderCameraInactive() {
        moonLanderCamera.SetActive(false);
    }

    [YarnCommand("reset_loop")]
    public void resetLoop() {
        TimeLoopSystem.reset();
    }

}
