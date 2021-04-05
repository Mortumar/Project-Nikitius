using UnityEngine;

public class PauseButton : MonoBehaviour
{
    [SerializeField] GameObject panelPause;
    
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
