using UnityEngine;

public class StartupSetup : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod]
    private static void OnRuntimeMethodLoad()
    {
        Debug.Log("Scene loaded, StartupSetup script executed.");

        // set skateboard to classic board when app starts
        var modelStartup = GameObject.Find("ModelSwitcherController"); // find the ModelSwitcherController game object
        var modelSwitcher = (ModelSwitcher) modelStartup.GetComponent(typeof(ModelSwitcher)); // get the ModelSwitcher script from the ModelSwitcherController game object
        modelSwitcher.init_board(); // call the init_board method from the ModelSwitcher script

        Shader.WarmupAllShaders();

        SharedTags.InitTags();
    }
}
