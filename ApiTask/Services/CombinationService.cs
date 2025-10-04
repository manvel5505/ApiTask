using ApiTask.Data;
using ApiTask.Dto;
using ApiTask.Interface;
using ApiTask.Model;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ApiTask.Service
{
    public class CombinationService : ICombinationService
    {
        private readonly MainDbContext data;
        private readonly ICombinationGenerator generator;
        public CombinationService(MainDbContext data, ICombinationGenerator generator)
        {
            this.data = data;
            this.generator = generator;
        }
        public async Task<CombinationResponseDto> GenerateCombinations([FromBody] CombinationRequestDto request)
        {
            if (request.Items == null || request.Length < 1 || request.Length > request.Items.Count)
            {
                throw new ArgumentException("Invalid input!");
            }

            var combinations = generator.GenerateCombinations(request.Items, request.Length);

            await using var transaction = await data.Database.BeginTransactionAsync();

            try
            {
                var requestEntity = new RequestEntity
                {
                    InputItems = JsonSerializer.Serialize(request.Items),
                    CombinationLength = request.Length,
                    CreatedAt = DateTime.UtcNow
                };

                data.Requests.Add(requestEntity);
                await data.SaveChangesAsync();

                foreach (var combo in combinations)
                {
                    var combinationEntity = new CombinationEntity
                    {
                        RequestId = requestEntity.Id,
                        Items = JsonSerializer.Serialize(combo)
                    };
                    data.Combinations.Add(combinationEntity);
                }

                await data.SaveChangesAsync();
                await transaction.CommitAsync();

                return new CombinationResponseDto
                {
                    Id = requestEntity.Id,
                    Combination = combinations
                };
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

    }
}
