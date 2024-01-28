using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeController : MonoBehaviour
{
    public GameObject rope;

    public GameObject hands1;
    public GameObject hands2;


    // Start is called before the first frame update
    void Start()
    {

    
        //heldSegment1.transform.position = hands1.transform.position;
        //heldSegment2.transform.position = hands2.transform.position;
        /*var cnt = transform.childCount;
        var lastSegment = transform.GetChild(cnt - 1).gameObject;
        for(var i = cnt-2; i >= 0; i--) {
            var nextSegment = transform.GetChild(i).gameObject;
            lastSegment.GetComponent<HingeJoint2D>(). = nextSegment.GetComponent<Rigidbody2D>();
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateRope() {
        Vector3 diff = hands2.transform.position - hands1.transform.position;
        Vector3 middle = hands1.transform.position + (hands2.transform.position - hands1.transform.position)/2f;
        rope.transform.position = middle;
        rope.transform.LookAt(rope.transform.position + diff);
        var rot = rope.transform.eulerAngles;
        rope.transform.eulerAngles = new Vector3(0, 0, rot.x);
        var sprite = rope.GetComponent<SpriteRenderer>();
        //var newScale = sprite.bounds.size.x / diff.magnitude;
        //var oldScale = rope.transform.localScale;
        //rope.transform.localScale = new Vector3(oldScale.x * newScale, oldScale.y, oldScale.z);
    }
}
