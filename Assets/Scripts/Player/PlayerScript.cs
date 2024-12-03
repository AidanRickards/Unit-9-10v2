using Player;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine.VFX;
using UnityEngine.Rendering;
using JetBrains.Annotations;
using UnityEngine.SceneManagement;

namespace Player
{
    public class PlayerScript : MonoBehaviour
    {
        public Rigidbody2D rb;
        public SpriteRenderer sr;
        public Animator animator;
        public BoxCollider2D bx;

        //variables for states
        public IdleState idleState;
        public RunningState runningState;
        public JumpState jumpState;
        public ClimbState climbState;
        public DeadState deadState;
        public WinState winState;

        public JoystickMove jm;
        public StateMachine sm;
        public UI UI;


        public int jumpPower = 14;
        public bool isJumping = false;

        public Transform groundCheck;
        public LayerMask groundLayer;
        bool isGrounded;

        public bool isDead = false;
        public bool isWin = false;

        public bool isLadder;
        public bool isClimbing;
        float vertical;

        public bool upClimb;
        public bool downClimb;

        float distFromLadderTop, distFromLadderBottom;

        //Sounds
        public AudioClip music;
        public AudioClip walk;
        public AudioClip jump;
        public AudioClip ladderClimb;
        public AudioClip death;
        public AudioClip win;

        public AudioSource noLoopSource;
        public AudioSource loop;
        public AudioSource musicSource;

        //raycasting
        public Transform foot;
        public LayerMask ladderLayer;
        void Start()
        {
            
            rb = GetComponent<Rigidbody2D>();
            sm = gameObject.AddComponent<StateMachine>();
            bx = GetComponent<BoxCollider2D>();

            //add new states
            idleState = new IdleState(this, sm);
            runningState = new RunningState(this, sm);
            jumpState = new JumpState(this, sm);
            climbState = new ClimbState(this, sm);
            deadState = new DeadState(this, sm);

            //initialise the statemachine with the default state
            sm.Init(idleState);

            musicSource.clip = music;
            musicSource.Play();
        }
        private void Update()
        {
            isGrounded = Physics2D.OverlapCapsule(groundCheck.position, new Vector2(0.372799990f, 0.06989999836f), CapsuleDirection2D.Horizontal, 0, groundLayer);
            sm.CurrentState.LogicUpdate();
            print("Grounded=" + isGrounded);

            vertical = jm.dirY;
            if (upClimb == true)
            {
                if (isLadder && Mathf.Abs(vertical) > 0)
                {
                    isClimbing = true;
                    bx.isTrigger = true;
                }
            }
            if (downClimb == true)
            {
                if (isLadder && Mathf.Abs(vertical) < 0)
                {
                    isClimbing = true;
                    bx.isTrigger = true;
                }
            }

            if (isWin)
            {
                SceneManager.LoadScene(1);
            }


            Debug.DrawRay(foot.position, Vector2.down * 0.2f, Color.red);
        }

        private void FixedUpdate()
        {
            sm.CurrentState.PhysicsUpdate();
            CheckLadder();


            /*if (isClimbing)
            {
                rb.gravityScale = 0f;
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, vertical * jm.playerSpeed);
            }
            else
            {
                rb.gravityScale = 1f;
            }*/
        }

        public void Jump()
        {
            if (isGrounded)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
            }
        }

        public void CheckForIdle()
        {
            if (isGrounded)
            {
                if (jm.dirX == 0)
                {
                    sm.ChangeState(idleState);
                    Debug.Log("Idle");
                }
            }
        }

        public void CheckForRun()
        {
            if (isGrounded)
            {
                if(jm.dirX != 0)
                {
                    sm.ChangeState(runningState);
                    Debug.Log("Running");
                }
            }
        }

        public void CheckForJump()
        {
            if (!isGrounded)
            {
                noLoopSource.PlayOneShot(jump);
                sm.ChangeState(jumpState);
                Debug.Log("Jumping");
            }
        }
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy"))
            {
                isDead = true;
            }
            if (collision.CompareTag("Princess"))
            {
                isWin = true;
            }
        }
        /*
        private void OnTriggerExit2D(Collider2D collision)
        {
            
            if (collision.CompareTag("Ladder"))
            {
                isLadder = false;
                isClimbing = false;
                bx.isTrigger = false;
                rb.linearVelocityY = 0f;
            }
        }
        */
        private void CheckLadder()
        {
            RaycastHit2D hit;
            hit = Physics2D.Raycast(foot.position, Vector2.down, 0.2f, ladderLayer);

            if (hit)
            {
                float y1 = hit.collider.GetComponent<SpriteRenderer>().bounds.max.y;
                float y2 = hit.collider.GetComponent<SpriteRenderer>().bounds.min.y;

                distFromLadderTop = y1 - foot.position.y;
                distFromLadderBottom = foot.position.y - y2;
            }
            else
            {
                distFromLadderBottom = distFromLadderTop = -1;
            }
        }

        public bool IsNearLadderBottom()
        {
            if(distFromLadderBottom > 0 && distFromLadderBottom < 1)
            {
                return true;
            }
            return false;
        }

        public bool IsNearLadderTop()
        {
            if (distFromLadderTop > -0.335 && distFromLadderTop < 0.1)
            {
                return true;
            }
            return false;
        }

        public void CheckForLadderClimb()
        {
            Debug.Log("checking ladder climb");
            if (IsNearLadderBottom())
            {
                Debug.Log("LadderBottom");
                if(!isJumping && isGrounded && jm.dirY == 1)
                {
                    sm.ChangeState(climbState);
                }
            }

            if (IsNearLadderTop())
            {
                Debug.Log("At top of ladder");
                if(!isJumping && isGrounded && jm.dirY == -1)
                {
                    loop.clip = ladderClimb;
                    loop.Play();
                    sm.ChangeState(climbState);
                }
            }      
        }

        public void CheckForDead()
        {
            if (isDead)
            {
                Debug.Log("Died");
                sm.ChangeState(deadState);
            }
        }

        public void Die()
        {
            SceneManager.LoadScene(0);
        }
        /*public void Win()
        {
            if (isWin)
            {
                sm.ChangeState(winState);
            }
        }*/
    }
}