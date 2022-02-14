﻿#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using _4kTiles_Backend.Context;
using _4kTiles_Backend.Entities;
using _4kTiles_Backend.Services.Repositories;
using AutoMapper;
using _4kTiles_Backend.DataObjects.DAO.Report;
using _4kTiles_Backend.DataObjects.DTO.Response;
using _4kTiles_Backend.DataObjects.DTO.Report;

namespace _4kTiles_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongReportsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ISongReportRepository _songReportRepository;
        private readonly IMapper _mapper;

        public SongReportsController(ISongReportRepository songReportRepository, IMapper mapper)
        {
            _songReportRepository = songReportRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SongReport>>> GetSongReportList()
        {
            return await _context.SongReports.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SongReport>> GetSongReport(int id)
        {
            var songReport = await _context.SongReports.FindAsync(id);

            if (songReport == null)
            {
                return NotFound();
            }

            return songReport;
        }

        /// <summary>
        /// Create new Report
        /// </summary>
        /// <param name="report">song DTO</param>
        /// <returns>Success message</returns>
        [HttpPost("Create")]
        public async Task<ActionResult> CreateReport([FromBody] CreateReportDTO report)
        {
            //Mapping data from source ofject to new obj.
            CreateReportDAO reportDAO = _mapper.Map<CreateReportDAO>(report);
            int result = await _songReportRepository.CreateReport(reportDAO);

            return Created("success", new ResponseDTO { StatusCode = StatusCodes.Status201Created, Message = "Report Created." });

        }

        [HttpPost]
        public async Task<ActionResult<SongReport>> PostSongReport(SongReport songReport)
        {
            _context.SongReports.Add(songReport);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSongReport", new { id = songReport.ReportId }, songReport);
        }
    }
}
