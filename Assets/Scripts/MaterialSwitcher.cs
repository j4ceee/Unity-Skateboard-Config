using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MaterialPairList
{
    public string uiName;
    public Material material;
    public Sprite uiSprite;
    public Color uiSpriteColor;
}

[System.Serializable]
public class TexturePairList
{
    public string uiName;
    public Texture2D texture;
    public Sprite uiSprite;
}

[System.Serializable]
public class TagDisplayNames
{
    public string modelTag;
    public int partMatIndex;
    public string uiName;
}

[System.Serializable]
public class CurrentSelection
{
    public string prefabTag;
    public string partTag;
    public int matTexIndex;
    public int objMatIndex;
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
        var mats = GetMaterials(prefabTag, modelTag, objMatIndex);

        var editedObjects = GameObject.FindGameObjectsWithTag(modelTag); // get all objects with the same tag

        if (mats == null || editedObjects == null) return; // if no materials or objects were found, abort

        // change material of all objects with the same tag
        foreach (var modelPart in editedObjects) // iterate through all objects with the same tag
        {
            // Debug.Log("Found " + modelTag + " with " + mats.Count + " decals.");

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

        // save current material
        foreach (var materialPair in _currentMaterials) // check if material is already in list
        {
            if (prefabTag == materialPair.prefabTag && modelTag == materialPair.partTag && materialPair.objMatIndex == objMatIndex) // if model tag and part tag were already assigned a material
            {
                materialPair.matTexIndex = matIndex; // update material to new material
                materialPair.objMatIndex = objMatIndex; // update objMatIndex to new objMatIndex
                break;
            }
        }
        _currentMaterials.Add(new CurrentSelection { prefabTag = prefabTag, partTag = modelTag, matTexIndex = matIndex, objMatIndex = objMatIndex}); // else add new material to list
    }

    public int GetCurrentMaterial(string prefabTag, string partTag, int objMatIndex)
    {
        foreach (var materialPair in _currentMaterials)
        {
            if (materialPair.prefabTag == prefabTag && materialPair.partTag == partTag && materialPair.objMatIndex == objMatIndex)
            {
                return materialPair.matTexIndex;
            }
            else if (partTag is Board or BearingCap && materialPair.partTag == partTag && materialPair.objMatIndex == objMatIndex)
            {
                return materialPair.matTexIndex;
            }
        }
        return Error404; // Error404 as error code for no material found
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
            // Debug.Log("Found " + modelTag + " with " + currDecalList.Count + " decals.");
            if (decalIndex != Error404 && decalIndex >= 0 && decalIndex < currDecalList.Count && currDecalList[decalIndex].texture)
            {
                Renderer modelRenderer = modelPart.GetComponent<Renderer>();
                Material[] objMaterials = modelRenderer.materials;

                if (objMatIndex >= 0 && objMatIndex < objMaterials.Length)
                {
                    objMaterials[objMatIndex].SetFloat(UseDecalTexture, 1f); // set bool UseDecalTexture to true

                    objMaterials[objMatIndex].SetTexture(DecalTexture, currDecalList[decalIndex].texture); // to the new decal texture
                    // Debug.Log("Set decal " + currDecalList[decalIndex].uiName + " for " + modelTag);
                }
            }
            else // set decal to none
            {
                modelPart.GetComponent<Renderer>().material.SetFloat(UseDecalTexture, 0f); // set bool UseDecalTexture to false
            }
        }
        // save current decal
        foreach (var texturePair in _currentDecals) // check if decal is already in list
        {
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
            if (decalPair.partTag == partTag && decalPair.objMatIndex == objMatIndex)
            {
                // Debug.Log("Found decal " + decalPair.matTexIndex + " for " + partTag);
                return decalPair.matTexIndex;
            }
        }
        return Error404; // Error404 as error code for no decal found
    }


    // deck materials are all ordered differently on the models (there are 3 unordered materials per model)
    // the relevant materials start with "01_" and "02_" for Grip and Bottom respectively
    // MaterialSwitcher returns the tags for the dropdown in UIManager, part of these returns is "partName" which is either "grip" or "deck"
    // depending on the partName, we need to find the corresponding material index of the material that starts with "01_" or "02_"
    /*
    public int GetDeckMaterialIndex(string prefabTag, string partTag, string partName)
    {
        var mats = GetMaterials(prefabTag, partTag);
        if (mats == null) return Error404; // Error404 as error code for no material found

        var matIndex = Error404; // Error404 as error code for no material found

        if (partName == "grip")
        {
            for (var i = 0; i < mats.Count; i++)
            {
                if (mats[i].material.name.StartsWith("01_"))
                {
                    matIndex = i;
                    break;
                }
            }
        }
        else if (partName == "deck")
        {
            for (var i = 0; i < mats.Count; i++)
            {
                if (mats[i].material.name.StartsWith("02_"))
                {
                    matIndex = i;
                    break;
                }
            }
        }

        return matIndex;
    }
    */
}
