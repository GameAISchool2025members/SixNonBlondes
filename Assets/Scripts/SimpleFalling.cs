using UnityEngine;

public class SimpleFalling: MonoBehaviour {
    [SerializeField] private float speed;
    
    void Update() {
        transform.position = new(transform.position.x, transform.position.y - speed * Time.deltaTime, transform.position.z);
    }
}
