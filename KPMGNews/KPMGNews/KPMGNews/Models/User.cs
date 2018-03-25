namespace KPMGNews.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User
    {
        [Key]
        [Column(Order = 0)]
        public int UserID { get; set; }

        [Key]
        [Column(Order = 1)]
        public string Login { get; set; }

        [Key]
        [Column(Order = 2)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Key]
        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserTypeID { get; set; }
    }
}
