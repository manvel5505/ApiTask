namespace ApiTask.Interface
{
    public interface ICombinationBuilder
    {
        IEnumerable<List<int>> GetCombinations(List<int> numbers, int length);
        IEnumerable<List<int>> GetCombinationsRecursive(List<int> numbers, int length, List<int> current);
    }
}
