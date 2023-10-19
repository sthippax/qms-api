using QmsDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QmsDAL.DataAccess
{
    public class AuthorizeDAL
    {
        #region GetUserAccount [Code Owner : Chenthilkumaran (11-04-2023)]
        public ContactList GetUserAccount(string indentifier)
        {
            string mailPattern = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*" + "@" + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$";
            Regex mailRegex = new Regex(mailPattern, RegexOptions.IgnoreCase);
            Regex wwidRegex = new Regex("^[0-9]+$");
            Regex idsidRegex = new Regex("^[a-zA-Z0-9]{1,25}$");
            //Regex sys_Regex = new Regex(@"^[a-zA-Z]{3}\_$");

            string path = "GC://corp.intel.com";
            DirectoryEntry entry = new DirectoryEntry(path);
            using (DirectorySearcher mySearcher = new DirectorySearcher(entry))
            {
                if (mailRegex.IsMatch(indentifier))
                {
                    mySearcher.Filter = "(&(objectClass=user)(objectCategory=person)(mail=" + indentifier + "))";
                }
                else if (wwidRegex.IsMatch(indentifier))
                {
                    mySearcher.Filter = "(&(objectClass=user)(objectCategory=person)(employeeid=" + indentifier + "))";
                }
                else if (idsidRegex.IsMatch(indentifier))
                {
                    mySearcher.Filter = "(&(objectClass=user)(objectCategory=person)(mailnickname=" + indentifier + "))";
                }
                else if (indentifier.Contains("_"))
                {
                    mySearcher.Filter = "(&(objectClass=user)(objectCategory=person)(samaccountname=" + indentifier + "))";
                }

                System.DirectoryServices.PropertyCollection props = entry.Properties;


                ContactList emp = new ContactList();

                try
                {
                    if (mySearcher != null)
                    {
                        foreach (SearchResult item in mySearcher.FindAll())
                        {
                            if (item.Properties.Count > 60)
                            {
                                emp.EmailId = item.Properties["mail"][0].ToString();
                                emp.Name = item.Properties["displayname"][0].ToString();
                                emp.IDSID = item.Properties["samaccountname"][0].ToString();
                                emp.WWID = Convert.ToInt32(Convert.ToString(item.Properties["employeeid"][0]).Substring(0, 8));
                                emp.EmployeeBadgeType = indentifier.Contains("_") ? "" : item.Properties["employeebadgetype"][0].ToString();
                                emp.AvatarURL = "https://photos.intel.com/images/" + item.Properties["employeeid"][0].ToString() + ".jpg";
                                emp.Role = "";
                                emp.Domain = "";
                                emp.Comments = "";
                                break;
                            }
                        }
                    }
                    if (emp.EmailId == null)
                    {
                        string mailid = GetUserGroupName(indentifier);
                        if (mailid != null)
                            emp.EmailId = mailid + "@intel.com";
                        else
                            emp.EmailId = null;
                    }
                    entry.Dispose();
                    //mySearcher.Dispose();
                    return emp;
                }
                catch (Exception e)
                {
                    //Logger.Error("GetUserAccount: {0} " + e.Message + e.StackTrace);
                    throw new Exception(e.Message);
                }
                finally
                {
                    //entry.Dispose();
                    //mySearcher.Dispose();               
                }

            }

        }

        #endregion
        #region GetUserGroupName [Code Owner : Chenthilkumaran (11-04-2023)]
        public string GetUserGroupName(string strGroupName)
        {
            string mailPattern = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*" + "@" + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$";
            Regex mailRegex = new Regex(mailPattern, RegexOptions.IgnoreCase);
            Regex wwidRegex = new Regex("^[0-9]+$");
            Regex idsidRegex = new Regex("^[a-zA-Z0-9]{1,25}$");

            string path = "GC://corp.intel.com";
            System.DirectoryServices.DirectoryEntry entry = new System.DirectoryServices.DirectoryEntry(path);
            DirectorySearcher mySearcher = new DirectorySearcher(entry);
            try
            {
                string strEmailName = string.Empty;
                if (!string.IsNullOrEmpty(strGroupName))
                {
                    if (mailRegex.IsMatch(strGroupName) & strGroupName.Contains("@intel.com"))
                    {
                        mySearcher.Filter = "(&(mail=" + strGroupName + "))";
                    }
                    else
                        mySearcher.Filter = "(&(name=" + strGroupName + "))";
                    //if (mySearcher != null)
                    //{

                    mySearcher.PropertiesToLoad.Add("name");
                    SearchResult srEmail = mySearcher.FindOne();
                    if (srEmail != null)
                    {
                        strEmailName = srEmail.Properties["name"][0].ToString();
                        GroupPrincipal getUserGP = null;
                        using (PrincipalContext ctx = new PrincipalContext(ContextType.Domain, "amr"))
                        {
                            try
                            {

                                if (getUserGP == null)
                                {

                                    getUserGP = GroupPrincipal.FindByIdentity(ctx, strEmailName);
                                }
                            }
                            finally
                            {
                                ctx.Dispose();
                            }
                        }
                        using (PrincipalContext ctx1 = new PrincipalContext(ContextType.Domain, "ccr"))
                        {
                            try
                            {

                                if (getUserGP == null)
                                {

                                    getUserGP = GroupPrincipal.FindByIdentity(ctx1, strEmailName);

                                }
                            }
                            finally
                            {
                                ctx1.Dispose();
                            }
                        }
                        using (PrincipalContext ctx2 = new PrincipalContext(ContextType.Domain, "gar"))
                        {
                            try
                            {

                                if (getUserGP == null)
                                {


                                    getUserGP = GroupPrincipal.FindByIdentity(ctx2, strEmailName);
                                }
                            }
                            finally
                            {
                                ctx2.Dispose();
                            }
                        }
                        using (PrincipalContext ctx3 = new PrincipalContext(ContextType.Domain, "ger"))
                        {
                            try
                            {

                                if (getUserGP == null)
                                {

                                    getUserGP = GroupPrincipal.FindByIdentity(ctx3, strEmailName);

                                }
                            }
                            finally
                            {
                                ctx3.Dispose();
                            }
                        }


                        if (getUserGP != null)
                        {

                            return Convert.ToString(getUserGP.DisplayName);

                        }
                        else
                        {
                            return null;
                        }

                    }
                    else
                    {
                        return null;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            finally
            {
                entry.Dispose();
                mySearcher.Dispose();
            }

        }
        #endregion
    }
}
