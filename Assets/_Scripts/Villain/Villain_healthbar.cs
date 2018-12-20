using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Villain_healthbar : MonoBehaviour
{
    public float villain_Maximum_Hp;

    public float villain_current_hp;

    public Image healthBar_base;
    public Image healthBar;
    public GameObject pos;

    void Start()
    {
        villain_current_hp = villain_Maximum_Hp;
        healthBar.fillAmount = villain_current_hp / villain_Maximum_Hp;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = villain_current_hp / villain_Maximum_Hp;
        healthBar.transform.position = pos.transform.position;
        healthBar_base.transform.position = pos.transform.position;
    }
}
