using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CloudPOC.UI_Mapping
{
    class PermissionsPage
    {
        public const string xpathPermissionManagement = "//span[.=\"Permission Management\"]";
        public const string xpathMart = "//span[.=\"Mart\"]";
        public const string xpathProfileUserErwin = "//span[.=\"erwin\"]";
        public const string xpathProfileB = "//span[.=\"B\"]";
        public const string xpathProfileC = "//span[.=\"C\"]";
        public const string xpathButtonReviewChanges = "//button[.=\"Review Changes\"]";
        public const string xpathButtonShowAllPermissions = "/html/body/div/div/div[4]/div[2]/div/div/div/div/div/div[2]/div/div/div/div[2]/div[2]/div[2]/div/div/div[2]/div/div/div/div/div/div[2]/div/div/table/tbody/tr/td/table/tbody/tr/td[3]/table/tbody/tr[2]/td[2]/em/button";
        public const string xpathAllPermissionsTitle = "//div[@class=\" x-window x-component \"]//div[@class=\"x-window-tl\"]";
        public const string xpathRowPermissions = "//table[@class='x-grid3-row-table']//tr";
        public const string classNameRowReviewChanges = "x-grid3-row";
    }
}
