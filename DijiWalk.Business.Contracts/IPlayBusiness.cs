using DijiWalk.Common.Response;
using DijiWalk.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DijiWalk.Business.Contracts
{
    public interface IPlayBusiness
    {
        Task<ApiResponse> AddRange(List<Play> plays);

        Task<ApiResponse> SetUp(List<Play> plays, int idGame);

        Task<ApiResponse> DeleteFromGame(List<Play> plays);
    }
}
