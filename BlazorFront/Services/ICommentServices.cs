using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuisnesLogicLayer.DTO;

namespace BlazorFront.Services
{
    public interface ICommentServices
    {
        public Task<IEnumerable<CommentInfoAndEditIDTO>> GetCommentsByAdId(int adId);
        public Task DeleteCommentById(int id);
        public Task EditComment(CommentInfoAndEditIDTO commentInfoAndEdit);
        public Task CreateComment(CommentCreateDTO commentCreate);
    }
}
