namespace UpAndDownSearch.Interfaces;

public interface IChild
{
    public List<IChild> GetParents();
}