using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour {
    private void Start() {
        StartCoroutine("ExitCredits");
    }

    private IEnumerator ExitCredits() {
        yield return new WaitForSeconds(17f);
        SceneManager.LoadScene("Menu");
    }
}