using RPG.Core;
using RPG.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour,IAction
    {
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] Weapon defaultWeapon = null;
        [SerializeField] Transform rightHandTransform = null;
        [SerializeField] Transform leftHandTransform = null;

        healt targetObject;
        float timeSinceLastAttack;
        

        private void Start()
        {
            SpawnWeapon(defaultWeapon);
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            if (targetObject == null)
            {
                return;
            }
            if (targetObject.IsDead() == true)
            {
                GetComponent<Animator>().ResetTrigger("attack");
                Cancel();
                return;
            }
            
            if (GetIsInRange() == false)
            {
                GetComponent<Mover>().MoveTo(targetObject.transform.position,1f);
            }
            else
            {
                attackMethod();
                GetComponent<Mover>().Cancel();
            }
        }
        public void SpawnWeapon(Weapon weapon)
        {
            defaultWeapon = weapon;
            Animator animator = GetComponent<Animator>();
            defaultWeapon.Spawn(rightHandTransform, leftHandTransform, animator);
        }

        private void attackMethod()

        {
            transform.LookAt(targetObject.transform);
            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                TriggerAttack();
                timeSinceLastAttack = 0;


            }

        }
        public bool CanAttack(GameObject combotTarget)
        {
            if (combotTarget == null)
            {
                return false;
            }
            healt healtToTest = GetComponent<healt>();
            return healtToTest != null && !healtToTest.IsDead();
        }
        public void Attack(GameObject target)
        {
            GetComponent<ActionShecduler>().StartAction(this);
            targetObject = target.GetComponent<healt>();
        }

        private void TriggerAttack()
        {
            GetComponent<Animator>().ResetTrigger("stopAttack");
            GetComponent<Animator>().SetTrigger("attack");
        }

        void Hit()
        {
            if (targetObject == null)
            {
                return;
            }
            if (defaultWeapon.HasProjectile())
            {
                defaultWeapon.LauncProjectile(rightHandTransform, leftHandTransform, targetObject);
            }
            else
            {
                targetObject.TakeDamage(defaultWeapon.GetDamage());
            }
            
            

        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, targetObject.transform.position) < defaultWeapon.GetRange();
        }

       

        public void Cancel()
        {
            StopAttack();
            targetObject = null;
        }

        private void StopAttack()
        {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("stopAttack");
        }
    }


}
