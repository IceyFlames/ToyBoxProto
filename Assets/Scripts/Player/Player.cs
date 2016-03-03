using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

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
    FEET,
    WEAPON
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
    //A float for how strong/ high you will jump
    private float _JumpStrength;




    //If any of the following arms or legs are false it means they are disabled and will play the appropiate animation
    private bool _LeftLegActive;
    private bool _RightLegActive;
    private bool _LeftArmActive;
    private bool _RightArmctive;

    //Probably super redudant but im just going to have pointers to them if ever i need them
    [SerializeField]
    private GameObject _LeftLegObj;

    [SerializeField]
    private GameObject _RightLegObj;

    [SerializeField]
    private GameObject _LeftArmObj;

    [SerializeField]
    private GameObject _RightArmObj;

    [SerializeField]
    private GameObject _LowerBodyObj;

    [SerializeField]
    private GameObject _HeadObj;



    private BodyPart _LeftLegComponent;
    private BodyPart _RightLegComponent;
    private BodyPart _LeftArmComponent;
    private BodyPart _RightArmComponent;

    //Does the character have a weapon equipped?
    private bool _isWeaponEquipped;

    //The characters animator which is used for working out which animation to play
    private Animator _Animator;

    //The rigidbody which is used for mostly jumping
    private Rigidbody _RigidBody;

    //Is the character jumping
    private bool _isJumping;

    //If the character is dead it cannot use its controls
    private bool _isDead;

    //Punch counter for switching between left and right
    private int _PunchCounter;

    //Punch counter for switching between left and right
    private int _KickCounter;


    [SerializeField]
    private string _PlayerName;

    AnimationEvent _Event;

    [SerializeField]
    private float _cooldown = 5;

    private bool _DoOnce;

    private float _KickCooldown;

    // Use this for initialization
    void Start()
    {
        _Animator = GetComponent<Animator>();
        _RigidBody = GetComponent<Rigidbody>();

        _LeftArmComponent = _LeftArmObj.GetComponent<BodyPart>();
        _RightArmComponent = _RightArmObj.GetComponent<BodyPart>();
        _RightLegComponent = _RightLegObj.GetComponent<BodyPart>();
        _LeftLegComponent = _LeftLegObj.GetComponent<BodyPart>();



        _isJumping = false;
        _isDead = false;

        _LeftLegActive = true;
        _RightLegActive = true;
        _RightArmctive = true;
        _LeftArmActive = true;

        _PunchCounter = 1;
        _KickCounter = 1;

        _KickCooldown = 0;

        _DoOnce = true;

    }

    // Update is called once per frame
    void Update()
    {
        RagDollDeath();
        _KickCooldown -= Time.deltaTime;
    }

    /// <Use Jump Summary>
    /// In a nutshell, this is for when the player is jumping
    /// </summary>
    public void UseJump()
    {

        return;
        if (_isJumping != true && !_isDead)
        {
            _RigidBody.AddForce(new Vector3(0, _JumpStrength, 0));
            Debug.Log("Player is jumping");
            _Animator.SetTrigger("IsJumpingStart");
            _isJumping = true;
        }
    }

    private void RagDollDeath()
    {
        if (!_LeftLegActive && !_RightLegActive && !_RightArmctive && !_LeftLegActive)
        {

            _isDead = true;

            _cooldown -= Time.deltaTime;

            if (_DoOnce)
            {
                #region SetRigidBodyFalse
                Rigidbody rb = _LeftLegObj.GetComponent<Rigidbody>();
                rb.isKinematic = false;

                rb = _RightLegObj.GetComponent<Rigidbody>();
                rb.isKinematic = false;

                rb = _RightArmObj.GetComponent<Rigidbody>();
                rb.isKinematic = false;

                rb = _LeftArmObj.GetComponent<Rigidbody>();
                rb.isKinematic = false;

                rb = _HeadObj.GetComponent<Rigidbody>();
                rb.isKinematic = false;

                rb = _LowerBodyObj.GetComponent<Rigidbody>();
                rb.isKinematic = false;
                #endregion

                #region SetTriggertoPhysics
                CapsuleCollider temp = _LeftLegObj.GetComponent<CapsuleCollider>();
                temp.isTrigger = false;

                temp = _RightLegObj.GetComponent<CapsuleCollider>();
                temp.isTrigger = false;

                temp = _RightArmObj.GetComponent<CapsuleCollider>();
                temp.isTrigger = false;

                temp = _LeftArmObj.GetComponent<CapsuleCollider>();
                temp.isTrigger = false;


                BoxCollider temp2 = _LowerBodyObj.GetComponent<BoxCollider>();
                temp2.isTrigger = false;

                SphereCollider temp3 = _HeadObj.GetComponent<SphereCollider>();
                temp3.isTrigger = false;
                #endregion
                Destroy(GetComponent<Rigidbody>());
                Destroy(GetComponent<CapsuleCollider>());

                _Animator.enabled = false;
                _DoOnce = false;
            }

            #region RestartGame
            if (_cooldown < 0)
            {

                switch (_PlayerName)
                {
                    case "Player1":
                        {
                            int Score = PlayerPrefs.GetInt("Player2Score");
                            Score++;
                            PlayerPrefs.SetInt("Player2Score", Score);
                            SceneManager.LoadScene(0);

                        }
                        break;

                    case "Player2":
                        {
                            int Score = PlayerPrefs.GetInt("Player1Score");
                            Score++;
                            PlayerPrefs.SetInt("Player1Score", Score);
                            SceneManager.LoadScene(0);
                        }
                        break;

                }
            }
            #endregion

        }
    }


    /// <UsePunch Summary>
    /// This allows the user to throw a punch, all that should be called here is a
    /// function call for the animator to play a specific animation :)
    /// </summary>
    public void UsePunch()
    {
        if (!_isDead)
        {
            if (_PunchCounter == 1 && _LeftArmActive)
            {
                _Animator.SetTrigger("isLeftPunching");


                _PunchCounter++;
                Debug.Log(_PunchCounter.ToString());

                _LeftArmComponent.tag = "Weapon";
                _RightArmComponent.tag = "Limb";
                return;
            }

            else if (!_LeftArmActive)
                _PunchCounter++;

            if (_PunchCounter == 2 && _RightArmctive)
            {
                _Animator.SetTrigger("isRightPunching");
                _PunchCounter--;
                Debug.Log(_PunchCounter.ToString());

                _LeftArmComponent.tag = "Limb";
                _RightArmComponent.tag = "Weapon";
                return;
            }

            else if (!_RightArmctive)
                _PunchCounter--;

        }
    }


    public void UseKick()
    {
        if (!_isDead)
        {
            if (_KickCounter == 1 && _LeftLegActive)
            {
                Debug.Log(_PunchCounter.ToString());

                _LeftLegComponent.tag = "Weapon";
                _RightLegComponent.tag = "Limb";
                _Animator.SetTrigger("isLeftKicking");
                return;
            }

            else if (!_LeftLegActive)
                _KickCounter++;

            if (_KickCounter == 2 && _RightLegActive)
            {

                Debug.Log(_PunchCounter.ToString());

                _LeftLegComponent.tag = "Limb";
                _RightLegComponent.tag = "Weapon";
                _Animator.SetTrigger("isRightKicking");
                return;
            }

            else if (!_RightLegActive)
                _KickCounter--;
        }
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
        if (!_isDead)
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

    public string ReturnName() { return _PlayerName; }

    public void IncrimentKickCounter()
    {
        _KickCounter++;

        if (_KickCounter > 2)
            _KickCounter = 1;
    }

    public void IncrimentPunchCounter()
    {
        _KickCounter++;

        if (_PunchCounter > 2)
            _PunchCounter = 1;
    }

}
