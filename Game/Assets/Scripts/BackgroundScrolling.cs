using UnityEngine;
using System.Collections;


/*
 * 
 * 
 * 
 * NOT USED
 * 
 *
 * 
 * 
 *
 * 
 * 
 */ 
public class BackgroundScrolling : MonoBehaviour {

    private SpriteRenderer attachedSprite;
    public Vector3[] posBound;
    public int screenPos;

	// Use this for initialization
	void Start () {
        attachedSprite = GetComponent<SpriteRenderer>();
        
		

		posBound = SpriteLocalToWorld(attachedSprite.sprite);
	}
	
    //// Update is called once per frame
    //void Update () {
       

    //    if (attachedSprite.renderer.is
        
    //}

    Vector3[] SpriteLocalToWorld(Sprite sp)
    {
        Vector3 pos = transform.position;
        Vector3[] array = new Vector3[2];
        //top left
        array[0] = pos + sp.bounds.min;
        // Bottom right
        array[1] = pos + sp.bounds.max;
        return array;
    }
}
