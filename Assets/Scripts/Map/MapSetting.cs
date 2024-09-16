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
        // 자식 오브젝트 중 Tag가 "GoldCoin"인 오브젝트를 카운트
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
        // x축 기준으로 일정 거리 뒤로 이동한 오브젝트를 삭제
        if (transform.position.x < -(distance + 30))
        {
            Debug.Log("Destroying map: " + transform.position.x); // Debug log
            Destroy(gameObject);
        }

        // 모든 골드 코인을 모으면 보너스를 획득
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
