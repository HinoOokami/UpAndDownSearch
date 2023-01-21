namespace UpAndDownSearch.Interfaces;

public interface IParent
{
    public List<IParent> GetChildren();
}