using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSetting : MonoBehaviour
{
    [SerializeField] private float distance = 20f;

    public float Distance => distance;

    public int currentGoldCoins = 0;
    private int maxGoldCoins;
    private bool isGoldMap;

    private void Awake()
    {
        // �ڽ� ������Ʈ �� Tag�� "GoldCoin"�� ������Ʈ�� ī��Ʈ
        maxGoldCoins = 0; // Initialize to 0
        Transform[] myChildren = this.GetComponentsInChildren<Transform>();

        foreach (Transform child in myChildren)
        {
            if (child.CompareTag("GoldCoin"))
                maxGoldCoins++;
        }

        isGoldMap = maxGoldCoins > 0;
    }

    private void Update()
    {
        // x�� �������� ���� �Ÿ� �ڷ� �̵��� ������Ʈ�� ����
        if (transform.position.x < -(distance + 30))
        {
            Debug.Log("Destroying map: " + transform.position.x); // Debug log
            Destroy(gameObject);
        }

        // ��� ��� ������ ������ ���ʽ��� ȹ��
        if (currentGoldCoins == maxGoldCoins && isGoldMap)
        {
            Bonus();
        }
    }

    private void Bonus()
    {
        isGoldMap = false;
        GameManager gameManager = FindObjectOfType<GameManager>();
        if (gameManager != null)
        {
            gameManager.Bonus();
        }
        else
        {
            Debug.LogError("GameManager not found!");
        }
    }
}
