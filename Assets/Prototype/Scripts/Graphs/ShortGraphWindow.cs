namespace WobbleShorts
{
	using UnityEngine;
	using UnityEditor;
	using GraphProcessor;

	public class ShortGraphWindow : BaseGraphWindow
	{
		BaseGraph tmpGraph;

		[MenuItem("Window/Short Graph")]
		public static BaseGraphWindow OpenWithTmpGraph()
		{
			var graphWindow = CreateWindow<ShortGraphWindow>();

			// When the graph is opened from the window, we don't save the graph to disk
			graphWindow.tmpGraph = ScriptableObject.CreateInstance<BaseGraph>();
			graphWindow.tmpGraph.hideFlags = HideFlags.HideAndDontSave;
			graphWindow.InitializeGraph(graphWindow.tmpGraph);

			graphWindow.Show();

			return graphWindow;
		}

		protected override void OnDestroy()
		{
			graphView?.Dispose();
			DestroyImmediate(tmpGraph);
		}

		protected override void InitializeWindow(BaseGraph graph)
		{
			titleContent = new GUIContent("Short: " + graph.name);

			if (graphView == null)
				graphView = new BaseGraphView(this);

			rootView.Add(graphView);
		}
	}
}