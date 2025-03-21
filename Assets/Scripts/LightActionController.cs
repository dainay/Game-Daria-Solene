using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.HighDefinition.ProbeSettings;

public class LightActionController : MonoBehaviour
{
    [SerializeField] Animator m_Animator;
    [SerializeField] private GameObject m_MagicBall;

    private bool isLighting = false;
    private GameObject instantiatedLight;

    [SerializeField] private Transform handL; //attach left hand to make position of the ball

    //all attached materials to change color
    [SerializeField] private Material red;
    [SerializeField] private Material yellow;
    [SerializeField] private Material blue;
    [SerializeField] private Material violet;
    [SerializeField] private Material green;

    [SerializeField] private LayerVisibilityManager layerVisibilityManager;

    public enum MagicColor
    {
        Red,
        Yellow,
        Blue,
        Violet,
        Green
    }
    public enum MagicLayer
    {
        RedLayer,
        OrangeLayer,
        BlueLayer,
        VioletLayer,
        GreenLayer
    }

    //use Dictionary to keep all settings of materials and possible colors for the spehre
    private Dictionary<MagicColor, (Material material, Color color, MagicLayer layer)> magicSettings;

    private void Start()
    {
        magicSettings = new Dictionary<MagicColor, (Material, Color, MagicLayer)>
        {
            { MagicColor.Red, (red, Color.red, MagicLayer.RedLayer) },
            { MagicColor.Yellow, (yellow, Color.yellow, MagicLayer.OrangeLayer) },
            { MagicColor.Blue, (blue, Color.blue, MagicLayer.BlueLayer) },
            { MagicColor.Violet, (violet, new Color(0.5f, 0f, 1f), MagicLayer.VioletLayer) },
            { MagicColor.Green, (green, Color.green, MagicLayer.GreenLayer) }
        };
    }

    public void OnLight(InputValue value)
    {
        DoLight();
    }
    private void DoLight()
    {
        isLighting = !isLighting;
        m_Animator.SetBool("Lighting", isLighting);

        if (isLighting)
        {
            AddMagicBall();
        }
        else
        {
            Destroy(instantiatedLight);

            if (layerVisibilityManager != null)
            {
                layerVisibilityManager.HideAllLayers();
            }
        }
    }
    private void AddMagicBall() {

        if (handL == null)
        {
            Debug.LogError("Hand_L transform is attached in Unity");
            return;
        }

        Vector3 Position = handL.position + new Vector3(0f, 0.3f, 0f);

        instantiatedLight = Instantiate(m_MagicBall, Position, handL.rotation);
        instantiatedLight.transform.SetParent(handL);
    }

    private int Counter = 0;
     public void OnChangeLightColor(InputValue value)
    {
        ApplyMaterialFromManager();
    }

    //to handle enum names
    private bool TryGetMagicColor(string tag, out MagicColor magicColor)
    {
        return Enum.TryParse(tag.Replace("Magic", ""), true, out magicColor);
    }


    private void ApplyMaterialFromManager()
    {
        var collectedTags = MagicCollectionManager.GetCollectedTags();

        if (collectedTags.Count == 0 || !isLighting)
        {
            Debug.Log("No tags collected. No light");
            return;
        }

        if (Counter >= collectedTags.Count)
        {
            Counter = 0;
        }

        string tag = collectedTags[Counter];
        Counter++;

        Debug.Log("TAG " + tag);

        //check if we have settings for the tag in the dictionary
        if (TryGetMagicColor(tag, out MagicColor magicColor) && magicSettings.TryGetValue(magicColor, out var settings))
        {
            if (instantiatedLight != null)
            {
                //both things that we have to change, sphere material and light
                Renderer sphereRenderer = instantiatedLight.transform.Find("SphereGlow")?.GetComponent<Renderer>();
                Light pointLight = instantiatedLight.transform.Find("Point Light")?.GetComponent<Light>();

                if (sphereRenderer != null)
                {
                    sphereRenderer.material = settings.material;
                }
                else
                {
                    Debug.LogError("Sphere is not foudn");
                }

                if (pointLight != null)
                {
                    pointLight.color = settings.color;
                }
                else
                {
                    Debug.LogError("Light is not found");
                }

                Debug.Log($"Applied material for {magicColor}");
            }

            //show the layer that we recieve from the dictionary
            layerVisibilityManager?.SetLayerVisibility(settings.layer.ToString());
        }
        else
        {
            Debug.LogWarning($"No material found for {tag}");
        }
    }
}
