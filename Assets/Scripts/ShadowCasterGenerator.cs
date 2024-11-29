using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Rendering.Universal;

public class ShadowCasterGenerator : MonoBehaviour
{
    [Header("Prefabs and Settings")]
    [SerializeField] private GameObject shadowCasterPrefab;
    [SerializeField] private float zOffset = 0f;

    private Tilemap tilemap;
    private CompositeCollider2D compositeCollider;
    private GameObject shadowCasterGO;
    private PolygonCollider2D polygonCollider;
    private ShadowCaster2D shadowCaster;

    private void Awake()
    {
        tilemap = GetComponent<Tilemap>();
        compositeCollider = GetComponent<CompositeCollider2D>();
        shadowCasterGO = Instantiate(shadowCasterPrefab, transform);
        shadowCasterGO.transform.position = transform.position + new Vector3(0, 0, zOffset);
        if (!shadowCasterGO.TryGetComponent(out polygonCollider))
        {
            return;
        }

        if (!shadowCasterGO.TryGetComponent(out shadowCaster))
        {
            return;
        }
        GenerateShadowCasters();
    }

    public void GenerateShadowCasters()
    {
        if (compositeCollider.pathCount == 0)
        {
            return;
        }
        polygonCollider.pathCount = 0;
        for (int i = 0; i < compositeCollider.pathCount; i++)
        {
            Vector2[] path = new Vector2[compositeCollider.GetPathPointCount(i)];
            compositeCollider.GetPath(i, path);


            polygonCollider.pathCount++;
            polygonCollider.SetPath(polygonCollider.pathCount - 1, path);
        }
    }
    public void OnTilemapUpdated()
    {
        GenerateShadowCasters();
    }
}
