using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DelegateDecompiler;

namespace Mvc4KnockoutCRUD.Models
{
    public class ClickCounterModel
    {
        public int NumberOfClicks { get; set; }

        public bool HasClickedTooManyTimes
        {
            get { return NumberOfClicks >= 3; }
        }

        public void RegisterClick()
        {
            NumberOfClicks++;
        }

        public void ResetClick()
        {
            NumberOfClicks = 0;
        }
    }
}