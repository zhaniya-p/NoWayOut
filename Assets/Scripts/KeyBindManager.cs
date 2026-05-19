using UnityEngine;
using TMPro;
using System;
using UnityEngine.InputSystem;

public class KeybindManager : MonoBehaviour
{
    public static KeybindManager Instance;

    [Header("UI Text")]
    public TMP_Text forwardText;
    public TMP_Text backwardText;
    public TMP_Text leftText;
    public TMP_Text rightText;
    public TMP_Text interactText;
    public TMP_Text runText;

    [Header("Current Keys")]
    public KeyCode forwardKey = KeyCode.W;
    public KeyCode backwardKey = KeyCode.S;
    public KeyCode leftKey = KeyCode.A;
    public KeyCode rightKey = KeyCode.D;
    public KeyCode interactKey = KeyCode.F;
    public KeyCode runKey = KeyCode.LeftShift;

    private Action<KeyCode> onKeyChanged;

    void Awake()
    {
        if (Instance == null)
            Instance = this;

        LoadKeys();
        UpdateUI();
    }

    void Update()
    {
        if (onKeyChanged != null)
        {
            foreach (Key key in Enum.GetValues(typeof(Key)))
            {
                if (key == Key.None)
                    continue;

                if (Keyboard.current[key].wasPressedThisFrame)
                {
                    KeyCode convertedKey =
                        ConvertToKeyCode(key);

                    if (convertedKey == KeyCode.None)
                        continue;

                    onKeyChanged.Invoke(convertedKey);

                    onKeyChanged = null;

                    SaveKeys();
                    UpdateUI();

                    break;
                }
            }
        }
    }

    public void ChangeForwardKey()
    {
        onKeyChanged = key => forwardKey = key;
    }

    public void ChangeBackwardKey()
    {
        onKeyChanged = key => backwardKey = key;
    }

    public void ChangeLeftKey()
    {
        onKeyChanged = key => leftKey = key;
    }

    public void ChangeRightKey()
    {
        onKeyChanged = key => rightKey = key;
    }

    public void ChangeInteractKey()
    {
        onKeyChanged = key => interactKey = key;
    }

    public void ChangeRunKey()
    {
        onKeyChanged = key => runKey = key;
    }

    void UpdateUI()
    {
        forwardText.text = forwardKey.ToString();
        backwardText.text = backwardKey.ToString();
        leftText.text = leftKey.ToString();
        rightText.text = rightKey.ToString();
        interactText.text = interactKey.ToString();
        runText.text = runKey.ToString();
    }

    void SaveKeys()
    {
        PlayerPrefs.SetString("ForwardKey", forwardKey.ToString());
        PlayerPrefs.SetString("BackwardKey", backwardKey.ToString());
        PlayerPrefs.SetString("LeftKey", leftKey.ToString());
        PlayerPrefs.SetString("RightKey", rightKey.ToString());
        PlayerPrefs.SetString("InteractKey", interactKey.ToString());
        PlayerPrefs.SetString("RunKey", runKey.ToString());

        PlayerPrefs.Save();
    }

    void LoadKeys()
    {
        if (PlayerPrefs.HasKey("ForwardKey"))
            forwardKey = (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("ForwardKey"));

        if (PlayerPrefs.HasKey("BackwardKey"))
            backwardKey = (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("BackwardKey"));

        if (PlayerPrefs.HasKey("LeftKey"))
            leftKey = (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("LeftKey"));

        if (PlayerPrefs.HasKey("RightKey"))
            rightKey = (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("RightKey"));

        if (PlayerPrefs.HasKey("InteractKey"))
            interactKey = (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("InteractKey"));

        if (PlayerPrefs.HasKey("RunKey"))
            runKey = (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("RunKey"));
    }

    KeyCode ConvertToKeyCode(Key key)
    {
        try
        {
            return (KeyCode)System.Enum.Parse(
                typeof(KeyCode),
                key.ToString(),
                true);
        }
        catch
        {
            return KeyCode.None;
        }
    }
}