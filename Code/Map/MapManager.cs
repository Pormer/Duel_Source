using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    [SerializeField] private MapInfo mapInfo;
    [SerializeField] Tilemap floorTile;

    private void Awake()
    {
        //mapInfo.Initialize(floorTile);
    }
}
