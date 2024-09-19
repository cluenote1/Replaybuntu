using UnityEngine;
using System.Collections.Generic;

public class MapMaker : MonoBehaviour
{
    public GameObject[] mapPrefabs;  // 랜덤하게 생성될 맵 프리팹 배열
    public GameObject firstTilePrefab;  // 첫 번째 타일을 지정할 프리팹
    public float spawnPositionX = 10f;  // 새로운 타일의 시작 위치
    public float resetPositionX = -10f;  // 타일이 사라질 위치
    public float tileWidth = 20f;  // 각 타일의 가로 크기
    public float moveSpeed = 2f;  // 타일이 왼쪽으로 움직이는 속도
    public Transform mapContainer;  // 생성된 타일을 담을 컨테이너 (선택 사항)
    public int initialTileCount = 3;  // 시작할 때 생성할 타일의 개수
    public float gap = 5f;  // 타일 간의 간격을 조절할 변수

    private Queue<GameObject> activeTiles = new Queue<GameObject>();  // 활성화된 타일을 관리할 큐
    private float lastTilePositionX = 0f;  // 마지막 타일의 X 위치

    void Start()
    {
        if (firstTilePrefab != null)
        {
            // 첫 번째 타일을 설정
            SpawnFirstTile();
        }

        // 나머지 타일을 생성
        for (int i = 1; i < initialTileCount; i++)
        {
            SpawnTile();
        }
    }

    void Update()
    {
        // 모든 활성화된 타일을 왼쪽으로 이동
        foreach (GameObject tile in activeTiles)
        {
            tile.transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
        }

        // 앞쪽 타일이 화면 밖으로 나가면 새로운 타일을 생성하고 오래된 타일은 삭제
        if (activeTiles.Count > 0 && activeTiles.Peek().transform.position.x < resetPositionX)
        {
            DestroyTile();
            SpawnTile();
        }
    }

    void SpawnFirstTile()
    {
        // 첫 번째 타일을 인스턴스화
        GameObject tile = Instantiate(firstTilePrefab, mapContainer);

        // 타일의 위치를 설정
        tile.transform.position = new Vector2(lastTilePositionX + tileWidth / 2, 0);
        lastTilePositionX += tileWidth;  // 마지막 타일 위치 업데이트

        // 새 타일을 큐에 추가
        activeTiles.Enqueue(tile);
    }

    void SpawnTile()
    {
        // 새 타일을 인스턴스화하고 위치를 설정
        GameObject tile = Instantiate(mapPrefabs[Random.Range(0, mapPrefabs.Length)], mapContainer);

        // 타일 간의 간격을 적용하여 위치 설정
        tile.transform.position = new Vector2(lastTilePositionX + tileWidth / 2 + gap / 2, 0);
        lastTilePositionX += tileWidth + gap;  // 마지막 타일 위치를 간격을 포함하여 업데이트

        // 새 타일을 큐에 추가
        activeTiles.Enqueue(tile);
    }

    void DestroyTile()
    {
        // 큐에서 첫 번째 타일을 제거하고 삭제
        GameObject oldTile = activeTiles.Dequeue();
        Destroy(oldTile);

        // 삭제된 타일의 위치를 기반으로 다음 타일의 위치를 업데이트
        lastTilePositionX -= (tileWidth + gap);
    }
}
