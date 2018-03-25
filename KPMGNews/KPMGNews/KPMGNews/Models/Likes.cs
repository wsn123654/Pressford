namespace KPMGNews.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Likes
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Column(Order = 1)]
        public int NewsID { get; set; }

        [Column(Order = 2)]
        [DisplayName("User")]
        public int UserID { get; set; }

        [Column(Order = 3)]
        [DisplayName("Date/Time")]
        public DateTime timestamp { get; set; }

    }
}