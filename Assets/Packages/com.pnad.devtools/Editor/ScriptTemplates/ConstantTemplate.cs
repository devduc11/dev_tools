namespace EditorTools.ProjectInit.Templates
{
    public static class ConstantTemplate
    {
        public static string GetContent()
        {
            return
@"public static class Constant
{
   public const int DATA_SAVE_VERSION = 1;
}
";
        }
    }
}