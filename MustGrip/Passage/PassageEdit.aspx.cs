using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MustGrip.Passage
{
    public partial class PassageEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(Request.Params["wife"] != null && Request.Params["wife"]=="orchid"))
            {
                Response.Redirect("PassageList.aspx");
            }
        }
    }
}