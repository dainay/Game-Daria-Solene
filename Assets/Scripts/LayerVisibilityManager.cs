using System.Collections.Generic;
using UnityEngine;
public class LayerVisibilityManager : MonoBehaviour
{
    [SerializeField] private int redLayer;
    [SerializeField] private int orangeLayer;
    [SerializeField] private int blueLayer;
    [SerializeField] private int violetLayer;
    [SerializeField] private int greenLayer;

    private List<GameObject> hiddenObjects = new List<GameObject>(); // all hidden objects
    private int currentActiveLayer = -1; // put -1 if we see nothing
    private void Start()
    {
        // find all hidden objects and disable them
        hiddenObjects.AddRange(GameObject.FindGameObjectsWithTag("Hidden"));

        foreach (GameObject obj in hiddenObjects)
        {
            obj.SetActive(false);
        }
    }
    public void SetLayerVisibility(string layerName)
    {
        Camera mainCamera = Camera.main; // Get the main camera

        int targetLayer = -1; //put -1 to have error if layer is not assigned

        if (layerName == "red") targetLayer = redLayer;
        else if (layerName == "orange") targetLayer = orangeLayer;
        else if (layerName == "blue") targetLayer = blueLayer;
        else if (layerName == "violet") targetLayer = violetLayer;
        else if (layerName == "green") targetLayer = greenLayer;

        if (targetLayer == -1)
        {
            Debug.LogWarning("Layer " + layerName + " is not found");
            return;
        }

        if (currentActiveLayer != -1)
        {
            ToggleObjectsOnLayer(currentActiveLayer, false);
        }

        ToggleObjectsOnLayer(targetLayer, true);
        currentActiveLayer = targetLayer;

        int[] magicLayers = { redLayer, orangeLayer, blueLayer, violetLayer, greenLayer };

        foreach (int layer in magicLayers) //check every layer and keep on only one that we targeted
        {
            if (layer == targetLayer)
            {
                mainCamera.cullingMask |= (1 << layer); 
            }
            else
            {
                mainCamera.cullingMask &= ~(1 << layer); // Disable other layers
            }
        }
        Debug.Log("Layer " + layerName + " is now visible, others are hidden.");
    }
    private void ToggleObjectsOnLayer(int layerIndex, bool enable)
    {
        // toggle all objects on the layer that we chose
        foreach (GameObject obj in hiddenObjects)
        {
            if (obj.layer == layerIndex)
            {
                obj.SetActive(enable);
            }
        }
    }
    public void HideAllLayers()
    {
        //check if we see something
        if (currentActiveLayer != -1)
        {
            // turn off all objects on the current layer
            ToggleObjectsOnLayer(currentActiveLayer, false);

            // Disable the current layer
            Camera.main.cullingMask &= ~(1 << currentActiveLayer);

            currentActiveLayer = -1; // removing the current layer from the variable
        }

        Debug.Log("All layers are hidden.");
    }

}
