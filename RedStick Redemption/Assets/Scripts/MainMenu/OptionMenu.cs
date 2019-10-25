using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionMenu : MonoBehaviour
{
    private GameObject mainMenuMusicControllerObj;
    private MusicController controllerMusic;
    public Slider volumeSlider;

    private void Awake()
    {
        mainMenuMusicControllerObj = GameObject.FindGameObjectWithTag("MusicControllertag");

        controllerMusic = mainMenuMusicControllerObj.GetComponent<MusicController>();

        volumeSlider.value = controllerMusic.getAudioTheme().volume;

    }

    public void Update()
    {
        Vector3 mouse = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
        
        Debug.Log("Mouse Pos Screen : " + mouse.ToString());

        controllerMusic.getAudioTheme().volume = volumeSlider.value;
    }
    public void GotoMainScene()
    {
        SceneManager.LoadScene("PremierNiveau");
    }

    public void GotoMenuScene()
    {
        SceneManager.LoadScene("mainMenu");
    }

    public void GotoExit()
    {
        Debug.Log("Je suis en train de quitter !");
        Application.Quit(); //Uniquement dans le cas avec le runtime et non dans l'editor.
    }

    public void GotoOptions()
    {
        SceneManager.LoadScene("OptionMenu");
    }

}
