using UnityEngine;
using System.Collections.Generic;

public class MapMaker : MonoBehaviour
{
    public GameObject[] mapPrefabs;  // �����ϰ� ������ �� ������ �迭
    public GameObject firstTilePrefab;  // ù ��° Ÿ���� ������ ������
    public float spawnPositionX = 10f;  // ���ο� Ÿ���� ���� ��ġ
    public float resetPositionX = -10f;  // Ÿ���� ����� ��ġ
    public float tileWidth = 20f;  // �� Ÿ���� ���� ũ��
    public float moveSpeed = 2f;  // Ÿ���� �������� �����̴� �ӵ�
    public Transform mapContainer;  // ������ Ÿ���� ���� �����̳� (���� ����)
    public int initialTileCount = 3;  // ������ �� ������ Ÿ���� ����
    public float gap = 5f;  // Ÿ�� ���� ������ ������ ����

    private Queue<GameObject> activeTiles = new Queue<GameObject>();  // Ȱ��ȭ�� Ÿ���� ������ ť
    private float lastTilePositionX = 0f;  // ������ Ÿ���� X ��ġ

    void Start()
    {
        if (firstTilePrefab != null)
        {
            // ù ��° Ÿ���� ����
            SpawnFirstTile();
        }

        // ������ Ÿ���� ����
        for (int i = 1; i < initialTileCount; i++)
        {
            SpawnTile();
        }
    }

    void Update()
    {
        // ��� Ȱ��ȭ�� Ÿ���� �������� �̵�
        foreach (GameObject tile in activeTiles)
        {
            tile.transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
        }

        // ���� Ÿ���� ȭ�� ������ ������ ���ο� Ÿ���� �����ϰ� ������ Ÿ���� ����
        if (activeTiles.Count > 0 && activeTiles.Peek().transform.position.x < resetPositionX)
        {
            DestroyTile();
            SpawnTile();
        }
    }

    void SpawnFirstTile()
    {
        // ù ��° Ÿ���� �ν��Ͻ�ȭ
        GameObject tile = Instantiate(firstTilePrefab, mapContainer);

        // Ÿ���� ��ġ�� ����
        tile.transform.position = new Vector2(lastTilePositionX + tileWidth / 2, 0);
        lastTilePositionX += tileWidth;  // ������ Ÿ�� ��ġ ������Ʈ

        // �� Ÿ���� ť�� �߰�
        activeTiles.Enqueue(tile);
    }

    void SpawnTile()
    {
        // �� Ÿ���� �ν��Ͻ�ȭ�ϰ� ��ġ�� ����
        GameObject tile = Instantiate(mapPrefabs[Random.Range(0, mapPrefabs.Length)], mapContainer);

        // Ÿ�� ���� ������ �����Ͽ� ��ġ ����
        tile.transform.position = new Vector2(lastTilePositionX + tileWidth / 2 + gap / 2, 0);
        lastTilePositionX += tileWidth + gap;  // ������ Ÿ�� ��ġ�� ������ �����Ͽ� ������Ʈ

        // �� Ÿ���� ť�� �߰�
        activeTiles.Enqueue(tile);
    }

    void DestroyTile()
    {
        // ť���� ù ��° Ÿ���� �����ϰ� ����
        GameObject oldTile = activeTiles.Dequeue();
        Destroy(oldTile);

        // ������ Ÿ���� ��ġ�� ������� ���� Ÿ���� ��ġ�� ������Ʈ
        lastTilePositionX -= (tileWidth + gap);
    }
}
