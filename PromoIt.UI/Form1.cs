using PromoIt.Entities;
using PromoIt.Entitis;
using PromoIt.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PromoIt.UI
{
    public partial class Form1 : Form
    {
        public Hashtable hash = new Hashtable();

        public Form1()
        {
            InitializeComponent();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        // company add
        private void InsertCompany_Click(object sender, EventArgs e)
        {
            Company company = new Company();
            company.IDCompany = int.Parse(textIDcom.Text);
            company.NameCompany = textNameCom.Text;
            company.OwnerCompany = textOwnerCom.Text;
            //company.Phone = textPhoneCom.Text;
            company.EmailCompany = textEmailCom.Text;

            // command add
            MainManager.Instance.Companies.ExportFromDB("insert into companies values(@ID,@Name,@Owner,@Phone,@Email)", company);

            // Clear input
            Clear();
        }
        // company Import
        private void BTNimportCom_Click(object sender, EventArgs e)
        {
            hash = (Hashtable)MainManager.Instance.Companies.ImportData("select * from companies");
        }
        // company Retrieval
        private void RetrievalCompany_Click(object sender, EventArgs e)
        {
            Company company = new Company();
            company = (Company)hash[textIDcom.Text];
            if (company is null)
            {
                MessageBox.Show(textIDcom.Text + " is not exist");
            }
            else
            {
                textNameCom.Text  = company.NameCompany;
                textOwnerCom.Text = company.OwnerCompany;
                //textPhoneCom.Text = company.Phone;
                textEmailCom.Text = company.EmailCompany;
            }
        }
        // company Delete
        private void DeleteCompany_Click(object sender, EventArgs e)
        {
            Company company = new Company();
            company = (Company)hash[textIDcom.Text];
            MainManager.Instance.Companies.ExportFromDB("delete from companies where IDCompany = @ID", company);
        }
        // company Update
        private void UpdateCompany_Click(object sender, EventArgs e)
        {
            Company company = new Company();
            company.IDCompany = int.Parse(textIDcom.Text);
            company.NameCompany = textNameCom.Text;
            company.OwnerCompany = textOwnerCom.Text;
            //company.Phone = textPhoneCom.Text;
            company.EmailCompany = textEmailCom.Text;

            MainManager.Instance.Companies.ExportFromDB("update companies set NameCompany=@Name,OwnerCompany=@Owner,Phone=@Phone,EmailCompany=@Email where IDCompany = @ID", company);
        }


        // Associations Import
        private void BTNimportAssn_Click(object sender, EventArgs e)
        {
            hash = (Hashtable)MainManager.Instance.Associations.ImportData("select * from Associations");
        }
        // Associations add
        private void AddAssn_Click(object sender, EventArgs e)
        {
            Association Asso = new Association();
            //Asso.IDassn = textIDassn.Text;
            Asso.NameAssn= textNameAssn.Text;
            Asso.EmailAssn = textEmailAssn.Text;
            // לבדוק משתנים בפקודות
            // command add
            MainManager.Instance.Associations.ExportFromDB("insert into Associations values(@ID,@Name,@Email,@link,@Hashtag)", Asso);

            // Clear input
            Clear();
        }
        // Associations Retrieval
        private void RetrievalAssn_Click(object sender, EventArgs e)
        {
            //BTNimportAssn_Click.Click();
            //hash = (Hashtable)MainManager.Instance.Associations.ImportData("select * from Associations");
            Association Asso = new Association();
            Asso = (Association)hash[textIDassn.Text];
            if (Asso is null)
            {
                MessageBox.Show(textIDassn.Text + " is not exist");
            }
            else
            {
                textNameAssn.Text = Asso.NameAssn;
                textEmailAssn.Text = Asso.EmailAssn;
            }
        }
        // Associations Delete
        private void DeleteAssn_Click(object sender, EventArgs e)
        {
            Association Asso = new Association();
            Asso = (Association)hash[textIDassn.Text];
            MainManager.Instance.Associations.ExportFromDB("delete from Associations where IDassn = @ID", Asso);
        }
        // Associations Update
        private void UpdateAssn_Click(object sender, EventArgs e)
        {
            Association Asso = new Association();
            //Asso.IDassn = textIDassn.Text;
            Asso.NameAssn = textNameAssn.Text;
            Asso.EmailAssn = textEmailAssn.Text;
            // לבדוק משתנים בפקודות
            MainManager.Instance.Associations.ExportFromDB("update Associations set NameAssn=@Name,EmailAssn=@Email,linkURL=@link,Hashtag=@Hashtag where IDassn = @ID", Asso);
        }


        // Activists Import
        private void ImportActiv_Click(object sender, EventArgs e)
        {
            hash = (Hashtable)MainManager.Instance.Activists.ImportData("select * from Activists");
        }

        private void AddActiv_Click(object sender, EventArgs e)
        {
            Activist activ= new Activist();
            //activ.IDactivist = textIDactiv.Text;
            activ.NameActivist= textNameActiv.Text;
            activ.EmailActivist= textEmailActiv.Text;
            activ.AddressActivist = textAddressActiv.Text;
            activ.phoneActivist = textphoneActiv.Text;

            //activ.SumOfMoney = "0";
            //activ.IDassn = "";
            //activ.NameAssn = "";

            // command add
            MainManager.Instance.Activists.ExportFromDB("insert into Activists values(@ID,@Name,@Email,@Address,@phone,@Money,@IDassn,@NameAssn);", activ);

            // Clear input
            Clear();
        }

        private void RetrievalActiv_Click(object sender, EventArgs e)
        {
            Activist Activ = new Activist();
            Activ = (Activist)hash[textIDactiv.Text];
            if (Activ is null)
            {
                MessageBox.Show(textIDassn.Text + " is not exist");
            }
            else
            {
                textNameActiv.Text = Activ.NameActivist;
                textEmailActiv.Text = Activ.EmailActivist;
                textAddressActiv.Text = Activ.AddressActivist;
                textphoneActiv.Text = Activ.phoneActivist;
            }
        }

        private void DeleteActiv_Click(object sender, EventArgs e)
        {
            Activist activ = new Activist();
            activ = (Activist)hash[textIDactiv.Text];
            MainManager.Instance.Activists.ExportFromDB("delete from Activists where IDactivist = @ID", activ);
        }

        private void UpdateActiv_Click(object sender, EventArgs e)
        {
            Activist activ = new Activist();
            //activ.IDactivist = textIDactiv.Text;
            activ.NameActivist = textNameActiv.Text;
            activ.EmailActivist = textEmailActiv.Text;
            activ.AddressActivist = textAddressActiv.Text;
            activ.phoneActivist = textphoneActiv.Text;

            MainManager.Instance.Activists.ExportFromDB("update Associations set NameActivist=@Name,EmailActivist=@Email,AddressActivist=@Address,phoneActivist=@phone where IDactivist = @ID", activ);
        }

        public void Clear()
        {
            // company
            textIDcom.Text = "";
            textNameCom.Text = "";
            textOwnerCom.Text = "";
            textPhoneCom.Text = "";
            textEmailCom.Text = "";

            // Associations
            textIDassn.Text = "";
            textNameAssn.Text = "";
            textEmailAssn.Text = "";
            textlinkURL.Text = "";
            textHashtag.Text = "";

            // Activist
            textIDactiv.Text = "";
            textNameActiv.Text = "";
            textEmailActiv.Text = "";
            textAddressActiv.Text = "";
            textphoneActiv.Text = "";
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }
    }
}

/*
private void BTNimport_Click(object sender, EventArgs e)
{
}
*/