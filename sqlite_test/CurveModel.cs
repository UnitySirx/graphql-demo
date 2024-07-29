using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sqlite_test
{
    [SqlSugar.SugarTable(TableName = "CURVE")]
    [Table(Name = "CURVE")]
    public class CurveModel
    {
        [SqlSugar.SugarColumn(IsIdentity = true)]
        [Column(IsIdentity = true)]
        public int Id { get; set; }
        public string ABSORB_HC {get;set;}
        public string ABSORB_HS {get;set;}
        public string ABSORB_LC {get;set;}
        public string ABSORB_LS { get; set; }
        public string FVALUE    { get; set; }
        public int NUM { get; set; }
        public short SEQ { get; set; }
        public short TIM {  get; set; } 

    }
}
