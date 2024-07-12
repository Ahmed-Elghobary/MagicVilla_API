using AutoMapper;
using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Logging;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.Dto;
using MagicVilla_VillaAPI.Repository.IRepository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace MagicVilla_VillaAPI.Controllers.v2
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]

    [ApiVersion("2.0")]
    public class VillaNumberAPIv2Controller : ControllerBase
    {
        private readonly ILogging _logger;
        private readonly IMapper _mapper;
        private readonly IVillaNumberRepository _dbVillaNumber;
        private readonly IVillaRepository _dbVilla;
        protected APIResponse _response;

        public VillaNumberAPIv2Controller(ILogging logger, IVillaNumberRepository dbVillaNumbr, IMapper mapper, IVillaRepository dbVilla)
        {
            _logger = logger;
            _mapper = mapper;
            _dbVillaNumber = dbVillaNumbr;
            _response = new APIResponse();
            _dbVilla = dbVilla;
        }



        //[MapToApiVersion("2.0")]
        [HttpGet("GetString")]
        public IEnumerable<string> Get()
        {
            return new string[] { "Ahmed", "Elghobary" };

        }





    }
}
