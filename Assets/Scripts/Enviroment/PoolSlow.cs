using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;
public class PoolSlow : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider playerobj)
    {
        if(playerobj.gameObject.tag == "Player")
        {
            ThirdPersonCharacter player = playerobj.gameObject.GetComponent<ThirdPersonCharacter>();
            player.m_MoveSpeedMultiplier = .5f;
        }

    }

    void OnTriggerExit(Collider playerobj)
    {
        if (playerobj.gameObject.tag == "Player")
        {
            ThirdPersonCharacter player = playerobj.gameObject.GetComponent<ThirdPersonCharacter>();
            player.m_MoveSpeedMultiplier = 1;
        }
    }

}
