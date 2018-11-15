using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour {

    public AudioSource background_music;
    public Image background_image;
    public float background_music_fade_speed;
    public float background_image_fade_speed;
    private AudioSource beep;

    public void StartGame()
    {
        StartCoroutine(StartGame_Coroutine());
    }

    private IEnumerator StartGame_Coroutine()
    {
        Vector4 background_color = background_image.color;
        beep = this.GetComponent<AudioSource>();
        beep.Play();
        while (background_music.volume > 0)
        {
            //Fade volume away
            background_music.volume -= background_music_fade_speed * Time.deltaTime;

            //Fade background away
            background_color -= new Vector4(0, 0, 0, background_image_fade_speed * Time.deltaTime);
            background_image.color = background_color;

            yield return null;
        }
        SceneManager.LoadScene("LoadingScene");
        yield return null;
    }
}
