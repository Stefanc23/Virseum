using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GVRGazeInteraction : MonoBehaviour
{
    public Image gazeReticle;
    public float totalTime = 2f;
    private bool gvrStatus;
    private float gvrTimer;

    public int distanceOfRay = 10;
    private RaycastHit _hit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        if(gvrStatus) {
            gvrTimer += Time.deltaTime;
            gazeReticle.fillAmount = gvrTimer / totalTime;
        }

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if(Physics.Raycast(ray, out _hit, distanceOfRay)) {
            if(gazeReticle.fillAmount == 1 && _hit.transform.CompareTag("BtnTutorial")) {
                _hit.transform.gameObject.GetComponent<ShowHideTutorial>().ShowAndHideTutorial();
                Debug.Log("test");
            }
        }
    }

    public void GVROn() { 
        gvrStatus = true;
    }

    public void GVROff() { 
        gvrStatus = false;
        gvrTimer = 0;
        gazeReticle.fillAmount = 0;
    }
}
