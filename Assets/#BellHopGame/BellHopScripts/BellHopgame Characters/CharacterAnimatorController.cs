﻿
using System;
using UnityEngine;
using UnityEngine.AI;
namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(CapsuleCollider))]
    [RequireComponent(typeof(Animator))]
    public class CharacterAnimatorController : MonoBehaviour
    {

        #region Vars
        [SerializeField] float m_MovingTurnSpeed = 360;
        [SerializeField] float m_StationaryTurnSpeed = 180;
        [SerializeField] float m_JumpPower = 6f;
        [Range(1f, 4f)] [SerializeField] float m_GravityMultiplier = 2f;
        [SerializeField] float m_RunCycleLegOffset = 0.2f; //specific to the character in sample assets, will need to be modified to work with others
        [SerializeField] float m_MoveSpeedMultiplier = 1f;
        [SerializeField] float m_AnimSpeedMultiplier = 1f;
        [SerializeField] float m_GroundCheckDistance = 0.2f;
        Rigidbody m_Rigidbody;
        Animator m_Animator;
        bool m_IsGrounded;
        float m_OrigGroundCheckDistance;
        const float k_Half = 0.5f;
        float m_TurnAmount;
        float m_ForwardAmount;
        Vector3 m_GroundNormal;
        float m_CapsuleHeight;
        Vector3 m_CapsuleCenter;
        CapsuleCollider m_Capsule;
        bool m_Crouching;

        bool reachedRot = false;

        NavMeshAgent agent;
        bool AgentIsAwake = false;
        bool isUpdateAIPos = true;
        bool NOtStartedWalking;
        bool arrived;
        bool IsMecanim;
        [SerializeField]
        Transform CahsedDESTINATION = null;
        #endregion


        #region Original

        public void Awake()
        {
            m_Animator = GetComponent<Animator>();
            m_Rigidbody = GetComponent<Rigidbody>();
            m_Capsule = GetComponent<CapsuleCollider>();
            m_CapsuleHeight = m_Capsule.height;
            m_CapsuleCenter = m_Capsule.center;

            m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            m_OrigGroundCheckDistance = m_GroundCheckDistance;


            // IsMecanim = true;
            agent = GetComponent<NavMeshAgent>();
            //if (IsMecanim) agent.updateRotation = false;
        }

        void ScaleCapsuleForCrouching(bool crouch)
        {
            if (m_IsGrounded && crouch)
            {
                if (m_Crouching) return;
                m_Capsule.height = m_Capsule.height / 2f;
                m_Capsule.center = m_Capsule.center / 2f;
                m_Crouching = true;
            }
            else
            {
                Ray crouchRay = new Ray(m_Rigidbody.position + Vector3.up * m_Capsule.radius * k_Half, Vector3.up);
                float crouchRayLength = m_CapsuleHeight - m_Capsule.radius * k_Half;
                if (Physics.SphereCast(crouchRay, m_Capsule.radius * k_Half, crouchRayLength, Physics.AllLayers, QueryTriggerInteraction.Ignore))
                {
                    m_Crouching = true;
                    return;
                }
                m_Capsule.height = m_CapsuleHeight;
                m_Capsule.center = m_CapsuleCenter;
                m_Crouching = false;
            }
        }

        void PreventStandingInLowHeadroom()
        {
            // prevent standing up in crouch-only zones
            if (!m_Crouching)
            {
                Ray crouchRay = new Ray(m_Rigidbody.position + Vector3.up * m_Capsule.radius * k_Half, Vector3.up);
                float crouchRayLength = m_CapsuleHeight - m_Capsule.radius * k_Half;
                if (Physics.SphereCast(crouchRay, m_Capsule.radius * k_Half, crouchRayLength, Physics.AllLayers, QueryTriggerInteraction.Ignore))
                {
                    m_Crouching = true;
                }
            }
        }

        void UpdateAnimator(Vector3 move)
        {
            // update the animator parameters
            m_Animator.SetFloat("Forward", m_ForwardAmount, 0.1f, Time.deltaTime);
            m_Animator.SetFloat("Turn", m_TurnAmount, 0.1f, Time.deltaTime);
            m_Animator.SetBool("Crouch", m_Crouching);
            m_Animator.SetBool("OnGround", m_IsGrounded);
            if (!m_IsGrounded)
            {
                m_Animator.SetFloat("Jump", m_Rigidbody.velocity.y);
            }

            // calculate which leg is behind, so as to leave that leg trailing in the jump animation
            // (This code is reliant on the specific run cycle offset in our animations,
            // and assumes one leg passes the other at the normalized clip times of 0.0 and 0.5)
            float runCycle =
                Mathf.Repeat(
                    m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime + m_RunCycleLegOffset, 1);
            float jumpLeg = (runCycle < k_Half ? 1 : -1) * m_ForwardAmount;
            if (m_IsGrounded)
            {
                m_Animator.SetFloat("JumpLeg", jumpLeg);
            }

            // the anim speed multiplier allows the overall speed of walking/running to be tweaked in the inspector,
            // which affects the movement speed because of the root motion.
            if (m_IsGrounded && move.magnitude > 0)
            {
                m_Animator.speed = m_AnimSpeedMultiplier;
            }
            else
            {
                // don't use that while airborne
                m_Animator.speed = 1;
            }
        }

        void HandleAirborneMovement()
        {
            // apply extra gravity from multiplier:
            Vector3 extraGravityForce = (Physics.gravity * m_GravityMultiplier) - Physics.gravity;
            m_Rigidbody.AddForce(extraGravityForce);

            m_GroundCheckDistance = m_Rigidbody.velocity.y < 0 ? m_OrigGroundCheckDistance : 0.01f;
        }

        void HandleGroundedMovement(bool crouch, bool jump)
        {

            //prevent jumping
            jump = false;

            // check whether conditions are right to allow a jump:
            if (jump && !crouch && m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Grounded"))
            {
                // jump!
                m_Rigidbody.velocity = new Vector3(m_Rigidbody.velocity.x, m_JumpPower, m_Rigidbody.velocity.z);
                m_IsGrounded = false;
                m_Animator.applyRootMotion = false;
                m_GroundCheckDistance = 0.1f;
            }
        }

        void ApplyExtraTurnRotation()
        {

            // help the character turn faster (this is in addition to root rotation in the animation)
            float turnSpeed = Mathf.Lerp(m_StationaryTurnSpeed, m_MovingTurnSpeed, m_ForwardAmount);
            transform.Rotate(0, m_TurnAmount * turnSpeed * Time.deltaTime, 0);

            //else
            //{
            //    float turnSpeed = Mathf.Lerp(m_StationaryTurnSpeed, m_MovingTurnSpeed, 0.6f);
            //    float tothis = m_TurnAmount * turnSpeed * Time.deltaTime;
            //    transform.Rotate(0, tothis, 0);
            //}

        }

        public void OnAnimatorMove()
        {
            // we implement this function to override the default root motion.
            // this allows us to modify the positional speed before it's applied.
            if (m_IsGrounded && Time.deltaTime > 0)
            {
                Vector3 v = (m_Animator.deltaPosition * m_MoveSpeedMultiplier) / Time.deltaTime;

                // we preserve the existing y part of the current velocity.
                v.y = m_Rigidbody.velocity.y;
                m_Rigidbody.velocity = v;
            }
        }

        void CheckGroundStatus()
        {
            RaycastHit hitInfo;
#if UNITY_EDITOR
            // helper to visualise the ground check ray in the scene view
            Debug.DrawLine(transform.position + (Vector3.up * 0.5f), transform.position + (Vector3.up * 0.5f) + (Vector3.down * m_GroundCheckDistance));
#endif
            // 0.1f is a small offset to start the ray from inside the character
            // it is also good to note that the transform position in the sample assets is at the base of the character
            if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, m_GroundCheckDistance))
            {
                m_GroundNormal = hitInfo.normal;
                m_IsGrounded = true;
                m_Animator.applyRootMotion = true;
            }
            else
            {
                m_IsGrounded = false;
                m_GroundNormal = Vector3.up;
                m_Animator.applyRootMotion = false;
            }
        }

        public void Move(Vector3 move, bool crouch, bool jump)
        {

            // convert the world relative moveInput vector into a local-relative
            // turn amount and forward amount required to head in the desired
            // direction.
            if (move.magnitude > 1f) move.Normalize();
            move = transform.InverseTransformDirection(move);
            CheckGroundStatus();
            move = Vector3.ProjectOnPlane(move, m_GroundNormal);
            m_TurnAmount = Mathf.Atan2(move.x, move.z);
            m_ForwardAmount = move.z;


            ApplyExtraTurnRotation();

            // control and velocity handling is different when grounded and airborne:
            if (m_IsGrounded)
            {
                HandleGroundedMovement(crouch, jump);
            }
            else
            {
                HandleAirborneMovement();
            }

            ScaleCapsuleForCrouching(crouch);
            PreventStandingInLowHeadroom();
            // send input and other state parameters to the animator
            UpdateAnimator(move);
        }
        #endregion



        #region dwellergame
        public Animator Get_myAnimator() { return this.m_Animator; }


        void TurnToSimple(Vector3 argDirection, Action argOnRotCompledCallBAck)
        {
            if (reachedRot) return;

            if (argDirection.magnitude > 1f) argDirection.Normalize();
            argDirection = transform.InverseTransformDirection(argDirection);
            CheckGroundStatus();
            argDirection = Vector3.ProjectOnPlane(argDirection, m_GroundNormal);





            m_TurnAmount = Mathf.Atan2(argDirection.x, argDirection.z);
            m_ForwardAmount = argDirection.z * -m_TurnAmount / 8f;



            UpdateAnimator(argDirection);
            ApplyExtraTurnimple(argOnRotCompledCallBAck);

        }

        void ApplyExtraTurnimple(Action argOnRotCompledCallBAck)
        {
            float turnSpeed = Mathf.Lerp(m_StationaryTurnSpeed, m_MovingTurnSpeed, 0.5f);
            float tothis = m_TurnAmount * turnSpeed * Time.deltaTime;
            transform.Rotate(0, tothis, 0);

            if (Mathf.Abs(tothis) < 0.4f) //it's the abs value! otherwise no turning smoothly if turning left
            {
                if (!reachedRot)
                {
                    reachedRot = true;
                    //print("fin trn");
                    UpdateAnimator(Vector3.zero);
                    m_Animator.SetFloat("Turn", 0.0f);
                    argOnRotCompledCallBAck();

                }
            }


        }

        bool coroutineturnStarted = false;

        public void Reset_ReachedRot()
        {
            reachedRot = false;
            // print("reset");
        }
        #endregion


        #region Agent
        public void ActivateAgent()
        {

            agent.enabled = true;
            if (agent.isActiveAndEnabled)
            {
                // print("ACTIVIA");
            }
            else
            {
                // print("POOP");
            }
            AgentIsAwake = true;
        }

        public void Set_TargetTRans(Transform artT)
        {
            CahsedDESTINATION = artT;
        }

        public void Set_Destination(Transform argDest, bool isRortateOnly)
        {
            agent.isStopped = true;
            isUpdateAIPos = !isRortateOnly;
            CahsedDESTINATION = argDest;
            NOtStartedWalking = false;
            arrived = false;
            if (!isRortateOnly)
            {
                agent.isStopped = false;
                agent.updateRotation = true;
                agent.updatePosition = true;
                agent.SetDestination(argDest.position);
                // agent.ResetPath();
            }
            else
            {


                agent.updateRotation = false;
                agent.updatePosition = false;
                agent.isStopped = true;
                //ector3 fakeloc = this.transform.position + ((argDest.position - this.transform.position).normalized / 4f);
                //agent.SetDestination(fakeloc);

            } // agent.ResetPath();
        }

        public void WarpMeAgentto(Transform argDest)
        {
            agent.Warp(argDest.position);
            SignalEndTask();
        }

        public void ResetAgent()
        {
            agent.ResetPath();
        }
        //  bool SetRotateOnly_onSetDest = false;
        void USeAI()
        {

            if (agent.remainingDistance > agent.stoppingDistance)
            {
                Move(agent.desiredVelocity, false, false);
            }
            else //reached destination
            {
                if (!NOtStartedWalking)
                {
                    //print("started Nav To TArget");

                    NOtStartedWalking = true;
                }
                Move(Vector3.zero, false, false);
            }
            CheckIfReached();
        }

        void NoAI()
        {
            Vector3 heading = CahsedDESTINATION.position - this.transform.position;
            //float distance = heading.magnitude;
            //Vector3 direction = heading / distance;


            //Debug.DrawRay(new Vector3(transform.position.x, 1f, transform.position.z), new Vector3(heading.x, 0f, heading.z), Color.red, 0.1f);
            //Debug.DrawRay(new Vector3(transform.position.x, 1f, transform.position.z), new Vector3(direction.x, 0f, direction.z), Color.blue, 0.1f);

            TurnToSimple(heading, SignalEndTask);
        }

        void SignalEndTask()
        {
            BellHopGameEventManager.Instance.Call_SimpleTaskEnded();
        }
        void CheckIfReached()
        {

            if (!agent.pathPending)
            {
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    //  print("agent.remainingDistance <= agent.stoppingDistance");
                    if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                    {
                        //string str = "x";

                        //if (agent.velocity.sqrMagnitude == 0f) { str += "_1_"; }
                        //if (!agent.hasPath) { str += "_2_"; }
                        //print(str);

                        //  print("!agent.hasPath || agent.velocity.sqrMagnitude == 0");
                        if (!arrived)
                        {
                            // print("NOW WE HERE do i need to reset rot now?");
                            SignalEndTask();
                            //DoUseAi = false;
                            arrived = true;
                            // Reset_ReachedRot();

                        }
                    }
                }
            }
        }
        #endregion

        void FixedUpdate()
        {
            if (CahsedDESTINATION == null) return;

            if (isUpdateAIPos) USeAI();
            else
                NoAI();
        }
    }
}
