using GraphProcessor;
using UnityEditor;
using UnityEngine.UIElements;

namespace WobbleShorts
{
    [CustomEditor(typeof(BaseGraph), true)]
    public class GraphAssetInspector : GraphInspector
    {
        protected override void CreateInspector()
        {
            base.CreateInspector();

            root.Add(new Button(() => EditorWindow.GetWindow<DefaultGraphWindow>().InitializeGraph(target as BaseGraph))
            {
                text = "Show Editor"
            });
        }
    }

}