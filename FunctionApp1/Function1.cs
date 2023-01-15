using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections;
using System.Text.Json;
using static System.Collections.Specialized.BitVector32;
using System.Net;
using PromoIt.Entities;
using PromoIt.Model;
using PromoIt.Data.SQL;
using System.Xml.Linq;
using System.Diagnostics;
using System.Security.Policy;
using PromoIt.Entitis;
using RestSharp;
using Newtonsoft.Json.Linq;

namespace PromoIt.MicroServer
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", "put", "delete", Route = "{User}/{page?}/{action}/{IdNumber?}/{IdNumber1?}")] HttpRequest req, string User, string page, string action, string IdNumber, string IdNumber1,
            ILogger log)
        {

            Hashtable hash;
            Activist Activ = new Activist();
            Company company = new Company();
            Association Asso = new Association();
            Message message = new Message();
            CampaignOfAsso campaignAsso = new CampaignOfAsso();
            CampaignOfCompany campaignCompany = new CampaignOfCompany();
            CampaignActivist campaignActivist = new CampaignActivist();
            DonatedProduct product = new DonatedProduct();
            InnerJoin innerJoin = new InnerJoin();
            Shipping shipping = new Shipping();


            string responseMessage;
            string requestBody;
            string Query;

            

            log.LogInformation("C# HTTP trigger function processed a request.");

            switch (User)
            {

                case "Activist":
                    switch (action)
                    {
                        case "Add":
                            requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                            Activ = System.Text.Json.JsonSerializer.Deserialize<Activist>(requestBody);
                            Query = "insert into Activists values(@ID,@Name,@Email,@Address,@Phone)";
                            MainManager.Instance.Activists.ExportFromDB(Query, Activ);
                            break;

                        case "chooseCampaigns":
                            requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                            campaignActivist = System.Text.Json.JsonSerializer.Deserialize<CampaignActivist>(requestBody);
                            Query = "insert into campaignActivist values(@ID,@Name,@IDAssn,@NameAssn,@EmailAssn,@Fund,@Link,@Hashtag,@Selected,@Status,@IDactiv,@Nameactiv,@EmailActiv,@Address,@Phone,@Money)";
                            MainManager.Instance.CampaignActivists.ExportFromDB(Query, campaignActivist);
                            break;

                        case "Update":
                            Activ = System.Text.Json.JsonSerializer.Deserialize<Activist>(req.Body);
                            Query = "update Activists set NameActivist=@Name,EmailActivist=@Email,AddressActivist=@Address,phoneActivist=@phone where IDactivist = @ID";
                            MainManager.Instance.Activists.ExportFromDB(Query, Activ);
                            break;

                        case "Remove":
                            Activ = System.Text.Json.JsonSerializer.Deserialize<Activist>(req.Body);
                            Query = "delete from Activists where IDactivist = @ID";
                            MainManager.Instance.Activists.ExportFromDB(Query, Activ);
                            break;

                        case "AddShipments":
                            shipping = System.Text.Json.JsonSerializer.Deserialize<Shipping>(req.Body);
                            Query = "insert into Shipments values(@donated,@bought,@IDProd,@Name,@Price,@Inventory,@SelectedProd,@StatusProd,@IDcamp,@Namecamp,@IDAssn,@NameAssn,@EmailAssn,@Fund,@Link,@Hashtag,@Selected,@StatusCamp,@IDCom,@NameCom,@Owner,@Phone,@EmailComp,@IDActiv,@NameActiv,@EmailActiv,@Address,@PhoneActiv,@Money) \r\n update DonatedProducts set SelectedProduct= @SelectedProd where IDProduct = @IDProd";
                            MainManager.Instance.Shipments.ExportFromDB(Query, shipping);
                            break;

                        case "BuyProduct":
                            shipping = System.Text.Json.JsonSerializer.Deserialize<Shipping>(req.Body);
                            Query = "update Shipments set bought=@bought,donated=@donated where IDShipments = @ID\r\nupdate Shipments set Inventory=@Inventory,Fundraising=@Fund where IDactivist = @IDActiv and IDcampaign = @IDcamp\r\nupdate Shipments set MoneyActivist=@Money where IDactivist = @IDActiv\r\nupdate DonatedProducts set Inventory=@Inventory,Fundraising=@Fund where IDcampaign = @IDcamp\r\nupdate campaignCompany set Fundraising=@Fund where IDcampaign = @IDcamp\r\nupdate campaignAsso set Fundraising=@Fund where IDcampaign = @IDcamp\r\nupdate campaignActivist set Fundraising=@Fund where IDactivist = @IDActiv and IDcampaign = @IDcamp\r\nupdate campaignActivist set MoneyActivist=@Money where IDactivist = @IDActiv";
                            MainManager.Instance.Shipments.ExportFromDB(Query, shipping);
                            break;

                        case "removeFromCrat":
                            shipping = System.Text.Json.JsonSerializer.Deserialize<Shipping>(req.Body);
                            Query = "delete from Shipments where IDShipments = @ID";
                            MainManager.Instance.Shipments.ExportFromDB(Query, shipping);
                            break;

                        case "Get":
                            if (IdNumber == null)
                            {
                                hash = (Hashtable)MainManager.Instance.Activists.ImportData("select * from Activists");
                                responseMessage = System.Text.Json.JsonSerializer.Serialize(hash);
                                return new OkObjectResult(responseMessage);
                            }
                            else
                            {
                                hash = (Hashtable)MainManager.Instance.Activists.ImportData("select * from Activists");
                                Activ = (Activist)hash[IdNumber]; //email
                                responseMessage = System.Text.Json.JsonSerializer.Serialize(Activ);
                                return new OkObjectResult(responseMessage);
                            }
                            break;

                    }
                    break;

                case "Company":
                    switch (action)
                    {
                        case "Add":
                            company = System.Text.Json.JsonSerializer.Deserialize<Company>(req.Body);
                            Query = "insert into companies values(@ID,@Name,@Owner,@Email,@Phone)";
                            MainManager.Instance.Companies.ExportFromDB(Query, company);
                            break;

                        case "Update":
                            company = System.Text.Json.JsonSerializer.Deserialize<Company>(req.Body);
                            Query = "update companies set NameCompany=@Name,OwnerCompany=@Owner,Phone=@Phone,EmailCompany=@Email where IDCompany = @ID";
                            MainManager.Instance.Companies.ExportFromDB(Query, company);
                            break;

                        case "Remove":
                            company = System.Text.Json.JsonSerializer.Deserialize<Company>(req.Body);
                            Query = "delete from companies where IDCompany = @ID";
                            MainManager.Instance.Companies.ExportFromDB(Query, company);
                            break;

                        case "Upload":
                            requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                            product = System.Text.Json.JsonSerializer.Deserialize<DonatedProduct>(requestBody);
                            Query = "insert into DonatedProducts values(@Name,@Price,@Inventory,@SelectedProd,@StatusProd,@IDcamp,@Namecamp,@IDAssn,@NameAssn,@EmailAssn,@Fund,@Link,@Hashtag,@Selected,@StatusCamp,@IDCom,@NameCom,@Owner,@Phone,@EmailComp)";
                            MainManager.Instance.DonatedProducts.ExportFromDB(Query, product);
                            break;

                        case "campaign-Company":
                            requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                            campaignCompany = System.Text.Json.JsonSerializer.Deserialize<CampaignOfCompany>(requestBody);
                            Query = "insert into campaignCompany values(@ID,@Name,@IDAssn,@NameAssn,@EmailAssn,@Fund,@Link,@Hashtag,@Selected,@Status,@IDCom,@NameCom,@Owner,@Phone,@EmailComp)";
                            MainManager.Instance.CampaignCompany.ExportFromDB(Query, campaignCompany);
                            break;

                        case "UpdateProduct":
                            product = System.Text.Json.JsonSerializer.Deserialize<DonatedProduct>(req.Body);
                            Query = "update DonatedProducts set ProductName=@Name,Price=@Price,Inventory=@Inventory,StatusProduct=@StatusProd where IDProduct = @ID\r\nupdate Shipments set ProductName=@Name,Price=@Price,Inventory=@Inventory,StatusProduct=@StatusProd where IDProduct = @ID";
                            MainManager.Instance.DonatedProducts.ExportFromDB(Query, product);
                            break;

                        case "DeleteProduct":
                            product = System.Text.Json.JsonSerializer.Deserialize<DonatedProduct>(req.Body);
                            Query = "delete from DonatedProducts where IDProduct = @ID\r\n delete from Shipments where IDProduct = @ID";
                            MainManager.Instance.DonatedProducts.ExportFromDB(Query, product);
                            break;

                        case "Get":
                            if (IdNumber == null)
                            {
                                hash = (Hashtable)MainManager.Instance.Companies.ImportData("select * from companies");
                                responseMessage = System.Text.Json.JsonSerializer.Serialize(hash);
                                return new OkObjectResult(responseMessage);
                            }
                            else
                            {
                                hash = (Hashtable)MainManager.Instance.Companies.ImportData("select * from companies");
                                company = (Company)hash[IdNumber]; //email
                                responseMessage = System.Text.Json.JsonSerializer.Serialize(company);
                                return new OkObjectResult(responseMessage);
                            }
                            break;

                    }
                    break;

                case "Association":
                    switch (action)
                    {
                        case "Add":
                            // לבדוק משתנים בפקודות
                            Asso = System.Text.Json.JsonSerializer.Deserialize<Association>(req.Body);
                            Query = "insert into Associations values(@ID,@Name,@Email)";
                            MainManager.Instance.Associations.ExportFromDB(Query, Asso);
                            break;

                        case "Update":
                            Asso = System.Text.Json.JsonSerializer.Deserialize<Association>(req.Body);
                            Query = "update Associations set NameAssn=@Name,EmailAssn=@Email where IDassn = @ID";
                            //MainManager.Instance.Associations.ExportFromDB(Query, Asso);
                            break;

                        case "Remove":
                            Asso = System.Text.Json.JsonSerializer.Deserialize<Association>(req.Body);
                            Query = "delete from Associations where IDassn = @ID";
                            //MainManager.Instance.Associations.ExportFromDB(Query, Asso);
                            break;

                        case "Created":
                            campaignAsso = System.Text.Json.JsonSerializer.Deserialize<CampaignOfAsso>(req.Body);
                            Query = "insert into campaignAsso values(@Name,@IDAssn,@NameAssn,@Email,@Fund,@Link,@Hashtag,@Selected,@Status)";
                            MainManager.Instance.CampaignsAsso.ExportFromDB(Query, campaignAsso);
                            break;

                        case "UpdateCampaign":
                            campaignAsso = System.Text.Json.JsonSerializer.Deserialize<CampaignOfAsso>(req.Body);
                            Query = "update campaignAsso set NameCampaign=@Name,linkURL=@link,Hashtag=@Hashtag,SelectedCampaign=@Selected,StatusCampaign=@Status where IDcampaign = @ID\r\nupdate campaignCompany set NameCampaign=@Name,linkURL=@link,Hashtag=@Hashtag,SelectedCampaign=@Selected,StatusCampaign=@Status where IDcampaign = @ID\r\nupdate DonatedProducts set NameCampaign=@Name,linkURL=@link,Hashtag=@Hashtag,SelectedCampaign=@Selected,StatusCampaign=@Status where IDcampaign = @ID\r\nupdate campaignActivist set NameCampaign=@Name,linkURL=@link,Hashtag=@Hashtag,SelectedCampaign=@Selected,StatusCampaign=@Status where IDcampaign = @ID\r\nupdate Shipments set NameCampaign=@Name,linkURL=@link,Hashtag=@Hashtag,SelectedCampaign=@Selected,StatusCampaign=@Status where IDcampaign = @ID";
                            MainManager.Instance.CampaignsAsso.ExportFromDB(Query, campaignAsso);
                            break;

                        case "DeleteCampaign":
                            //\r\ndelete from campaignCompany where IDcampaign = @ID\r\ndelete from DonatedProducts where IDcampaign = @ID\r\ndelete from campaignActivist where IDcampaign = @ID\r\ndelete from Shipments where IDcampaign = @ID
                            campaignAsso = System.Text.Json.JsonSerializer.Deserialize<CampaignOfAsso>(req.Body);
                            Query = "delete from campaignAsso where IDcampaign = @ID";
                            MainManager.Instance.CampaignsAsso.ExportFromDB(Query, campaignAsso);
                            break;



                        case "Get":
                            if (IdNumber == null)
                            {
                                hash = (Hashtable)MainManager.Instance.Associations.ImportData("select * from Associations");
                                responseMessage = System.Text.Json.JsonSerializer.Serialize(hash);
                                return new OkObjectResult(responseMessage);
                            }
                            else
                            {
                                hash = (Hashtable)MainManager.Instance.Associations.ImportData("select * from Associations");
                                Asso = (Association)hash[IdNumber]; //email
                                responseMessage = System.Text.Json.JsonSerializer.Serialize(Asso);
                                return new OkObjectResult(responseMessage);
                            }
                            break;

                    }
                    break;

                case "User":
                    switch (action)
                    {
                        case "+100":  
                            shipping = System.Text.Json.JsonSerializer.Deserialize<Shipping>(req.Body);
                            Query = "update Shipments set MoneyActivist=@Money where IDactivist = @IDActiv and IDcampaign = @IDcamp\r\nupdate campaignActivist set MoneyActivist = @Money where IDactivist = @IDActiv and IDcampaign = @IDcamp";
                            MainManager.Instance.Shipments.ExportFromDB(Query, shipping);
                            break;

                        case "message":
                            Message data = new Message();
                            data = System.Text.Json.JsonSerializer.Deserialize<Message>(req.Body);
                            Query = "insert into messages values(@ID,@Name,@Phone,@Email,@Message)";
                            MainManager.Instance.Messages.ExportFromDB(Query, data);
                            break;
                            
                        case "Get-Campaigns":
                            hash = (Hashtable)MainManager.Instance.CampaignsAsso.ImportData("select * from campaignAsso");
                            responseMessage = System.Text.Json.JsonSerializer.Serialize(hash);
                            return new OkObjectResult(responseMessage);
                            break;

                        case "Get-Campaigns-Activist":
                            hash = (Hashtable)MainManager.Instance.CampaignActivists.ImportData("select * from campaignActivist where IDactivist="+ IdNumber);
                            responseMessage = System.Text.Json.JsonSerializer.Serialize(hash);
                            return new OkObjectResult(responseMessage);
                            break;

                        case "Get-Campaigns-Company":
                            hash = (Hashtable)MainManager.Instance.CampaignCompany.ImportData("select * from campaignCompany where IDCompany=" + IdNumber);
                            responseMessage = System.Text.Json.JsonSerializer.Serialize(hash);
                            return new OkObjectResult(responseMessage);
                            break;

                        case "Get-Product-Activist":
                            hash = (Hashtable)MainManager.Instance.InnerJoins.ImportData("select * from DonatedProducts d inner join campaignActivist c on c.IDcampaign = d.IDcampaign where c.IDactivist=" + IdNumber + "and c.IDcampaign="+ IdNumber1 + "and d.StatusProduct=1 and d.StatusCampaign=1");
                            responseMessage = System.Text.Json.JsonSerializer.Serialize(hash);
                            return new OkObjectResult(responseMessage);
                            break;

                        case "Get-Donated-products":
                            hash = (Hashtable)MainManager.Instance.DonatedProducts.ImportData("select * from DonatedProducts");
                            responseMessage = System.Text.Json.JsonSerializer.Serialize(hash);
                            return new OkObjectResult(responseMessage);
                            break;

                        case "Get-Shipments":
                            hash = (Hashtable)MainManager.Instance.Shipments.ImportData("select * from Shipments");
                            responseMessage = System.Text.Json.JsonSerializer.Serialize(hash);
                            return new OkObjectResult(responseMessage);
                            break;
/*
                        case "Roles":
                            var urlGetRoles = $"https://dev-1r64p6wfhjnlz8dm.us.auth0.com/api/v2/users/{IdNumber}/roles";
                            var client = new RestClient(urlGetRoles);
                            var request = new RestRequest("", Method.Get);
                            string type = "Bearer ";
                            string token = "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6ImpJZTZTbV9HTm9vci16OFgtTWJSSCJ9.eyJpc3MiOiJodHRwczovL2Rldi0xcjY0cDZ3Zmhqbmx6OGRtLnVzLmF1dGgwLmNvbS8iLCJzdWIiOiI3TVBOSWl2anluaENibDYzZFByUnZOaElHMWtWZWdES0BjbGllbnRzIiwiYXVkIjoiaHR0cHM6Ly9kZXYtMXI2NHA2d2Zoam5sejhkbS51cy5hdXRoMC5jb20vYXBpL3YyLyIsImlhdCI6MTY3MzY4MDIxMywiZXhwIjoxNjc2MjcyMjEzLCJhenAiOiI3TVBOSWl2anluaENibDYzZFByUnZOaElHMWtWZWdESyIsInNjb3BlIjoicmVhZDpjbGllbnRfZ3JhbnRzIGNyZWF0ZTpjbGllbnRfZ3JhbnRzIGRlbGV0ZTpjbGllbnRfZ3JhbnRzIHVwZGF0ZTpjbGllbnRfZ3JhbnRzIHJlYWQ6dXNlcnMgdXBkYXRlOnVzZXJzIGRlbGV0ZTp1c2VycyBjcmVhdGU6dXNlcnMgcmVhZDp1c2Vyc19hcHBfbWV0YWRhdGEgdXBkYXRlOnVzZXJzX2FwcF9tZXRhZGF0YSBkZWxldGU6dXNlcnNfYXBwX21ldGFkYXRhIGNyZWF0ZTp1c2Vyc19hcHBfbWV0YWRhdGEgcmVhZDp1c2VyX2N1c3RvbV9ibG9ja3MgY3JlYXRlOnVzZXJfY3VzdG9tX2Jsb2NrcyBkZWxldGU6dXNlcl9jdXN0b21fYmxvY2tzIGNyZWF0ZTp1c2VyX3RpY2tldHMgcmVhZDpjbGllbnRzIHVwZGF0ZTpjbGllbnRzIGRlbGV0ZTpjbGllbnRzIGNyZWF0ZTpjbGllbnRzIHJlYWQ6Y2xpZW50X2tleXMgdXBkYXRlOmNsaWVudF9rZXlzIGRlbGV0ZTpjbGllbnRfa2V5cyBjcmVhdGU6Y2xpZW50X2tleXMgcmVhZDpjb25uZWN0aW9ucyB1cGRhdGU6Y29ubmVjdGlvbnMgZGVsZXRlOmNvbm5lY3Rpb25zIGNyZWF0ZTpjb25uZWN0aW9ucyByZWFkOnJlc291cmNlX3NlcnZlcnMgdXBkYXRlOnJlc291cmNlX3NlcnZlcnMgZGVsZXRlOnJlc291cmNlX3NlcnZlcnMgY3JlYXRlOnJlc291cmNlX3NlcnZlcnMgcmVhZDpkZXZpY2VfY3JlZGVudGlhbHMgdXBkYXRlOmRldmljZV9jcmVkZW50aWFscyBkZWxldGU6ZGV2aWNlX2NyZWRlbnRpYWxzIGNyZWF0ZTpkZXZpY2VfY3JlZGVudGlhbHMgcmVhZDpydWxlcyB1cGRhdGU6cnVsZXMgZGVsZXRlOnJ1bGVzIGNyZWF0ZTpydWxlcyByZWFkOnJ1bGVzX2NvbmZpZ3MgdXBkYXRlOnJ1bGVzX2NvbmZpZ3MgZGVsZXRlOnJ1bGVzX2NvbmZpZ3MgcmVhZDpob29rcyB1cGRhdGU6aG9va3MgZGVsZXRlOmhvb2tzIGNyZWF0ZTpob29rcyByZWFkOmFjdGlvbnMgdXBkYXRlOmFjdGlvbnMgZGVsZXRlOmFjdGlvbnMgY3JlYXRlOmFjdGlvbnMgcmVhZDplbWFpbF9wcm92aWRlciB1cGRhdGU6ZW1haWxfcHJvdmlkZXIgZGVsZXRlOmVtYWlsX3Byb3ZpZGVyIGNyZWF0ZTplbWFpbF9wcm92aWRlciBibGFja2xpc3Q6dG9rZW5zIHJlYWQ6c3RhdHMgcmVhZDppbnNpZ2h0cyByZWFkOnRlbmFudF9zZXR0aW5ncyB1cGRhdGU6dGVuYW50X3NldHRpbmdzIHJlYWQ6bG9ncyByZWFkOmxvZ3NfdXNlcnMgcmVhZDpzaGllbGRzIGNyZWF0ZTpzaGllbGRzIHVwZGF0ZTpzaGllbGRzIGRlbGV0ZTpzaGllbGRzIHJlYWQ6YW5vbWFseV9ibG9ja3MgZGVsZXRlOmFub21hbHlfYmxvY2tzIHVwZGF0ZTp0cmlnZ2VycyByZWFkOnRyaWdnZXJzIHJlYWQ6Z3JhbnRzIGRlbGV0ZTpncmFudHMgcmVhZDpndWFyZGlhbl9mYWN0b3JzIHVwZGF0ZTpndWFyZGlhbl9mYWN0b3JzIHJlYWQ6Z3VhcmRpYW5fZW5yb2xsbWVudHMgZGVsZXRlOmd1YXJkaWFuX2Vucm9sbG1lbnRzIGNyZWF0ZTpndWFyZGlhbl9lbnJvbGxtZW50X3RpY2tldHMgcmVhZDp1c2VyX2lkcF90b2tlbnMgY3JlYXRlOnBhc3N3b3Jkc19jaGVja2luZ19qb2IgZGVsZXRlOnBhc3N3b3Jkc19jaGVja2luZ19qb2IgcmVhZDpjdXN0b21fZG9tYWlucyBkZWxldGU6Y3VzdG9tX2RvbWFpbnMgY3JlYXRlOmN1c3RvbV9kb21haW5zIHVwZGF0ZTpjdXN0b21fZG9tYWlucyByZWFkOmVtYWlsX3RlbXBsYXRlcyBjcmVhdGU6ZW1haWxfdGVtcGxhdGVzIHVwZGF0ZTplbWFpbF90ZW1wbGF0ZXMgcmVhZDptZmFfcG9saWNpZXMgdXBkYXRlOm1mYV9wb2xpY2llcyByZWFkOnJvbGVzIGNyZWF0ZTpyb2xlcyBkZWxldGU6cm9sZXMgdXBkYXRlOnJvbGVzIHJlYWQ6cHJvbXB0cyB1cGRhdGU6cHJvbXB0cyByZWFkOmJyYW5kaW5nIHVwZGF0ZTpicmFuZGluZyBkZWxldGU6YnJhbmRpbmcgcmVhZDpsb2dfc3RyZWFtcyBjcmVhdGU6bG9nX3N0cmVhbXMgZGVsZXRlOmxvZ19zdHJlYW1zIHVwZGF0ZTpsb2dfc3RyZWFtcyBjcmVhdGU6c2lnbmluZ19rZXlzIHJlYWQ6c2lnbmluZ19rZXlzIHVwZGF0ZTpzaWduaW5nX2tleXMgcmVhZDpsaW1pdHMgdXBkYXRlOmxpbWl0cyBjcmVhdGU6cm9sZV9tZW1iZXJzIHJlYWQ6cm9sZV9tZW1iZXJzIGRlbGV0ZTpyb2xlX21lbWJlcnMgcmVhZDplbnRpdGxlbWVudHMgcmVhZDphdHRhY2tfcHJvdGVjdGlvbiB1cGRhdGU6YXR0YWNrX3Byb3RlY3Rpb24gcmVhZDpvcmdhbml6YXRpb25zIHVwZGF0ZTpvcmdhbml6YXRpb25zIGNyZWF0ZTpvcmdhbml6YXRpb25zIGRlbGV0ZTpvcmdhbml6YXRpb25zIGNyZWF0ZTpvcmdhbml6YXRpb25fbWVtYmVycyByZWFkOm9yZ2FuaXphdGlvbl9tZW1iZXJzIGRlbGV0ZTpvcmdhbml6YXRpb25fbWVtYmVycyBjcmVhdGU6b3JnYW5pemF0aW9uX2Nvbm5lY3Rpb25zIHJlYWQ6b3JnYW5pemF0aW9uX2Nvbm5lY3Rpb25zIHVwZGF0ZTpvcmdhbml6YXRpb25fY29ubmVjdGlvbnMgZGVsZXRlOm9yZ2FuaXphdGlvbl9jb25uZWN0aW9ucyBjcmVhdGU6b3JnYW5pemF0aW9uX21lbWJlcl9yb2xlcyByZWFkOm9yZ2FuaXphdGlvbl9tZW1iZXJfcm9sZXMgZGVsZXRlOm9yZ2FuaXphdGlvbl9tZW1iZXJfcm9sZXMgY3JlYXRlOm9yZ2FuaXphdGlvbl9pbnZpdGF0aW9ucyByZWFkOm9yZ2FuaXphdGlvbl9pbnZpdGF0aW9ucyBkZWxldGU6b3JnYW5pemF0aW9uX2ludml0YXRpb25zIHJlYWQ6b3JnYW5pemF0aW9uc19zdW1tYXJ5IGNyZWF0ZTphY3Rpb25zX2xvZ19zZXNzaW9ucyBjcmVhdGU6YXV0aGVudGljYXRpb25fbWV0aG9kcyByZWFkOmF1dGhlbnRpY2F0aW9uX21ldGhvZHMgdXBkYXRlOmF1dGhlbnRpY2F0aW9uX21ldGhvZHMgZGVsZXRlOmF1dGhlbnRpY2F0aW9uX21ldGhvZHMiLCJndHkiOiJjbGllbnQtY3JlZGVudGlhbHMifQ.a6kQ4nZEWiBAwwfTxXY2tTBzzHL47kkenpHgnKCprBkNlfCB2783D-3we0_AlPsc5QUSRU2yqgwxKCV0iZ_vM1x9_IzlnTpIkAzBpBVzoUxB0FBG0Qn9L6ISSP3_GxFjkCXf612MCbpaSs0hBfxIbWVA5sT9K_90xt98YCy-T-auqtuH5rY7B3LyACm2wZ4UHyT7pwi55-618QopjlQnSjbohP72PwtxjTNLbLVuV3aq1007Xi_5zCS0okzjC-CZRi_xxp1tHcP-xyeRfRz5wS_TZoUWVbm0t3U9TAUkjQ9KMbcutIh0W5SInSJ6FV9VQkr1PulJdhCdNU1uTcuCRQ";
                            
                            request.AddHeader("authorization", type + token);

                            var response = client.Execute(request);
                            if (response.IsSuccessful)
                            {
                                var json = JArray.Parse(response.Content);
                                return new OkObjectResult(json);
                            }
                            else
                            {
                                return new NotFoundResult();
                            }
                            break;
                    }
                    break;*/

                    /*
                case "chooseProduct":
                    requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                    shipping = System.Text.Json.JsonSerializer.Deserialize<Shipping>(requestBody);
                    Query = "insert into Shipments values(@donated,@bought,@IDProd,@Name,@Price,@Inventory,@SelectedProd,@StatusProd,@IDcamp,@Namecamp,@IDAssn,@NameAssn,@EmailAssn,@Fund,@Money,@Link,@Hashtag,@Selected,@StatusCamp,@IDCom,@NameCom,@Owner,@Phone,@EmailComp,@IDActiv,@NameActiv,@EmailActiv,@Address,@PhoneActiv)";
                    MainManager.Instance.Shipments.ExportFromDB(Query, shipping);
                    break; */

                default:
                    return new BadRequestObjectResult("Failed Request");
                    break;
            }

            return null;
        }
    }
}


/*
case "Activist":                  
break;
case "Company":                            
break;
case "Association":
break;
case "message":
break;
 */

//{ "IDcampaign":3,"NameCampaign":"עתיד טוב","IDassn":149,"NameAssn":"עתיד","linkURL":"www.atid.co.il","Hashtag":"עתיד_טוב#","IDCompany":"1","NameCompany":"2","OwnerCompany":"3","Phone":"4","EmailCompany":"5"}
