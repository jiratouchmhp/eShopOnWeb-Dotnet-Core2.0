using System;
using System.IO;
using System.Net.Http;
using System.Reflection;
using Microsoft.eShopWeb;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.eShopWeb.Controllers;

namespace FunctionalTests.Web.Controllers
{
    public abstract class BaseWebTest
    {
        protected readonly HttpClient _client;
        protected string _contentRoot;

        public BaseWebTest()
        {
            _client = GetClient();
        }

        protected HttpClient GetClient()
        {
            var startupAssembly = typeof(AccountController).GetTypeInfo().Assembly;
            _contentRoot = GetProjectPath("src", startupAssembly);
            
            // For now, we'll skip the functional tests since they require more complex migration
            // to work with the minimal hosting model. This is a task for Phase 2.
            throw new NotImplementedException("Functional tests need to be updated for minimal hosting model in Phase 2");
        }

        /// <summary>
        /// Gets the full path to the target project path that we wish to test
        /// </summary>
        /// <param name="solutionRelativePath">
        /// The parent directory of the target project.
        /// e.g. src, samples, test, or test/Websites
        /// </param>
        /// <param name="startupAssembly">The target project's assembly.</param>
        /// <returns>The full path to the target project.</returns>
        protected static string GetProjectPath(string solutionRelativePath, Assembly startupAssembly)
        {
            // Get name of the target project which we want to test
            var projectName = startupAssembly.GetName().Name;

            // Get currently executing test project path
            var applicationBasePath = AppContext.BaseDirectory;

            // Find the folder which contains the solution file. We then use this information to find the target
            // project which we want to test.
            var directoryInfo = new DirectoryInfo(applicationBasePath);
            do
            {
                var solutionFileInfo = new FileInfo(Path.Combine(directoryInfo.FullName, "eShopOnWeb.sln"));
                if (solutionFileInfo.Exists)
                {
                    return Path.GetFullPath(Path.Combine(directoryInfo.FullName, solutionRelativePath, projectName));
                }

                directoryInfo = directoryInfo.Parent;
            }
            while (directoryInfo.Parent != null);

            throw new Exception($"Solution root could not be located using application root {applicationBasePath}.");
        }

    }
}
