using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Blink : MonoBehaviour
{
    [SerializeField] float blinkDuration = .75f;
    TextMeshProUGUI myText;

    void Start()
    {
        myText = GetComponent<TextMeshProUGUI>();
        StartCoroutine(BlinkText());
    }

    IEnumerator BlinkText(){
        while(true){
            if(myText.text == "good"){
                yield break; //return
            }
            yield return new WaitForSeconds(blinkDuration);
            var tempColor1 = myText.color;
            tempColor1.a = 0f;
            myText.color = tempColor1;
            yield return new WaitForSeconds(blinkDuration);
            var tempColor2 = myText.color;
            tempColor2.a = 255f;
            myText.color = tempColor2;
        }
    }
}
