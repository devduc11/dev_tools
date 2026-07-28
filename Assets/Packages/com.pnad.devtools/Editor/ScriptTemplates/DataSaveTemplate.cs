namespace EditorTools.ProjectInit.Templates
{
    public static class DataSaveTemplate
    {
        public static string GetContent()
        {
            return
@"using System.Collections.Generic;
using Eagle.BaseGame;
using UnityEngine;

[System.Serializable]
public partial class DataSave : BaseDataSave
{
    [SerializeField] private SaveValue<bool> isSfxOn = new(true);
    [SerializeField] private SaveValue<bool> isMusicOn = new(true);
    [SerializeField] private SaveValue<bool> isVibrateOn = new(true);
    [SerializeField] private SaveValue<bool> isRate = new(false);
    [SerializeField] private SaveValue<int> session = new(0);
}";
        }
    }
}