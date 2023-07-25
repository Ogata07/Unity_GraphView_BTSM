using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class DFSTest<T> : MonoBehaviour
{
    //参考
    //https://www.hanachiru-blog.com/entry/2020/05/21/120000
    private Dictionary<T, Node> graph;
    private Dictionary<T, bool> memo;
    private T memoStart;
    /// <summary>
    /// 初期化
    /// </summary>
    /// <param name="vertices">頂点の集合</param>
    public DFSTest(IEnumerable<T> vertices) { 
        graph = new Dictionary<T, Node>();
        memo = new Dictionary<T, bool>();
        memoStart=default(T);
        foreach(var v in vertices) {
            graph[v] = new Node();
        }
    }
    /// <summary>
    /// グラフに辺を追加する
    /// </summary>
    /// <param name="from">接続元</param>
    /// <param name="to">接続先</param>
    /// <returns></returns>
    public DFSTest<T> Add(T from, T to) {
        memo = new Dictionary<T, bool>();
        memoStart = default(T);
        graph[from].Add(to);
        return this;
    }
    public bool IsExist(T start, T target) {
        //条件が異なっていなければメモしておいたのを返す
        if (Equals(memoStart, start) && memo.ContainsKey(target)) return memo[target];

        foreach (var node in graph) node.Value.IsSeen = false;
        var stack=new Stack<T>();
        stack.Push(start);
        graph[start].IsSeen = true;

        while (stack.Count != 0) { 
            T state=stack.Pop();
            foreach(var v in graph[state].To) {
                if (graph[v].IsSeen == false)
                {
                    graph[v].IsSeen = true;
                    stack.Push(v);
                }
            }
        }

        memo = graph.ToDictionary(graph => graph.Key, graph => graph.Value.IsSeen);
        memoStart = start;

        return graph[target].IsSeen;
    }
    public class Node
    {
        public List<T> To { get; private set; }
        public bool IsSeen { get; set;}
        public Node()
        {
            To = new List<T>();
            IsSeen=false;
        }
        public void Add(T item)
            =>To.Add(item);
    }
}
