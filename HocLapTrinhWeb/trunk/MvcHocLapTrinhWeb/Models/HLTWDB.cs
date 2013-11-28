using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MvcHocLapTrinhWeb.Models
{
    public class HLTWDB : DbContext
    {
        public DbSet<tbl_News> tbl_Newss { get; set; }
        public DbSet<tbl_NewsType> tbl_NewsTypes { get; set; }
    }

    public class tbl_News
    {
        [Key]
        public int NewsID { get; set; }
        public int NewsTypeID { get; set; }
        public string Title { get; set; }
        public Nullable<int> VideoID { get; set; }
        public string Brief { get; set; }
        public string Content { get; set; }
        public string Thumbnail { get; set; }
        public string Image { get; set; }
        public Nullable<bool> IsShowImage { get; set; }
        public string Keyword { get; set; }
        public Nullable<bool> IsHot { get; set; }
        public Nullable<int> Viewed { get; set; }
        public Nullable<int> Priority { get; set; }
        public string IPAddress { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public string IPUpdate { get; set; }
        public string RefAddress { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<int> MoveFrom { get; set; }

        [ForeignKey("NewsTypeID")]
        public virtual tbl_NewsType tbl_News_Types { get; set; }
    }

    public class tbl_NewsType
    {
        [Key]
        public int NewsTypeID { get; set; }
        public string NewsTypeName { get; set; }
        public string Description { get; set; }
        public Nullable<int> Priority { get; set; }
        public Nullable<int> IsActive { get; set; }
        public Nullable<int> ParentID { get; set; }
        public string ImageURL { get; set; }
    }
}