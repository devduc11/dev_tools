namespace EditorTools.ProjectInit.Templates
{
    public static class UIManagerTemplate
    {
        public static string GetContent()
        {
            return
@"using Eagle.BaseGame;
using Teo.AutoReference;
using UnityEngine;

public class UIManager : BaseUIManager<UIManager>
{
    [SerializeField, FindInScene] private Canvas canvas;
    public Canvas Canvas => canvas;

    [SerializeField, FindInScene, Name(""MainCanvas"")]
    private Transform parent;
    public Transform MainCanvas => parent;

    private RectTransform rectTransformCanvas;
    public Vector2 CanvasSize => rectTransformCanvas.rect.size;

    protected override string GetFolderPrefabs()
    {
        return ""Assets/_Project/Prefab/UI"";
    }

    protected override Transform GetParent()
    {
        return parent;
    }

    protected override void OnInitCompleted()
    {
        Show<LoadingUI>();
    }

    protected override void Awake()
    {
        base.Awake();
        rectTransformCanvas = canvas.GetComponent<RectTransform>();
        ShowPauseUI(false);
    }

    public new T Show<T>(System.Action<T> onPreShow = null) where T : BaseUI
    {
        base.Show<T>(onPreShow);
        return GetUI<T>();
    }

    public Vector2 WorldToUI(Vector3 worldPos)
    {
        Vector2 screenPoint = Camera.main.WorldToScreenPoint(worldPos);
        return screenPoint;
    }
   
}";
        }
    }
}