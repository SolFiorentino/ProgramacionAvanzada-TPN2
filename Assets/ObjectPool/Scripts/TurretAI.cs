using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAI : MonoBehaviour
{

    public enum TurretType
    {
        Single = 1,
        Dual = 2,
        Catapult = 3,
    }

    public GameObject currentTarget;
    public Transform turreyHead;

    public float attackDist = 10.0f;
    public float attackDamage;
    public float shootCoolDown;
    private float timer;
    public float loockSpeed;

    public Vector3 randomRot;
    public Animator animator;

    [Header("[Turret Type]")]
    public TurretType turretType = TurretType.Single;

    public Transform muzzleMain;
    public Transform muzzleSub;
    public GameObject muzzleEff;
    public GameObject bullet;
    private bool shootLeft = true;

    private Transform lockOnPos;

    void Start()
    {
        InvokeRepeating("ChackForTarget", 0, 0.2f); 

        if (transform.GetChild(0).GetComponent<Animator>())
        {
            animator = transform.GetChild(0).GetComponent<Animator>();
        }

        randomRot = new Vector3(0, Random.Range(0, 359), 0);
    }

    void Update()
    {
        if (currentTarget != null)
        {
            FollowTarget();

            float currentTargetDist = Vector3.Distance(transform.position, currentTarget.transform.position);
            if (currentTargetDist > attackDist)
            {
                currentTarget = null;
            }
        }
        else
        {
            IdleRitate();
        }

        timer += Time.deltaTime;
        if (timer >= shootCoolDown)
        {
            if (currentTarget != null)
            {
                timer = 0;

                if (animator != null)
                {
                    animator.SetTrigger("Fire");
                    ShootTrigger();
                }
                else
                {
                    ShootTrigger();
                }
            }
        }

        
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Clic detectado.");
            ChackForTarget();

            if (currentTarget != null)
            {
                Debug.Log("Disparando al objetivo.");
                ShootTrigger();
            }
            else
            {
                Debug.Log("No hay objetivo para disparar."); 
            }
        }
    }

    private void ChackForTarget()
    {
        Collider[] colls = Physics.OverlapSphere(transform.position, attackDist);
        float distAway = Mathf.Infinity;

        for (int i = 0; i < colls.Length; i++)
        {
            if (colls[i].tag == "Player") 
            {
                float dist = Vector3.Distance(transform.position, colls[i].transform.position);
                if (dist < distAway)
                {
                    currentTarget = colls[i].gameObject;
                    distAway = dist;
                }
            }
        }
    }

    private void FollowTarget()
    {
        Vector3 targetDir = currentTarget.transform.position - turreyHead.position;
        targetDir.y = 0;

        if (turretType == TurretType.Single)
        {
            turreyHead.forward = targetDir;
        }
        else
        {
            turreyHead.transform.rotation = Quaternion.RotateTowards(turreyHead.rotation, Quaternion.LookRotation(targetDir), loockSpeed * Time.deltaTime);
        }
    }

    private void ShootTrigger()
    {
        Debug.Log("Llamada a ShootTrigger"); 
        Shoot(currentTarget);
    }

    public void IdleRitate()
    {
        bool refreshRandom = false;

        if (turreyHead.rotation != Quaternion.Euler(randomRot))
        {
            turreyHead.rotation = Quaternion.RotateTowards(turreyHead.transform.rotation, Quaternion.Euler(randomRot), loockSpeed * Time.deltaTime * 0.2f);
        }
        else
        {
            refreshRandom = true;

            if (refreshRandom)
            {
                int randomAngle = Random.Range(0, 359);
                randomRot = new Vector3(0, randomAngle, 0);
                refreshRandom = false;
            }
        }
    }

    public void Shoot(GameObject go)
    {
        Debug.Log("Disparo iniciado."); 
        if (turretType == TurretType.Catapult)
        {
            lockOnPos = go.transform;
            GameObject missleGo = ObjectPooling.SharedInstance.GetPooledObject(1);
            if (missleGo != null)
            {
                missleGo.transform.position = muzzleMain.transform.position;
                missleGo.transform.rotation = muzzleMain.rotation;
                missleGo.SetActive(true);

                Projectile projectile = missleGo.GetComponent<Projectile>();
                projectile.target = lockOnPos;
            }
        }
        else if (turretType == TurretType.Dual)
        {
            if (shootLeft)
            {
                GameObject missleGo = ObjectPooling.SharedInstance.GetPooledObject(3);
                if (missleGo != null)
                {
                    missleGo.transform.position = muzzleMain.transform.position;
                    missleGo.transform.rotation = muzzleMain.rotation;
                    missleGo.SetActive(true);

                    Projectile projectile = missleGo.GetComponent<Projectile>();
                    projectile.target = transform.GetComponent<TurretAI>().currentTarget.transform;
                }
            }
            else
            {
                GameObject missleGo = ObjectPooling.SharedInstance.GetPooledObject(3);
                if (missleGo != null)
                {
                    missleGo.transform.position = muzzleSub.transform.position;
                    missleGo.transform.rotation = muzzleSub.rotation;
                    missleGo.SetActive(true);

                    Projectile projectile = missleGo.GetComponent<Projectile>();
                    projectile.target = transform.GetComponent<TurretAI>().currentTarget.transform;
                }
            }

            shootLeft = !shootLeft;
        }
        else
        {
            GameObject missleGo = ObjectPooling.SharedInstance.GetPooledObject(2);
            if (missleGo != null)
            {
                missleGo.transform.position = muzzleMain.transform.position;
                missleGo.transform.rotation = muzzleMain.rotation;
                missleGo.SetActive(true);

                Projectile projectile = missleGo.GetComponent<Projectile>();
                projectile.target = currentTarget.transform;
            }
        }
    }
}



