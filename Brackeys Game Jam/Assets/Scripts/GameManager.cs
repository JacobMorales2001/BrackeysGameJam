using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Game State")]
    public bool gameRewind = false;
    public GameObject RewindFilter;
    public bool gamePaused = false;
    public GameObject pauseMenu;
    private bool RewindTimerisON;
    public float RewindTimer;
    public float RewindTimeMax = 5;

    [Header("Media Symbol Images")]
    public Image MediaSymbol;
    public Sprite PauseImage;
    public Sprite PlayImage;
    public Sprite RewindImage;

     [Header("Sounds")]
    public AudioClip PauseButtonAudio;
    public AudioClip RewindAudio;
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
      if(RewindTimerisON)
      {
          RewindTimer += Time.deltaTime;
      }

       if (RewindTimer >= RewindTimeMax)
        {   
            Debug.Log("boop");
            RewindFilter.SetActive(false);
            MediaSymbol.sprite = PlayImage;
            RewindTimerisON = false;
            RewindTimeMax *= 2;
        }

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
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Quit");
    }

    public void LoadLevel(string level)
    {
        SceneManager.LoadScene(level);
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
        RewindTimerisON = true;
        RewindFilter.SetActive(true);
        MediaSymbol.sprite = RewindImage;
        GameManagerAUDIO.PlayOneShot(RewindAudio);
    }
}
