using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using NUnit.Framework;

namespace RegexTests
{
    /*
         https://blogs.msdn.microsoft.com/msbuild/2006/11/02/msbuild-visual-studio-aware-error-messages-and-message-formats/
         By Mike Fourie
         MSBuild recognizes error messages and warnings that have been specially formatted by many command line tools that typically write to the console. 
         For instance, take a look at the following error messages – they are all properly formatted to be MSBuild and Visual Studio friendly.

         Main.cs(17,20): warning CS0168: The variable ‘foo’ is declared but never used
         C:\dir1\foo.resx(2) : error BC30188: Declaration expected.
         cl : Command line warning D4024 : unrecognized source file type ‘foo.cs’, object file assumed
         error CS0006: Metadata file ‘System.dll’ could not be found.

         These messages confirm to special format that is shown below, and comprise 5 parts – the order of these parts are important and should not change: 

         Origin (Required)
         Origin can be blank. If present, the origin is usually a tool name, like ‘cl’ in one of the examples. 
         But it could also be a filename, like ‘Main.cs’ shown in another example. 
         If it is a filename, then it must be an absolute or a relative filename, followed by an optional paranthesized line/column information in one of the following forms:

            (line) or (line-line) or (line-col) or (line,col-col) or (line,col,line,col)

         Lines and columns start at 1 in a file – i.e. the beginning of a file is 1, and the leftmost column is 1. If the Origin is a tool name, then it must not change based on local – i.e. it needs to be locale neutral.

         Subcategory (Optional)
         Subcategory is used to classify the category itself further, and should not be localized.

         Category (Required)
         Category must be either ‘error’ or ‘warning’. Case does not matter. Like origin, category must not be localized.

         Code (Required)
         Code identifies an application specific error code / warning code. Code must not be localized and it must not contain spaces.

         Text (Optional)
         User friendly text that explains the error, and *must* be localized if you cater to multiple locales.

        https://msdn.microsoft.com/en-us/library/yxkt8b26.aspx
        By MSDN

        The format of the output should be:
        
        {filename (line# [, column#]) | toolname} :[any text] {error | warning} code####:localizable string[ any text ]

        Where:
        {a | b} is a choice of either a or b.
        [ccc] is an optional string or parameter.
        
        For example:
        C:\sourcefile.cpp(134) : error C2143: syntax error : missing ';' before '}'
        LINK : fatal error LNK1104: cannot open file 'somelib.lib'
    */


    [TestFixture]
    public class TestRegex
    {
        public static Regex Regex => new Regex(@"^.*(error|warning) [a-zA-Z]+[0-9]+[ ]?:.*$");

        [TestCase("BuildServer1", @"16:36:30.960 Fakes\DirectaApiProviderFake.cs(13,43): error CS0535: 'DirectaApiProviderFake' does not implement interface member 'IDirectaApiProvider.GetSiteResource(string)' [C:\GoAgent\pipelines\NetBackupAdapterTrunk\NetBackupAdapter.Testing\NetBackupAdapter.Testing.csproj]")]
        [TestCase("BuildServer2", @"17:00:50.705 C:\GoAgent\pipelines\DirectaTrunk\packages\Microsoft.TypeScript.MSBuild.2.2.1\tools\microsoft.TypeScript.targets(235,7): error MSB4064: The ""OutputLogFile"" parameter is not supported by the ""VsTsc"" task. Verify the parameter exists on the task, and it is a settable public instance property. [C:\GoAgent\pipelines\DirectaTrunk\Biomni.Directa.Web.UI.Pages\Biomni.Directa.Web.UI.Pages.csproj]")]
        [TestCase("MikeFourie1", @"Main.cs(17,20): warning CS0168: The variable ‘foo’ is declared but never used")]
        [TestCase("MikeFourie2", @"C:\dir1\foo.resx(2) : error BC30188: Declaration expected.")]
        [TestCase("MikeFourie3", @"cl : Command line warning D4024 : unrecognized source file type ‘foo.cs’, object file assumed")]
        [TestCase("MikeFourie4", @"error CS0006: Metadata file ‘System.dll’ could not be found.")]
        [TestCase("Msdn1", @"C:\sourcefile.cpp(134) : error C2143: syntax error : missing ';' before '}'")]
        [TestCase("Msdn2", @"LINK : fatal error LNK1104: cannot open file 'somelib.lib'")]
        public void TheseStringsShouldBeMatched(string name, string input)
        {
            Assert.That(Regex.IsMatch(input), Is.True, name);
        }

        [TestCase("WarningSummary", "16:36:21.085       0 Warning(s)")]
        [TestCase("ErrorSummary", "16:36:21.085       0 Error(s)")]
        [TestCase("BuildServerFalsePos1", @"15:03:02.475 Copying file from ""C:\GoAgent\pipelines\DirectaTrunk\Deployment\..\Biomni.Directa.Web.UI.Pages\Images\Icons\ServiceCatalogue\warning.png"" to ""C:\GoAgent\pipelines\DirectaTrunk\DirectaArtifacts\Deployment\Output\WebSite\Images\Icons\ServiceCatalogue\warning.png"".")]
        public void TheseStringsShouldNotBeMatched(string name, string input)
        {
            Assert.That(Regex.IsMatch(input), Is.False, name);
        }
    }
}
