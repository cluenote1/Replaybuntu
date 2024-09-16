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

    // ���� Ű �Է� Ȯ��
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

    // Ű ���� ����� �߰��� ��� �� �κ��� Ȱ��ȭ�մϴ�.
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

    // ��ư ���� ����� ȣ���մϴ�.
    public void ChangeButton(int index)
    {
        idx = index;
    }
}
