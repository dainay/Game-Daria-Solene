using System.Collections.Generic;
using UnityEngine;

public class LayerVisibilityManager : MonoBehaviour
{
    [SerializeField] private int redLayer;
    [SerializeField] private int orangeLayer;
    [SerializeField] private int blueLayer;
    [SerializeField] private int violetLayer;
    [SerializeField] private int greenLayer;

    //all hidden crystals will be here
    private List<GameObject> hiddenObjects = new List<GameObject>(); // Все скрытые объекты
    private int currentActiveLayer = -1; // Текущий активный слой (-1, если ничего не активно)


    private void Start()
    {
        // Находим все объекты с тегом "hidden" и сохраняем их
        hiddenObjects.AddRange(GameObject.FindGameObjectsWithTag("Hidden"));
         

        foreach (GameObject obj in hiddenObjects)
        {
            obj.SetActive(false);
        }

        //Debug.Log("All" + hiddenObjects.Count + "hidden objects are disabled at the start.");
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

        // Включаем объекты на новом слое
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
        // Включаем или выключаем только объекты на указанном слое
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
        // Проверяем, если текущий активный слой включён
        if (currentActiveLayer != -1)
        {
            // Выключаем все объекты на текущем активном слое
            ToggleObjectsOnLayer(currentActiveLayer, false);

            // Отключаем видимость слоя в камере
            Camera.main.cullingMask &= ~(1 << currentActiveLayer);

            currentActiveLayer = -1; // Сбрасываем текущий активный слой
        }

        Debug.Log("All layers are hidden.");
    }

}
