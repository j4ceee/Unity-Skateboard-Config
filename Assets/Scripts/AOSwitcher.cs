using UnityEngine;

public class AOSwitcher : MonoBehaviour
{
    [Tooltip("The floor object to apply the AO to")]
    public GameObject floor;

    [Tooltip("AO Map for classic & oldschool decks with default wheels")]
    public Texture2D classicAoDef; // classic & oldschool share the same AO
    [Tooltip("AO Map for classic & oldschool decks with longboard wheels")]
    public Texture2D classicAoLong; // classic & oldschool share the same AO

    [Tooltip("AO Map for longboard decks with default wheels")]
    public Texture2D longboardAoDef;
    [Tooltip("AO Map for longboard decks with longboard wheels")]
    public Texture2D longboardAoLong;

    [Tooltip("AO Map for roundtail decks with default wheels")]
    public Texture2D roundtailAoDef;
    [Tooltip("AO Map for roundtail decks with longboard wheels")]
    public Texture2D roundtailAoLong;

    private static readonly int FloorAOMap = Shader.PropertyToID("_Floor_AO_Map");

    public void SetFloorAO(GameObject wheels, GameObject deck)
    {
        string wheelType = wheels.tag;
        string deckType = deck.tag;

        Texture2D selectedAO = null;

        if (wheelType == "wheel_def" && deckType is "deck_def" or "deck_old")
        {
            selectedAO = classicAoDef;
        } else if (wheelType == "wheel_long" && deckType is "deck_def" or "deck_old")
        {
            selectedAO = classicAoLong;
        } else if (wheelType == "wheel_def" && deckType == "deck_long")
        {
            selectedAO = longboardAoDef;
        } else if (wheelType == "wheel_long" && deckType == "deck_long")
        {
            selectedAO = longboardAoLong;
        } else if (wheelType == "wheel_def" && deckType == "deck_round")
        {
            selectedAO = roundtailAoDef;
        } else if (wheelType == "wheel_long" && deckType == "deck_round")
        {
            selectedAO = roundtailAoLong;
        }

        if (selectedAO != null)
        {
            floor.GetComponent<Renderer>().material.SetTexture(FloorAOMap, selectedAO);
        }
    }
}
