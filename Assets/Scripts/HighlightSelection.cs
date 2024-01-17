using UnityEngine;

public class HightlightSelection : MonoBehaviour
{
    public ModelSwitcher modelSwitcher; // store ModelSwitcher script

    private bool _enableHighlighting = true; // enable or disable highlight

    private bool _deckHighlighting = true; // enable or disable deck highlight
    private bool _wheelHighlighting = true; // enable or disable wheel highlight

    private bool _isDeckSelected; // store if deck is selected
    private bool _isWheelsSelected; // store if wheels are selected

    public void SwitchHighlighting()
    {
        _enableHighlighting = !_enableHighlighting;
    }

    public void EnableDeckHighlighting(bool b)
    {
        _deckHighlighting = b;
    }

    public void EnableWheelHighlighting(bool b)
    {
        _wheelHighlighting = b;
    }

    // Update is called once per frame
    void Update()
    {
        if (_enableHighlighting)
        {
            if (Camera.main != null)
            {
                var ray = Camera.main.ScreenPointToRay(Input
                    .mousePosition); // cast ray from main camera to mouse position
                if (Physics.Raycast(ray, out var hit)) // if raycast hits something
                {
                    var selection = hit.transform; // store hit transform

                    string deckTag = modelSwitcher.GetCurrentTags()[0]; // get current deck tag
                    string wheelTag = modelSwitcher.GetCurrentTags()[1]; // get current wheels tag
                    if (selection.CompareTag(deckTag)) // if hit transform has same tag as current deck
                    {
                        if (!_isDeckSelected) // if deck is not selected yet
                        {
                            if (_deckHighlighting)
                            {
                                SetDeckOutline(true); // enable deck outline
                            }

                            if (_isWheelsSelected) // if wheels are selected
                            {
                                SetWheelOutline(false); // disable wheel outline
                            }
                        }
                    }
                    else if (selection.CompareTag(wheelTag)) // if hit transform has same tag as current wheels
                    {
                        if (!_isWheelsSelected) // if wheels are not selected yet
                        {
                            if (_wheelHighlighting)
                            {
                                SetWheelOutline(true);
                            }

                            if (_isDeckSelected) // if deck is selected
                            {
                                SetDeckOutline(false); // set deck outline to false
                            }
                        }
                    }
                    else
                    {
                        SetDeckOutline(false);
                        SetWheelOutline(false);
                    }
                }
                else
                {
                    SetDeckOutline(false);
                    SetWheelOutline(false);
                }
            }
        }
    }

    private void SetDeckOutline(bool b)
    {
        modelSwitcher.SetDeckOutline(b); // switch deck outline
        _isDeckSelected = b; // set deck selected to false
    }

    private void SetWheelOutline(bool b)
    {
        modelSwitcher.SetWheelOutline(b); // switch wheel outline
        _isWheelsSelected = b; // set wheels selected to false
    }
}