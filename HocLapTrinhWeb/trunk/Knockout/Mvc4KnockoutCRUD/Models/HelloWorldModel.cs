using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DelegateDecompiler;

namespace Mvc4KnockoutCRUD.Models
{
    public class HelloWorldModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Computed]
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }
    }

}