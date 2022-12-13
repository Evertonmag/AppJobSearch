using JobSearch.Shared.Utility.Language;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSearch.Shared.Models
{
    public class Job
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "CompanyName", ResourceType = typeof(Fields))]
        [Required(ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E001")]
        public required string Company { get; set; }

        [Display(Name = "JobTitle", ResourceType = typeof(Fields))]
        [Required(ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E001")]
        public required string JobTitle { get; set; }

        [Display(Name = "CityState", ResourceType = typeof(Fields))]
        [Required(ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E001")]
        public required string CityState { get; set; }

        [Display(Name = "InitialSalary", ResourceType = typeof(Fields))]
        [Required(ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E001")]
        public required double InitialSalary { get; set; }

        [Display(Name = "FinalSalary", ResourceType = typeof(Fields))]
        [Required(ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E001")]
        public required double FinalSalary { get; set; }

        [Display(Name = "ContractType", ResourceType = typeof(Fields))]
        [Required(ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E001")]
        public required string ContractType { get; set; }

        [Display(Name = "TecnologyTools", ResourceType = typeof(Fields))]
        [Required(ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E001")]
        public required string TecnologyTools { get; set; }

        [Display(Name = "CompanyDescription", ResourceType = typeof(Fields))]
        public string? CompanyDescription { get; set; }

        [Display(Name = "JobDescription", ResourceType = typeof(Fields))]
        [Required(ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E001")]
        public required string JobDescription { get; set; }

        [Display(Name = "Benefits", ResourceType = typeof(Fields))]
        public string? Benefits { get; set; }

        [Display(Name = "InterestedSendEmailTo", ResourceType = typeof(Fields))]
        [EmailAddress(ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E002")]
        [Required(ErrorMessageResourceType = typeof(Message), ErrorMessageResourceName = "MSG_E001")]
        public required string InterestedSendEmailTo { get; set; }

        [Display(Name = "PublicationDate", ResourceType = typeof(Fields))]
        public DateTime PublicationDate { get; set; }
        
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }
    }
}
