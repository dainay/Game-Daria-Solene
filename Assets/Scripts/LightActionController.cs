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
            StartCoroutine("AddMagicBall", 1000f);
        }
        else
        {
            Destroy(instantiatedLight);
           
            if (layerVisibilityManager != null)
            {
                layerVisibilityManager.HideAllLayers(); //hide current layer with hidden crystals 
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
    private void Update()
    {
        // Check for 'K' key press
        if (Input.GetKeyDown(KeyCode.K))
        {
            ApplyMaterialFromManager();
        }
    }
    private void ApplyMaterialFromManager()
    {
        // Get all collected tags
        var collectedTags = MagicCollectionManager.GetCollectedTags();

        // If no tags are collected, do nothing
        if (collectedTags.Count == 0 || isLighting == false)
        {
            Debug.Log("No tags collected. No light");
            return;
        }

        if (Counter >= collectedTags.Count)
        {
            Counter = 0;
        }

        string Tag = collectedTags[Counter];
        Debug.Log("TAG" + Tag);

        Material materialToApply = null;
        Color lightColor = Color.white;
        string layerName = null;

        switch (Tag)
        {
            case "MagicRed":
                materialToApply = red;
                lightColor = Color.red;
                layerName = "red";
                break;
            case "MagicYellow":
                materialToApply = yellow;
                lightColor = Color.yellow;
                layerName = "orange";
                break;
            case "MagicBlue":
                materialToApply = blue;
                lightColor = Color.blue;
                layerName = "blue";
                break;
            case "MagicViolet":
                materialToApply = violet;
                lightColor = new Color(0.5f, 0f, 1f); // Purple
                layerName = "violet";
                break;
            case "MagicGreen":
                materialToApply = green;
                lightColor = Color.green;
                layerName = "green";
                break;
            default:
                lightColor = Color.white;
                Debug.LogWarning($"No material found for tag: {Tag}");
                return;
        }

        // Apply the material directly to SphereGlow
        if (materialToApply != null && instantiatedLight != null)
        {

            instantiatedLight.transform.Find("SphereGlow").GetComponent<Renderer>().material = materialToApply;
            instantiatedLight.transform.Find("Point Light").GetComponent<Light>().color = lightColor;
            Debug.Log($"Applied material for tag: {Tag} to SphereGlow.");
        }

         if (!string.IsNullOrEmpty(layerName) && layerVisibilityManager != null)
        {
            layerVisibilityManager.SetLayerVisibility(layerName);
        }
        Counter++;
        Debug.Log(Counter + "Counter");
    }
}
