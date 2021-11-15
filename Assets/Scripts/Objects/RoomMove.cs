using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomMove : MonoBehaviour
{
    // Camera/Player Movement 
    [SerializeField] private Vector2 cameraChange;
    [SerializeField] private Vector3 playerChange; //shift/move the player
    private CameraMovement cam; // referencing to the script 


    // Text Transition Room
    [SerializeField] private bool needText; // an variable to determine whenever if it is required atm. 
    [SerializeField] private string placeName;
    public GameObject text; // an reference to the actual title card.
    public Text placeText; // an reference to the text on the title card. 
    public GameObject textBad;



    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.GetComponent<CameraMovement>();
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player") && !other.isTrigger)
        {
            PreventOverlapText();
            cam.minPosition += cameraChange;
            cam.maxPosition += cameraChange;
            other.transform.position += playerChange;

            if(needText)
            {
                StartCoroutine(placeNameCo());
            }
        }
    }

    private IEnumerator placeNameCo()
    {
        text.SetActive(true);
        placeText.text = placeName;
        placeText.CrossFadeAlpha(0, 2.5f, false);
        yield return new WaitForSeconds(3f);
        text.SetActive(false);

    }

    private void PreventOverlapText()
    {
        textBad.SetActive(false);
    }
}
