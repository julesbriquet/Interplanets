using UnityEngine;
using System.Collections;

public class BadRessourceHandler : MonoBehaviour
{

    public int lifeTime;

    // Use this for initialization
    void Start()
    {
        Destroy(this.gameObject, lifeTime);
    }

}
