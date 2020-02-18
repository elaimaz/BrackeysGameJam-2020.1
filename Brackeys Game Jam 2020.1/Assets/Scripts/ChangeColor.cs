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
    
    private void Update () {

        // Assign Renderer component to rend variable
        rend = GetComponent<Renderer>();

        // Change sprite color to selected color
        rend.material.color = colorToTurnTo;
    }	
}
