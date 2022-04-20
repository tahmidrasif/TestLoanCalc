using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using TestWebProj.Data;

namespace TestWebProj
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        string role = "Admin";
        BankingAppEntities db = new BankingAppEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            
            DynamicMenuControlPopulation();
        }



        protected void DynamicMenuControlPopulation()
        {
            var menuView = db.MenuView.Where(x => x.ParentID == 0 && x.RoleName==role).OrderBy(x=>x.MenuOrder).ToList();
            string currentPage = Path.GetFileName(Request.Url.AbsolutePath);
            List<MenuItem> menuItemList = new List<MenuItem>();

            foreach (MenuView row in menuView)
            {
                MenuItem menuItem = new MenuItem
                   {
                       Value = row.ID.ToString(),
                       Text = row.MenuName,
                       NavigateUrl = row.PropertyName,
                       Selected = row.PropertyName.ToString().EndsWith(currentPage, StringComparison.CurrentCultureIgnoreCase),

                   };

                var childmenuview = db.MenuView.Where(x => x.ParentID == row.ID && x.RoleName == role).OrderBy(x => x.MenuOrder).ToList();
                    NavigationMenu.Items.Add(SetChildMenu(menuItem, childmenuview));
               
            }

        }

        private MenuItem SetChildMenu(MenuItem menuItem, List<MenuView> childmenuview)
        {
            foreach (MenuView childrow in childmenuview)
            {
                MenuItem menuItem1 = new MenuItem
                {
                    Value = childrow.ID.ToString(),
                    Text = childrow.MenuName,
                    NavigateUrl = childrow.PropertyName,
                    //Selected = childrow.PropertyName.ToString().EndsWith(currentPage, StringComparison.CurrentCultureIgnoreCase),

                };
                var childmenuview1 = db.MenuView.Where(x => x.ParentID == childrow.ID && x.RoleName == role).OrderBy(x => x.MenuOrder).ToList();
                SetChildMenu(menuItem1, childmenuview1);

                menuItem.ChildItems.Add(menuItem1);
            }

            return menuItem;
        }


    }
}
