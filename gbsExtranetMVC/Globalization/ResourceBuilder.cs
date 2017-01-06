using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Resources.Abstract;
using Resources.Entities;

namespace Resources.Utility
{
    public class ResourceBuilder
    {


        /// <summary>
        /// Generates a class with properties for each resource key
        /// </summary>
        /// <param name="provider">Resource provider instance</param>
        /// <param name="namespaceName">Name of namespace containing the resource class</param>
        /// <param name="className">Name of the class</param>
        /// <param name="filePath">Where to generate the source file</param>
        /// <param name="summaryCulture">If not null, adds a &lt;summary&gt; tag to each property using the specified culture.</param>
        /// <returns>Generated class file full path</returns>
        public string Create(BaseResourceProvider provider, string namespaceName = "Resources", string className = "Resources", string filePath = null, string summaryCulture = null)
        {

            List<string> ListOfFunctionNames = new List<string>();
            // Retrieve all resources           
            MethodInfo method = provider.GetType().GetMethod("ReadResources", BindingFlags.Instance | BindingFlags.NonPublic);

            IList<ResourceEntry> resources = method.Invoke(provider, null) as List<ResourceEntry>;

            if (resources == null || resources.Count == 0)
                throw new Exception(string.Format("No resources were found in {0}", provider.GetType().Name));

            // Get a unique list of resource names (keys)
            var keys = resources.Select(r => r.Name).Distinct();

            #region Templates
            const string header =
@"using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Resources.Abstract;
using Resources.Concrete;
    
namespace {0} {{
        public class {1} {{
            private static IResourceProvider resourceProvider = new {2}();

    {3}
        }}        
}}"; // {0}: namespace {1}:class name   {2}:provider class name   {3}: body  

            const string property =
@"
        {1}
        public static {2} {3} {{
               get {{
                   return ({2}) resourceProvider.GetResource(""{0}"", CultureInfo.CurrentUICulture.Name);
               }}
            }}"; // {0}: key
            #endregion


            string lineSeparator = ((char)0x2028).ToString();
            string paragraphSeparator = ((char)0x2029).ToString();

            // store keys in a string builder
            var sbKeys = new StringBuilder();

            foreach (string key in keys)
            {
                var resource = resources.Where(r => (summaryCulture == null ? true : r.Culture.ToLowerInvariant() == summaryCulture.ToLowerInvariant()) && r.Name == key).FirstOrDefault();
                if (resource == null)
                {
                    throw new Exception(string.Format("Could not find resource {0}", key));
                }

                string ResourceValue = (resource.Value != null ? resource.Value.Replace("\r", string.Empty).Replace("\n", string.Empty).Replace(Environment.NewLine, string.Empty).Replace(lineSeparator, string.Empty).Replace(paragraphSeparator, string.Empty).Replace("©", " Copy Right Symbol:") : "");
                string FunctionName = key.Replace(" ", "").Replace(".", "_").Replace("%", "_Percent").Replace("&", "n").Replace("/", string.Empty).Replace("!", string.Empty).Replace("-", "_");

                string NumsinFunctionANme = new String(FunctionName.TakeWhile(Char.IsDigit).ToArray());

                if (NumsinFunctionANme != "")
                {
                    FunctionName = FunctionName.Replace(NumsinFunctionANme, string.Empty);
                }

                if (ListOfFunctionNames.Any(a => a == FunctionName) == false)
                {

                    sbKeys.Append(new String(' ', 12)); // indentation
                    sbKeys.AppendFormat(property, key,
                        summaryCulture == null ? string.Empty : string.Format("/// <summary>{0}</summary>", ResourceValue),
                        resource.Type, FunctionName);
                    sbKeys.AppendLine();

                    ListOfFunctionNames.Add(FunctionName);
                }
            }


           
            if (filePath == null)
                filePath = Path.Combine(@"Some Physical Path, Normally I sent it from Global.asax", "Resources.cs");
            // write to file
            using (var writer = File.CreateText(filePath))
            {

                // write header along with keys
                writer.WriteLine(header, namespaceName, className, provider.GetType().Name, sbKeys.ToString());

                writer.Flush();
                writer.Close();
            }

            return filePath;
        }


    }
}
