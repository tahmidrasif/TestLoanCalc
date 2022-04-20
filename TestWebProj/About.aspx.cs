using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TestWebProj.Data;

namespace TestWebProj
{
    public partial class About : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void btnTransaction_Click(object sender, EventArgs e)
        {
            BankingAppEntities db = new BankingAppEntities();
            var currentBalance = db.tblTransaction.OrderByDescending(x => x.ID).FirstOrDefault().Balance;
            tblTransaction tran = new tblTransaction()
            {
                CrDr = ddlCrDr.SelectedValue,
                AccountNo = txtFromAcc.Text,
                Date = Convert.ToDateTime(txtDate.Text),
                Amount = Convert.ToDouble(txtAmount.Text),
                Balance = ddlCrDr.SelectedValue == "Cr" ? currentBalance + Convert.ToDouble(txtAmount.Text) : currentBalance - Convert.ToDouble(txtAmount.Text),
                TxnType=txtType.Text??txtType.Text
            };
            db.tblTransaction.AddObject(tran);
            db.SaveChanges();

        }

        //protected void ddlCrDr_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (ddlCrDr.SelectedValue == "Cr")
        //    {
        //        txtFromAcc.Enabled = false;
        //        ddlToAccount.Enabled = false;
        //        txtToAcc.Enabled = true;
        //        ddlFromAccount.Enabled = true;

        //    }
        //    if (ddlCrDr.SelectedValue == "Dr")
        //    {
        //        txtToAcc.Enabled = false;
        //        ddlFromAccount.Enabled = false;
        //        txtFromAcc.Enabled = true;
        //        ddlToAccount.Enabled = true;
        //    }
        //}
    }
}
