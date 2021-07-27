using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHideTutorial : MonoBehaviour
{
    [SerializeField] GameObject TutorialObject;    
    [SerializeField] TMPro.TMP_Text btnTutorialText;
    private bool show = false;
    
    public void ShowAndHideTutorial() {
        if(!show) {
            TutorialObject.SetActive(true);
            btnTutorialText.text = "Hide Tutorial";
            show = true;
        } else {            
            TutorialObject.SetActive(false);
            btnTutorialText.text = "Show Tutorial";
            show = false;
        }
    }
}
