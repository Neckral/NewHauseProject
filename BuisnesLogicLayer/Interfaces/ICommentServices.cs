using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccesLayer.Enteties;
using BuisnesLogicLayer.DTO;
namespace BuisnesLogicLayer.Interfaces
{
    public interface ICommentServices
    {
        /*--------------------Common Methods from Generic repository--------------------*/
        public Task<IEnumerable<CommentInfoAndEditIDTO>> GetAllComments();

        public Task<CommentInfoAndEditIDTO> GetCommentById(int id);

        public Task AddNewComment(CommentCreateDTO commentDTO);

        public Task UpdateComment(CommentInfoAndEditIDTO commentEditDTO);

        public Task DeleteCommentById(int id);

        /*------------------------------Individual methods------------------------------*/
        public Task<IEnumerable<CommentInfoAndEditIDTO>> GetCommentsByAdId(int adId);

        public Task RemoveCommentByUserIdAndAdId(string userId, int adId);
        
        
    }
}
