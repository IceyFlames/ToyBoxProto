using UnityEngine;
using System.Collections;

//Author: Anthony Bogli
//Date Created: February 22, 2016
//Description: Each individual mesh within the animated 

public class BodyPart : MonoBehaviour
{
    [SerializeField]
    private GameObject _parent;

    [SerializeField]
    private GameObject _MeshChild;

    private Player _Player;

    [SerializeField]
    private Limbs _Limb;

    [SerializeField]
    private int _Health;

    [SerializeField]
    private int _WeaponStrength;

    private SkinnedMeshRenderer _Renderer;

    private string _ObjectName;

    // Use this for initialization
    void Start()
    {

        _Player = _parent.GetComponent<Player>();
        _Renderer = _MeshChild.GetComponent<SkinnedMeshRenderer>();
        _ObjectName = _Player.ReturnName();
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

            if (BodyPart.ReturnName() != _ObjectName)
            {
                _Health -= BodyPart.GetWeaponStrength();

                if (_Health < 0)
                {
                    _Player.MissingBodyPart(_Limb);
                    _Renderer.enabled = false;
                    this.gameObject.SetActive(false);
                }
            }

        }
    }

    void OnTriggerEnter(Collider collision)
    {
        //We Only need to check if the limb is being hit by another weapon because we change the tag of this object to weapon
        //When its doing the appropiate animation anyways :)
        if (collision.gameObject.tag == "Weapon")
        {
            BodyPart BodyPart = collision.gameObject.GetComponent<BodyPart>();

            if (BodyPart.ReturnName() != _ObjectName)
            {
                _Health -= BodyPart.GetWeaponStrength();

                if (_Health < 0)
                {
                    _Player.MissingBodyPart(_Limb);
                    _Renderer.enabled = false;
                    this.gameObject.SetActive(false);
                }
            }

        }
    }

    public int GetWeaponStrength() { return _WeaponStrength; }
    public string ReturnName() { return _ObjectName; }


   
}
