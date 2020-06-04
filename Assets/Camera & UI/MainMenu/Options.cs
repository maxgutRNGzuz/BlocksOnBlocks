using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    [SerializeField] Toggle ultraGraphics;

    void Start() {
        CheckGraphics();
    }

    void CheckGraphics(){
        int qualityLevel = QualitySettings.GetQualityLevel();
        if(qualityLevel == 0){
            ultraGraphics.isOn = false;
        }
        else if(qualityLevel == 1){
            ultraGraphics.isOn = true;
        }
    }

    public void SetGraphics(){
        if(ultraGraphics.isOn){
            QualitySettings.SetQualityLevel(1);
        }
        else if(!ultraGraphics.isOn){
            QualitySettings.SetQualityLevel(0);
        }
        print(QualitySettings.GetQualityLevel());
    }
}
