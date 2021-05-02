using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class ARModeUI : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Dropdown weaponSelector;
    [SerializeField] string kerisInformation;
    [SerializeField] string cluritInformation;
    // [SerializeField] string calukInformation;
    [SerializeField] GameObject infoPanel;
    [SerializeField] TMPro.TMP_Text infoText;
    [SerializeField] GameObject instructionText;
    [SerializeField] GameObject keris;
    [SerializeField] GameObject clurit;
    // [SerializeField] GameObject caluk;

    private int currentlySelected;
    private string currentlySelectedWeaponInformation;
    GameObject currentAugmentation;
    SmartTerrain smartTerrain;
    PositionalDeviceTracker positionalDeviceTracker;

    void Start()
    {
        currentAugmentation = keris;
        infoPanel.SetActive(false);
        infoText.text = kerisInformation.Replace("<br>", "\n");
        clurit.SetActive(false);
        currentlySelected = 0;
        currentlySelectedWeaponInformation = kerisInformation;
        weaponSelector.onValueChanged.AddListener(delegate {
            WeaponSelectorValueChangedHandler(weaponSelector);
        });
    }

    void Destroy() {
        weaponSelector.onValueChanged.RemoveAllListeners();
    }

    public void WeaponSelectorValueChangedHandler(TMPro.TMP_Dropdown target) {
        ChangeSelected(target.value);
    }

    public void ChangeSelected(int index) {
        currentlySelected = index;
        switch(currentlySelected) {
            case 0: {
                currentlySelectedWeaponInformation = kerisInformation;
                infoText.text = currentlySelectedWeaponInformation.Replace("<br>", "\n");
                currentAugmentation = keris;
                keris.SetActive(true);
                clurit.SetActive(false);
                // caluk.SetActive(false);
                break;
            }
            case 1: {
                currentlySelectedWeaponInformation = cluritInformation;
                infoText.text = currentlySelectedWeaponInformation.Replace("<br>", "\n");
                currentAugmentation = clurit;
                keris.SetActive(false);
                clurit.SetActive(true);
                // caluk.SetActive(false);
                break;
            }
            // case 2: {
            //     currentlySelectedWeaponInformation = calukInformation;
            //     infoText.text = currentlySelectedWeaponInformation.Replace("<br>", "\n");
            //     // currentAugmentation = caluk;
            //     keris.SetActive(false);
            //     clurit.SetActive(false);
            //     // caluk.SetActive(true);
            //     break;
            // }
        }
        Debug.Log(currentlySelectedWeaponInformation);
    }

    public void ShowInfoPanel() {
        infoPanel.SetActive(true);
    }

    public void HideInfoPanel() {
        infoPanel.SetActive(false);
    }

    public void ResetScene()
    {
        // reset augmentations
        this.currentAugmentation.transform.position = Vector3.zero;
        this.currentAugmentation.transform.localEulerAngles = Vector3.zero;
    }

    public void ResetTrackers()
    {
        this.smartTerrain = TrackerManager.Instance.GetTracker<SmartTerrain>();
        this.positionalDeviceTracker = TrackerManager.Instance.GetTracker<PositionalDeviceTracker>();

        // Stop and restart trackers
        this.smartTerrain.Stop(); // stop SmartTerrain tracker before PositionalDeviceTracker
        this.positionalDeviceTracker.Reset();
        this.smartTerrain.Start(); // start SmartTerrain tracker after PositionalDeviceTracker
    }

    public void ShowInstructions() {
        instructionText.SetActive(true);
    }

    public void HideInstructions() {
        instructionText.SetActive(false);
    }
}
