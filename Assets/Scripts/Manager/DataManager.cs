using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class DataManager : MonoBehaviour
{
    // �̱��� ������ ����Ͽ� ���� �� ��𼭳� ������ �� �ְ� ��
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
        // �ν��Ͻ��� ������ �� ������Ʈ�� �����ϰ� instance�� ����
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);  // �� ��ȯ �ÿ��� �ı����� �ʵ��� ����
            instance = this;
        }
        else
        {
            Destroy(gameObject);  // �̹� �ν��Ͻ��� �����ϸ� �� ������Ʈ �ı�
        }
    }

    // ������ ��������, ���¸� �����ϴ� ������
    public int Score = 0;
    public int Stage = 0;
    public bool isDead = false;  // �÷��̾ �׾����� ����
    public bool isEnd = false;   // ������ �������� ����

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void FirstLoad()
    {
        // ���� ���� �� ������ ����Ʈ�� �׷��� ���� �ʱ�ȭ
        Application.targetFrameRate = 70;
        QualitySettings.vSyncCount = 0;  // vSync ��Ȱ��ȭ (������ ���� ����)

        // �÷��̾� ��� ����: �ְ� ����, �����, ȿ���� ���� Ȯ��
        if (!PlayerPrefs.HasKey("BestScore"))
        {
            PlayerPrefs.SetInt("BestScore", 0);  // ó�� ������ �� �ְ� ���� �⺻�� 0
        }
        if (!PlayerPrefs.HasKey("BGMScale"))
        {
            PlayerPrefs.SetFloat("BGMScale", 1f);  // ����� ���� �⺻�� 1 (100%)
        }
        if (!PlayerPrefs.HasKey("SFXScale"))
        {
            PlayerPrefs.SetFloat("SFXScale", 1f);  // ȿ���� ���� �⺻�� 1 (100%)
        }
    }
}
