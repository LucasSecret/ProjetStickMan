using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // Add the TextMesh Pro namespace to access the various functions.

public class DebugerUiScript : MonoBehaviour
{
    // Start is called before the first frame update

    public Text prefab;
    public Canvas canvas;
    public PlayerControllerScript playerController;
    Text life;

    void Start()
    {
         life = Instantiate(prefab) as Text;
        //Parent to the panel
        life.transform.SetParent(canvas.transform, false);
        //Set the text box's text element font size and style:
        life.fontSize = 20;
        //Set the text box's text element to the current textToDisplay:
        
    }

    // Update is called once per frame
    void Update()
    {
        life.text = "life is : " + playerController.IsOnGround;
    }
}
