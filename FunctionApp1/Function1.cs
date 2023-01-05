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

namespace PromoIt.MicroServer
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", "put", "delete", Route = "{User}/{page?}/{action}/{IdNumber?}")] HttpRequest req, string User, string action, string IdNumber,
            ILogger log)
        {

            Hashtable hash;
            Activist Activ = new Activist();
            Company company = new Company();
            Association Asso = new Association();
            Message message = new Message();
            CampaignOfAsso campaignAsso = new CampaignOfAsso();

            string responseMessage;
            string requestBody;
            string Query;

            

            log.LogInformation("C# HTTP trigger function processed a request.");

            switch (action)
            {
                case "Get":
                    if (IdNumber == null)
                    {
                        if (User== "User")
                        {
                            hash = (Hashtable)MainManager.Instance.CampaignsAsso.ImportData("select * from campaignAsso");
                            responseMessage = System.Text.Json.JsonSerializer.Serialize(hash);
                            return new OkObjectResult(responseMessage);
                        }
                        else
                        {
                            switch (User)
                            {
                                case "Activist":
                                    hash = (Hashtable)MainManager.Instance.Activists.ImportData("select * from Activists");
                                    break;
                                case "Company":
                                    hash = (Hashtable)MainManager.Instance.Companies.ImportData("select * from companies");
                                    break;
                                case "Association":
                                    hash = (Hashtable)MainManager.Instance.Associations.ImportData("select * from Associations");
                                    break;

                                    responseMessage = System.Text.Json.JsonSerializer.Serialize(hash);
                                    return new OkObjectResult(responseMessage);
                            }
                        }

                    }
                    else
                    {
                        switch (User)
                        {
                            case "Activist":
                                hash = (Hashtable)MainManager.Instance.Activists.ImportData("select * from Activists");
                                Activ = (Activist)hash[IdNumber];
                                responseMessage = System.Text.Json.JsonSerializer.Serialize(Activ);
                                break;
                            case "Company":
                                hash = (Hashtable)MainManager.Instance.Companies.ImportData("select * from companies");
                                company = (Company)hash[IdNumber];
                                responseMessage = System.Text.Json.JsonSerializer.Serialize(company);
                                break;
                            case "Association":
                                hash = (Hashtable)MainManager.Instance.Associations.ImportData("select * from Associations");
                                Asso = (Association)hash[IdNumber];
                                responseMessage = System.Text.Json.JsonSerializer.Serialize(Asso);
                                break;
                        }
                    }
                    break;

                case "Add":
                    switch (User)
                    {
                        case "Activist":
                            //SumOfMoney
                            //Activ.SumOfMoney = 0;
                            requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                            Activ = System.Text.Json.JsonSerializer.Deserialize<Activist>(req.Body);
                            Query = "insert into Activists values(@ID,@Name,@Email,@Address,@Phone,@Money)";
                            MainManager.Instance.Activists.ExportFromDB(Query, Activ);

                            break;
                        case "Company":
                            company = System.Text.Json.JsonSerializer.Deserialize<Company>(req.Body);
                            Query = "insert into companies values(@ID,@Name,@Owner,@Email,@Phone)";
                            MainManager.Instance.Companies.ExportFromDB(Query, company);
                            break;
                        case "Association":
                            // לבדוק משתנים בפקודות
                            Asso = System.Text.Json.JsonSerializer.Deserialize<Association>(req.Body);
                            Query = "insert into Associations values(@ID,@Name,@Email)";
                            MainManager.Instance.Associations.ExportFromDB(Query, Asso);
                            break;
                    }
                    break;

                case "Campaign":
                    campaignAsso = System.Text.Json.JsonSerializer.Deserialize<CampaignOfAsso>(req.Body);
                    Query = "insert into campaignAsso values(@Name,@IDAssn,@NameAssn,@Link,@Hashtag)";
                    MainManager.Instance.CampaignsAsso.ExportFromDB(Query, campaignAsso);
                    break;

                case "message":
                    Message data = new Message();
                    data = System.Text.Json.JsonSerializer.Deserialize<Message>(req.Body);
                    Query = "insert into messages values(@ID,@Name,@Phone,@Email,@Message)";
                    MainManager.Instance.Messages.ExportFromDB(Query, data);
                    break;

                case "GetList":
                    Query = "select * from campaignAsso where IDassn = " + IdNumber;
                    hash = (Hashtable)MainManager.Instance.Activists.ImportData(Query);
                    responseMessage = System.Text.Json.JsonSerializer.Serialize(hash);
                    break;

                case "Update":
                    //Product product = new Product();
                    //requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                    switch (User)
                    {
                        case "Activist":
                            Activ = System.Text.Json.JsonSerializer.Deserialize<Activist>(req.Body);
                            Query = "update Activists set NameActivist=@Name,EmailActivist=@Email,AddressActivist=@Address,phoneActivist=@phone where IDactivist = @ID";
                            MainManager.Instance.Activists.ExportFromDB(Query, Activ);
                            break;
                        case "Company":
                            company = System.Text.Json.JsonSerializer.Deserialize<Company>(req.Body);
                            Query = "update companies set NameCompany=@Name,OwnerCompany=@Owner,Phone=@Phone,EmailCompany=@Email where IDCompany = @ID";
                            MainManager.Instance.Companies.ExportFromDB(Query, company);
                            break;
                        case "Association":
                            Asso = System.Text.Json.JsonSerializer.Deserialize<Association>(req.Body);
                            Query = "update Associations set NameAssn=@Name,EmailAssn=@Email,linkURL=@link,Hashtag=@Hashtag where IDassn = @ID";
                            MainManager.Instance.Associations.ExportFromDB(Query, Asso);
                            break;
                    }
                    break;

                case "Remove":
                    //requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                    switch (User)
                    {
                        case "Activist":
                            Activ = System.Text.Json.JsonSerializer.Deserialize<Activist>(req.Body);
                            Query = "delete from Activists where IDactivist = @ID";
                            MainManager.Instance.Activists.ExportFromDB(Query, Activ);

                            break;
                        case "Company":
                            company = System.Text.Json.JsonSerializer.Deserialize<Company>(req.Body);
                            Query = "delete from companies where IDCompany = @ID";
                            MainManager.Instance.Companies.ExportFromDB(Query, company);
                            break;
                        case "Association":
                            Asso = System.Text.Json.JsonSerializer.Deserialize<Association>(req.Body);
                            Query = "delete from Associations where IDassn = @ID";
                            MainManager.Instance.Associations.ExportFromDB(Query, Asso);
                            break;
                    }
                    break;



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