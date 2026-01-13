using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float damage; //the base damage tied to the sword
    public float force; //the extra damage based on the charge

    private float chargeStart; //when you started holding the attack button
    public float chargeProgress; // how long youve held the charge
    public float fullChargePoint;// how long you need to hold charge to get the projectile

    public int direction; // 1=up 2=right 3=down 4=left. this is all place holder stuff
    public int newDirection; //holds new direction. also place holder

    public Transform attackPoint;//where the script will check for enemies
    public Transform swordAim; // rotates the sword around the player
    public Transform cornerA; // the corner of the hitbox, this is here because I am using overlap area and not coliders. if you think thats a bad call please tell me
    public Transform cornerB; // the other corner of the hitbox

    public GameObject projectile;
    public LayerMask EffectedBySword;

    public AudioSource sxf; //later I will probably add a few more clips or another audio source for the projectile


    // Update is called once per frame
    void Update()
    {
        chargeProgress = Mathf.Clamp(Time.time - chargeStart, 0, fullChargePoint);
        DirectionCheck();

        if (Input.GetKeyDown(KeyCode.J))
        {
            BeginCharge();
        }

        if (Input.GetKeyUp(KeyCode.J))
        {




            if (fullChargePoint == chargeProgress) // checking if youve charged long enough to shoot the projectile
            {
                ChargedEffect();
            }


            Slash(); // no matter the charge you still do the sword slash
        }
    }





    public void DirectionCheck()// im sure we will have another system for this in movement. this is just a place holder
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            newDirection = 1;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            newDirection = 2;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            newDirection = 3;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            newDirection = 4;
        }

        if (newDirection != direction)
        {
            direction = newDirection;
            switch (direction)
            {
                case 1:
                    swordAim.right = new Vector2(0, 0);
                    break;
                case 2:
                    swordAim.right = new Vector2(0, -1);
                    break;
                case 3:
                    swordAim.right = new Vector2(-10, 0.1f);
                    break;
                case 4:
                    swordAim.right = new Vector2(0, 1);
                    break;
            }
        }

    }


    public void BeginCharge()
    {
        chargeStart = Time.time;
    }

    public void Slash()
    {
        sfx.Play();
        force = chargeProgress;

        Collider2D[] hit = Physics2D.OverlapAreaAll(cornerA.position, cornerB.position, EffectedBySword);
        for (int i = 0; i < hit.Length; i++)
        {
            
            /*
            EnemyControler2D Enemy = hit[i].gameObject.GetComponent<EnemyControler>();// place holder names
            if (Enemy != null)
            {
                Enemy.TakeDamage(damage + force); //placeholder names
            }
            */
        }

        chargeProgress = 0;
    }

    public void ChargedEffect()
    {
        Instantiate(projectile, attackPoint.position, swordAim.rotation);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector3(cornerA.position.x, cornerA.position.y), new Vector3(cornerB.position.x, cornerA.position.y));
        Gizmos.DrawLine(new Vector3(cornerB.position.x, cornerA.position.y), new Vector3(cornerB.position.x, cornerB.position.y));
        Gizmos.DrawLine(new Vector3(cornerB.position.x, cornerB.position.y), new Vector3(cornerA.position.x, cornerB.position.y));
        Gizmos.DrawLine(new Vector3(cornerA.position.x, cornerB.position.y), new Vector3(cornerA.position.x, cornerA.position.y));
    }
}






