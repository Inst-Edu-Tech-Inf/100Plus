using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coins : MonoBehaviour
{
    [Header("Obrazki"), SerializeField]
    public Image obraz;
    GameManager gm;
    //Transform startPosition;
    Vector3 kierunek, cel;// = new Vector3(0,1,0);
    float droga;
    // Start is called before the first frame update
    void Start()
    {
      /*  Vector3 pozycja = transform.position;
        pozycja.x = pozycja.x + Random.Range(-5.0f, 5.0f);
        //pozycja.x = pozycja.x + Random.Range(-5.0f, 5.0f);
        pozycja.z = pozycja.z + Random.Range(-5.0f, 5.0f);
        transform.position = pozycja;*/
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        //transform.position = gm.activeCard.gameObject.transform.position;
        var heading = gm.victoryPoints.gameObject.transform.position - transform.position;//pozycja;//target.position - player.position;
         var distance = heading.magnitude;
         var direction = heading / distance; // This is now the normalized direction.
         //Debug.Log(direction);
         kierunek = direction;
         heading.y = 0;
         droga = distance;// gm.victoryPoints.gameObject.transform.position;
        //transform.position = transform.position + new Vector3(horizontalInput * movementSpeed * Time.deltaTime, verticalInput * movementSpeed * Time.deltaTime, 0);

        // tempColor.a = 1f - (heading.magnitude / droga);
        // obraz.color = tempColor;
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + kierunek * GameManager.COINS_SPEED * Time.deltaTime; //new Vector3(horizontalInput * GameManager.COINS_SPEED * Time.deltaTime, verticalInput * GameManager.COINS_SPEED * Time.deltaTime, 0);
        //image = GetComponent<Image>();
        var tempColor = obraz.color;
        

        var heading = gm.victoryPoints.gameObject.transform.position - transform.position;
       // Debug.Log("Alpha:" + (( heading.magnitude / droga)));
        tempColor.a = ( heading.magnitude / droga);
        obraz.color = tempColor;
        if (heading.sqrMagnitude < GameManager.COINS_RANGE_SQUARED)
        {
            // Target is within range.
            gm.DiscardCoin(this.gameObject);
        }
    }
}
