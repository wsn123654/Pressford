namespace KPMGNews.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class News
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NewsID { get; set; }

        [Column(Order = 1)]
        public string Text { get; set; }

        [Column(Order = 2)]
        [DisplayName("User")]
        public int UserID { get; set; }

        [Column(Order = 3)]
        [DisplayName("Date/Time")]
        public DateTime timestamp { get; set; }

        [Column(Order = 4)]
        public bool deleted { get; set; }
    }
}