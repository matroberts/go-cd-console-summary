# go-cd-console-summary
Makes a web page which picks out errors and warnings from a go.cd console.log.  

How to set up console summary:

1. Add the file console-summary.html to your project.
1. In Go.CD add console-summary.html as an artifact to the Job.
1. In Go.CD add console-summary.html as a custom tab for the Job.

Console Summary finds the file cruise-output/console.log relative to its current location, scans the file for errors and warnings, and displays the result in the page.
