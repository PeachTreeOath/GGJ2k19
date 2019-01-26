using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SpawnZone : MonoBehaviour
{
    [SerializeField]
    public List<Rect> spawnZones = new List<Rect>();

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 0, 1, .4f);
        foreach(Rect item in spawnZones)
        {
            Gizmos.DrawCube(item.center, item.size);
        }
    }

    public Vector2 GetSpawnLocation()
    {
        Dictionary<Rect,int> weights = new Dictionary<Rect, int>();
        Vector2 spawnPos;

        float totalArea = 0;

        foreach(Rect item in spawnZones)
        {
            totalArea += item.height * item.width;
        }

        int i = 0;
        foreach(Rect item in spawnZones)
        {
            float itemArea = item.height * item.width;

            weights.Add(item, (int)(100*(itemArea / totalArea)));
        }

        Rect zone = WeightedRandomizer.From(weights).TakeOne();

        spawnPos = new Vector2(Random.Range(zone.xMin, zone.xMax), Random.Range(zone.xMin, zone.yMax));

        return spawnPos; 
    }
}
