using ApiTask.Interface;

namespace ApiTask.Service
{
    public class CombinationGenerationService : ICombinationGenerator
    {
        public List<List<string>> GenerateCombinations(List<int> items, int length)
        {
            if (items == null || items.Count == 0)
            {
                throw new ArgumentNullException(nameof(items) + " cant be null!");
            }
            if (length < 1 || length > items.Count)
            {
                throw new ArgumentException("length does not much with items!");
            }
            if (items.Any(x => x < 1 || x > 3))
            {
                throw new ArgumentException("items supposed to be between 1 to 3!");
            }

            var itemNames = new List<string>();

            var letterCounts = new Dictionary<char, int>
            {
                {'A', 0}, {'B', 0}, {'C', 0}
            };

            foreach (var num in items)
            {
                char letter = (char)('A' + (num - 1));
                letterCounts[letter]++;
                itemNames.Add($"{letter}{letterCounts[letter]}");
            }

            var combinations = new List<List<string>>();
            var indices = Enumerable.Range(0, itemNames.Count).ToList();

            foreach (var indexCombo in GetCombinations(indices, length))
            {
                var combo = indexCombo.Select(i => itemNames[i]).ToList();

                if (IsValidCombination(combo))
                {
                    combinations.Add(combo);
                }
            }

            return combinations;
        }

        public bool IsValidCombination(List<string> combination)
        {
            var letters = combination.Select(item => item[0]).Distinct();
            return letters.Count() == combination.Count;
        }

        public IEnumerable<List<int>> GetCombinations(List<int> numbers, int length)
        {
            return GetCombinationsRecursive(numbers, length, new List<int>());
        }

        public IEnumerable<List<int>> GetCombinationsRecursive(List<int> numbers, int length, List<int> current)
        {
            if (current.Count == length)
            {
                yield return current;
                yield break;
            }

            for (int i = 0; i < numbers.Count; i++)
            {
                var newNumbers = numbers.Skip(i + 1).ToList();
                var newCurrent = new List<int>(current) { numbers[i] };
                foreach (var combo in GetCombinationsRecursive(newNumbers, length, newCurrent))
                {
                    yield return combo;
                }
            }
        }
    }
}
