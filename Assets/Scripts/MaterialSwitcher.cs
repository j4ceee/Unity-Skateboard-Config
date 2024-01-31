using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MaterialPairList
{
    public string uiName; // name to display in UI
    public Material material; // material to apply to the model
    public Sprite uiSprite; // sprite to display in UI
    public Color uiSpriteColor; // color of the sprite in the UI
}

[System.Serializable]
public class TexturePairList
{
    public string uiName; // name to display in UI
    public Texture2D texture; // texture to apply to the material
    public Sprite uiSprite; // sprite to display in UI (colour not necessary, as the texture is already coloured / supposed to be greyscale)
}

[System.Serializable]
public class TagDisplayNames
{
    public string modelTag; // tag of the prefab
    public int partMatIndex; // index of the material in the list of materials for the model
    public string uiName; // name to display in UI
}

[System.Serializable]
public class CurrentSelection
{
    public string prefabTag; // tag of the prefab
    public string partTag; // tag of the model part
    public int matTexIndex; // index of the material / texture in the list of possible materials / textures to choose from
    public int objMatIndex; // index of the material or texture in the materials or textures list of the model (material slot)
}

[System.Serializable]
public class CurrentHue
{
    public string partTag; // tag of the model part
    public int matTexIndex; // same as CurrentSelection.matTexIndex
    public int objMatIndex; // same as CurrentSelection.objMatIndex
    public int hueIndex; // int value of the hue (should be between 0 and 360)
}

public class MaterialSwitcher : MonoBehaviour
{
    public ModelSwitcher modelSwitcher;
    public UIManager uiManager;

    // Materials & Decals Lists for Default Wheels--------------------------------------------------
    [Tooltip("Put all materials for the default wheels here. First will be the default material, others will be material variants. Supported: 8")]
    public List<MaterialPairList> wheelDefaultMaterials = new List<MaterialPairList>(); // get all materials for default wheels
    [Tooltip("Put all decal textures for the default wheels here in the same order as for longboard wheels.")]
    public List<TexturePairList> wheelDefaultDecals = new List<TexturePairList>(); // get all decals for default wheels

    // Materials & Decals Lists for Longboard Wheels-------------------------------------------------
    [Tooltip("Put all materials for the longboard wheels here. First will be the default material, others will be material variants. Supported: 8")]
    public List<MaterialPairList> wheelLongMaterials = new List<MaterialPairList>(); // get all materials for longboard wheels
    [Tooltip("Put all decal textures for the longboard wheels here in the same order as for default wheels.")]
    public List<TexturePairList> wheelLongDecals = new List<TexturePairList>(); // get all decals for longboard wheels

    // Materials & Decals Lists for Default Bearings-------------------------------------------------
    [Tooltip("Put all materials for the default bearings here. First will be the default material, others will be material variants. Supported: 8")]
    public List<MaterialPairList> bearingDefaultMaterials = new List<MaterialPairList>(); // get all materials for default bearings
    [Tooltip("Put all decal textures for the default bearings here in the same order as for longboard bearings.")]
    public List<TexturePairList> bearingDefaultDecals = new List<TexturePairList>(); // get all decals for default bearings

    // Materials & Decals Lists for Longboard Bearings-----------------------------------------------
    [Tooltip("Put all materials for the longboard bearings here. First will be the default material, others will be material variants. Supported: 8")]
    public List<MaterialPairList> bearingLongMaterials = new List<MaterialPairList>(); // get all materials for longboard bearings
    [Tooltip("Put all decal textures for the longboard bearings here in the same order as for default bearings.")]
    public List<TexturePairList> bearingLongDecals = new List<TexturePairList>(); // get all decals for longboard bearings

    // Materials & Decals Lists for Classic Deck----------------------------------------------------
    [Tooltip("Put all materials for the classic deck grip here. First will be the default material, others will be material variants. Supported: 8")]
    public List<MaterialPairList> classicDeckGripMaterials = new List<MaterialPairList>(); // get all materials for classic deck
    [Tooltip("Put all decal textures for the classic deck grip here in the same order as for others decks.")]
    public List<TexturePairList> classicDeckGripDecals = new List<TexturePairList>(); // get all decals for classic deck

    [Tooltip("Put all materials for the classic deck bottom here. First will be the default material, others will be material variants. Supported: 8")]
    public List<MaterialPairList> classicDeckBottomMaterials = new List<MaterialPairList>(); // get all materials for classic deck
    [Tooltip("Put all decal textures for the classic deck bottom here in the same order as for others decks.")]
    public List<TexturePairList> classicDeckBottomDecals = new List<TexturePairList>(); // get all decals for classic deck

    // Materials & Decals Lists for Longboard Deck----------------------------------------------------
    [Tooltip("Put all materials for the longboard deck grip here. First will be the default material, others will be material variants. Supported: 8")]
    public List<MaterialPairList> longboardDeckGripMaterials = new List<MaterialPairList>(); // get all materials for longboard deck
    [Tooltip("Put all decal textures for the longboard deck grip here in the same order as for others decks.")]
    public List<TexturePairList> longboardDeckGripDecals = new List<TexturePairList>(); // get all decals for longboard deck

    [Tooltip("Put all materials for the longboard deck bottom here. First will be the default material, others will be material variants. Supported: 8")]
    public List<MaterialPairList> longboardDeckBottomMaterials = new List<MaterialPairList>(); // get all materials for longboard deck
    [Tooltip("Put all decal textures for the longboard deck bottom here in the same order as for others decks.")]
    public List<TexturePairList> longboardDeckBottomDecals = new List<TexturePairList>(); // get all decals for longboard deck

    // Materials & Decals Lists for Roundtail Deck----------------------------------------------------
    [Tooltip("Put all materials for the roundtail deck grip here. First will be the default material, others will be material variants. Supported: 8")]
    public List<MaterialPairList> roundtailDeckGripMaterials = new List<MaterialPairList>(); // get all materials for roundtail deck
    [Tooltip("Put all decal textures for the roundtail deck grip here in the same order as for others decks.")]
    public List<TexturePairList> roundtailDeckGripDecals = new List<TexturePairList>(); // get all decals for roundtail deck

    [Tooltip("Put all materials for the roundtail deck bottom here. First will be the default material, others will be material variants. Supported: 8")]
    public List<MaterialPairList> roundtailDeckBottomMaterials = new List<MaterialPairList>(); // get all materials for roundtail deck
    [Tooltip("Put all decal textures for the roundtail deck bottom here in the same order as for others decks.")]
    public List<TexturePairList> roundtailDeckBottomDecals = new List<TexturePairList>(); // get all decals for roundtail deck

    // Materials & Decals Lists for Oldschool Deck----------------------------------------------------
    [Tooltip("Put all materials for the oldschool deck grip here. First will be the default material, others will be material variants. Supported: 8")]
    public List<MaterialPairList> oldschoolDeckGripMaterials = new List<MaterialPairList>(); // get all materials for oldschool deck
    [Tooltip("Put all decal textures for the oldschool deck grip here in the same order as for others decks.")]
    public List<TexturePairList> oldschoolDeckGripDecals = new List<TexturePairList>(); // get all decals for oldschool deck

    [Tooltip("Put all materials for the oldschool deck bottom here. First will be the default material, others will be material variants. Supported: 8")]
    public List<MaterialPairList> oldschoolDeckBottomMaterials = new List<MaterialPairList>(); // get all materials for oldschool deck
    [Tooltip("Put all decal textures for the oldschool deck bottom here in the same order as for others decks.")]
    public List<TexturePairList> oldschoolDeckBottomDecals = new List<TexturePairList>(); // get all decals for oldschool deck

    private readonly List<TagDisplayNames> _wheelTags = new List<TagDisplayNames>(); // list of wheel tags and their corresponding UI names
    private readonly List<TagDisplayNames> _deckDefTags = new List<TagDisplayNames>(); // list of deck tags and their corresponding UI names
    private readonly List<TagDisplayNames> _deckLongTags = new List<TagDisplayNames>();
    private readonly List<TagDisplayNames> _deckRoundTags = new List<TagDisplayNames>();
    private readonly List<TagDisplayNames> _deckOldTags = new List<TagDisplayNames>();

    private const string AxleMain = "axle_main";
    // private const string AxleBase = "axle_base";
    private const string BearingCap = "bearing_cap";
    private const string Wheel = "wheel";

    private const string Board = "board";

    private static readonly string[] UniqueMaterialTags = new string[] { Wheel }; // tags of prefabs that have a unique material for each prefab (e.g. wheels)

    private string _defWheelTag;
    private string _longWheelTag;
    private string _defDeckTag;
    private string _longDeckTag;
    private string _roundDeckTag;
    private string _oldDeckTag;

    public const int Error404 = 999;


    // Start is called before the first frame update
    private void Start()
    {
        _wheelTags.Add(new TagDisplayNames { modelTag = AxleMain, partMatIndex = 1, uiName = "Axle" });
        // _wheelTags.Add(new TagDisplayNames { modelTag = AxleBase, uiName = "Axle Base" });
        _wheelTags.Add(new TagDisplayNames { modelTag = BearingCap, partMatIndex = 0, uiName = "Bearings" });
        _wheelTags.Add(new TagDisplayNames { modelTag = Wheel, partMatIndex = 0, uiName = "Wheels" });


        _deckDefTags.Add(new TagDisplayNames { modelTag = Board, partMatIndex = 2, uiName = "Grip" });
        _deckDefTags.Add(new TagDisplayNames { modelTag = Board, partMatIndex = 1, uiName = "Deck" });

        _deckLongTags.Add(new TagDisplayNames { modelTag = Board, partMatIndex = 2, uiName = "Grip" });
        _deckLongTags.Add(new TagDisplayNames { modelTag = Board, partMatIndex = 1, uiName = "Deck" });

        _deckRoundTags.Add(new TagDisplayNames { modelTag = Board, partMatIndex = 2, uiName = "Grip" });
        _deckRoundTags.Add(new TagDisplayNames { modelTag = Board, partMatIndex = 1, uiName = "Deck" });

        _deckOldTags.Add(new TagDisplayNames { modelTag = Board, partMatIndex = 2, uiName = "Grip" });
        _deckOldTags.Add(new TagDisplayNames { modelTag = Board, partMatIndex = 1, uiName = "Deck" });

        _defWheelTag = modelSwitcher.wheelPrefabs.defaultWheelPrefab.tag;
        _longWheelTag = modelSwitcher.wheelPrefabs.longboardWheelPrefab.tag;
        _defDeckTag = modelSwitcher.deckPrefabs.classicDeckPrefab.tag;
        _longDeckTag = modelSwitcher.deckPrefabs.longboardDeckPrefab.tag;
        _roundDeckTag = modelSwitcher.deckPrefabs.roundtailDeckPrefab.tag;
        _oldDeckTag = modelSwitcher.deckPrefabs.oldschoolDeckPrefab.tag;
    }

    /*
     * UI Functionality
     */
    public IEnumerable<TagDisplayNames> GiveModelPartsOptions(string currPrefabTag)
    {
        // Debug.Log("" + _defDeckTag);
        // TODO: check each prefab tag initialized in Start() for null (when calling GiveModelPartsOptions() immediately after startup, Start() has not been called yet)

        var selectionCol = _deckDefTags;

        // compare current prefab tag to all possible prefab tags
        if (currPrefabTag == _defDeckTag)
        {
            // do nothing, _deckDefTags is selected by default
        }
        else if (currPrefabTag == _longDeckTag)
        {
            selectionCol = _deckLongTags;
        }
        else if (currPrefabTag == _roundDeckTag)
        {
            selectionCol = _deckRoundTags;
        }
        else if (currPrefabTag == _oldDeckTag)
        {
            selectionCol = _deckOldTags;
        }
        else if (currPrefabTag == _defWheelTag || currPrefabTag == _longWheelTag)
        {
            selectionCol = _wheelTags;
        }
        else
        {
            Debug.LogError("Invalid prefab tag given to MaterialSwitcher.");
        }

        return selectionCol;
    }

    public List<MaterialPairList> GetMaterials(string prefabTag, string modelTag, int objMatIndex) // gives the materials for the selected model part
    {
        List<MaterialPairList> matList = null;
        // Debug.Log("Getting materials for " + modelTag + " at material slot " + objMatIndex);

        switch (modelTag)
        {
            case AxleMain:
                break; // TODO: add axle materials
            case BearingCap:
                if (prefabTag == _defWheelTag)
                {
                    matList = bearingDefaultMaterials;
                }
                else if (prefabTag == _longWheelTag)
                {
                    matList = bearingLongMaterials;
                }
                break;
            case Wheel:
                if (prefabTag == _defWheelTag)
                {
                    matList = wheelDefaultMaterials;
                }
                else if (prefabTag == _longWheelTag)
                {
                    matList = wheelLongMaterials;
                }
                break;
            case Board:
                if (prefabTag == _defDeckTag)
                {
                    if (objMatIndex == _deckDefTags[0].partMatIndex)
                    {
                        matList = classicDeckGripMaterials;
                    }
                    else if (objMatIndex == _deckDefTags[1].partMatIndex)
                    {
                        matList = classicDeckBottomMaterials;
                    }
                }
                else if (prefabTag == _longDeckTag)
                {
                    if (objMatIndex == _deckLongTags[0].partMatIndex)
                    {
                        matList = longboardDeckGripMaterials;
                        // Debug.Log("Found longboard deck grip materials.");
                    }
                    else if (objMatIndex == _deckLongTags[1].partMatIndex)
                    {
                        matList = longboardDeckBottomMaterials;
                        // Debug.Log("Found longboard deck bottom materials.");
                    }
                }
                else if (prefabTag == _roundDeckTag)
                {
                    if (objMatIndex == _deckRoundTags[0].partMatIndex)
                    {
                        matList = roundtailDeckGripMaterials;
                    }
                    else if (objMatIndex == _deckRoundTags[1].partMatIndex)
                    {
                        matList = roundtailDeckBottomMaterials;
                    }
                }
                else if (prefabTag == _oldDeckTag)
                {
                    if (objMatIndex == _deckOldTags[0].partMatIndex)
                    {
                        matList = oldschoolDeckGripMaterials;
                    }
                    else if (objMatIndex == _deckOldTags[1].partMatIndex)
                    {
                        matList = oldschoolDeckBottomMaterials;
                    }
                }
                break;
        }
        if (matList == null)
        {
            Debug.LogError("No materials found for " + modelTag + " at material slot " + objMatIndex);
        }

        return matList;
    }

    // Store all current materials
    private readonly List<CurrentSelection> _currentMaterials = new List<CurrentSelection>();
    private static readonly int UseDecalTexture = Shader.PropertyToID("_Use_Decal_Texture");
    private static readonly int DecalTexture = Shader.PropertyToID("_Decal_Texture");

    // when an object has multiple materials, they get stored in an array of materials (Renderer.materials)
    // we need to know which material to change = objMatIndex
    public void ChangeMaterial(string prefabTag, string modelTag, int matIndex, int objMatIndex) // changes the material of the selected model part
    {
        //debug_currentMaterials();

        var mats = GetMaterials(prefabTag, modelTag, objMatIndex);

        var editedObjects = GameObject.FindGameObjectsWithTag(modelTag); // get all objects with the same tag

        if (mats == null || editedObjects == null) return; // if no materials or objects were found, abort

        // change material of all objects with the same tag
        foreach (var modelPart in editedObjects) // iterate through all objects with the same tag
        {
            // Debug.Log("Found " + modelTag + " with " + mats.Count + " materials.");

            Renderer modelRenderer = modelPart.GetComponent<Renderer>();
            Material[] objMaterials = modelRenderer.materials;

            if (objMatIndex >= 0 && objMatIndex < objMaterials.Length && matIndex >= 0 && matIndex < mats.Count)
            {
                objMaterials[objMatIndex] = mats[matIndex].material; // change material of selected object to new material
                modelRenderer.materials = objMaterials; // Reassign the modified materials array
            }

            // set decal to previously selected decal (if any)
            var currDecalIndex = GetCurrentDecal(modelTag, objMatIndex);
            ChangeDecal(prefabTag, modelTag, currDecalIndex, objMatIndex);
        }

        // Debug.Log("Change Material - Model Tag: " + modelTag + " / Prefab Tag: " + prefabTag + " / Material slot: " + objMatIndex + " / Material Index: " + matIndex + ".");
        // save current material
        bool unique = false;

        foreach (var uniqueTag in UniqueMaterialTags) // check if material is unique across different prefabs
        {
            if (modelTag == uniqueTag)
            {
                unique = true;
            }
        }

        bool found = false;

        foreach (var materialPair in _currentMaterials) // check if material is already in list
        {
            // materials of wheel models should be unique across different prefabs
            if (unique && materialPair.prefabTag == prefabTag && materialPair.partTag == modelTag && materialPair.objMatIndex == objMatIndex) // check for prefab tag, part tag and material slot
            {
                //Debug.Log("Updated wheel material for " + modelTag + " from material " + materialPair.matTexIndex + " to " + matIndex + ".");
                materialPair.matTexIndex = matIndex; // update material to new material
                // materialPair.objMatIndex = objMatIndex; // update objMatIndex to new objMatIndex

                //debug_currentMaterials();
                found = true;
                break;
            }

            // materials of everything else should be consistent even across different prefabs
            if (!unique && materialPair.partTag == modelTag && materialPair.objMatIndex == objMatIndex) // only check for part tag and material slot
            {
                //Debug.Log("Updated material for " + modelTag + " from material " + materialPair.matTexIndex + " to " + matIndex + ".");
                materialPair.matTexIndex = matIndex; // update material to new material

                //debug_currentMaterials();
                found = true;
                break;
            }
        }

        if (found) return;

        // if material is not in list yet, add it
        // Debug.Log("Added material "+ matIndex + " for " + modelTag + " at material slot " + objMatIndex + ".");
        _currentMaterials.Add(new CurrentSelection { prefabTag = prefabTag, partTag = modelTag, matTexIndex = matIndex, objMatIndex = objMatIndex}); // else add new material to list
        //debug_currentMaterials();
    }

    public int GetCurrentMaterial(string prefabTag, string partTag, int objMatIndex)
    {
        // Debug.Log("Change Material - Model Tag: " + partTag + " / Prefab Tag: " + prefabTag + " / Material slot: " + objMatIndex + ".");
        bool unique = false;

        foreach (var uniqueTag in UniqueMaterialTags) // check if material is unique across different prefabs
        {
            if (partTag == uniqueTag)
            {
                unique = true;
            }
        }

        bool found = false;
        int tmpMatIndex = 0;

        foreach (var materialPair in _currentMaterials)
        {
            // materials of wheel models should be unique across different prefabs
            if (unique && materialPair.prefabTag == prefabTag && materialPair.partTag == partTag &&
                materialPair.objMatIndex == objMatIndex) // check for prefab tag, part tag and material slot
            {
                // Debug.Log("Found wheel material " + materialPair.matTexIndex + " for " + partTag + " at material slot " + objMatIndex + ".");

                tmpMatIndex = materialPair.matTexIndex;
                found = true;
                break;
            }

            else if (!unique && materialPair.partTag == partTag && materialPair.objMatIndex == objMatIndex) // only check for part tag and material slot
            {
                // Debug.Log("Found material " + materialPair.matTexIndex + " for " + partTag + " at material slot " + objMatIndex + ".");

                tmpMatIndex = materialPair.matTexIndex;
                found = true;
                break;
            }
        }

        return found ? tmpMatIndex : Error404; // Error404 as error code for no material found
    }

    private void debug_currentMaterials()
    {
        Debug.Log("++++++++++++++++++++ Current Materials: ++++++++++++++++++++\u2557");
        foreach (var materialPair in _currentMaterials)
        {
            Debug.Log("-- Prefab Tag: " + materialPair.prefabTag + " / Part Tag: " + materialPair.partTag + " / Material Index: " + materialPair.matTexIndex + " / Material Slot: " + materialPair.objMatIndex + ".");
        }
        Debug.Log("+++++++++++++++++++++++++++++++++++++++++++++++++++++\u255d");
    }


    /*
     * Decal Functionality #######################################################
     */
    public List<TexturePairList> GetDecals(string prefabTag, string modelTag, int objMatIndex) // gives the materials for the selected model part
    {
        List<TexturePairList> decalList = null;

        switch (modelTag)
        {
            case AxleMain:
                break; // TODO: add axle materials
            case BearingCap:
                if (prefabTag == _defWheelTag)
                {
                    decalList = bearingDefaultDecals;
                }
                else if (prefabTag == _longWheelTag)
                {
                    decalList = bearingLongDecals;
                }
                break;
            case Wheel:
                if (prefabTag == _defWheelTag)
                {
                    decalList = wheelDefaultDecals;
                }
                else if (prefabTag == _longWheelTag)
                {
                    decalList = wheelLongDecals;
                }
                break;
            case Board:
                if (prefabTag == _defDeckTag)
                {
                    if (objMatIndex == _deckDefTags[0].partMatIndex)
                    {
                        decalList = classicDeckGripDecals;
                    }
                    else if (objMatIndex == _deckDefTags[1].partMatIndex)
                    {
                        decalList = classicDeckBottomDecals;
                    }
                }
                else if (prefabTag == _longDeckTag)
                {
                    if (objMatIndex == _deckLongTags[0].partMatIndex)
                    {
                        decalList = longboardDeckGripDecals;
                    }
                    else if (objMatIndex == _deckLongTags[1].partMatIndex)
                    {
                        decalList = longboardDeckBottomDecals;
                    }
                }
                else if (prefabTag == _roundDeckTag)
                {
                    if (objMatIndex == _deckRoundTags[0].partMatIndex)
                    {
                        decalList = roundtailDeckGripDecals;
                    }
                    else if (objMatIndex == _deckRoundTags[1].partMatIndex)
                    {
                        decalList = roundtailDeckBottomDecals;
                    }
                }
                else if (prefabTag == _oldDeckTag)
                {
                    if (objMatIndex == _deckOldTags[0].partMatIndex)
                    {
                        decalList = oldschoolDeckGripDecals;
                    }
                    else if (objMatIndex == _deckOldTags[1].partMatIndex)
                    {
                        decalList = oldschoolDeckBottomDecals;
                    }
                }
                break;
        }
        if (decalList == null)
        {
            Debug.LogError("No decals found for " + modelTag + " at material slot " + objMatIndex);
        }

        return decalList;
    }

    // Store all current decals
    private readonly List<CurrentSelection> _currentDecals = new List<CurrentSelection>();


    public void ChangeDecal(string prefabTag, string modelTag, int decalIndex, int objMatIndex) // changes the material of the selected model part
    {
        var currDecalList = GetDecals(prefabTag, modelTag, objMatIndex);

        var editedObjects = GameObject.FindGameObjectsWithTag(modelTag);

        if (currDecalList == null || editedObjects == null) return; // if no decals or objects were found, abort

        // change decal of all objects with the same tag
        foreach (var modelPart in editedObjects)
        {
            Renderer modelRenderer = modelPart.GetComponent<Renderer>();
            Material[] objMaterials = modelRenderer.materials;

            // Debug.Log("Found " + modelTag + " with " + currDecalList.Count + " decals.");
            if (decalIndex != Error404 && decalIndex >= 0 && decalIndex < currDecalList.Count && currDecalList[decalIndex].texture)
            {
                if (objMatIndex >= 0 && objMatIndex < objMaterials.Length)
                {
                    objMaterials[objMatIndex].SetFloat(UseDecalTexture, 1f); // set bool UseDecalTexture to true

                    objMaterials[objMatIndex].SetTexture(DecalTexture, currDecalList[decalIndex].texture); // to the new decal texture
                    // Debug.Log("Set decal " + currDecalList[decalIndex].uiName + " for " + modelTag);

                    // Update the hue of the decal
                    var hueValue = GetHue(modelTag, objMatIndex, decalIndex);
                    objMaterials[objMatIndex].SetInt(DecalHue, hueValue);
                }
            }
            else // set decal to none
            {
                objMaterials[objMatIndex].SetFloat(UseDecalTexture, 0f); // set bool UseDecalTexture to false
            }
        }
        // save current decal
        foreach (var texturePair in _currentDecals) // check if decal is already in list
        {
            // all decals should be shared across different prefabs -> only check for part tag and material slot (not prefab tag)
            if (modelTag == texturePair.partTag && objMatIndex == texturePair.objMatIndex) // if part tag was already assigned a decal
            {
                texturePair.matTexIndex = decalIndex; // update decal to new material
                texturePair.objMatIndex = objMatIndex; // update objMatIndex to new objMatIndex
                return;
            }
        }
        _currentDecals.Add(new CurrentSelection { prefabTag = prefabTag, partTag = modelTag, matTexIndex = decalIndex, objMatIndex = objMatIndex}); // else add new material to list
    }


    public int GetCurrentDecal(string partTag, int objMatIndex)
    {
        foreach (var decalPair in _currentDecals)
        {
            // all decals should be shared across different prefabs
            if (decalPair.partTag == partTag && decalPair.objMatIndex == objMatIndex)
            {
                // Debug.Log("Found decal " + decalPair.matTexIndex + " for " + partTag);
                return decalPair.matTexIndex;
            }
        }
        return Error404; // Error404 as error code for no decal found
    }


        // list of all current decal hues
    private readonly List<CurrentHue> _currentHues = new List<CurrentHue>();
    private static readonly int DecalHue = Shader.PropertyToID("_Decal_Hue");

    // change the hue of the decal
    public void UpdateDecalHue(string partTag, int objMatIndex, int decalIndex, int hueValue)
    {
        // Find the corresponding decal
        var editedObjects = GameObject.FindGameObjectsWithTag(partTag);
        foreach (var modelPart in editedObjects)
        {
            Renderer modelRenderer = modelPart.GetComponent<Renderer>();
            Material[] objMaterials = modelRenderer.materials;

            // Check if the decal exists
            if (objMaterials[objMatIndex].GetFloat(UseDecalTexture) == 1f)
            {
                // Update the hue of the decal
                objMaterials[objMatIndex].SetInt(DecalHue, hueValue);
            }
        }

        // Update the hue in the _currentHues list
        UpdateHueList(partTag, objMatIndex, decalIndex, hueValue);
    }

    // Update the _currentHues list
    public void UpdateHueList(string partTag, int objMatIndex, int decalIndex, int hueValue = 0)
    {
        // find the current hue in the list
        CurrentHue currentHue = _currentHues.Find(hue => hue.partTag == partTag && hue.objMatIndex == objMatIndex && hue.matTexIndex == decalIndex);
        if (currentHue == null)
        {
            currentHue = new CurrentHue { partTag = partTag, objMatIndex = objMatIndex, matTexIndex = decalIndex, hueIndex = hueValue };
            _currentHues.Add(currentHue);
        }
        else
        {
            currentHue.hueIndex = hueValue;
        }
    }

    public int GetHue(string partTag, int objMatIndex, int decalIndex)
    {
        // find the current hue in the list
        var currentHue = _currentHues.Find(hue => hue.partTag == partTag && hue.objMatIndex == objMatIndex && hue.matTexIndex == decalIndex);
        var hueValue = currentHue?.hueIndex ?? 0; // Use default hue value if CurrentHue object does not exist

        return hueValue;
    }
}
