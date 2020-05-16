using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyDragon : MonoBehaviour
{
    [SerializeField] private string animalName;
    [SerializeField] private int hp;

    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float flySpeed;
    private float waitSpeed = 0.0f;
    [SerializeField] private float turningSpeed = 0.01f;
    
    private float applySpeed;
    private Vector3 direction;
    

    //Status
    private bool isAction;
    private bool isInit;
    private bool isWalking;
    private bool isRunning;
    private bool isFlying;
    private bool isDie;


    [SerializeField] private float initTime;
    [SerializeField] private float waitTime;
    [SerializeField] private float walkTime;
    [SerializeField] private float runTime;    
    [SerializeField] private float flyTime;
    private float currentTime;

    //Component
    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody rigid;

    void Start()
    {
        currentTime = initTime;
        isInit = true;
        isAction = true;  
        ElapseTime(); 
    }

    void Update()
    {
        if(!isDie)
        {
            Move();
            Rotation();
            ElapseTime();
        } 
       
    }
    private void Move()
    {
        if((isWalking || isRunning || isFlying) && !isInit )
        {
            rigid.MovePosition(transform.position + (-transform.forward * applySpeed * Time.deltaTime));
        }
    }
    private void Rotation()
    {
        if((isWalking || isRunning || isFlying) && !isInit )
        {
            Vector3 _rotation = Vector3.Lerp(transform.eulerAngles, new Vector3(0f, direction.y, 0f), turningSpeed);
            rigid.MoveRotation(Quaternion.Euler(_rotation));
        }
    }    
    private void DamageDirection(Vector3 _targetPos)
    {
        direction = Quaternion.LookRotation(transform.position - _targetPos).eulerAngles;   
    }

    private void ElapseTime()
    {
        if(isAction)
        {
            currentTime -= Time.deltaTime;
            if(currentTime <= 0)
            {
                isInit = false;
                ReSet();
            }        
        }
    }

    private void ReSet()
    {
        isWalking = false; isFlying = false; isRunning = false; isAction = true;
        applySpeed = waitSpeed;
        anim.SetBool("Walk", isWalking);
        anim.SetBool("Run", isRunning);
        anim.SetBool("Fly", isFlying);
        direction.Set(0f, Random.Range(0f, 180.0f), 0f);
        RandomAction();
    }

    private void RandomAction()
    {
        int _random = Random.Range(0,4);

        if(_random == 0)
            Wait();
        else if(_random == 1)
            Walk();
        else if(_random == 2)
            Run();
        else if(_random == 3)
            Fly();
    }

    private void Wait()
    {
        currentTime = waitTime;
        applySpeed = waitSpeed;
        isWalking = false; isFlying = false; isRunning = false;
    }
    private void Walk()
    {
        currentTime = walkTime;  
        applySpeed = walkSpeed;   
        isRunning = false; isFlying = false;
        isWalking = true;
        anim.SetBool("Walk", isWalking);   
    }

    private void Run()
    {
        currentTime = runTime;
        applySpeed = runSpeed;
        isWalking = false; isFlying = false;   
        isRunning = true;
        anim.SetBool("Run", isRunning);
    }
    private void Fly()
    {        
        currentTime = flyTime;
        applySpeed = flySpeed;
        isWalking = false; isRunning = false;
        isFlying = true;
        anim.SetBool("Fly", isFlying);              
    }

    public void Damage(int _dmg, Vector3 _targetPos)
    {
        if(!isDie && !isInit)
        {
            hp -= _dmg;

            if(hp <= 0)
            {
                Die();
                return;
            }
            anim.SetTrigger("GetHit");
            DamageDirection(_targetPos);
        }
    }

    private void Die()
    {
        isWalking = false;
        isRunning = false;
        isFlying = false;
        isDie = true;        
        anim.SetTrigger("Die");
        Destroy(gameObject, 2.5f);
        EnemyController.currentEnemyCount --;
    }
}
