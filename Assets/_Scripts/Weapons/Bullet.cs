using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public Vector3 shootforce;
    public int direction;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(shootforce * Time.deltaTime *direction);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Hit();
    }

    void Hit()
    {
        Debug.Log("It hit!");
        Destroy(gameObject);
        //also give damage
    }
}
