using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropOrbs : MonoBehaviour
{
    public GameObject glowOrb;
    public Transform player;
    public bool placingOrb = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && placingOrb == false){
            placingOrb = true;
            StartCoroutine(DropOrb());
        }
    }

    IEnumerator DropOrb(){
        Instantiate(glowOrb, new Vector3(player.localPosition.x, player.localPosition.y, player.localPosition.z), Quaternion.identity);
        yield return new WaitForSeconds(0.1f);
        placingOrb = false;
    }
}
