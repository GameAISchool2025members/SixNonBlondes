using System.Collections;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class SpawnerController: MonoBehaviour {
    public GameObject prefabToSpawn;
    private RectTransform rectTransform;
    public float spawnInterval = 2.0f;
    public AnimationCurve spawnAnimationCurve;
    public bool useAnimationCurve = false;
    public GameObject parent;

    void Start() {
        rectTransform = GetComponent<RectTransform>();
        if (prefabToSpawn == null) {
            Debug.LogError("Prefab to spawn is not assigned in the SpawnerController.");
            return;
        } else {
            if (rectTransform.rect.width <= 0 || rectTransform.rect.height <= 0) {
                Debug.LogError("RectTransform bounds are invalid. Please ensure the RectTransform has a valid size.");
                return;
            }
        }

        if (prefabToSpawn == null) {
            Debug.LogError("Prefab to spawn is not assigned in the SpawnerController.");
            return;
        }

        StartCoroutine(SpawnPrefab());
    }

    public virtual IEnumerator SpawnPrefab() {
        while (true) {
            Vector2 rectPosition = rectTransform.position;

            float width = rectTransform.rect.width;
            float height = rectTransform.rect.height;

            Vector2 spawnPosition = new(
                rectPosition.x + Random.Range(-width / 2, width / 2),
                rectPosition.y + Random.Range(-height / 2, height / 2)
            );

            GameObject spawnedObject = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity, rectTransform);

            if (spawnedObject == null) {
                Debug.LogError("Failed to instantiate prefab.");
                yield break;
            }

            if (parent) spawnedObject.transform.SetParent(parent.transform);

            spawnedObject.SetActive(true);

            float currentSpawnInterval = spawnInterval;
            if (useAnimationCurve && spawnAnimationCurve != null) {
                currentSpawnInterval = spawnAnimationCurve.Evaluate(Time.time);
            }

            yield return new WaitForSeconds(currentSpawnInterval);
        }
    }   
}
