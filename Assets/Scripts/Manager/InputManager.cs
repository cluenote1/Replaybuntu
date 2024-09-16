using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public KeyCode[] jumpKeys;

    // 점프 키 입력 확인
    public bool GetJumpKey()
    {
        foreach (KeyCode key in jumpKeys)
        {
            if (Input.GetKeyDown(key))
            {
                return true;
            }
        }
        return false;
    }

    // 키 설정 기능을 추가할 경우 이 부분을 활성화합니다.
    private void OnGUI()
    {
        Event keyEvent = Event.current;
        if (keyEvent.isKey && idx != -1)
        {
            jumpKeys[idx] = keyEvent.keyCode;
            idx = -1;
        }
    }

    private int idx = -1;

    // 버튼 변경 기능을 호출합니다.
    public void ChangeButton(int index)
    {
        idx = index;
    }
}
