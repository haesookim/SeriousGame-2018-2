using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(Load());
	}
    private IEnumerator Load()
    {
        bool GM = GeneralManager.GM.isInitialized;
        bool DM = DialogueManager.DM.isInitialized;
        bool UI = FindObjectOfType<DontDestroy>().isInitialized;
        while(!GM || !DM || !UI)
        {
            yield return null;
        }
        SceneManager.LoadScene("SampleScene");
        yield return null;
    }
}
