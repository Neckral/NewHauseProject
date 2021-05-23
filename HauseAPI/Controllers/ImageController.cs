using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuisnesLogicLayer.Interfaces;
using BuisnesLogicLayer.Services;
using BuisnesLogicLayer.DTO;
using Microsoft.AspNetCore.Authorization;

namespace HauseAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImageController : Controller
    {
        private readonly IImageServices imageServices;

        public ImageController(IImageServices imageServices) => this.imageServices = imageServices;

        [HttpGet("GetByAdId/{adId}")]
        public async Task<IEnumerable<ImageEditInfoDTO>> GetImagesByAdId(int adId)
        {
            return await imageServices.GetImagesByAdId(adId);
        }

        [Authorize]
        [HttpDelete("DeleteById/{id}")]
        public async Task DeleteImageById(int id)
        {
            await imageServices.DeleteImageById(id);
        }
    }
}
