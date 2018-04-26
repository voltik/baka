using System;
using System.DirectoryServices.Protocols;
using System.DirectoryServices.AccountManagement;
using System.Net;
using System.DirectoryServices;

namespace TestAD
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Domain name: " + System.Environment.UserDomainName);
            Console.WriteLine("User name: " + System.Environment.UserName);
            //testAD();
            testLDAP2();
            Console.ReadKey();
        }

        private static void testAD()
        {
            using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, "81.2.234.128"/*"bakalari.local"*/))
            {
                // validate the credentials
                bool isValid = pc.ValidateCredentials("test1", "jen1Pro2Test3!");
                Console.WriteLine("isValid = " + isValid);
                //var usr = UserPrincipal.FindByIdentity(pc, /*IdentityType.SamAccountName, "test1"*/ /*@"bakalari\test1"*/ "test1@bakalari.local");
                var usr = UserPrincipal.FindByIdentity(pc, /*"test1@bakalari.local"*/ "CN=test1 test2,CN=Users,DC=bakalari,DC=local");
                Console.WriteLine("exp date=" + usr.AccountExpirationDate);
                Console.WriteLine("DN=" + usr.DistinguishedName);
            }
        }

        private static void testLDAP()
        {
            var connection = new LdapConnection("ldap.forumsys.com");
            connection.AuthType = AuthType.Basic;
            connection.SessionOptions.ProtocolVersion = 3;
            var credential = new NetworkCredential("cn=read-only-admin,dc=example,dc=com", "password");
            connection.Credential = credential;
            connection.Bind();
            Console.WriteLine("logged in");
        }

        private static void testLDAP2()
        {
            var connection = new LdapConnection("81.2.234.128");
            connection.AuthType = AuthType.Basic;
            connection.SessionOptions.ProtocolVersion = 3;
            var credential = new NetworkCredential("CN=test1 test2,CN=Users,DC=bakalari,DC=local", "jen1Pro2Test3!");
            connection.Credential = credential;
            connection.Bind();
            Console.WriteLine("logged in");

            DirectoryEntry rootEntry = new DirectoryEntry("LDAP://81.2.234.128", "CN=test1 test2,CN=Users,DC=bakalari,DC=local", "jen1Pro2Test3!", AuthenticationTypes.ServerBind);
            
            DirectorySearcher searcher = new DirectorySearcher(rootEntry);
            var queryFormat = "(&(objectClass=user)(objectCategory=person)(sAMAccountName=test1))";
            searcher.Filter = queryFormat;
            foreach (SearchResult result in searcher.FindAll())
            {
                Console.WriteLine("account name: {0}", result.Properties["samaccountname"].Count > 0 ? result.Properties["samaccountname"][0] : string.Empty);
                Console.WriteLine("common name: {0}", result.Properties["cn"].Count > 0 ? result.Properties["cn"][0] : string.Empty);
            }

            /*DirectoryEntry childEntry = rootEntry.Children.Add("CN=TestUserX", "user");
            childEntry.CommitChanges();
            rootEntry.CommitChanges();
            childEntry.Invoke("SetPassword", new object[] { "password" });
            childEntry.CommitChanges();
            */
        }
    }
}
