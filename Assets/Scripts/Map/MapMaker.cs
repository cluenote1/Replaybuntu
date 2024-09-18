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
    [SerializeField] private float mapGap = 10f;  // �� ���� ���� ����

    private MapMove mapMove;
    private float lastMapPositionX = 0;

    private void Awake()
    {
        mapMove = GetComponent<MapMove>();
    }

    private void Update()
    {
        if (mapMove.currentDistance > mapMove.maxDistance)
        {
            SpawnRandomMap();
        }
    }

    private void SpawnRandomMap()
    {
        GameObject go;

        if (DataManager.Instance.isEnd)
        {
            go = Instantiate(noneMap);
        }
        else
        {
            int random = UnityEngine.Random.Range(0, stageMap[DataManager.Instance.Stage].map.Count);
            go = Instantiate(stageMap[DataManager.Instance.Stage].map[random]);
        }

        mapMove.distance += mapMove.maxDistance;
        mapMove.currentDistance -= mapMove.maxDistance;
        mapMove.maxDistance = go.GetComponent<MapSetting>().Distance;

        // ���ο� ���� ��ġ�� ������ �� ��ġ + �������� ����
        go.transform.position = new Vector3(lastMapPositionX + mapGap, transform.position.y, 0);
        lastMapPositionX = go.transform.position.x;

        go.transform.parent = transform;
    }
}
