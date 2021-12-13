using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    [SerializeField] LoadGame loadScene;
    [SerializeField] string sceneName;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            Destroy(other.gameObject);
            loadScene.LoadSceneOne(sceneName);
        }
    }
}
