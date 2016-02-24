using UnityEngine;
using System.Collections;


enum WeaponType
{
    NORMAL,
    SPECIALITEAM
}


public class Equippable : MonoBehaviour
{
    [SerializeField]
    private WeaponType _Weapon;

    [SerializeField]
    private int _WeaponStrength;

    [SerializeField]
    private int _NumOfUses;

    [SerializeField]
    private bool _UnlimitedUses;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Its quite irrelvant as far as things goes
    }

    public virtual void UseAbility() { }

    public int GetWeaponStrength() { return _WeaponStrength; }
}
