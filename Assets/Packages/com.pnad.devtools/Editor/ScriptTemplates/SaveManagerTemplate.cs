namespace EditorTools.ProjectInit.Templates
{
    public static class SaveManagerTemplate
    {
        public static string GetContent()
        {
            return
@"using Eagle.BaseGame;
using UnityEngine;

public class SaveManager : BaseSaveManager<SaveManager, DataSave>
{
    void OnDestroy()
    {
#if UNITY_EDITOR
        SaveData();
#endif
    }

    [ContextMenu(""Clear Data"")]
    public override void ClearData()
    {
        base.ClearData();
    }

    [ContextMenu(""Save Data"")]
    public override void SaveData()
    {
        base.SaveData();
    }

    #region Music

    #endregion

    protected override void Migrate(int fromVersion)
    {
    }

    protected override int Version()
    {
        return Constant.DATA_SAVE_VERSION;
    }

    protected override void LoadData()
    {
        base.LoadData();
    }
}";
        }
    }
}