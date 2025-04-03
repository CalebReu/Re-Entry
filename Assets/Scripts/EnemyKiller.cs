using UnityEngine;

public class EnemyKiller : MonoBehaviour
{
    public EnemyController ec;

    public void Die() {
       ec.die();
    }
}
