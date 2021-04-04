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
    // для чистоты лучше запоминать послдений Time.timeScale какой был при нажатии на паузу и его применять
    // вдруг ты захочешь сделать замедление времени? а у тебя после паузы оно будет вырубаться
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
