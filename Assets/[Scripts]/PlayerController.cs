using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum PlayerDirections
{
    North,
    East,
    South,
    West
}

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Vector3 targetLocation;
    [SerializeField] private float speed;
    [SerializeField] private PlayerDirections direction = PlayerDirections.South;
    public bool isWalking;
    [SerializeField] private int EncounterPercentage;
    private Animator animator;
    
    public LayerMask grass;
    public LayerMask NPC;    
    public LayerMask collisionObject;

    private Vector2 input;
    public bool isInteract;
    [Header("Dust Particals")]
    private ParticleSystem dustTrail;
    public Color trailGround;
    public Color trailGrass;

    

    // Start is called before the first frame update
    void Start()
    {       
        SoundManager.Instance.PlayMusic(Sound.OVERWORLD_MUSIC);
        animator = GetComponent<Animator>();
        dustTrail = GetComponentInChildren<ParticleSystem>();
        isInteract = false;

        if (DataBase.Instance.isLoose)
        {
            DataBase.Instance.LoadPlayer();            
        }
        else
        {
            transform.position = DataBase.Instance.quickSavedPosition;
            DataBase.Instance.isLoose = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        SetAnimationClip();
        if (Input.GetKeyDown(KeyCode.K))
        {
            DataBase.Instance.SaveGame();
        }
    }

    private void Move()
    {
        if (!isWalking)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");
            if (input.x != 0) input.y = 0;
            if (input != Vector2.zero)
            {
                targetLocation = transform.position;
                targetLocation.x += input.x;
                targetLocation.y += input.y;
                if (input.x > 0)
                    direction = PlayerDirections.East;
                else if (input.x < 0)
                    direction = PlayerDirections.West;
                else if (input.y > 0)
                    direction = PlayerDirections.North;
                else if (input.y < 0)
                    direction = PlayerDirections.South;
                if (IsWalkable(targetLocation))
                    StartCoroutine(MoveCheck(targetLocation));
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InteractNPC();
        }

    }
    IEnumerator MoveCheck(Vector3 targetPosition)
    {
        isWalking = true;
        SoundManager.Instance.PlaySoundFX(Sound.WALK, Chanel.PLAYER_FOOTSTEP_FX);
        createDustTrail();
        while ((targetPosition - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetLocation, step);
            yield return null;
        }
        transform.position = targetPosition;
        isWalking = false;
        CheckEncounters();
    }

    void InteractNPC()
    {
        Vector3 facingDirection = new Vector3(0, 0, 0);
        switch (direction)
        {
            case PlayerDirections.East:
                facingDirection = Vector3.right;
                break;
            case PlayerDirections.North:
                facingDirection = Vector3.up;
                break;
            case PlayerDirections.South:
                facingDirection = Vector3.down;
                break;
            case PlayerDirections.West:
                facingDirection = Vector3.left;
                break;
        }
        Vector3 interact = transform.position + facingDirection;
        var inter = Physics2D.OverlapCircle(interact, 0.3f, NPC);
        if (inter != null)
        {
            inter.GetComponent<NPC>().Interact();
            isInteract = true;
        }

    }

    private void SetAnimationClip()
    {
        animator.SetBool("IsMoving", isWalking);
        animator.SetInteger("Direction", (int)direction);

    }

    private bool IsWalkable(Vector3 targetPosition)
    {
        if (Physics2D.OverlapCircle(targetPosition, 0.3f, collisionObject) != null)
        {
            return false;
        }        
        return true;
    }

    private void CheckEncounters()
    {
        if (Physics2D.OverlapCircle(transform.position, 0.2f, grass))
        {
            
            if (Random.Range(1, 101) <= EncounterPercentage)
            {
                DataBase.Instance.quickSavedPosition = transform.position;
                SceneManager.LoadScene("EncounterScene");
            }
        }
    }

    private void createDustTrail()
    {
        dustTrail.GetComponent<Renderer>().material.SetColor("_Color", trailGround);
        if (Physics2D.OverlapCircle(transform.position, 0.2f, grass))
        {
            dustTrail.GetComponent<Renderer>().material.SetColor("_Color", trailGrass);
        }
        else
        {
            dustTrail.GetComponent<Renderer>().material.SetColor("_Color", trailGround);
        }
        dustTrail.Play();
    }
}
