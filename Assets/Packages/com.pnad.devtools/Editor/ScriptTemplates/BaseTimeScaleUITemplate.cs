namespace EditorTools.ProjectInit.Templates
{
    public static class BaseTimeScaleUITemplate
    {
        public static string GetContent()
        {
            return
@"using Eagle.BaseGame;
using UnityEngine;

public class BaseTimeScaleUI : BaseUI
{
    protected override void OnEnable()
    {
        base.OnEnable();
        Time.timeScale = 0;
    }

    protected override void Invisible()
    {
        base.Invisible();

        GameObject uiOnTop = GetUIOnTop();
        if(uiOnTop == null)
        {
            Time.timeScale = 1;
            return;
        }
        
        BaseTimeScaleUI baseTimeScaleUI = uiOnTop.GetComponent<BaseTimeScaleUI>();
        if(baseTimeScaleUI == null)
        {
            Time.timeScale = 1;
        }
       
    }

    private GameObject GetUIOnTop()
    {
        Transform mainCanvas = UIManager.Instance.MainCanvas;
        for (int i = mainCanvas.childCount - 1; i >= 0 ; i--)
        {
            GameObject child = mainCanvas.GetChild(i).gameObject;        
            if(child.activeInHierarchy)
            {
                return child;
            }
        }
        return null;
    }

}";
        }
    }
}