using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PauseButton : MonoBehaviour
{
    [SerializeField] GameObject panelPause;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }    
    public void PauseOn()
    {
        panelPause.SetActive(true);
        Time.timeScale = 0;

    }
    public void Pauseoff()
    {
        panelPause.SetActive(false);
        Time.timeScale = 1;
    }
}
