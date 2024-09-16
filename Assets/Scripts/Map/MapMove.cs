using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMove : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private float currentMapSpeed;
    [SerializeField] private float moveSpeed = 14f;
    [SerializeField] private float changeSpeed = 6f;
    [SerializeField] private List<float> stageTime;

    public int MaxStage => stageTime.Count;

    public float distance = 0f;
    public float currentDistance = 0f;
    public float time = 0f;
    public float maxTime;

    public float maxDistance = 0f;

    private void Start()
    {
        // 스테이지에 맞는 초기 maxTime 설정
        maxTime = stageTime[DataManager.Instance.Stage];
    }

    private void Update()
    {
        if (DataManager.Instance.isDead)
            return;

        // 현재 스테이지에 따른 이동 속도 조정
        currentMapSpeed = moveSpeed + (DataManager.Instance.Stage * changeSpeed);
        currentDistance += Time.deltaTime * currentMapSpeed;

        // 맵을 2D 좌표에서 좌측으로 이동
        transform.Translate(Vector2.left * Time.deltaTime * currentMapSpeed);

        time += Time.deltaTime;

        // 스테이지 변경 조건 확인
        if (time > maxTime && DataManager.Instance.Stage + 1 < MaxStage)
        {
            UpdateStage();
            time -= maxTime;
            maxTime = stageTime[DataManager.Instance.Stage];
        }
        else if (time > maxTime && DataManager.Instance.Stage + 1 >= MaxStage)
        {
            gameManager.Ending();
        }
    }

    private void UpdateStage()
    {
        gameManager.SpeedUp();
    }
}
