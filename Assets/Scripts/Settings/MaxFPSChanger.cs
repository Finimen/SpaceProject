using UnityEngine;

public class MaxFPSChanger : MonoBehaviour
{
    [SerializeField] private bool setCurrentFPS;
    [SerializeField] private int currentFPS = 60;

    private void Awake()
    {
        QualitySettings.vSyncCount = 0;

        if (setCurrentFPS)
        {
            Application.targetFrameRate = currentFPS;
        }
        else
        {
            Application.targetFrameRate = Screen.currentResolution.refreshRate == 0 ? 60 : Screen.currentResolution.refreshRate;
        }
    }
}