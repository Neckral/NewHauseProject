using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuisnesLogicLayer.Interfaces;
using BuisnesLogicLayer.Services;
using BuisnesLogicLayer.DTO;

namespace HauseAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommentController : Controller
    {
        private readonly ICommentServices commentServices;

        public CommentController(ICommentServices commentServices) => this.commentServices = commentServices;
        // Get all comments
        [HttpGet]
        public async Task<IEnumerable<CommentInfoAndEditIDTO>> GetAllComments()
        {
            return await commentServices.GetAllComments();
        }

        [HttpGet("/Comment/{id}")]
        public async Task<CommentInfoAndEditIDTO> GetCommentById(int id)
        {
            return await commentServices.GetCommentById(id);
        }

        // Get comments by adId
        [HttpGet("/Comment/ad/{adId}")]
        public async Task<IEnumerable<CommentInfoAndEditIDTO>> GetCommentsByAdId(int adId)
        {
           return await commentServices.GetCommentsByAdId(adId);
        }

        // Add new comment
        [HttpPost]
        public async Task AddNewComment([FromBody] CommentCreateDTO commentDTO)
        {
           await commentServices.AddNewComment(commentDTO);
        }

        // Delete comment by ID
        [HttpDelete("/Comment/{id}")]
        public async Task RemoveCommentById(int id)
        {
            await commentServices .DeleteCommentById(id);
        }

        // Delete comment by userId and adId
        [HttpDelete("/Comment/{userId}/{adId}")]
        public async Task RemoveCommentByUserIdAndAdId(string userId, int adId)
        {
            await commentServices.RemoveCommentByUserIdAndAdId(userId, adId);
        }
        
        // Edit comment
        [HttpPut]
        public async Task UpdateComment([FromBody] CommentInfoAndEditIDTO commentEditDTO)
        {
            await commentServices.UpdateComment(commentEditDTO);
        }
        
    }
}
