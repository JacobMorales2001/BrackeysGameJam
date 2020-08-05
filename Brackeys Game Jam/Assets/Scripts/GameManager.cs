using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Game State")]
    public bool gameRewind = false;
    public GameObject RewindFilter;
    public bool gamePaused = false;
    public GameObject pauseMenu;

    [Header("Media Symbol Images")]
    public Image MediaSymbol;
    public Sprite PauseImage;
    public Sprite PlayImage;
    public Sprite RewindImage;

     [Header("Sounds")]
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
               MediaSymbol.sprite = PauseImage;
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
                 MediaSymbol.sprite = PlayImage;
            }
        }
        
    }

     public void GamePause()
    {
        if (gamePaused == false)
            {
                MediaSymbol.sprite = PauseImage;
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
         MediaSymbol.sprite = PlayImage;
    }

    public void RewindGame()
    {
        RewindFilter.SetActive(true);
        MediaSymbol.sprite = RewindImage;
    }
}
