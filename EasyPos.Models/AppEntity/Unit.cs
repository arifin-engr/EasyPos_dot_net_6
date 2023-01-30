using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EasyPos.Models.AppEntity
{
    public class Unit:BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Display(Name = "Related To Unit")]
        public int? RelatedUnitId { get; set; }
        public string? Operator { get; set; }
        [Display(Name = "Related By Value")]
        public int? RelatedBy { get; set; }
        [ForeignKey("RelatedUnitId")]
        [ValidateNever]
        public Unit? RelatedUnit { get; set; }
    }
}
