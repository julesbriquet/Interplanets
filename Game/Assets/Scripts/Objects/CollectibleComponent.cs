using UnityEngine;
using System.Collections;

public class CollectibleComponent : CachedBase
{

    public enum CollectibleType
    {
        CONTROL_BOX,
        ARMOR_BOX,
        WEAPON_BOX,
        ENERGY_BOX
    };

    public float rotationSpeed = 0f;
    public int energyQuantity;
    private Vector3 startPosition;
    public CollectibleType typeOfCollectible;

    public override void Awake()
    {
        base.Awake(); //does the caching.
        //Debug.Log ("Awake called!");
    }


    // Use this for initialization
    void Start()
    {
        startPosition = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //this.transform.position = new Vector3(this.transform.position.x, startPosition.y + (Mathf.Sin(Time.time) * 0.3f), this.transform.position.z);
        //this.transform.eulerAngles = Vector3.up * Mathf.MoveTowardsAngle(transform.eulerAngles.y, transform.eulerAngles.y + rotationSpeed, rotationSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player playerEntity = other.GetComponent<Player>();

            if (this.typeOfCollectible == CollectibleType.CONTROL_BOX)
                playerEntity.LevelUpControl();
            else if (this.typeOfCollectible == CollectibleType.ENERGY_BOX)
                playerEntity.GetEnergy(energyQuantity);

            Destroy(gameObject);
        }
    }
}
