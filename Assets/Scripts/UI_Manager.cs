using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public ModelSwitcher modelSwitcher; // store ModelSwitcher script
    public MaterialSwitcher materialSwitcher; // store MaterialSwitcher script

    public HightlightSelection selectionManager; // store HighlightSelection script

    public Canvas deckUI;
    public Canvas wheelsUI;

    private Animator _deckUIAnimator;
    private Animator _wheelsUIAnimator;

    private bool _enableUIopening = true; // enable or disable UI opening
    private static readonly int IsVisible = Animator.StringToHash("isVisible");

    private void Start()
    {
        _deckUIAnimator = deckUI.GetComponent<Animator>(); // get animator component of deckUI
        _wheelsUIAnimator = wheelsUI.GetComponent<Animator>(); // get animator component of wheelsUI

        // Initialize _materialDropdown to the dropdown component of the dropdownObject (on-screen material editor)
        _materialDropdown = dropdownObject.GetComponent<TMP_Dropdown>();
        if (_materialDropdown == null)
        {
            Debug.LogError("Dropdown component not found on dropdownObject");
        }
        else
        {
            _materialDropdown.onValueChanged.AddListener(OnDropdownOptionSelected);
        }

        _labelText = dropdownObject.GetComponentInChildren<TextMeshProUGUI>(); // get label text component of dropdownObject

        ShowMaterialPicker(false);
    }

    /* ############################################################################################
     * Control world space UI for deck and wheels
     */
    public void SwitchUIAvailability()
    {
        if (_enableUIopening) // if UI is visible
        {
            ShowUI("none"); // hide all UI
            ShowMaterialEditorWindow(false);
        }

        _enableUIopening = !_enableUIopening; // toggle UI availability
    }

    public void SetUILargeWheels()
    {
        deckUI.transform.localPosition = new Vector3(0.1136f, 0.08f, 0);
    }

    public void SetUIDefWheels()
    {
        deckUI.transform.localPosition = new Vector3(0.1136f, 0.07f, 0);
    }

    public void SetUIDefDeck()
    {
        wheelsUI.transform.localPosition = new Vector3(0f, 0.0268f, -0.22121f);
    }

    public void SetUILongDeck()
    {
        wheelsUI.transform.localPosition = new Vector3(0f, 0.0268f, -0.415f);
    }

    public void ShowUI(string type)
    {
        if (_enableUIopening)
        {
            string deckTag = modelSwitcher.GetCurrentTags()[0]; // get current deck tag
            string wheelTag = modelSwitcher.GetCurrentTags()[1]; // get current wheels tag

            if (type == deckTag)
            {
                ShowDeckUI();
                selectionManager
                    .EnableDeckHighlighting(false); // we don't want to highlight the deck if we have the deck UI open

                HideWheelsUI();
                selectionManager
                    .EnableWheelHighlighting(true); // highlight wheels in case we want to switch to wheel ui
            }
            else if (type == wheelTag)
            {
                ShowWheelsUI();
                selectionManager
                    .EnableWheelHighlighting(
                        false); // we don't want to highlight the wheels if we have the wheels UI open

                HideDeckUI();
                selectionManager.EnableDeckHighlighting(true); // highlight deck in case we want to switch to deck ui
            }
            else
            {
                HideDeckUI();
                selectionManager.EnableDeckHighlighting(true);

                HideWheelsUI();
                selectionManager.EnableWheelHighlighting(true);
            }
        }
    }

    private void ShowDeckUI()
    {
        // Trigger fade-in animation for deckUI and fade-out for wheelsUI
        if (_deckUIAnimator != null)
            _deckUIAnimator.SetBool(IsVisible, true);
    }

    private void ShowWheelsUI()
    {
        // Trigger fade-in animation for wheelsUI and fade-out for deckUI
        if (_wheelsUIAnimator != null)
            _wheelsUIAnimator.SetBool(IsVisible, true);
    }

    private void HideDeckUI()
    {
        // Trigger fade-out animation for deckUI and fade-in for wheelsUI
        if (_deckUIAnimator != null)
            _deckUIAnimator.SetBool(IsVisible, false);
    }

    private void HideWheelsUI()
    {
        // Trigger fade-out animation for wheelsUI and fade-in for deckUI
        if (_wheelsUIAnimator != null)
            _wheelsUIAnimator.SetBool(IsVisible, false);
    }


    /* ############################################################################################
     * Control on-screen general UI
     */
    public void ScreenBtnHide(GameObject btn) // completely hides the corresponding button from the ui
    {
        var btnCanvas = btn.GetComponent<CanvasGroup>();

        if (btnCanvas != null)
        {
            btnCanvas.alpha = btnCanvas.alpha == 0 ? 1 : // if button is hidden -> show button
                // else: hide button
                0;

            btnCanvas.interactable = !btnCanvas.interactable;
            btnCanvas.blocksRaycasts = !btnCanvas.blocksRaycasts;
        }
    }

    public void ScreenBtnTranslucent(GameObject btn) // makes the corresponding button translucent
    {
        var btnCanvas = btn.GetComponent<CanvasGroup>(); // get canvas group component
        // var btnButton = btn.GetComponent<Button>(); // get button component
        // var btnColour = btnButton.colors; // get colours of button component

        if (btnCanvas != null)
        {
            if (btnCanvas.alpha == 0.5f)
            {
                // make button visible
                btnCanvas.alpha = 1;
            }
            else
            {
                // make button translucent
                btnCanvas.alpha = 0.5f;
            }

            // Apply the modified colors back to the button
            // btnButton.colors = btnColour;
        }
    }


    /* ############################################################################################
     * Control on-screen material editor UI
     */

    public Canvas onScreenUI;
    private static readonly int ShowMatEditor = Animator.StringToHash("showMatEditor");

    public GameObject dropdownObject;

    private List<TagDisplayNames> _currentOptions = new List<TagDisplayNames>(); // keep track of current options

    private TMP_Dropdown _materialDropdown; // store dropdown component of dropdownObject
    private TextMeshProUGUI _labelText;

    public void ShowMaterialEditorWindow(bool show)
    {
        if (_enableUIopening)
        {
            if (onScreenUI != null)
            {
                onScreenUI.GetComponent<Animator>().SetBool(ShowMatEditor, show);
            }
        }
    }

    // Dropdown functionality ++++++++++++++++++++++++++++++++
    private string _currentPrefabTag; // store current prefab tag
    private int _partMatIndex; // store current deck part name
    private string _partTag; // store current deck part name

    public void PopulateMaterialDropdown(string selectedPrefabTag)
    {
        // if there are any dropdown options, destroy them before adding new ones
        _partMatIndex = 0;
        _partTag = "";
        _currentOptions.Clear(); // clear current options
        _materialDropdown.ClearOptions(); // clear dropdown options
        _materialDropdown.options.Insert(0, new TMP_Dropdown.OptionData("None")); // add placeholder option
        _materialDropdown.value = 0; // Set the dropdown to show the placeholder by default
        if (_labelText != null)
        {
            // Set initial text
            _labelText.text = "Select Option:";
        }

        ShowMaterialPicker(false);

        _currentOptions =
            new List<TagDisplayNames>(
                materialSwitcher.GiveModelPartsOptions(selectedPrefabTag)); // get options from MaterialSwitcher script

        foreach (var option in _currentOptions)
        {
            AddDropdownOptions(option.uiName);
        }

        _currentPrefabTag = selectedPrefabTag; // save current prefab tag
    }

    private void AddDropdownOptions(string uiName) // add options to dropdown
    {
        var newOption = new TMP_Dropdown.OptionData(uiName); // create new option
        _materialDropdown.options.Add(newOption); // add new option to dropdown
    }

    private void OnDropdownOptionSelected(int index)
    {
        if (index > _currentOptions.Count) // if index is out of bounds
        {
            Debug.LogError("Invalid dropdown option selected");
            return;
        }

        if (index == 0) // if placeholder is selected
        {
            ShowMaterialPicker(false); // hide material picker, placeholder is at index 0
            return;
        }

        _partTag = _currentOptions[index - 1].modelTag; // get tag of selected option, -1 because placeholder is at index 0
        // Debug.Log("Selected tag: " + selectedTag);

        _partMatIndex = _currentOptions[index - 1].partMatIndex; // store material index of selected model part
        // this is necessary because some materials have two parts (e.g. deck & grip) and we need to know which one is selected to change the material

        // Clear listeners for all toggle components in of all toggle buttons in _currentMatButtons and _currentDecButtons
        // + delete all buttons in _currentMatButtons and _currentDecButtons
        foreach (var button in _currentMatButtons)
        {
            Toggle toggleComponent = button.GetComponent<Toggle>();
            if (toggleComponent != null)
            {
                toggleComponent.onValueChanged.RemoveAllListeners();
                Destroy(button);
            }
        }
        foreach (var button in _currentDecButtons)
        {
            Toggle toggleComponent = button.GetComponent<Toggle>();
            if (toggleComponent != null)
            {
                toggleComponent.onValueChanged.RemoveAllListeners();
                Destroy(button);
            }
        }
        _currentMatButtons?.Clear();
        _currentDecButtons?.Clear();

        PopulateMaterialPicker(); // populate material picker with materials of selected tag
        PopulateDecalPicker(); // populate decal picker with decals of selected tag

        ShowMaterialPicker(true); // show material picker
    }

    // Material picker functionality ++++++++++++++++++++++++++++++++
    [Tooltip("The object that contains the material & decal picker + sliders")]
    public GameObject materialEditorContainer;

    public GameObject materialButtonsContainer;
    public GameObject materialButtonPrefab; // store material button prefab

    private List<MaterialPairList> _availableMaterials; // store current materials as returned from MaterialSwitcher script

    private readonly List<GameObject> _currentMatButtons = new List<GameObject>(); // store current material buttons

    // Shows both material and decal picker
    private void ShowMaterialPicker(bool show) // show or hide the material picker
    {
        if (materialEditorContainer != null)
        {
            materialEditorContainer.SetActive(show);
        }
    }
    // End of material and decal picker

    private void PopulateMaterialPicker() // populate the material picker with the materials of the selected tag
    {
        _availableMaterials = new List<MaterialPairList>(materialSwitcher.GetMaterials(_currentPrefabTag, _partTag, _partMatIndex)); // get materials from MaterialSwitcher script & store them in a new list

        if (_availableMaterials != null)
        {
            int previousMaterial = materialSwitcher.GetCurrentMaterial(_currentPrefabTag, _partTag, _partMatIndex); // get previous material from MaterialSwitcher script

            Toggle toggleToActivate = null;

            // enable AllowSwitchOff for materialButtonsContainer
            ToggleGroup toggleGroup = materialButtonsContainer.GetComponent<ToggleGroup>();
            if (toggleGroup != null)
            {
                toggleGroup.allowSwitchOff = true;
            }

            for (int i = 0; i < _availableMaterials.Count; i++)
            {
                GameObject newToggle = Instantiate(materialButtonPrefab, materialButtonsContainer.transform); // create new button from toggle prefab

                // all toggle component functionality
                Toggle toggleComponent = newToggle.GetComponent<Toggle>();
                if (toggleComponent != null)
                {
                    toggleComponent.group = materialButtonsContainer.GetComponent<ToggleGroup>(); // set toggle group to toggle group of materialButtonsContainer
                    var i1 = i;
                    var i2 = i;
                    toggleComponent.onValueChanged.AddListener(isOn =>  OnMatButtonClicked(_currentPrefabTag, _partTag, i1, _availableMaterials[i2].uiSpriteColor, isOn));
                    // execute this function when toggle is clicked
                    // pass colour to later change background for decal picker buttons

                    if (_availableMaterials[i] == _availableMaterials[0]) // if first button
                    {
                        toggleToActivate = toggleComponent; // store toggle component of first material
                    }
                    else if (previousMaterial == i) // if previous material is the same as the current material
                    {
                        toggleToActivate = toggleComponent; // store toggle component of current material
                        // Debug.Log("Previous material is the same as the current material");
                    }
                    else
                    {
                        toggleComponent.isOn = false; // set toggle to false
                    }
                }

                // Navigate to the matSprite Image component
                // assuming prefab structure is matToggle -> check -> matSprite
                Transform matSpriteTransform = newToggle.transform.Find("check/matSprite"); // find matSprite (child of toggle button)
                if (matSpriteTransform != null)
                {
                    Image matSpriteImage = matSpriteTransform.GetComponent<Image>(); // get image component of matSprite
                    if (matSpriteImage != null && _availableMaterials[i].uiSprite != null)
                    {
                        matSpriteImage.sprite = _availableMaterials[i].uiSprite; // set sprite of matSprite to the sprite of the PairMaterial entry
                        matSpriteImage.color = _availableMaterials[i].uiSpriteColor; // set color of matSprite to the color of the PairMaterial entry
                    }
                }

                _currentMatButtons?.Add(newToggle); // add new button to list of current buttons
            }
            ActivateToggle(toggleToActivate);

            if (toggleGroup != null)
            {
                toggleGroup.allowSwitchOff = false;
            }
        }
    }

    private Color _currentColour; // store current UI colour
    private void OnMatButtonClicked(string prefabTag, string partTag, int matIndex, Color btnColor, bool isOn)
    {
        if (!isOn) return; // only execute if toggle is on

        materialSwitcher.ChangeMaterial(prefabTag, partTag, matIndex, _partMatIndex); // change material of selected model part

        int decalIndex = materialSwitcher.GetCurrentDecal(partTag, _partMatIndex);

        // change background color of decal picker buttons
        foreach (var button in _currentDecButtons)
        {
            if (button != _currentDecButtons[0]) // if button is not the no decal button
            {
                Image matSpriteImage = button.transform.Find("check/matSprite").GetComponent<Image>(); // get image component of matSprite
                if (matSpriteImage != null)
                {
                    matSpriteImage.color = btnColor; // set color of matSprite to the color of the PairMaterial entry
                }

                // set corresponding decal button to active if decal has previously been selected
                if (decalIndex != MaterialSwitcher.Error404 && decalIndex == _currentDecButtons.IndexOf(button) - 1)
                {
                    button.GetComponent<Toggle>().isOn = true;
                }
            }
        }

        _currentColour = btnColor;
    }

    // Decal picker functionality ++++++++++++++++++++++++++++++++
    public GameObject decalButtonsContainer;
    public GameObject decalHueSlider;

    //public GameObject materialButtonPrefab; // store material button prefab
    public GameObject noneSelectedButtonPrefab;

    private List<TexturePairList> _availableDecals; // store current materials as returned from MaterialSwitcher script

    private readonly List<GameObject> _currentDecButtons = new List<GameObject>(); // store current material buttons

    private void PopulateDecalPicker() // populate the decal picker with the decals of the selected tag
    {
        // Create none decal button
        GameObject noneDecalButton = Instantiate(noneSelectedButtonPrefab, decalButtonsContainer.transform); // create new button from toggle prefab
        noneDecalButton.GetComponent<Toggle>().group = decalButtonsContainer.GetComponent<ToggleGroup>(); // set toggle group to toggle group of decalButtonsContainer
        noneDecalButton.GetComponent<Toggle>().onValueChanged.AddListener(isOn => OnDecButtonClicked(_currentPrefabTag, _partTag, MaterialSwitcher.Error404, isOn));
        _currentDecButtons?.Add(noneDecalButton); // add no decal button to list of current buttons

        // Reset decal hue slider & clear listeners
        decalHueSlider.GetComponent<Slider>().onValueChanged.RemoveAllListeners();
        decalHueSlider.SetActive(false); // hide decal hue slider
        decalHueSlider.GetComponent<Slider>().value = 0; // reset decal hue slider value

        var tmpList = materialSwitcher.GetDecals(_currentPrefabTag, _partTag, _partMatIndex); // get decals from MaterialSwitcher script
        _availableDecals = new List<TexturePairList>(tmpList); // get decals from MaterialSwitcher script & store them in a new list

        if (_availableDecals != null)
        {
            int previousDecal = materialSwitcher.GetCurrentDecal(_partTag, _partMatIndex); // get previous decal from MaterialSwitcher script

            Toggle toggleToActivate = null;

            // enable AllowSwitchOff for decalButtonsContainer
            ToggleGroup toggleGroup = decalButtonsContainer.GetComponent<ToggleGroup>();
            if (toggleGroup != null)
            {
                toggleGroup.allowSwitchOff = true;
            }

            for (int i = 0; i < _availableDecals.Count; i++)
            {
                GameObject newToggle = Instantiate(materialButtonPrefab, decalButtonsContainer.transform); // create new button from toggle prefab

                // all toggle component functionality
                Toggle toggleComponent = newToggle.GetComponent<Toggle>();
                if (toggleComponent != null)
                {
                    toggleComponent.group = decalButtonsContainer.GetComponent<ToggleGroup>(); // set toggle group to toggle group of decalButtonsContainer
                    var i1 = i;
                    toggleComponent.onValueChanged.RemoveAllListeners();
                    toggleComponent.onValueChanged.AddListener(isOn => OnDecButtonClicked(_currentPrefabTag, _partTag, i1, isOn));
                    // execute this function when toggle is clicked

                    if (previousDecal == i) // if previous decal is the same as the current decal
                    {
                        decalHueSlider.SetActive(true); // show decal hue slider
                        var hue = materialSwitcher.GetHue(_partTag, _partMatIndex, i);
                        decalHueSlider.GetComponent<Slider>().value = hue;

                        // Remove existing listeners and add a new one
                        decalHueSlider.GetComponent<Slider>().onValueChanged.RemoveAllListeners();
                        decalHueSlider.GetComponent<Slider>().onValueChanged.AddListener(value =>
                        {
                            int hueValue = Mathf.RoundToInt(value);
                            materialSwitcher.UpdateDecalHue(_partTag, _partMatIndex, i, hueValue);
                        });


                        toggleToActivate = toggleComponent; // store toggle component of current decal
                    }
                    else
                    {
                        toggleComponent.isOn = false; // set toggle to false
                    }
                }

                // Navigate to the matSprite Image component
                // assuming prefab structure is matToggle -> check -> matSprite
                Transform decSpriteObject = newToggle.transform.Find("check/decSprite"); // find matSprite (child of toggle button)
                if (decSpriteObject != null)
                {
                    // enable decal sprite object (starts disabled)
                    decSpriteObject.gameObject.SetActive(true);

                    // change decal sprite to the selected decal
                    Image matSpriteImage = decSpriteObject.GetComponent<Image>(); // get image component of matSprite
                    if (matSpriteImage != null && _availableDecals[i].uiSprite != null)
                    {
                        matSpriteImage.sprite = _availableDecals[i].uiSprite; // set sprite of matSprite to the sprite of the PairMaterial entry
                    }
                }

                Image matBgSpriteImage = newToggle.transform.Find("check/matSprite").GetComponent<Image>(); // get image component of matSprite
                if (matBgSpriteImage != null)
                {
                    matBgSpriteImage.color = _currentColour; // set color of matSprite to the color of the PairMaterial entry
                }

                _currentDecButtons?.Add(newToggle); // add new button to list of current buttons
            }

            ActivateToggle(toggleToActivate);

            if (toggleGroup != null)
            {
                toggleGroup.allowSwitchOff = false;
            }
        }
    }

    private void OnDecButtonClicked(string prefabTag, string partTag, int decalIndex, bool isOn)
    {
        if (isOn)
        {
            if (decalIndex == MaterialSwitcher.Error404) // if no decal button is clicked
            {
                decalHueSlider.SetActive(false); // hide decal hue slider
                // Debug.Log("Hid decal hue slider");
                // Debug.Log("'No decal' button clicked");
            }
            else
            {
                //clear listeners
                decalHueSlider.GetComponent<Slider>().onValueChanged.RemoveAllListeners();

                decalHueSlider.SetActive(true);
                var hue = materialSwitcher.GetHue(partTag, _partMatIndex, decalIndex);
                decalHueSlider.GetComponent<Slider>().value = hue;

                // Remove existing listeners and add a new one
                decalHueSlider.GetComponent<Slider>().onValueChanged.RemoveAllListeners();
                decalHueSlider.GetComponent<Slider>().onValueChanged.AddListener(value =>
                {
                    int hueValue = Mathf.RoundToInt(value);
                    materialSwitcher.UpdateDecalHue(partTag, _partMatIndex, decalIndex, hueValue);
                });
            }
            materialSwitcher.ChangeDecal(prefabTag, partTag, decalIndex, _partMatIndex); // change decal texture of selected model part
            // Debug.Log("Decal button of model part " + partTag + " clicked");
        }
    }

    private static void ActivateToggle(Toggle toggle)
    {
        if (toggle != null) // if the toggle to activate is either the first material or the previous material
        {
            toggle.isOn = true; // activate toggle of previous material
        }
    }
}