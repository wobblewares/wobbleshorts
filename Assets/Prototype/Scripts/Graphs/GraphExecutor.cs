using NodeGraphProcessor.Examples;
using UnityEngine;
using WobbleShorts;

public class GraphExecutor : MonoBehaviour
{
    [SerializeField] private ShortGraph _graph;
    private ClipGraphProcessor _processor = null;
    
    private void Awake()
    {
        if (_graph != null) 
            ExecuteGraph(_graph);
    }
    
    /// <summary>
    /// Execute the graph, node by node.
    /// </summary>
    public void ExecuteGraph(ShortGraph graph)
    {
        _processor = new ClipGraphProcessor(graph);
        _processor.Run();
    }
}