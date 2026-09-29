using UnityEngine;
using UnityEngine.UI;

public class UiButtons : MonoBehaviour
{
    public GameObject settingsPanel;
    bool autotoggle = false;
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        settingsPanel.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SettingsPanelOpen()
    {
        settingsPanel.gameObject.SetActive(true);
    }

    public void SettingsPanelClose()
    {
        settingsPanel.gameObject.SetActive(false);
    }

    public void AutoToggle()
    {
        
    }
}
