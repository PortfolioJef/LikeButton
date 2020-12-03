using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace likeButtonApi.Data
{
    [Table("like")]
    public class Like
    {
        [Key]
        public int id { get; set; }
        public int qtdlikes { get; set; }
    }
}
