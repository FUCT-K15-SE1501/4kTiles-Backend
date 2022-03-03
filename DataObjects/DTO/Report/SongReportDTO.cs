﻿using System.ComponentModel.DataAnnotations;

namespace _4kTiles_Backend.DataObjects.DTO.Report
{
    public class SongReportDTO
    {
        public int ReportId { get; set; }
        [Required]
        public int SongId { get; set; }
        public int AccountId { get; set; }
        public string ReportTitle { get; set; } = null!;
        public string ReportReason { get; set; } = null!;
        public DateTime ReportDate { get; set; }
        public bool? ReportStatus { get; set; }
    }
}
