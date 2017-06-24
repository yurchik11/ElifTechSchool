using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ElifTechSchool
{
    public class Company
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal? Earning { get; set; }
        public decimal? EarningWithChild { get; set; }
        public List<Company> children { get; set; }

}
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private static CompaniesTreeEntities context;

        [System.Web.Services.WebMethod()]
        [System.Web.Script.Services.ScriptMethod()]
        public static Company GetDataForTree()
        {
            context = new CompaniesTreeEntities();
            var mainCompanyFromDB = context.Companies.First(c => c.ID == 1);
            Company mainCompany = new Company();
            mainCompany.ID = mainCompanyFromDB.ID;
            mainCompany.Name = mainCompanyFromDB.Name;
            mainCompany.Earning = mainCompanyFromDB.Earning;

            
            decimal? earningWithChild;
            mainCompany.children = SearchSubCompanies(mainCompany.ID, out earningWithChild);
            mainCompany.EarningWithChild = mainCompany.Earning + earningWithChild;
            //***BBBBBBBUUUUUUUUUUGGGGGGGGGGGG***
            foreach (var myCompany in mainCompany.children)
            {
                if (context.Companies.Count(c => c.ID == myCompany.ID) > 0)
                {
                    myCompany.children = SearchSubCompanies(myCompany.ID,out earningWithChild);
                    myCompany.EarningWithChild = myCompany.Earning + earningWithChild;
                }
                foreach (var mySubCompany in myCompany.children)
                {
                    if (context.Companies.Count(c => c.ID == mySubCompany.ID) > 0)
                    {
                        mySubCompany.children = SearchSubCompanies(mySubCompany.ID, out earningWithChild);
                        mySubCompany.EarningWithChild = mySubCompany.Earning + earningWithChild;
                    }
                    foreach (var mySubSubCompany in mySubCompany.children)
                    {
                        if (context.Companies.Count(c => c.ID == mySubSubCompany.ID) > 0)
                        {
                            mySubSubCompany.children = SearchSubCompanies(mySubSubCompany.ID, out earningWithChild);
                            mySubSubCompany.EarningWithChild = mySubSubCompany.Earning + earningWithChild;
                        }
                    }
                }
            }

            return mainCompany;
        }

        private static List<Company> SearchSubCompanies(int idMainCompany, out decimal? earningWithChild)
        {
            var mySubCompaniesFromDB = context.Companies.Where(c => c.BelongToID == idMainCompany).ToList();

            List<Company> mySubCompanies = new List<Company>();
            earningWithChild = 0;
            foreach (var mySubCompany in mySubCompaniesFromDB)
            {
                Company newSubCompany = new Company();
                newSubCompany.ID = mySubCompany.ID;
                newSubCompany.Name = mySubCompany.Name;
                newSubCompany.Earning = mySubCompany.Earning;
                earningWithChild += mySubCompany.Earning;
                mySubCompanies.Add(newSubCompany);
            }
            return mySubCompanies;
        }


        [System.Web.Services.WebMethod()]
        [System.Web.Script.Services.ScriptMethod()]
        public static Company AddNewCompany(Company newCompany)
        {
            context = new CompaniesTreeEntities();
            context.Companies.Add(new Companies()
            {
                Name = newCompany.Name,
                Earning = newCompany.Earning,
                BelongToID = newCompany.ID
            });
            context.SaveChanges();
            return GetDataForTree();
        }

        [System.Web.Services.WebMethod()]
        [System.Web.Script.Services.ScriptMethod()]
        public static void EditCompany(Company company)
        {
            context = new CompaniesTreeEntities();
            var companyDB = context.Companies.First(c => c.ID == company.ID);
            companyDB.Earning = company.Earning;
            companyDB.Name = company.Name;
            context.SaveChanges();
        }

        [System.Web.Services.WebMethod()]
        [System.Web.Script.Services.ScriptMethod()]
        public static Company DeleteCompany(int clickedID)
        {
            context = new CompaniesTreeEntities();
            var companyDB = context.Companies.First(c => c.ID == clickedID);
            context.Companies.Remove(companyDB);
            context.SaveChanges();
            return GetDataForTree();
        }
    }
}