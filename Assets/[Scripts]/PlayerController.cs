using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Vector3 targetLocation;
    [SerializeField] private float speed;
    private bool isWalking;
    [SerializeField] private int EncounterPercentage;
    private Animator animator;
    public LayerMask grass;
    private Vector2 input;
    public LayerMask collisionObject;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
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
                if (IsWalkable(targetLocation))
                    StartCoroutine(MoveCheck(targetLocation));
            }
        }

    }
    IEnumerator MoveCheck(Vector3 targetPosition)
    {
        isWalking = true;
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
                //Battle Scene Change
                //SceneManager.LoadScene("BattleScene");
            }
        }
    }
}
