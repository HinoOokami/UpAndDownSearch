using UpAndDownSearch.Interfaces;

namespace UpAndDownSearch;

public class UpAndDownSearchEngine
{
    private List<IParent> ChildrenList { get; } = new();
    private Queue<IParent> ChildrenQueue { get; } = new();
    private Stack<IParent> ChildrenStack { get; } = new();
    private List<IChild> ParentsList { get; } = new();
    private Queue<IChild> ParentsQueue { get; } = new();
    private Stack<IChild> ParentsStack { get; } = new();
    public List<IChild> FindParentWide(IChild child)
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
    public List<IChild> FindParentDeep(IChild child)
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
    public List<IParent> FindChildWide(IParent parent)
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
    public List<IParent> FindChildDeep(IParent parent)
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