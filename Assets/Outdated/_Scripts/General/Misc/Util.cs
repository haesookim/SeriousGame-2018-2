using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Util {

    public class UI : MonoBehaviour
    {
        public static void icon_float(GameObject obj, float oscillationHeight)
        {
            float y = oscillationHeight * Mathf.Sin((Time.fixedTime) * 5);
            obj.transform.position = new Vector2(obj.transform.position.x, obj.transform.position.y + y);
        }

        public static IEnumerator fade_away(Image img, float start_time, float speed, bool destroy)
        {
            Vector4 img_color = img.color;
            
            //Reset visibility(alpha) back to 1
            img.color = img_color + new Vector4(0, 0, 0, 1);

            //Wait for start_time duration
            float time = Time.fixedTime + start_time;
            while (time > Time.fixedTime) {
                yield return null;
            }
            while (img.color.a > 0) {
                img_color -= new Vector4(0, 0, 0, speed * Time.deltaTime);
                img.color = img_color;
                yield return null;
            }
            if (destroy) {
                Destroy(img.gameObject);
            }
            yield return null;
        }
    }

}