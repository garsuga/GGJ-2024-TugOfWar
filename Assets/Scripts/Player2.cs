using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Player2 : MonoBehaviour
{
    public Sprite[] sprites;
    public GameObject ropeTransform;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeSprite()
    {
        this.gameObject.GetComponent<PlayableDirector>().Play();
        var currentSprite = sprites[Random.Range(0, sprites.Length)];
        this.gameObject.GetComponent<SpriteRenderer>().sprite = currentSprite;

        if(currentSprite == sprites[0])
        {
            ropeTransform.transform.position = new Vector3(this.gameObject.transform.position.x - 0.8f, this.gameObject.transform.position.y + 1f, this.gameObject.transform.position.z);
        }
        else if (currentSprite == sprites[1])
        {
            ropeTransform.transform.position = new Vector3(this.gameObject.transform.position.x + 2.3f, this.gameObject.transform.position.y + 1.1f, this.gameObject.transform.position.z);
        }
        else if (currentSprite == sprites[2])
        {
            ropeTransform.transform.position = new Vector3(this.gameObject.transform.position.x + 1.8f, this.gameObject.transform.position.y + 0.7f, this.gameObject.transform.position.z);
        }
        else
        {
            ropeTransform.transform.position = new Vector3(this.gameObject.transform.position.x + 1.6f, this.gameObject.transform.position.y + 1.8f, this.gameObject.transform.position.z);
        }
    }
}
