using System;
using System.Collections.Generic;

namespace EAW_WebApi.Models
{
    public partial class StoreGroupMapping
    {
        public int Id { get; set; }
        public int StoreGroupId { get; set; }
        public int StoreId { get; set; }

        public virtual Store Store { get; set; }
        public virtual StoreGroup StoreGroup { get; set; }
    }
}
