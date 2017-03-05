# go-cd-console-summary
A web page which picks out MSBuild errors and warnings from a go.cd console.log.  

Set up:

1. Add the file console-summary.html to your project.
1. In Go.CD add console-summary.html as an artifact to the Job.
1. In Go.CD add console-summary.html as a custom tab for the Job.

Console Summary finds the file cruise-output/console.log relative to its current location, scans the file for errors and warnings, and displays the result in the page.  It is designed to pick out [compiler](https://msdn.microsoft.com/en-us/library/yxkt8b26.aspx) and [MSBuild](https://blogs.msdn.microsoft.com/msbuild/2006/11/02/msbuild-visual-studio-aware-error-messages-and-message-formats/) errors and warnings.

