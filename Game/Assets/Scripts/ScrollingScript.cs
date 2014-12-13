using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingScript : CachedBase {

    public Vector2 speed = new Vector2(2, 2);
    public Vector2 direction = new Vector2(-1, 0);

    public bool isLinkedToCamera = false;
    public bool isLooped = false;

    private List<Transform> loopedTransform;

    // This put transform and rigidbody in cache
    public override void Awake()
    {
        base.Awake(); //does the caching.
        //Debug.Log ("Awake called!");
    }

    void Start()
    {
        if (isLooped)
        {
            loopedTransform = new List<Transform>();

            for (int i = 0; i < this.transform.childCount; ++i)
            {
                Transform childTrans = transform.GetChild(i);

                if (childTrans.renderer != null)
                    loopedTransform.Add(childTrans);
            }

            loopedTransform = loopedTransform.OrderBy(t => t.position.x).ToList();
        }
    }

	// Update is called once per frame
	void Update () {
        Vector3 movement = new Vector3(speed.x * direction.x, speed.y * direction.y, 0);

        movement *= Time.deltaTime;
        transform.Translate(movement);

        if (isLinkedToCamera)
            Camera.main.transform.Translate(movement);


        if (isLooped)
        {
            Transform firstChild = loopedTransform.FirstOrDefault();

            if (firstChild.position.x < Camera.main.transform.position.x)
            {
                if (firstChild.renderer.IsVisibleFrom(Camera.main) == false)
                {
                    Transform lastChild = loopedTransform.LastOrDefault();
                    Vector3 lastPosition = lastChild.transform.position;
                    Vector3 lastSize = (lastChild.renderer.bounds.max - lastChild.renderer.bounds.min);

                    firstChild.position = new Vector3(lastPosition.x + lastSize.x, firstChild.position.y, firstChild.position.z);

                    loopedTransform.Remove(firstChild);
                    loopedTransform.Add(firstChild);
                }
            }
        }
	}
}
