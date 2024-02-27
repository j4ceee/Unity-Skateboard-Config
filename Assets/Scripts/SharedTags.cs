using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class TagDisplayNames
{
    public string modelTag; // tag of the prefab
    public int partMatIndex; // index of the material in the list of materials for the model
    public string uiName; // name to display in UI
}

public class SharedTags : MonoBehaviour
{
    public static readonly List<TagDisplayNames> WheelModelParts = new(); // list of wheel tags and their corresponding UI names
    public static readonly List<TagDisplayNames> DeckModelParts = new(); // list of deck tags and their corresponding UI names

    // name of tags on the models
    public const string AxleMain = "axle_main"; // tag on axle main model inside wheel prefabs
    // public const string AxleBase = "axle_base"; // tag on axle base model inside wheel prefabs
    public const string BearingCap = "bearing_cap"; // tag on bearing cap model inside wheel prefabs
    public const string Wheel = "wheel"; // tag on wheel model inside wheel prefabs
    public const string Board = "board"; // tag on board model inside deck prefabs

    // model tags that have unique materials for each prefab
    public static readonly string[] UniqueMaterialTags = new string[] { Wheel }; // tags of models that have a unique material for each prefab (e.g. wheels)

    public const string DefWheelPrefTag = "wheel_def";
    public const string LongWheelPrefTag = "wheel_long";
    public const string DefDeckPrefTag = "deck_def";
    public const string LongDeckPrefTag = "deck_long";
    public const string RoundDeckPrefTag = "deck_round";
    public const string OldDeckPrefTag = "deck_old";

    // list of all wheel prefab tags
    public static readonly string[] WheelPrefTags = new string[] { DefWheelPrefTag, LongWheelPrefTag };

    // list of all deck prefab tags
    public static readonly string[] DeckPrefTags = new string[] { DefDeckPrefTag, LongDeckPrefTag, RoundDeckPrefTag, OldDeckPrefTag };


    // Start is called before the first frame update
    public static void InitTags()
    {
        //Debug.Log("SharedTags script executed.");
        // add tags and their corresponding UI names to the lists
        WheelModelParts.Add(new TagDisplayNames { modelTag = AxleMain, partMatIndex = 1, uiName = "Axle" });
        // WheelModelParts.Add(new TagDisplayNames { modelTag = AxleBase, uiName = "Axle Base" });
        WheelModelParts.Add(new TagDisplayNames { modelTag = BearingCap, partMatIndex = 0, uiName = "Bearings" });
        WheelModelParts.Add(new TagDisplayNames { modelTag = Wheel, partMatIndex = 0, uiName = "Wheels" });

        DeckModelParts.Add(new TagDisplayNames { modelTag = Board, partMatIndex = 2, uiName = "Grip" });
        DeckModelParts.Add(new TagDisplayNames { modelTag = Board, partMatIndex = 1, uiName = "Deck" });
    }
}
