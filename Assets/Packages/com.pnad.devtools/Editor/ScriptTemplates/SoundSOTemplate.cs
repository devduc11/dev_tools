namespace EditorTools.ProjectInit.Templates
{
    public static class SoundSOTemplate
    {
        public static string GetContent()
        {
            return
@"using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = ""SoundSO"", menuName = ""ScriptableObject/SoundSO"", order = 1)]
public class SoundSO : ScriptableObject
{
    [SerializeField] List<SoundElement> list;

    public List<SoundElement> List
    {
        get => list;
        set => list = value;
    }

    public SoundElement GetSoundBySoundType(SoundType soundType)
    {
        foreach (var item in list)
        {
            if (item.SoundType == soundType)
            {
                return item;
            }
        }

        return null;
    }

    [ContextMenu(""Get Sound Type"")]
    private void GetSoundTypeByAudioClipName()
    {
        Debug.Log($""datdb -1111"");
        string name = ""Fairy Dust"";
        foreach (var item in list)
        {
            if (item.AudioClip.name == name)
            {
                Debug.Log($""datdb - {item.SoundType}"");
                return;
            }
        }
    }
}

public enum SoundType
{
    None,
    Main, // nhạc nền home và ingame
    Button, // click vào các button
}

[System.Serializable]
public class SoundElement
{
    [SerializeField] SoundType soundType;
    [SerializeField] AudioClip audioClip;

    // (-80 dB) - (0 dB) - (+20 dB)
    //  0.0001f  -  1.0f  -  10.0f
    [Range(0.0001f, 10.0f)]
    public float dB;

    public SoundType SoundType
    {
        get => soundType;
        set => soundType = value;
    }

    public AudioClip AudioClip
    {
        get => audioClip;
        set => audioClip = value;
    }
}
";
        }
    }
}