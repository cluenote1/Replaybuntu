using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class DataManager : MonoBehaviour
{
    // 싱글톤 패턴을 사용하여 게임 내 어디서나 접근할 수 있게 함
    static DataManager instance;

    public static DataManager Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        // 인스턴스가 없으면 이 오브젝트를 유지하고 instance로 설정
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);  // 씬 전환 시에도 파괴되지 않도록 설정
            instance = this;
        }
        else
        {
            Destroy(gameObject);  // 이미 인스턴스가 존재하면 이 오브젝트 파괴
        }
    }

    // 점수와 스테이지, 상태를 관리하는 변수들
    public int Score = 0;
    public int Stage = 0;
    public bool isDead = false;  // 플레이어가 죽었는지 여부
    public bool isEnd = false;   // 게임이 끝났는지 여부

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void FirstLoad()
    {
        // 게임 시작 시 프레임 레이트와 그래픽 설정 초기화
        Application.targetFrameRate = 70;
        QualitySettings.vSyncCount = 0;  // vSync 비활성화 (프레임 제한 해제)

        // 플레이어 기록 관리: 최고 점수, 배경음, 효과음 설정 확인
        if (!PlayerPrefs.HasKey("BestScore"))
        {
            PlayerPrefs.SetInt("BestScore", 0);  // 처음 시작할 때 최고 점수 기본값 0
        }
        if (!PlayerPrefs.HasKey("BGMScale"))
        {
            PlayerPrefs.SetFloat("BGMScale", 1f);  // 배경음 볼륨 기본값 1 (100%)
        }
        if (!PlayerPrefs.HasKey("SFXScale"))
        {
            PlayerPrefs.SetFloat("SFXScale", 1f);  // 효과음 볼륨 기본값 1 (100%)
        }
    }
}
