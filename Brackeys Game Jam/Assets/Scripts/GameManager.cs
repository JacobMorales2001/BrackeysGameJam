using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  public bool gamePaused = false;
  public GameObject pauseMenu;
  public AudioClip PauseButtonAudio;
 AudioSource GameManagerAUDIO;
    // Start is called before the first frame update
     void Awake()
    {
        gamePaused = false;
        Time.timeScale = 1;
        
    }

    void Start()
    {
       GameManagerAUDIO = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || (Input.GetKeyDown(KeyCode.P)))
        {
            if (gamePaused == false)
            {
                Time.timeScale = 0;
                gamePaused = true;
               
                pauseMenu.SetActive(true);
                GameManagerAUDIO.PlayOneShot(PauseButtonAudio);
            }
            else
            {
                pauseMenu.SetActive(false);
                
                gamePaused = false;
                Time.timeScale = 1;
                GameManagerAUDIO.PlayOneShot(PauseButtonAudio);
            }
        }
        
    }

     public void GamePause()
    {
        if (gamePaused == false)
            {
                Time.timeScale = 0;
                gamePaused = true;
               
                pauseMenu.SetActive(true);
            }
    }
    public void GameUnPause()
    {
        pauseMenu.SetActive(false);
        
        gamePaused = false;
        Time.timeScale = 1;
    }
}
