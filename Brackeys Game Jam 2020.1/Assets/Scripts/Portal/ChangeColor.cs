using UnityEngine;

public class ChangeColor : MonoBehaviour {

    // Reference to Sprite Renderer component
    private Renderer rend;

    // Color value that we can set in Inspector
    // It's White by default
    [SerializeField]
    private Color colorToTurnTo = Color.white;
    
    
    public Color PortalJumpColor = new Color(0f, 0.55f, 1f);
    public Color PortalTimeSpeedColor = Color.red;
    public Color PortalShieldJumpColor = Color.green;
    // Use this for initialization
    private void Start () {

        // Assign Renderer component to rend variable
        rend = gameObject.GetComponent<Renderer>();

        // Change sprite color to selected color
        rend.material.color = colorToTurnTo;
    }

    public void ChangePortalColor(int portal)
    {   //0 Jump, 1 Time, 2 Shield.
        if (portal == 0)
        {
            colorToTurnTo = PortalJumpColor;
        }else if (portal == 1)
        {
            colorToTurnTo = PortalTimeSpeedColor;
        }else if (portal == 2)
        {
            colorToTurnTo = PortalShieldJumpColor;
        }
        
        // Change sprite color to selected color
        GetComponent<Renderer>().material.color = colorToTurnTo;
    }
}
