namespace KPMGNews.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Comments
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommentID { get; set; }

        [Column(Order = 1)]
        public int NewsID { get; set; }

        [Column(Order = 2)]
        public string Text { get; set; }

        [Column(Order = 3)]
        [DisplayName("User")]
        public int UserID { get; set; }

        [Column(Order = 4)]
        [DisplayName("Date/Time")]
        public DateTime timestamp { get; set; }

    }
}