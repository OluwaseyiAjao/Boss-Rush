using System;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.InputSystem;

namespace Anaiyah
{
    public class BossScript : MonoBehaviour
    {
        [Header ("Health")]
        [SerializeField] private int maxHealth ;
        [SerializeField] int currentHealth;
        
        [Header("Dodge")] 
        public float dodgeRange = 6f;
        public float aggroDistance = 10f;
        private bool isDodging = false;
        public float distance;
    
        [SerializeField] Animator animator;
        [SerializeField] public Transform player;
        [SerializeField] private float whentoPhaseTwo;
        [SerializeField] public float turnSpeed = 3.5f;

        public static BossScript Instance;
        public UnityEvent OnStartAttack;
        

        private enum Phases
        {
            Phase1 = 1,
            Phase2 
        }
        
        private Phases currentPhase = Phases.Phase1;

        private void Awake()
        {
           if(Instance == null)
           {
                Instance = this;
           }
           
           animator = GetComponent<Animator>();
           animator.SetBool("isMoving", false);
           
           OnStartAttack ??= new UnityEvent();
           OnStartAttack.AddListener(() => StartCoroutine(StartAttackCoolDown()));
           
           GetComponent<Damageable>().OnInitialize.Invoke(maxHealth);
        }

      


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
            Vector3 playerFaltY = new Vector3(player.position.x, 0, player.position.z);
            Vector3 bossFaltY = new Vector3(transform.position.x, 0, transform.position.z);
            
            
            distance = Vector3.Distance(playerFaltY, bossFaltY);
            
            if (distance <= BossScript.Instance.aggroDistance)
            {
                animator.SetBool("PlayerInRange", true);
            }
            else
            {
                animator.SetBool("PlayerInRange", false);
            }

            if (!isDodging && currentHealth < 10)
            {

                if (distance <= dodgeRange)
                {
                    ChooseDodgeDirection();
                }
            }
            
            
        }

        public void HealthUpdate()
        {
            currentHealth = maxHealth;
        }
        
        IEnumerator StartAttackCoolDown()
        {
            yield return new WaitForSeconds(3f);
           animator.SetTrigger("Attack");
        }

        public void TakeDamage(int damage)
        {
            currentHealth -= damage;

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                //Die();
            }
        }

        void HealthCheckforPhaseChange(int currentHealth, int maxHealth)
        {
             var percentageofRemainingHealth = currentHealth / (float)maxHealth;

             if (percentageofRemainingHealth <= whentoPhaseTwo)
             {
                 currentPhase = Phases.Phase2;
                 animator.SetInteger("Phase", (int)Phases.Phase2);
                 
             }
             
            
        }

        public void HitTrigger(int ctx)
        {
            animator.SetTrigger("isHit");
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

        IEnumerator EnemyStates()
        {
            if(currentHealth <= 25 )

                yield return new WaitForSeconds(0.2f);
        }

        public void ChangeHealth(int damage, int health)
        {
            currentHealth = health;
        }
    }
  
}
