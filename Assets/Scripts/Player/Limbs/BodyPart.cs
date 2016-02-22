using UnityEngine;
using System.Collections;

//Author: Anthony Bogli
//Date Created: February 22, 2016
//Description: Each individual mesh within the animated 

public class BodyPart : MonoBehaviour
{
    public GameObject _parent;
    private Player _Player;

    [SerializeField]
    private Limbs _Limb;

    [SerializeField]
    private int _Health;

    [SerializeField]
    private int _WeaponStrength;

    // Use this for initialization
    void Start()
    {
        _Player = _parent.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        //Its quite irrelvant as far as things goes
    }

    void OnCollisionEnter(Collision collision)
    {
        //We Only need to check if the limb is being hit by another weapon because we change the tag of this object to weapon
        //When its doing the appropiate animation anyways :)
        if (collision.gameObject.tag == "Weapon")
        {
            BodyPart BodyPart = collision.gameObject.GetComponent<BodyPart>();
            _Health -= BodyPart.GetWeaponStrength();

            if(_Health < 0)
            {
                _Player.MissingBodyPart(_Limb);
                this.gameObject.SetActive(false);
            }
        }
    }

   public int GetWeaponStrength() { return _WeaponStrength; }
}
