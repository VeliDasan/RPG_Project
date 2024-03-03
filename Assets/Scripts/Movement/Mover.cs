using RPG.Combat;
using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace RPG.Movement
{
    public class Mover : MonoBehaviour,IAction
    {
        [SerializeField] float maxSpeed = 6f;
        NavMeshAgent navMeshAgent;
        healt healt;
        private void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            healt = GetComponent<healt>();
        }
        void Update()
        {
            navMeshAgent.enabled = !healt.IsDead();
            UpdateAnimator();

        }

        public void startMoveAcition(Vector3 hit,float speedFraction)
        {
            GetComponent<ActionShecduler>().StartAction(this);
            GetComponent<Fighter>().Cancel();
            MoveTo(hit, speedFraction);

        }


        public void MoveTo(Vector3 hit,float speedFraction)
        {
            navMeshAgent.speed = maxSpeed * speedFraction;
            navMeshAgent.destination = hit;
            navMeshAgent.isStopped = false;
        }

        public void Cancel()
        {
            navMeshAgent.isStopped = true;
        }
      

        private void UpdateAnimator()
        {
            Vector3 velocity = navMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            GetComponent<Animator>().SetFloat("forwardSpeed", speed);

        }
    }

}


