using ApiTask.Dto;
using Microsoft.AspNetCore.Mvc;

namespace ApiTask.Interface
{
    public interface ICombinationService
    {
        Task<CombinationResponseDto> GenerateCombinations([FromBody] CombinationRequestDto request);
    }
}
