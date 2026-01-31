using UnityEngine;

public class WorldSwitcher : MonoBehaviour
{
    public GameObject worldDark;
    public GameObject worldDark1;
    public GameObject worldLight;
    public GameObject worldLight1;

    private bool isDark = true;

    void Start()
    {
        worldLight1.SetActive(false);
        worldLight.SetActive(true);
        worldDark.SetActive(false);
         worldDark1.SetActive(true);
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            isDark = !isDark;

            worldDark.SetActive(isDark);
             worldDark1.SetActive(isDark);
            worldLight.SetActive(!isDark);
            worldLight1.SetActive(!isDark);
        }
    }
}
