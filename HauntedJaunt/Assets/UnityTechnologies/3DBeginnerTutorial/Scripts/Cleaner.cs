using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleaner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(this.gameObject.name == "Orb(Clone)")
        {
            StartCoroutine(DestroyOrb());
        }
    }

    IEnumerator DestroyOrb(){
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
