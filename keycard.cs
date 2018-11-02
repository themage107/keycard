using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keycard : MonoBehaviour {

    float xPos;
    float yPos;
    float zPos;

    float maxY;
    float minY;

    bool moveUp;

    int i = 1;
    Transform keyCardPos;

    public bool keyCardInPoss;
    public GameObject exitCollider;

    public GameObject kCard;
    public BoxCollider2D keyCardCollider;

    // Use this for initialization
    void Start () {
        moveUp = true;

        // stop the door from allowing next level access
        exitCollider.GetComponent<BoxCollider2D>().enabled = false;

        // set movement settings
        keyCardPos = this.GetComponent<Transform>();
		xPos = keyCardPos.position.x;
        yPos = keyCardPos.position.y;
        zPos = keyCardPos.position.z;
        maxY = yPos + 0.15f;
        minY = yPos - 0.15f;
    }
	
	// Update is called once per frame, this is the object movement only
	void Update () {

        if (moveUp)
        {
            yPos += 0.00625f;            
            if(yPos >= maxY)
            {
                moveUp = false;
                i = 0;
            }
        }

        else
        {
            yPos -= 0.00625f;
            if (yPos <= minY)
            {                
                moveUp = true;
                i = 0;
            }
        }

        keyCardPos.position = new Vector3(xPos, yPos, zPos);
        i++;
    }

    //keycard touch logic
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "JohnnyHitBox")
        {
            kCard.GetComponent<MeshRenderer>().enabled = false;

            this.GetComponent<AudioSource>().Play();

            // can exit
            exitCollider.GetComponent<BoxCollider2D>().enabled = true;

            // keycard is in player's possesion
            keyCardInPoss = true;

            // hide the keycard collider
            keyCardCollider.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    public void resetLockAndKey()
    {
        GameObject k = GameObject.Find("keycard");
        k.GetComponent<MeshRenderer>().enabled = true;

        GameObject kColl = GameObject.Find("keycardCollider");
        kColl.GetComponent<BoxCollider2D>().enabled = true;

        kColl.GetComponent<keycard>().keyCardInPoss = false;

        // lvl 1- 9 keycard reset
        GameObject exitDoorReader = GameObject.Find("keycardReaderCollider");
        Texture r = exitDoorReader.GetComponent<keycardReader>().redLight;

        GameObject.Find("keycardReader").GetComponent<Renderer>().material.SetTexture("_EmissionMap", r);

        //exitDoorReader.GetComponent<Renderer>().material.SetTexture("_EmissionMap", redLight);
        exitDoorReader.GetComponent<BoxCollider2D>().enabled = true;
        exitDoorReader.GetComponent<keycardReader>().doorUnlocked = false;

        GameObject.Find("exitCollider").GetComponent<BoxCollider2D>().enabled = false;
    }
}
