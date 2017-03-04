using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace GoCdConsoleSummary
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                var info = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location);

                Console.WriteLine($@"{info.InternalName}
Version {info.ProductVersion}
{info.LegalCopyright}

{info.Comments}

Usage:
    {info.InternalName} [output-file]
Examples:
    {info.InternalName} Console-Summary.html");
                Environment.Exit(1);
            }

            var template = GetConsoleSummaryTemplate("GoCdConsoleSummary.Template.html");
            var url = GetUrl();
            File.WriteAllText(args[0], template.Replace("##URL##", url));
        }

        public static string GetUrl()
        {
            string url = ConfigurationManager.AppSettings["gocdUrl"];
            string pipelineName = Environment.GetEnvironmentVariable("GO_PIPELINE_NAME");      
            string pipelineCounter = Environment.GetEnvironmentVariable("GO_PIPELINE_COUNTER");
            string stageName = Environment.GetEnvironmentVariable("GO_STAGE_NAME");            
            string stageCounter = Environment.GetEnvironmentVariable("GO_STAGE_COUNTER");      
            string jobName = Environment.GetEnvironmentVariable("GO_JOB_NAME");                              
            return $"{url}/files/{pipelineName}/{pipelineCounter}/{stageName}/{stageCounter}/{jobName}/cruise-output/console.log";
        }

        public static string GetConsoleSummaryTemplate(string nameAndNamespace)
        {
            using (var reader = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(nameAndNamespace)))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
