using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMaker : MonoBehaviour
{
    [Serializable]
    public class Map
    {
        public List<GameObject> map = new List<GameObject>();
    }

    public List<Map> stageMap = new List<Map>();
    [SerializeField] private GameObject noneMap;
    [SerializeField] private float spawnThreshold = 20f; // 맵 조각을 생성할 거리 기준

    private MapMove mapMove;
    private GameObject lastMap;

    private void Awake()
    {
        mapMove = GetComponent<MapMove>();
    }

    private void Start()
    {
        SpawnInitialMap();
    }

    private void Update()
    {
        // 현재 거리와 최대 거리의 차이가 spawnThreshold보다 작으면 새 맵 생성
        if (mapMove.currentDistance > mapMove.maxDistance - spawnThreshold)
        {
            SpawnNewMap();
        }
    }

    private void SpawnInitialMap()
    {
        // 초기 맵 생성
        if (stageMap[DataManager.Instance.Stage].map.Count > 0)
        {
            GameObject initialMap = Instantiate(stageMap[DataManager.Instance.Stage].map[0]);
            mapMove.distance += mapMove.maxDistance;
            mapMove.currentDistance -= mapMove.maxDistance;
            mapMove.maxDistance = initialMap.GetComponent<MapSetting>().Distance;
            initialMap.transform.position = transform.position + Vector3.right * mapMove.distance;
            initialMap.transform.parent = transform;
            lastMap = initialMap;
        }
        else
        {
            Debug.LogError("No map prefab found in stageMap for the current stage.");
        }
    }

    private void SpawnNewMap()
    {
        if (DataManager.Instance.isEnd)
        {
            GameObject go1 = Instantiate(noneMap);
            mapMove.distance += mapMove.maxDistance;
            mapMove.currentDistance -= mapMove.maxDistance;
            mapMove.maxDistance = go1.GetComponent<MapSetting>().Distance;
            go1.transform.position = transform.position + Vector3.right * mapMove.distance;
            go1.transform.parent = transform;
            lastMap = go1;
            return;
        }

        if (stageMap[DataManager.Instance.Stage].map.Count > 0)
        {
            int random = UnityEngine.Random.Range(0, stageMap[DataManager.Instance.Stage].map.Count);
            GameObject go = Instantiate(stageMap[DataManager.Instance.Stage].map[random]);

            mapMove.distance += mapMove.maxDistance;
            mapMove.currentDistance -= mapMove.maxDistance;
            mapMove.maxDistance = go.GetComponent<MapSetting>().Distance;
            go.transform.position = transform.position + Vector3.right * mapMove.distance;
            go.transform.parent = transform;
            lastMap = go;
        }
        else
        {
            Debug.LogError("No map prefab found in stageMap for the current stage.");
        }
    }
}
