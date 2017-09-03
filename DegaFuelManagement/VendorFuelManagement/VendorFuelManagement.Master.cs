using System;
using System.Diagnostics;
using System.Web;
using System.Web.UI.WebControls;
using Degatech.Utilities.Web.Optimization;

namespace VendorFuelManagement
{
    public partial class FuelManagementMaster : System.Web.UI.MasterPage
    {
        #region Members
        protected bool _IsDebugging;
        #endregion

        #region Protected Methods
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckIfDebugging();
            AddPageScripts();
            AddPageStyles();
        }

        protected virtual void AddPageScripts()
        {
            BundleScripts scripts = new BundleScripts(Request.Url.AbsolutePath);
            AddHTMLToBody(scripts.GenerateScriptList());
        }

        protected virtual void AddPageStyles()
        {
            BundleStyles styles = new BundleStyles(Request.Url.AbsolutePath);
            AddHTMLToHeader(styles.GenerateStyleList());
        }

        protected void RedirectToLogin()
        {
            HttpContext.Current.Response.Redirect("~/default.aspx");
        }

        protected void AddHTMLToHeader(string html)
        {
            Literal literal = new Literal();
            literal.Text = html;
            Page.Header.Controls.Add(literal);
        }

        protected void AddHTMLToBody(string html)
        {
            Literal literal = new Literal();
            literal.Text = html;
            body.Controls.Add(literal);
        }

        [Conditional("DEBUG")]
        private void CheckIfDebugging()
        {
            _IsDebugging = true;
        }
        #endregion
    }
}