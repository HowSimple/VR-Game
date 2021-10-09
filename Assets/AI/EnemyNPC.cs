// 
// https://faramira.com/enemy-behaviour-with-finite-state-machine-using-csharp-delegates-in-unity/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Patterns;

public class EnemyNPC : MonoBehaviour
{
    public enum StateTypes
    {
        IDLE = 0,
        CHASE,
        ATTACK,
        DAMAGE,
        DIE
    }
    public Character npc;

    #region NPC data
    // The maximum speed at which the enemy NPC can move.
    //public float mMaxSpeed = controller.walkSpeed;

    // The walking speed of the enemy NPC
    //public float mWalkSpeed = 1.5f;

    // The maximum viweing distance of the enemy NPC
    public float mViewingDistance = 10.0f;

    // The maximum viewing angle of the enemy NPC.
    public float mViewingAngle = 60.0f;

    // The distance at which the enemy NPC will start attacking.
    public float mAttackDistance = 2.0f;

    // The turning rate of the enemy NPC.
    public float mTurnRate = 500.0f;

    // The tags for this NPC's enemy. Usually it will be 
    // the player. But for other games this enemy NPC
    // can not only attack the player but also other NPCs.
    public string[] mEnemyTags;

    // The gravity. 
    //public float Gravity = enemy.gravity;

    // The transform where the head is. This will 
    // determine the view of the enemy NPC based on 
    // its head (where eyes are located)
    public Transform mEyeLookAt;

    // The distance to the nearest enemy of the NPC.
    // In this demo we only have our player. So
    // this is the distance from the enemy NPC to the
    // Player.
    [HideInInspector]
    public float mDistanceToNearestEnemy;

    // The nearest enemy of the enemy NPC.
    // This is in case we have more than
    // one player in the scene. It could also 
    // mean other NPCs that are enemy to
    // this NPC.
    [HideInInspector]
    public GameObject mNearestEnemy;

    // The reference to the animator.
    Animator mAnimator;

    // The reference to the character controller.
    CharacterController controller;

    // The total damage count.
    int mDamageCount = 0;

    // The velocity vector.
    private Vector3 mVelocity = new Vector3(0.0f, 0.0f, 0.0f);

    // The Finite State Machine.
    public FSM mFsm;

    // The maximum damage count before the 
    // enemy NPC dies.
    public int mMaxNumDamages = 5;
    #endregion

    #region FSM State class with delegates.
    public class NPCState : State
    {
        protected EnemyNPC mNPC;
        private StateTypes mStateType;

        public StateTypes StateTypes {  get { return mStateType; } }

        public NPCState(FSM fsm, StateTypes type, EnemyNPC npc) : base(fsm)
        {
            mNPC = npc;
            mStateType = type;
        }

        public delegate void StateDelegate();

        public StateDelegate OnEnterDelegate { get; set; } = null;
        public StateDelegate OnExitDelegate { get; set; } = null;
        public StateDelegate OnUpdateDelegate { get; set; } = null;
        public StateDelegate OnFixedUpdateDelegate { get; set; } = null;

        public override void Enter()
        {
            OnEnterDelegate?.Invoke();
        }

        public override void Exit()
        {
            OnExitDelegate?.Invoke();
        }

        public override void Update()
        {
            OnUpdateDelegate?.Invoke();
        }

        public override void FixedUpdate()
        {
            OnFixedUpdateDelegate?.Invoke();
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        mAnimator = transform.GetChild(0).GetComponent<Animator>();
        controller = npc.GetComponent<CharacterController>();

        if (!mEyeLookAt)
        {
            mEyeLookAt = transform;
        }

        mFsm = new FSM();
        mFsm.Add((int)StateTypes.IDLE, new NPCState(mFsm, StateTypes.IDLE, this));
        mFsm.Add((int)StateTypes.CHASE, new NPCState(mFsm, StateTypes.CHASE, this));
        mFsm.Add((int)StateTypes.ATTACK, new NPCState(mFsm, StateTypes.ATTACK, this));
        mFsm.Add((int)StateTypes.DAMAGE, new NPCState(mFsm, StateTypes.DAMAGE, this));
        mFsm.Add((int)StateTypes.DIE, new NPCState(mFsm, StateTypes.DIE, this));

        Init_IdleState();
        Init_AttackState();
        Init_DieState();
        Init_DamageState();
        Init_ChaseState();

        mFsm.SetCurrentState(mFsm.GetState((int)StateTypes.IDLE));
    }

    void Update()
    {
        mFsm.Update();
    }

    void FixedUpdate()
    {
        mFsm.FixedUpdate();
    }

    #region Public utility functions

    public Vector3 GetEyeForwardVector()
    {
        return mEyeLookAt.up;
    }

    public GameObject GetNearestEnemyInSight(out float distance, float viewableDistance, bool useVieweingAngle = false)
    {
        distance = viewableDistance;
        GameObject nearest = null;
        for (int t = 0; t < mEnemyTags.Length; ++t)
        {
            GameObject[] gos = GameObject.FindGameObjectsWithTag(mEnemyTags[t]);
            for (int i = 0; i < gos.Length; ++i)
            {
                GameObject player = gos[i];

                Vector3 diff = player.transform.position - transform.position;
                float curDistance = diff.magnitude;
                if (curDistance < distance)
                {
                    diff.y = 0.0f;

                    if (useVieweingAngle)
                    {
                        float angleH = Vector3.Angle(diff, GetEyeForwardVector());
                        if (angleH <= mViewingAngle)
                        {
                            distance = curDistance;
                            nearest = player;
                        }
                    }
                    else
                    {
                        distance = curDistance;
                        nearest = player;
                    }
                }
            }
        }
        return nearest;
    }
   
   
    public static float Distance(GameObject obj, Vector3 pos)
    {
        return (obj.transform.position - pos).magnitude;
    }

   

    bool IsAlive()
    {
        return mDamageCount < mMaxNumDamages;
    }
    #endregion

    #region Public State Related Functions
    public void PlayAnimation(StateTypes type)
    {
        switch (type)
        {
            case StateTypes.ATTACK:
                {
                    mAnimator.SetBool("Attack", true);
                    break;
                }
            case StateTypes.DIE:
                {
                    mAnimator.SetTrigger("Die");
                    break;
                }
            case StateTypes.DAMAGE:
                {
                    mAnimator.SetTrigger("Damage");
                    break;
                }
        }
    }
    public void StopAnimation(StateTypes type)
    {
        switch (type)
        {
            case StateTypes.ATTACK:
                {
                    mAnimator.SetBool("Attack", false);
                    break;
                }
            case StateTypes.DIE:
                {
                    // trigger so no need to do anything.
                    break;
                }
            case StateTypes.DAMAGE:
                {
                    // trigger so no need to do anything.
                    break;
                }
            case StateTypes.IDLE:
                {
                    break;
                }
            case StateTypes.CHASE:
                {
                    mAnimator.SetFloat("PosZ", 0.0f);
                    mAnimator.SetFloat("PosX", 0.0f);
                    break;
                }
        }
    }
    #endregion

    #region Setup and initialize the various states.
    void Init_IdleState()
    {
        NPCState state = (NPCState)mFsm.GetState((int)StateTypes.IDLE);

        // Add a text message to the OnEnter and OnExit delegates.
        state.OnEnterDelegate += delegate ()
        {
            Debug.Log("OnEnter - IDLE");
        };
        state.OnExitDelegate += delegate ()
        {
            StopAnimation(StateTypes.IDLE);
            Debug.Log("OnExit - IDLE");
        };

        state.OnUpdateDelegate += delegate ()
        {
            ////Debug.Log("OnUpdate - IDLE");
            //if(Input.GetKeyDown("c"))
            //{
            //    SetState(StateTypes.CHASE);
            //}
            //else if(Input.GetKeyDown("d"))
            //{
            //    SetState(StateTypes.DAMAGE);
            //}
            //else if (Input.GetKeyDown("a"))
            //{
            //    SetState(StateTypes.ATTACK);
            //}
            mNearestEnemy = GetNearestEnemyInSight(out mDistanceToNearestEnemy, mViewingDistance);
            if (mNearestEnemy)
            {
                if (mDistanceToNearestEnemy > mAttackDistance)
                {
                    SetState(StateTypes.CHASE);
                    return;
                }
                SetState(StateTypes.ATTACK);
                return;
            }
            PlayAnimation(StateTypes.IDLE);
        };
    }

    void Init_AttackState()
    {
        NPCState state = (NPCState)mFsm.GetState((int)StateTypes.ATTACK);

        // Add a text message to the OnEnter and OnExit delegates.
        state.OnEnterDelegate += delegate ()
        {
            Debug.Log("OnEnter - ATTACK");
        };
        state.OnExitDelegate += delegate ()
        {
            Debug.Log("OnExit - ATTACK");
            StopAnimation(StateTypes.ATTACK);
        };

        state.OnUpdateDelegate += delegate ()
        {
            ////Debug.Log("OnUpdate - ATTACK");
            //if (Input.GetKeyDown("c"))
            //{
            //    SetState(StateTypes.CHASE);
            //}
            //else if (Input.GetKeyDown("d"))
            //{
            //    SetState(StateTypes.DAMAGE);
            //}
            mNearestEnemy = GetNearestEnemyInSight(out mDistanceToNearestEnemy, mViewingDistance);

            if (mNearestEnemy)
            {
                if (IsAlive())
                {
                    if (mDistanceToNearestEnemy < mAttackDistance)
                    {
                        //ApplyAttack();
                        PlayAnimation(StateTypes.ATTACK);
                    }
                    else if (mDistanceToNearestEnemy > mAttackDistance && mDistanceToNearestEnemy < mViewingDistance)
                    {
                        SetState(StateTypes.CHASE);
                    }
                }
                else
                {
                    SetState(StateTypes.IDLE);
                }
                return;
            }
            if (!mNearestEnemy || mDistanceToNearestEnemy > mViewingDistance)
            {
                SetState(StateTypes.IDLE);
                return;
            }
        };
    }
    void Init_DieState()
    {
        NPCState state = (NPCState)mFsm.GetState((int)StateTypes.DIE);

        // Add a text message to the OnEnter and OnExit delegates.
        state.OnEnterDelegate += delegate ()
        {
            Debug.Log("OnEnter - DIE");
        };
        state.OnExitDelegate += delegate ()
        {
            Debug.Log("OnExit - DIE");
        };

        state.OnUpdateDelegate += delegate ()
        {
            //Debug.Log("OnUpdate - DIE");
            PlayAnimation(StateTypes.DIE);
        };
    }
    void Init_DamageState()
    {
        NPCState state = (NPCState)mFsm.GetState((int)StateTypes.DAMAGE);

        // Add a text message to the OnEnter and OnExit delegates.
        state.OnEnterDelegate += delegate ()
        {
            mDamageCount++;
            Debug.Log("OnEnter - DAMAGE, Total damage taken: " + mDamageCount);
        };
        state.OnExitDelegate += delegate ()
        {
            Debug.Log("OnExit - DAMAGE");
        };

        state.OnUpdateDelegate += delegate ()
        {
            ////Debug.Log("OnUpdate - DAMAGE");
            if (mDamageCount == mMaxNumDamages)
            {
                SetState(StateTypes.DIE);
                return;
            }

            //if (Input.GetKeyDown("i"))
            //{
            //    SetState(StateTypes.IDLE);
            //}
            //else if (Input.GetKeyDown("c"))
            //{
            //    SetState(StateTypes.CHASE);
            //}
            //else if (Input.GetKeyDown("a"))
            //{
            //    SetState(StateTypes.ATTACK);
            //}

            PlayAnimation(StateTypes.DAMAGE);
            SetState(StateTypes.IDLE);
        };
    }
    void Init_ChaseState()
    {
        NPCState state = (NPCState)mFsm.GetState((int)StateTypes.CHASE);

        // Add a text message to the OnEnter and OnExit delegates.
        state.OnEnterDelegate += delegate ()
        {
            Debug.Log("OnEnter - CHASE");
        };
        state.OnExitDelegate += delegate ()
        {
            Debug.Log("OnExit - CHASE");
            StopAnimation(StateTypes.CHASE);
        };

        state.OnUpdateDelegate += delegate ()
        {
            ////Debug.Log("OnUpdate - CHASE");
            //if (Input.GetKeyDown("i"))
            //{
            //    SetState(StateTypes.IDLE);
            //}
            //else if (Input.GetKeyDown("d"))
            //{
            //    SetState(StateTypes.DAMAGE);
            //}
            //else if (Input.GetKeyDown("a"))
            //{
            //    SetState(StateTypes.ATTACK);
            //}
            mNearestEnemy = GetNearestEnemyInSight(out mDistanceToNearestEnemy, mViewingDistance);

            if (!mNearestEnemy/* || !isMoving*/)
            {
                SetState(StateTypes.IDLE);
                return;
            }

            if (mDistanceToNearestEnemy < mAttackDistance)
            {
                SetState(StateTypes.ATTACK);
                return;
            }
            //ApplyChase
            //mCurrentAnimationIndex
            npc.MoveTowards(mNearestEnemy.transform.position, npc.walkSpeed);
            PlayAnimation(StateTypes.CHASE);
        };
    }
    #endregion

    IEnumerator Coroutine_Die(float duration)
    {
        yield return new WaitForSeconds(duration);
        Debug.Log("NPC died. Removing gameobject");
        Destroy(gameObject);
    }

    // Helper function to set the state
    public void SetState(StateTypes type)
    {
        mFsm.SetCurrentState(mFsm.GetState((int)type));
        if(type == StateTypes.DIE)
        {
            StartCoroutine(Coroutine_Die(1.0f));
        }
    }
}
