using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class WheelPrefabs // custom class to hold wheel prefabs
{
	public GameObject defaultWheelPrefab; // default wheel
	public GameObject longboardWheelPrefab; // longboard wheel
}

[Serializable]
public class DeckPrefabs // custom class to hold deck prefabs
{
	public GameObject classicDeckPrefab; // classic deck
	public GameObject longboardDeckPrefab; // longboard deck
	public GameObject roundtailDeckPrefab; // roundtail deck
	public GameObject oldschoolDeckPrefab; // oldschool deck
}


public class ModelSwitcher : MonoBehaviour // class to switch models
{
	[Tooltip("Collection of wheel prefabs")]
	public WheelPrefabs wheelPrefabs; // create instance of wheels collection class
	[Tooltip("Collection of deck prefabs")]
	public DeckPrefabs deckPrefabs; // create instance of decks collection class

	private GameObject _currentLoadedDeck; // store current loaded deck
	private GameObject[] _currentLoadedWheels; // store current loaded wheels

	[Tooltip("Array of anchors for classic and oldschool decks (should be 2)")]
	public GameObject[] classicOldschoolAnchors; // array of anchors for classic and oldschool decks
	[Tooltip("Array of anchors for longboard decks (should be 2)")]
	public GameObject[] longboardAnchors; // array of anchors for longboard decks
	[Tooltip("Array of anchors for roundtail decks (should be 2)")]
	public GameObject[] roundtailAnchors; // array of anchors for roundtail decks

	[Tooltip("Anchor for decks in combination with default wheels")]
	public GameObject defDeckAnchor; // anchor for decks in combination with default wheels
	[Tooltip("Anchor for decks in combination with longboard wheels")]
	public GameObject longDeckAnchor; // anchor for decks in combination with longboard wheels

	[Tooltip("Reflection probe to update when switching models")]
	public ReflectionProbe reflectionProbe; // reflection probe to update when switching models

	[Tooltip("Object that has the AOSwitcher script attached")]
	public AOSwitcher aoSwitcher; // create instance of AO switcher

	[Tooltip("Object that has the UI_Manager script attached")]
	public UIManager uiManager; // create instance of UI manager

	[Tooltip("Object that has the MaterialSwitcher script attached")]
	public MaterialSwitcher materialSwitcher; // create instance of material switcher

	[Tooltip("Anchor for camera when wheels are selected")]
	public GameObject wheelCameraAnchor; // anchor for camera when wheels are selected

	public void init_board() // when opening the app, load the default board
	{
		SetDeck(DeckDef);
		SetWheels(WheelDef);
	}

	// TODO: Share tags between all scripts
	private static readonly string[] WheelTags = new string[] { "axle_main", "axle_base", "bearing_cap", "wheel" };	// array of wheel tags
	private static readonly string[] DeckTags = new string[] { "board" };	// array of wheel tags
	private const string WheelDef = "wheel_def";
	private const string WheelLong = "wheel_long";
	private const string DeckDef = "deck_def";
	private const string DeckLong = "deck_long";
	private const string DeckRound = "deck_round";
	private const string DeckOld = "deck_old";

	public void SetWheels(string wheelType) { // function to set wheels
		if (_currentLoadedWheels != null) { // if wheels already exist, destroy them
			foreach (var wheel in _currentLoadedWheels) // destroy each wheel in array of wheels
			{
				if (wheel != null)
				{
					Destroy(wheel);
				}
			}
        }

		// since the wheel prefabs are stored in a collection class, we need to get the corresponding prefab from the collection
		GameObject wheelPrefab = null;

		switch (wheelType)
		{
			case WheelDef:
				wheelPrefab = wheelPrefabs.defaultWheelPrefab;
				uiManager.SetUIDefWheels(); // set world space UI position for deck ui to default wheels
				break;
			case WheelLong:
				wheelPrefab = wheelPrefabs.longboardWheelPrefab;
				uiManager.SetUILargeWheels(); // set world space UI position for deck ui to longboard wheels
				break;
		}

		// need to check what deck is currently loaded to determine the correct wheel anchors
		GameObject[] wheelAnchors = GetWheelAnchorsBasedOnDeck(); // get wheel anchors based on deck

		_currentLoadedWheels = new GameObject[wheelAnchors.Length]; // create array of wheels with length of wheel anchors (2) and assign it to current loaded wheels
		for (int i = 0; i < wheelAnchors.Length; i++) { // for each wheel anchor (2)
			_currentLoadedWheels[i] = Instantiate(wheelPrefab); // instantiate the corresponding wheel prefab
			_currentLoadedWheels[i].transform.SetParent(wheelAnchors[i].transform, false); // and set the wheel prefab as child of the corresponding wheel anchor

			// Add outline to wheels
			var wheelOutline = _currentLoadedWheels[i].AddComponent<Outline>();
			wheelOutline.enabled = false;
			wheelOutline.OutlineColor = new Color(1.0f, 1.0f, 1.0f);
			wheelOutline.OutlineWidth = 4.0f;
			wheelOutline.OutlineMode = Outline.Mode.OutlineVisible;

			// Add the script ObjectInteraction to the wheels, this script is used call functions when the wheels are clicked
			_currentLoadedWheels[i].AddComponent<ObjectInteraction>();
		}

		// update deck anchor to make sure the deck fits the selected wheels
		if (_currentLoadedDeck != null) {
			UpdateDeckAnchor();
			aoSwitcher.SetFloorAO(_currentLoadedWheels[0], _currentLoadedDeck);
		}
		if (reflectionProbe != null) {
			reflectionProbe.RenderProbe();
		}

		LoadMaterial(_currentLoadedWheels[0].tag, WheelTags);
	}

	public void SetDeck(string deckType)
	{
		// function to set deck
		if (_currentLoadedDeck != null)
		{
			Destroy(_currentLoadedDeck);
		}

		// since the wheel prefabs are stored in a collection class, we need to get the corresponding prefab from the collection
		GameObject deckPrefab = null;

		switch (deckType)
		{
			case DeckDef:
				deckPrefab = deckPrefabs.classicDeckPrefab;
				uiManager.SetUIDefDeck(); // set world space UI for wheels position to default deck
				break;
			case DeckLong:
				deckPrefab = deckPrefabs.longboardDeckPrefab;
				uiManager.SetUILongDeck(); // set world space UI for wheels position to long deck
				break;
			case DeckRound:
				deckPrefab = deckPrefabs.roundtailDeckPrefab;
				uiManager.SetUILongDeck(); // set world space UI for wheels position to long deck
				break;
			case DeckOld:
				deckPrefab = deckPrefabs.oldschoolDeckPrefab;
				uiManager.SetUIDefDeck(); // set world space UI for wheels position to default deck
				break;
		}

		// need to check what wheels are currently loaded to determine the correct deck anchor
		GameObject deckAnchor = GetDeckAnchorBasedOnWheels(); // get deck anchor based on wheels

		_currentLoadedDeck = Instantiate(deckPrefab, deckAnchor.transform, false); // instantiate the corresponding deck prefab

		// Add outline to deck
		var deckOutline = _currentLoadedDeck.AddComponent<Outline>();
		deckOutline.enabled = false;
		deckOutline.OutlineColor = new Color(1.0f, 1.0f, 1.0f);
		deckOutline.OutlineWidth = 4.0f;
		deckOutline.OutlineMode = Outline.Mode.OutlineVisible;

		// Add the script ObjectInteraction to the deck, this script is used call functions when the deck is clicked
		_currentLoadedDeck.AddComponent<ObjectInteraction>();

		// update wheel anchors to make sure the wheels fit the selected deck
		if (_currentLoadedWheels != null) {
			UpdateWheelAnchors();
			aoSwitcher.SetFloorAO(_currentLoadedWheels[0], _currentLoadedDeck);
		}
		if (reflectionProbe != null) {
			reflectionProbe.RenderProbe();
		}

		LoadMaterial(_currentLoadedDeck.tag, DeckTags);
	}

	private void UpdateWheelAnchors() {
		if (_currentLoadedDeck != null) { // only continue if deck is actually loaded
			GameObject[] wheelAnchors = GetWheelAnchorsBasedOnDeck(); // get correct / new wheel anchors based on deck
			for (int i = 0; i < _currentLoadedWheels.Length; i++) { // iterate through each wheel
				_currentLoadedWheels[i].transform.SetParent(wheelAnchors[i].transform, false); // set wheel as child of new wheel anchor
				if (_currentLoadedWheels[i].CompareTag("wheel_long")) // if longboard wheel, set wheel position slightly higher than default wheels
				{
					_currentLoadedWheels[i].transform.localPosition = new Vector3(0, 0.008481026f, 0); // longboard wheels are slightly higher than default wheels
				}
				else // if default wheel, set wheel position to origin of parent (new wheel anchor)
				{
					_currentLoadedWheels[i].transform.localPosition = Vector3.zero; // reset wheel position to origin of parent (new wheel anchor)
				}
			}

			wheelCameraAnchor.transform.localPosition = new Vector3(0f, wheelCameraAnchor.transform.localPosition.y, wheelAnchors[0].transform.localPosition.z); // update camera anchor position to match new wheel anchor
		}
	}

	private void UpdateDeckAnchor() {
		if (_currentLoadedWheels != null) { // only continue if wheels are actually loaded
			GameObject deckAnchor = GetDeckAnchorBasedOnWheels(); // get correct / new deck anchor based on wheelsq
			_currentLoadedDeck.transform.SetParent(deckAnchor.transform, false); // set deck as child of new deck anchor
			_currentLoadedDeck.transform.localPosition = Vector3.zero; // reset deck position to origin of parent (new deck anchor)
		}
	}

	// Helper methods to determine the correct anchors
	private GameObject[] GetWheelAnchorsBasedOnDeck() {
		GameObject[] wheelAnchors = classicOldschoolAnchors; // set default wheel anchors (for classic and oldschool decks)

		// replace default wheel anchors with longboard or roundtail wheel anchors if longboard or roundtail deck is selected
		if (_currentLoadedDeck != null)
		{
			if (_currentLoadedDeck.CompareTag("deck_long"))
			{
				wheelAnchors = longboardAnchors; // wheel anchors for longboard decks
			}
			else if (_currentLoadedDeck.CompareTag("deck_round"))
			{
				wheelAnchors = roundtailAnchors; // wheel anchors for roundtail decks
			}
		}

		return wheelAnchors; // return wheel anchors
	}

	private GameObject GetDeckAnchorBasedOnWheels() {
		GameObject deckAnchor = defDeckAnchor; // set default deck anchor (for default wheels)

		// replace default deck anchor with longboard deck anchor if longboard wheels are selected
		if (_currentLoadedWheels != null)
		{
			if (_currentLoadedWheels[0].CompareTag("wheel_long"))
			{
				deckAnchor = longDeckAnchor; // deck anchor for longboard wheels
			}
		}

		return deckAnchor; // return deck anchor
	}

	// Helper method to get current tags
	public String[] GetCurrentTags() {
		String[] tags = new String[2];
		tags[0] = _currentLoadedDeck.tag; // tag 0 is deck tag
		tags[1] = _currentLoadedWheels[0].tag; // tag 1 is wheel tag
		return tags;
	}

	public void SetDeckOutline(bool enable) {
		_currentLoadedDeck.GetComponent<Outline>().enabled = enable;
	}

	public void SetWheelOutline(bool enable) {
		foreach (var wheel in _currentLoadedWheels)
		{
			wheel.GetComponent<Outline>().enabled = enable;
		}
	}

	private void LoadMaterial(string currentPrefabTag, string[] currentModelTags)
	{
		if (currentPrefabTag == null) return;

		// this will throw an error on startup, because in the material switcher the Start() wasn't called yet -> tags the MaterialSwitcher uses are not yet initialized
		var modelPartOptions = materialSwitcher.GiveModelPartsOptions(currentPrefabTag); // get model part options for wheels

		if (modelPartOptions == null) return;

		// update material of modelTag) to match the selected material (if a material has already been selected)
		foreach (var modelTag in currentModelTags) // iterate through each modelTag
		{
			int objMatIndex = 0;

			// get material index
			foreach (var modelPart in modelPartOptions)
			{
				if (modelTag == modelPart.modelTag)
				{
					objMatIndex = modelPart.partMatIndex;

					// for every modelTag, check if a material has already been selected
					var materialIndex = materialSwitcher.GetCurrentMaterial(currentPrefabTag, modelTag, objMatIndex); // update material of mod to match the selected wheels
					if (materialIndex != 999) // if a material has already been selected
					{
						materialSwitcher.ChangeMaterial(currentPrefabTag, modelTag, materialIndex, objMatIndex); // update material of wheels (and bearings) to match the selected wheels
					}

					// for every modelTag, check if a decal has already been selected
					var decalIndex = materialSwitcher.GetCurrentDecal(modelTag, objMatIndex); // update decal of wheels (and bearings) to match the selected wheels
					// Debug.Log("decalIndex: " + decalIndex + " modelTag: " + modelTag + " objMatIndex: " + objMatIndex + " currentPrefabTag: " + currentPrefabTag + " currentModelTags: " + currentModelTags);
					if (decalIndex != 999) // if a decal has already been selected
					{
						StartCoroutine(ApplyDecalsAfterDelay(currentPrefabTag, modelTag, decalIndex, objMatIndex)); // decals need to be applied after a delay, otherwise they won't show up (materials are instantiated immediately, but not decals)
						//materialSwitcher.ChangeDecal(_currentLoadedWheels[0].tag, wheelTag, decalIndex); // update decal of wheels (and bearings) to match the selected wheels
					}
				}
			}
		}
	}

	// TODO: find a better way to apply decals after a delay
	IEnumerator ApplyDecalsAfterDelay(string prefabTag, string modelTag, int decalIndex, int objMatIndex)
	{
		yield return new WaitForEndOfFrame();
		//yield return new WaitForSeconds(10f);
		materialSwitcher.ChangeDecal(prefabTag, modelTag, decalIndex, objMatIndex);
	}
}
