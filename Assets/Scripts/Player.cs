using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public int life;
    [SerializeField] int damage;
    public UIManager UIManager;

    void OnTriggerEnter(Collider other) {
        Destroy(other.gameObject);
        life -= damage;
        UIManager.DeleteHeart();
    }
}

