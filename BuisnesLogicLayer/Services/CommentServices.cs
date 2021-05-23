using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuisnesLogicLayer.DTO;
using BuisnesLogicLayer.Interfaces;
using DataAccesLayer;
using DataAccesLayer.Repositories;
using DataAccesLayer.EF;
using DataAccesLayer.Interfaces;
using BuisnesLogicLayer.Converters;
using AutoMapper;
using DataAccesLayer.Enteties;

namespace BuisnesLogicLayer.Services
{
    public class CommentServices : ICommentServices
    {
        private readonly IUnitOfWork Database;
        private readonly IMapper mapper;
        public CommentServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            Database = unitOfWork;
            this.mapper = mapper;
        }
        /*--------------------Common Methods from Generic repository--------------------*/
        public async Task<IEnumerable<CommentInfoAndEditIDTO>> GetAllComments()
        {
            var comments = await Database.CommentRepository.GetAll();
            var mappedComments = mapper.Map<IEnumerable<Comment>, IEnumerable<CommentInfoAndEditIDTO>>(comments);
            foreach (var comment in mappedComments)
            {
                var user = await Database.UserRepository.GetById(comment.UserID);
                comment.UserNameSurname = user.Name + " " + user.Surname;
            }
            return mappedComments;
        }

        public async Task<CommentInfoAndEditIDTO> GetCommentById(int id)
        {
            var comment = await Database.CommentRepository.GetById(id);
            var mappedComment = mapper.Map<Comment, CommentInfoAndEditIDTO>(comment);
            var user = await Database.UserRepository.GetById(comment.UserID);
            mappedComment.UserNameSurname = user.Name + " " + user.Surname;
            return mappedComment;
        }

        public async Task AddNewComment(CommentCreateDTO commentDTO)
        {
            await Database.CommentRepository.Add(mapper.Map<CommentCreateDTO, Comment>(commentDTO));
        }

        public async Task DeleteCommentById(int id)
        {
            await Database.CommentRepository.DeleteById(id);
        }

        public async Task UpdateComment(CommentInfoAndEditIDTO commentEditDTO)
        {
            await Database.CommentRepository.Update(mapper.Map<CommentInfoAndEditIDTO, Comment>(commentEditDTO));
        }

        /*------------------------------Individual methods------------------------------*/
        public async Task<IEnumerable<CommentInfoAndEditIDTO>> GetCommentsByAdId(int adId)
        {
            var comments = await Database.CommentRepository.GetCommentsByAdId(adId);
            var mappedComments = mapper.Map<IEnumerable<Comment>, IEnumerable<CommentInfoAndEditIDTO>>(comments);
            foreach (var comment in mappedComments)
            {
                var user = await Database.UserRepository.GetById(comment.UserID);
                comment.UserNameSurname = user.Name + " " + user.Surname;
            }
            return mappedComments;
        }

        public async Task RemoveCommentByUserIdAndAdId(string userId, int adId)
        {
            await Database.CommentRepository.RemoveCommentByUserIdAndAdId(userId, adId);
        }
    }
}
