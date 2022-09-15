using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceApp.Models
{
    public partial class Service
    {
        public Service()
        {
            ClientServices = new HashSet<ClientService>();
            ServicePhotos = new HashSet<ServicePhoto>();
            
        }

        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public decimal Cost { get; set; }
        public int DurationInSeconds { get; set; }
        public string? Description { get; set; }
        public double? Discount { get; set; }
        public string? MainImagePath { get; set; }
        [NotMapped]
        public string? FullName { get; set; } 
        [NotMapped]
        public decimal? CostWithDisount { get; set; }
        public virtual ICollection<ClientService> ClientServices { get; set; }
        public virtual ICollection<ServicePhoto> ServicePhotos { get; set; }
    }
}
