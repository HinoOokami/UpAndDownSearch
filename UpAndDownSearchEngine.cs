using UpAndDownSearch.Interfaces;

namespace UpAndDownSearch;

/// <summary>
/// Naive realization of tree search up and down
/// </summary>
public static class UpAndDownSearchEngine
{
    private static List<IParent> ChildrenList { get; } = new();
    private static Queue<IParent> ChildrenQueue { get; } = new();
    private static Stack<IParent> ChildrenStack { get; } = new();
    private static List<IChild> ParentsList { get; } = new();
    private static Queue<IChild> ParentsQueue { get; } = new();
    private static Stack<IChild> ParentsStack { get; } = new();

    /// <summary>
    /// Gets IChild and in cycle calls its GetParents method and then theirs parents and so on...
    /// Internally uses Queue for wide search
    /// </summary>
    /// <param name="child">IChild</param>
    /// <returns>List&lt;IChild&gt;</returns>
    public static List<IChild> FindParentWide(IChild child)
    {
        ParentsList.Clear();
        ParentsQueue.Clear();
        ParentsList.AddRange(child.GetParents().Distinct());
        foreach (var parent in ParentsList) ParentsQueue.Enqueue(parent);
        while (ParentsQueue.Count > 0)
        {
            var parent = ParentsQueue.Dequeue();
            var parents = parent.GetParents().Distinct().Except(ParentsList).ToList();
            foreach (var p in parents) ParentsQueue.Enqueue(p);
            ParentsList.AddRange(parents);
        }

        return ParentsList;
    }

    /// <summary>
    /// Gets IChild and in cycle calls its GetParents method and then theirs parents and so on...
    /// Internally uses Stack for deep search
    /// </summary>
    /// <param name="child">IChild</param>
    /// <returns>List&lt;IChild&gt;</returns>
    public static List<IChild> FindParentDeep(IChild child)
    {
        ParentsList.Clear();
        ParentsStack.Clear();
        ParentsList.AddRange(child.GetParents().Distinct());
        foreach (var parent in ParentsList) ParentsStack.Push(parent);
        while (ParentsStack.Count > 0)
        {
            var parent = ParentsStack.Pop();
            var parents = parent.GetParents().Distinct().Except(ParentsList).ToList();
            foreach (var p in parents) ParentsStack.Push(p);
            ParentsList.AddRange(parents);
        }

        return ParentsList;
    }

    /// <summary>
    /// Gets IParent and in cycle calls its GetChildren method and then theirs children and so on...
    /// Internally uses Queue for wide search
    /// </summary>
    /// <param name="parent">IChild</param>
    /// <returns>List&lt;IParent&gt;</returns>
    public static List<IParent> FindChildWide(IParent parent)
    {
        ChildrenList.Clear();
        ChildrenQueue.Clear();
        ChildrenList.AddRange(parent.GetChildren().Distinct());
        foreach (var child in ChildrenList) ChildrenQueue.Enqueue(child);
        while (ChildrenQueue.Count > 0)
        {
            var child = ChildrenQueue.Dequeue();
            var children = child.GetChildren().Distinct().Except(ChildrenList).ToList();
            foreach (var c in children) ChildrenQueue.Enqueue(c);
            ChildrenList.AddRange(children);
        }

        return ChildrenList;
    }

    /// <summary>
    /// Gets IParent and in cycle calls its GetChildren method and then theirs children and so on...
    /// Internally uses Stack for deep search
    /// </summary>
    /// <param name="parent">IChild</param>
    /// <returns>List&lt;IParent&gt;</returns>
    public static List<IParent> FindChildDeep(IParent parent)
    {
        ChildrenList.Clear();
        ChildrenStack.Clear();
        ChildrenList.AddRange(parent.GetChildren().Distinct());
        foreach (var child in ChildrenList) ChildrenStack.Push(child);
        while (ChildrenStack.Count > 0)
        {
            var child = ChildrenStack.Pop();
            var children = child.GetChildren().Distinct().Except(ChildrenList).ToList();
            foreach (var c in children) ChildrenStack.Push(c);
            ChildrenList.AddRange(children);
        }

        return ChildrenList;
    }
}