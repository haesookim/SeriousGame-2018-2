using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Animator", menuName = "Animator")]
public class AnimatorSelector : ScriptableObject {
    public int weapon_damage;

    public Animator anim;

    public IEnumerator attack() {
        yield return null;
    }
}
    