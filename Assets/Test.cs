using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount >0){
            StartCoroutine(LoadScene());
        }
    }

    IEnumerator LoadScene(){
        AsyncOperation operation = SceneManager.LoadSceneAsync(0);
        while(!operation.isDone){
            float progress = Mathf.Clamp01(operation.progress/0.9f);
            print(progress);
            yield return null;
        }
    }
}
