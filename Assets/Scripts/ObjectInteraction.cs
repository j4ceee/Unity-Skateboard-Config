using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectInteraction : MonoBehaviour
{
    private UIManager _uiManager;
    private CameraManager _cameraManager;

    void Start()
    {
        // Find the UI_Manager in the scene
        _uiManager = FindObjectOfType<UIManager>();

        // Find the CameraManager in the scene
        _cameraManager = FindObjectOfType<CameraManager>();
    }

    void OnMouseUpAsButton()
    {
        // Call ShowUI with this object's tag when it is clicked
        if (_uiManager != null&& !EventSystem.current.IsPointerOverGameObject())
        {
            _uiManager.ShowUI(gameObject.tag);
            _uiManager.ShowMaterialEditorWindow(true);
            _uiManager.PopulateMaterialDropdown(gameObject.tag);
        }
        if (_cameraManager != null && !EventSystem.current.IsPointerOverGameObject())
        {
            _cameraManager.SwitchCamera(gameObject.tag);
        }
    }
}