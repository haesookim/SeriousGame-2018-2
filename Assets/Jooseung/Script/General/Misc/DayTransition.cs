using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayTransition : MonoBehaviour {

    public float hold_speed;
    public float speed;

    private void OnEnable()
    {
        Image background = this.GetComponent<Image>();
        Text text = this.transform.GetChild(0).GetComponent<Text>();
        if(GeneralManager.GM != null)
            text.text = "Day " + GeneralManager.GM.CurrentDay.ToString();
        StartCoroutine(fade_away(background, text));
    }

    private IEnumerator fade_away(Image background, Text text)
    {
        Color init_background_color = background.color;
        Color init_text_color = text.color;


        Color background_color = background.color;
        Color text_color = text.color;

        float timer = hold_speed + Time.fixedTime;
        while(timer > Time.fixedTime)
        {
            yield return null;
        }

        while(this.GetComponent<Image>().color.a >= 0)
        {
            background_color -= new Color(0, 0, 0, speed * Time.deltaTime);
            text_color -= new Color(0, 0, 0, speed * Time.deltaTime);
            background.color = background_color;
            text.color = text_color;
            yield return null;
        }
        this.gameObject.SetActive(false);
        background.color = init_background_color;
        text.color = init_text_color;
        yield return null;
    }
}
