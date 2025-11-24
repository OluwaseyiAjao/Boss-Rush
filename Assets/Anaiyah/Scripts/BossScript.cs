using UnityEngine;

public class BossScript : MonoBehaviour
{
    [Header ("Health")]
    [SerializeField] private int maxHealth = 20;
    int currentHealth;

    [Header("Dodge")] 
    public float dodgeRange = 6f;
    private bool isDodging = false;
    
    private Animator animator;
    private Transform player;
    

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        
        if(player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("LowHealth", currentHealth < 10);

        if (!isDodging && currentHealth < 10)
        {
            float distance = Vector3.Distance(player.position, transform.position);

            if (distance <= dodgeRange)
            {
                ChooseDodgeDirection();
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
           // Die();
        }
    }

    void ChooseDodgeDirection()
    {
        Vector3 toPlayer = player.position - transform.position;
        Vector3 localDir = transform.TransformDirection(toPlayer);

        if (Mathf.Abs(localDir.x) > Mathf.Abs(localDir.z))
        {
            if (localDir.x > 0)
                animator.SetTrigger("DodgeRight");
            else
                animator.SetTrigger("DodgeLeft");
        }
        else
        {
            animator.SetTrigger("DodgeBack");
        }

        isDodging = true;
    }

    public void OnDodge()

    {
        isDodging = false;
    }
}
