using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TestWebProj.Data;

namespace TestWebProj
{
    public partial class _Default : BasePage
    {
        public string PropertyName
        {
            get
            {
                return "Default";
            }
        }
      
        BankingAppEntities db = new BankingAppEntities();


        //protected void Page_PreInit(object sender, EventArgs e)
        //{
        //    //Work and It will assign the values to label.  
        //    //ClientScript.RegisterClientScriptBlock(GetType(), "sas", "<script> alert('_Default Page_PreInit');</script>", false);
        //}
        //protected void Page_Init(object sender, EventArgs e)
        //{
        //    base(PropertyName);
        //}
        protected override void OnLoad(EventArgs e)
        { 
        } 
        //protected void Page_InitComplete(object sender, EventArgs e)
        //{
        //    //Work and It will assign the values to label.  
        //    //ClientScript.RegisterClientScriptBlock(GetType(), "sas", "<script> alert('_DefaultPage_InitComplete');</script>", false);
        //}
        //protected override void OnPreLoad(EventArgs e)
        //{
        //    //Work and It will assign the values to label.  
        //    //If the page is post back, then label contrl values will be loaded from view state.  
        //    //E.g: If you string str = lblName.Text, then str will contain viewstate values.  
        //    //ClientScript.RegisterClientScriptBlock(GetType(), "sas", "<script> alert('_DefaultOnPreLoad');</script>", false);
        //}
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        //protected void btnSubmit_Click(object sender, EventArgs e)
        //{
        //    //Work and It will assign the values to label.  
        //    //ClientScript.RegisterClientScriptBlock(GetType(), "sas", "<script> alert('_DefaultbtnSubmit_Click');</script>", false);
        //}
        //protected void Page_LoadComplete(object sender, EventArgs e)
        //{
        //    //Work and It will assign the values to label.  
        //    //ClientScript.RegisterClientScriptBlock(GetType(), "sas", "<script> alert('_DefaultPage_LoadComplete');</script>", false);
        //}
        //protected override void OnPreRender(EventArgs e)
        //{
        //    //Work and It will assign the values to label.  
        //    //ClientScript.RegisterClientScriptBlock(GetType(), "sas", "<script> alert('_DefaultOnPreRender');</script>", false);
        //}
        //protected override void OnSaveStateComplete(EventArgs e)
        //{
        //    //Work and It will assign the values to label.  
        //    //But "SaveStateComplete" values will not be available during post back. i.e. View state.  
        //    //ClientScript.RegisterClientScriptBlock(GetType(), "sas", "<script> alert('_DefaultOnSaveStateComplete');</script>", false);
        //}
        //protected void Page_UnLoad(object sender, EventArgs e)
        //{
        //    //Work and it will not effect label contrl, view stae and post back data.  
        //    //ClientScript.RegisterClientScriptBlock(GetType(), "sas", "<script> alert('_DefaultPage_UnLoad');</script>", false);
        //}

        protected void Button1_Click(object sender, EventArgs e)
        {

            List<Schedule> scheduleList = new List<Schedule>();
            var principalAmt = Convert.ToDouble(txtPrincipal.Text);
            var tenor = Convert.ToDouble(txtTenor.Text);
            var tenorInYear = Convert.ToDouble(txtTenor.Text) / 12;
            var interestRate = Convert.ToDouble(txtInterestRate.Text) / 100;
            var interestRateInMonth = interestRate / 12;
            DateTime initialDate = DateTime.Now;


            var monthlyEMI = principalAmt * interestRateInMonth / (1 - 1 / Math.Pow(1 + interestRateInMonth, tenor));
            var totalPayment = monthlyEMI * tenor;

            int installmentCount = 0;
            while (installmentCount < tenor + 1)
            {

                Schedule obj = new Schedule();
                int numberOfDays = 0;
                DateTime nextDate;


                if (installmentCount == 0)
                {
                    initialDate = DateTime.Now;//new DateTime(2021, 01, 31); 
                    obj.EmiDate = initialDate;
                    obj.SchedulePayment = 0;
                    obj.InstallmentNo = 0;
                    obj.principalAmt = 0;
                    obj.InterestAmt = 0;
                    obj.BeginingBalance = principalAmt;
                }
                else
                {
                    initialDate = initialDate.AddMonths(1).Date;
                    obj.EmiDate = initialDate;
                    nextDate = initialDate.AddMonths(1).Date;
                    numberOfDays = (nextDate - initialDate).Days;


                    var emiInterestAmt = (principalAmt * numberOfDays * interestRate) / 360;
                    var emiPrincipalAmt = monthlyEMI - emiInterestAmt;
                    if (principalAmt - monthlyEMI < monthlyEMI)
                    {
                        obj.SchedulePayment = principalAmt + emiInterestAmt;
                        emiPrincipalAmt = principalAmt;
                    }
                    else 
                    {
                        obj.SchedulePayment = monthlyEMI;
                    }
                    obj.InstallmentNo = installmentCount;
                    obj.principalAmt = emiPrincipalAmt;
                    obj.InterestAmt = emiInterestAmt;
                   
                    principalAmt = principalAmt - emiPrincipalAmt;
                    obj.BeginingBalance = principalAmt;
                }
                scheduleList.Add(obj);

                installmentCount++;
            }
            Schedule obj1 = new Schedule();
            obj1.InstallmentNo = 0;
            obj1.SchedulePayment = scheduleList.Sum(x => x.SchedulePayment);
            obj1.InterestAmt = scheduleList.Sum(x => x.InterestAmt);
            obj1.principalAmt = scheduleList.Sum(x => x.principalAmt);

            scheduleList.Add(obj1);

            GridView1.DataSource = scheduleList;
            GridView1.DataBind();

            Session["LoanSchedule"] = scheduleList;
            btnDisburse.Visible = true;
        }

        protected void btnDisburse_Click(object sender, EventArgs e)
        {
            BankingAppEntities db = new BankingAppEntities();
            string loanAcc = txtLoanAcc.Text;
            List<LoanSchedule> list = new List<LoanSchedule>();
            List<Schedule> scheduleList = (List<Schedule>)Session["LoanSchedule"];
            foreach (var item in scheduleList)
            {
                LoanSchedule obj = new LoanSchedule()
                {
                    EMIDate = item.EmiDate,
                    LoanAccountNo = loanAcc,
                    InstallmentNo = item.InstallmentNo,
                    BegningBalance = item.BeginingBalance,
                    SchedulePaymentAmt = item.SchedulePayment,
                    PrincipalAmt = item.principalAmt,
                    InterestAmt = item.InterestAmt
                };
                list.Add(obj);

            }

            list.ForEach(x => db.LoanSchedule.AddObject(x));

            tblTransaction tblTran = new tblTransaction();
            tblTran.AccountNo = loanAcc;
            tblTran.TxnType = "Transfer";
            tblTran.Amount = Convert.ToDouble(txtPrincipal.Text);
            tblTran.CrDr = "Cr";
            tblTran.Balance = tblTran.Amount;
            tblTran.Date = DateTime.Now;
            db.tblTransaction.AddObject(tblTran);

            db.SaveChanges();
        }

       
    }

    public class Schedule
    {
        public int InstallmentNo { get; set; }
        public DateTime? EmiDate { get; set; }
        public double BeginingBalance { get; set; }
        public double SchedulePayment { get; set; }
        public double ExtraPayment { get; set; }
        public double TotalPayment { get; set; }
        public double principalAmt { get; set; }
        public double InterestAmt { get; set; }
    }
}
