using System;

public partial class usercontrols_ucNavigation : HocLapTrinhWeb.UI.UCBase
{
    protected override void Page_Load(object sender, EventArgs e)
    {
        base.Page_Load(sender, e);
    }

    protected string SetClass(params string[] defaultpage)
    {
        try
        {
            foreach (var t in defaultpage)
            {
                if (t == "" && CurrentPage.GetRequestURL().ToLower() == "/index.aspx")
                    return "active";
                if (t != "" && CurrentPage.GetRequestURL().ToLower().Contains(t.ToLower()))
                    return "active";
            }
            return "";
        }
        catch { return ""; }
    }
}