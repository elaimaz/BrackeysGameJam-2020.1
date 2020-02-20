using UnityEngine;

public class ChangeColor : MonoBehaviour {

    // Reference to Sprite Renderer component
    private Renderer rend;

    // Color value that we can set in Inspector
    // It's White by default
    [SerializeField]
    private Color colorToTurnTo = Color.white;

    // Use this for initialization
    private void Start () {

        // Assign Renderer component to rend variable
        rend = GetComponent<Renderer>();

        // Change sprite color to selected color
        rend.material.color = colorToTurnTo;
    }

    public void ChangePortalColor(int portal)
    {
        if (portal == 0)
        {
            colorToTurnTo = new Color(0f, 0.55f, 1f);
        }else if (portal == 1)
        {
            colorToTurnTo = Color.red;
        }else if (portal == 2)
        {
            colorToTurnTo = Color.green;
        }
        // Change sprite color to selected color
        rend.material.color = colorToTurnTo;
    }
}
