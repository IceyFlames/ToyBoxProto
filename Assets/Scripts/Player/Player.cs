using UnityEngine;
using System.Collections;

//Author: Anthony Bogli
//Date Created: February 22, 2016
//Description: Creating a player class this handles holding the 
//Attacking, Jumping and Equipping functions however they are invoked
//Within only the input manager class 


/// <Limbs Summary>
/// The Limbs is just a enum that helps us identify which part of the body each mesh is
/// </Limbs Summary>
public enum Limbs
{ 
    LEFTARM,
    RIGHTARM,
    LEFTLEG,
    RIGHTLEG,
    BODY,
    HEAD,
    FEET
}

/// <Player Class>
///This is just setting up the player class
///It Has some functions to handle movement, but those functions
///Are only called within the Input Manager within the scene
///The Input Managers Name should be called literally "Input Manager" 
/// </Player Class>

public class Player : MonoBehaviour
{
    [SerializeField]
    //This is how fast the character moves
    private float _MovementSpeed;

    [SerializeField]
    //This is the characters attack strength (we dont need to worry about attack speed since thats animation driven)
    //Note: This is redudant now each body part may have a corresponding strength
    private int _AttackStrength;

    //If any of the following arms or legs are false it means they are disabled and will play the appropiate animation
    private bool _LeftLegActive;   
    private bool _RightLegActive;
    private bool _LeftArmActive;
    private bool _RightArmctive;

    //Does the character have a weapon equipped?
    private bool _isWeaponEquipped;

    //The characters animator which is used for working out which animation to play
    private Animator _Animator;

    //The rigidbody which is used for mostly jumping
    private Rigidbody _RigidBody;

    // Use this for initialization
    void Start()
    {
        _Animator = GetComponent<Animator>();
        _RigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <Use Jump Summary>
    /// In a nutshell, this is for when the player is jumping
    /// </summary>
    public void UseJump()
    {

    }

    /// <UsePunch Summary>
    /// This allows the user to throw a punch, all that should be called here is a
    /// function call for the animator to play a specific animation :)
    /// </summary>
    public void UsePunch()
    {

    }

    /// <Pick Up Weapon>
    /// Allows the user to pick up a object and use it as a weapon
    /// Where this code is launched from is within the input manager
    /// </summary>
    public void PickUpWeapon()
    {

    }

    /// <Throw Weapon>
    /// This checks if the character is currently has a weapon equipped
    /// and if it does it will throw the object with a intial velocity towards whereever the player is facing
    /// W.I.P Stretch goal feature
    /// </summary>
    public void ThrowWeapon()
    {

    }
    
    /// <Move In Direction>
    /// This Function is called from the InputManager
    /// The character moves in a vector Direction and the 
    /// Character is rotated towards the relative point its heading towards
    /// </summary>
    /// <param name="Direction"></param>
    public void MoveInDirection(Vector3 Direction)
    {
        Vector3 NewDirection = (Direction * _MovementSpeed * Time.deltaTime);
        Debug.Log("The Current Direction" + NewDirection.ToString());
        transform.position += NewDirection;


        Vector3 NewPosition = transform.position + NewDirection;


        if (Direction != Vector3.zero)
        {
            Vector3 relativePos = transform.position - NewPosition;
            Quaternion rotation = Quaternion.LookRotation(-relativePos);
            transform.rotation = rotation;
        }

        transform.position += NewDirection;

        _Animator.SetTrigger("IsRunning");
    }


    /// <Missing Body parts Summary>
    /// All the Body parts (gameobjects) should have a limb script which
    /// when there hp reaches 0 they will call this function which intern
    /// will check which body parts are missing and prior to that will play the appropiate animation
    /// </summary>
    /// <param name="limb"></param>
    public void MissingBodyPart(Limbs limb)
    {
        switch (limb)
        {
            case Limbs.BODY:
                break;
            case Limbs.FEET:
                break;
            case Limbs.HEAD:
                break;
            case Limbs.LEFTARM:
                _LeftArmActive = false;
                break;
            case Limbs.LEFTLEG:
                _LeftLegActive = false;
                break;
            case Limbs.RIGHTARM:
                _RightArmctive = false;
                break;
            case Limbs.RIGHTLEG:
                _RightLegActive = false;
                break;
        }

    }

}
