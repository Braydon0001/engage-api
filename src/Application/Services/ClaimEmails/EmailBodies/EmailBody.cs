using Engage.Application.Services.ClaimEmails.Models;
using System.Text;

namespace Engage.Application.Services.ClaimEmails.EmailBodies;

public static class EmailBody
{
    public static string GetRejectedClaimsBody(string creatorName, string approverName, string claimNumber, string rejectedReason)
    {
        var stringBuilder = new StringBuilder();

        stringBuilder.Append("<table>");

        stringBuilder.Append("<tr>");
        stringBuilder.Append("<td>");
        stringBuilder.Append("<p>");
        stringBuilder.Append("Hi " + creatorName + ",");
        stringBuilder.Append("</p>");
        stringBuilder.Append("</td>");
        stringBuilder.Append("</tr>");

        stringBuilder.Append("<tr>");
        stringBuilder.Append("<td>");
        stringBuilder.Append("<p>");
        stringBuilder.Append("The following Claim Number has been rejected by " + approverName);
        stringBuilder.Append("</p>");
        stringBuilder.Append("</td>");
        stringBuilder.Append("</tr>");

        stringBuilder.Append("<tr>");
        stringBuilder.Append("<td>");
        stringBuilder.Append("<p>");
        stringBuilder.Append("Claim No. " + claimNumber);
        stringBuilder.Append("</p>");
        stringBuilder.Append("<p>");
        stringBuilder.Append("Reason: " + rejectedReason);
        stringBuilder.Append("</p>");
        stringBuilder.Append("</td>");
        stringBuilder.Append("</tr>");

        stringBuilder.Append("<tr>");
        stringBuilder.Append("<td>");
        stringBuilder.Append("<p>");
        stringBuilder.Append("Kind Regards,");
        stringBuilder.Append("<br>");
        stringBuilder.Append("Engage Claims");
        stringBuilder.Append("</p>");
        stringBuilder.Append("</td>");
        stringBuilder.Append("</tr>");

        stringBuilder.Append("</table>");

        return stringBuilder.ToString();
    }

    public static string GetDisputedClaimsBody(string creatorName, string approverName, string claimNumber, string disputedReason)
    {
        var stringBuilder = new StringBuilder();

        stringBuilder.Append("<table>");

        stringBuilder.Append("<tr>");
        stringBuilder.Append("<td>");
        stringBuilder.Append("<p>");
        stringBuilder.Append("Hi " + creatorName + ",");
        stringBuilder.Append("</p>");
        stringBuilder.Append("</td>");
        stringBuilder.Append("</tr>");

        stringBuilder.Append("<tr>");
        stringBuilder.Append("<td>");
        stringBuilder.Append("<p>");
        stringBuilder.Append("The following Claim Number has been disputed by " + approverName);
        stringBuilder.Append("</p>");
        stringBuilder.Append("</td>");
        stringBuilder.Append("</tr>");

        stringBuilder.Append("<tr>");
        stringBuilder.Append("<td>");
        stringBuilder.Append("<p>");
        stringBuilder.Append("Claim No. " + claimNumber);
        stringBuilder.Append("</p>");
        stringBuilder.Append("<p>");
        stringBuilder.Append("Reason: " + disputedReason);
        stringBuilder.Append("</p>");
        stringBuilder.Append("</td>");
        stringBuilder.Append("</tr>");

        stringBuilder.Append("<tr>");
        stringBuilder.Append("<td>");
        stringBuilder.Append("<p>");
        stringBuilder.Append("Kind Regards,");
        stringBuilder.Append("<br>");
        stringBuilder.Append("Engage Claims");
        stringBuilder.Append("</p>");
        stringBuilder.Append("</td>");
        stringBuilder.Append("</tr>");

        stringBuilder.Append("</table>");

        return stringBuilder.ToString();
    }

    public static string GetClaimApprovalReminderBody(string name, string cutOffDate)
    {
        var stringBuilder = new StringBuilder();

        stringBuilder.Append("<table>");

        stringBuilder.Append("<tr>");
        stringBuilder.Append("<td>");
        stringBuilder.Append("<p>");
        stringBuilder.Append("Dear " + name + ",");
        stringBuilder.Append("</p>");
        stringBuilder.Append("</td>");
        stringBuilder.Append("</tr>");

        stringBuilder.Append("<tr>");
        stringBuilder.Append("<td>");
        stringBuilder.Append("<p>");
        stringBuilder.Append("Please note that you have claims that are awaiting your Approval/Dispute. ");
        stringBuilder.Append("</p>");
        stringBuilder.Append("<p>");
        stringBuilder.Append("Please ensure that you approve them before " + cutOffDate + " as failure to do so will result in the claims being automatically approved and billed to you.");
        stringBuilder.Append("</p>");
        stringBuilder.Append("</td>");
        stringBuilder.Append("</tr>");

        stringBuilder.Append("<tr>");
        stringBuilder.Append("<td>");
        stringBuilder.Append("<p>");
        stringBuilder.Append("Kind Regards,");
        stringBuilder.Append("<br>");
        stringBuilder.Append("Engage Claims");
        stringBuilder.Append("</p>");
        stringBuilder.Append("</td>");
        stringBuilder.Append("</tr>");

        stringBuilder.Append("</table>");

        return stringBuilder.ToString();
    }

    public static string GetClaimPaymentBody(string name, List<ClaimNumber> claimNumbers, decimal totalAmount)
    {
        var stringBuilder = new StringBuilder();

        stringBuilder.Append("<table>");

        stringBuilder.Append("<tr>");
        stringBuilder.Append("<td>");
        stringBuilder.Append("<p>");
        stringBuilder.Append("Hi " + name + ",");
        stringBuilder.Append("</p>");
        stringBuilder.Append("</td>");
        stringBuilder.Append("</tr>");

        stringBuilder.Append("<tr>");
        stringBuilder.Append("<td>");
        stringBuilder.Append("<p>");
        stringBuilder.Append("Please note the below claims have been paid into the stipulated account.");
        stringBuilder.Append("</p>");
        stringBuilder.Append("</td>");
        stringBuilder.Append("</tr>");

        stringBuilder.Append("<tr>");
        stringBuilder.Append("<td>");
        stringBuilder.Append("<p>");
        stringBuilder.Append("Claim Numbers:");
        stringBuilder.Append("<br>");
        foreach (var claimNumber in claimNumbers)
        {
            stringBuilder.Append(claimNumber.ClaimNo + ": R" + claimNumber.Amount);
            stringBuilder.Append("<br>");
        }
        stringBuilder.Append("</p>");
        stringBuilder.Append("</td>");
        stringBuilder.Append("</tr>");

        stringBuilder.Append("<tr>");
        stringBuilder.Append("<td>");
        stringBuilder.Append("<p>");
        stringBuilder.Append("Total Paid: R" + totalAmount);
        stringBuilder.Append("</p>");
        stringBuilder.Append("</td>");
        stringBuilder.Append("</tr>");

        stringBuilder.Append("<tr>");
        stringBuilder.Append("<td>");
        stringBuilder.Append("<p>");
        stringBuilder.Append("Kind Regards,");
        stringBuilder.Append("<br>");
        stringBuilder.Append("Engage Claims");
        stringBuilder.Append("</p>");
        stringBuilder.Append("</td>");
        stringBuilder.Append("</tr>");

        stringBuilder.Append("</table>");

        return stringBuilder.ToString();
    }

    public static string GetClaimFloatWarningBody(string name, string title, decimal totalAmount)
    {
        var stringBuilder = new StringBuilder();

        stringBuilder.Append("<table>");

        stringBuilder.Append("<tr>");
        stringBuilder.Append("<td>");
        stringBuilder.Append("<p>");
        stringBuilder.Append("Hi " + name + ",");
        stringBuilder.Append("</p>");
        stringBuilder.Append("</td>");
        stringBuilder.Append("</tr>");

        stringBuilder.Append("<tr>");
        stringBuilder.Append("<td>");
        stringBuilder.Append("<p>");
        stringBuilder.Append("Please note that " + title + " has reached the minimum amount of R" + totalAmount);
        stringBuilder.Append("</p>");
        stringBuilder.Append("</td>");
        stringBuilder.Append("</tr>");

        stringBuilder.Append("<tr>");
        stringBuilder.Append("<td>");
        stringBuilder.Append("<p>");
        stringBuilder.Append("Kind Regards,");
        stringBuilder.Append("<br>");
        stringBuilder.Append("Engage Claims");
        stringBuilder.Append("</p>");
        stringBuilder.Append("</td>");
        stringBuilder.Append("</tr>");

        stringBuilder.Append("</table>");

        return stringBuilder.ToString();
    }
    public static string GetEmployeeTerminationBody
        (string name, string employeeName, string terminationDate, string terminationReason, string terminatedBy)
    {
        var stringBuilder = new StringBuilder();

        stringBuilder.Append("<table>");

        stringBuilder.Append("<tr>");
        stringBuilder.Append("<td>");
        stringBuilder.Append("<p>");
        stringBuilder.Append("Good day " + name + ",");
        stringBuilder.Append("</p>");
        stringBuilder.Append("</td>");
        stringBuilder.Append("</tr>");

        stringBuilder.Append("<tr>");
        stringBuilder.Append("<td>");
        stringBuilder.Append("<p>");
        stringBuilder.Append(employeeName + " has been terminated on " + terminationDate + ".");
        stringBuilder.Append("</p>");
        stringBuilder.Append("</td>");
        stringBuilder.Append("</tr>");

        stringBuilder.Append("<tr>");
        stringBuilder.Append("<td>");
        stringBuilder.Append("<p>");
        stringBuilder.Append("Termination Reason: " + terminationReason + ".");
        stringBuilder.Append("</p>");
        stringBuilder.Append("</td>");
        stringBuilder.Append("</tr>");

        stringBuilder.Append("<tr>");
        stringBuilder.Append("<td>");
        stringBuilder.Append("<p>");
        stringBuilder.Append("Terminated by: " + terminatedBy + ".");
        stringBuilder.Append("</p>");
        stringBuilder.Append("</td>");
        stringBuilder.Append("</tr>");

        stringBuilder.Append("<tr>");
        stringBuilder.Append("<td>");
        stringBuilder.Append("<p>");
        stringBuilder.Append("Kind Regards,");
        stringBuilder.Append("<br>");
        stringBuilder.Append("Engage Team");
        stringBuilder.Append("</p>");
        stringBuilder.Append("</td>");
        stringBuilder.Append("</tr>");

        stringBuilder.Append("</table>");

        return stringBuilder.ToString();
    }
    public static string GetEmployeeStoreCalendarCurrentReportBody(string employeeName)
    {
        StringBuilder stringBuilder = new StringBuilder();

        stringBuilder.Append("<table>");

        stringBuilder.Append("<tr>");
        stringBuilder.Append("<td>");
        stringBuilder.Append("<p>");
        stringBuilder.Append("Good day " + employeeName + ",");
        stringBuilder.Append("</p>");
        stringBuilder.Append("</td>");
        stringBuilder.Append("</tr>");

        stringBuilder.Append("<tr>");
        stringBuilder.Append("<td>");
        stringBuilder.Append("<p>");
        stringBuilder.Append("Find attached this week's Store visits.");
        stringBuilder.Append("</p>");
        stringBuilder.Append("</td>");
        stringBuilder.Append("</tr>");

        stringBuilder.Append("<tr>");
        stringBuilder.Append("<td>");
        stringBuilder.Append("<p>");
        stringBuilder.Append("Kind Regards,");
        stringBuilder.Append("<br>");
        stringBuilder.Append("Engage Team");
        stringBuilder.Append("</p>");
        stringBuilder.Append("</td>");
        stringBuilder.Append("</tr>");

        stringBuilder.Append("</table>");

        return stringBuilder.ToString();
    }
    public static string GetEmployeeStoreCalendarPreviousReportBody(string employeeName)
    {
        StringBuilder stringBuilder = new StringBuilder();

        stringBuilder.Append("<table>");

        stringBuilder.Append("<tr>");
        stringBuilder.Append("<td>");
        stringBuilder.Append("<p>");
        stringBuilder.Append("Good day " + employeeName + ",");
        stringBuilder.Append("</p>");
        stringBuilder.Append("</td>");
        stringBuilder.Append("</tr>");

        stringBuilder.Append("<tr>");
        stringBuilder.Append("<td>");
        stringBuilder.Append("<p>");
        stringBuilder.Append("Please find attached the Employee Store Calendar report for last week.");
        stringBuilder.Append("</p>");
        stringBuilder.Append("</td>");
        stringBuilder.Append("</tr>");

        stringBuilder.Append("<tr>");
        stringBuilder.Append("<td>");
        stringBuilder.Append("<p>");
        stringBuilder.Append("Kind Regards,");
        stringBuilder.Append("<br>");
        stringBuilder.Append("Engage Team");
        stringBuilder.Append("</p>");
        stringBuilder.Append("</td>");
        stringBuilder.Append("</tr>");

        stringBuilder.Append("</table>");

        return stringBuilder.ToString();
    }
    public static string GetContactReportCompleteBody(string storeName, string employeeName)
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("<table>");

        stringBuilder.Append("<tr>");
        stringBuilder.Append("<td>");
        stringBuilder.Append("<p>");
        stringBuilder.Append("Good day,");
        stringBuilder.Append("</p>");
        stringBuilder.Append("</td>");
        stringBuilder.Append("</tr>");

        stringBuilder.Append("<tr>");
        stringBuilder.Append("<td>");
        stringBuilder.Append("<p>");
        stringBuilder.Append("Please find attached the contact report for " + storeName + " completed by " + employeeName);
        stringBuilder.Append("</p>");
        stringBuilder.Append("</td>");
        stringBuilder.Append("</tr>");

        stringBuilder.Append("<tr>");
        stringBuilder.Append("<td>");
        stringBuilder.Append("<p>");
        stringBuilder.Append("Kind Regards,");
        stringBuilder.Append("<br>");
        stringBuilder.Append("Engage Team");
        stringBuilder.Append("</p>");
        stringBuilder.Append("</td>");
        stringBuilder.Append("</tr>");

        stringBuilder.Append("</table>");

        return stringBuilder.ToString();
    }
    public static string GetOrderSubmitBody(string orderCreator, string storeName, string orderDate)
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("<table>");

        stringBuilder.Append("<tr>");
        stringBuilder.Append("<td>");
        stringBuilder.Append("<p>");
        stringBuilder.Append("Good day.");
        stringBuilder.Append("</p>");
        stringBuilder.Append("</td>");
        stringBuilder.Append("</tr>");

        stringBuilder.Append("<tr>");
        stringBuilder.Append("<td>");
        stringBuilder.Append("<p>");
        stringBuilder.Append(" ");
        stringBuilder.Append("</p>");
        stringBuilder.Append("</td>");
        stringBuilder.Append("</tr>");

        stringBuilder.Append("<tr>");
        stringBuilder.Append("<td>");
        stringBuilder.Append("<p>");
        stringBuilder.Append($"Please find attached the order summery for an order placed by {orderCreator} for {storeName} on {orderDate}.");
        stringBuilder.Append("</p>");
        stringBuilder.Append("</td>");
        stringBuilder.Append("</tr>");

        stringBuilder.Append("<tr>");
        stringBuilder.Append("<td>");
        stringBuilder.Append("<p>");
        stringBuilder.Append("Kind Regards,");
        stringBuilder.Append("<br>");
        stringBuilder.Append("Engage Team");
        stringBuilder.Append("</p>");
        stringBuilder.Append("</td>");
        stringBuilder.Append("</tr>");

        stringBuilder.Append("</table>");

        return stringBuilder.ToString();
    }
    public static string GetEmployeeStoreCalendarManagerStoreVisit(string name, string storeName, string calendarDate)
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("<table>");

        stringBuilder.Append("<tr>");
        stringBuilder.Append("<td>");
        stringBuilder.Append("<p>");
        stringBuilder.Append("Good day,");
        stringBuilder.Append("</p>");
        stringBuilder.Append("</td>");
        stringBuilder.Append("</tr>");

        stringBuilder.Append("<tr>");
        stringBuilder.Append("<td>");
        stringBuilder.Append("<p>");
        stringBuilder.Append(" ");
        stringBuilder.Append("</p>");
        stringBuilder.Append("</td>");
        stringBuilder.Append("</tr>");

        stringBuilder.Append("<tr>");
        stringBuilder.Append("<td>");
        stringBuilder.Append("<p>");
        stringBuilder.Append($"A new store visit has been created for you at {storeName} on {calendarDate} by {name}.");
        stringBuilder.Append("</p>");
        stringBuilder.Append("</td>");
        stringBuilder.Append("</tr>");

        stringBuilder.Append("<tr>");
        stringBuilder.Append("<td>");
        stringBuilder.Append("<p>");
        stringBuilder.Append("Kind Regards,");
        stringBuilder.Append("<br>");
        stringBuilder.Append("Engage Team");
        stringBuilder.Append("</p>");
        stringBuilder.Append("</td>");
        stringBuilder.Append("</tr>");

        stringBuilder.Append("</table>");

        return stringBuilder.ToString();
    }

    public static string GetOrderStagingSubmitBody(string orderCreator, string storeName, string orderDate)
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("<table>");

        stringBuilder.Append("<tr>");
        stringBuilder.Append("<td>");
        stringBuilder.Append("<p>");
        stringBuilder.Append("Good day.");
        stringBuilder.Append("</p>");
        stringBuilder.Append("</td>");
        stringBuilder.Append("</tr>");

        stringBuilder.Append("<tr>");
        stringBuilder.Append("<td>");
        stringBuilder.Append("<p>");
        stringBuilder.Append(" ");
        stringBuilder.Append("</p>");
        stringBuilder.Append("</td>");
        stringBuilder.Append("</tr>");

        stringBuilder.Append("<tr>");
        stringBuilder.Append("<td>");
        stringBuilder.Append("<p>");
        stringBuilder.Append($"Please find attached the order summery for an order placed by {orderCreator} for {storeName} on {orderDate}.");
        stringBuilder.Append("</p>");
        stringBuilder.Append("</td>");
        stringBuilder.Append("</tr>");

        stringBuilder.Append("<tr>");
        stringBuilder.Append("<td>");
        stringBuilder.Append("<p>");
        stringBuilder.Append("Kind Regards,");
        stringBuilder.Append("<br>");
        stringBuilder.Append("Engage Team");
        stringBuilder.Append("</p>");
        stringBuilder.Append("</td>");
        stringBuilder.Append("</tr>");

        stringBuilder.Append("</table>");

        return stringBuilder.ToString();
    }

    public static string GetSurveyFormPOSUpdateBody(string employeeName, string storeName, string requestDate, string submissionId)
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("<table>");

        stringBuilder.Append("<tr>");
        stringBuilder.Append("<td>");
        stringBuilder.Append("<p>");
        stringBuilder.Append($"Employee {employeeName} is completing a POS update survey for {storeName}.");
        stringBuilder.Append("</p>");
        stringBuilder.Append("</td>");
        stringBuilder.Append("</tr>");

        stringBuilder.Append("<tr>");
        stringBuilder.Append("<td>");
        stringBuilder.Append("<p>");
        stringBuilder.Append("Please issue the relevant POS Update.");
        stringBuilder.Append("</p>");
        stringBuilder.Append("</td>");
        stringBuilder.Append("</tr>");

        stringBuilder.Append("<tr>");
        stringBuilder.Append("<td>");
        stringBuilder.Append("<p>");
        stringBuilder.Append("Details:");
        stringBuilder.Append("<br>");
        stringBuilder.Append($"Employee: {employeeName}");
        stringBuilder.Append("<br>");
        stringBuilder.Append($"Store: {storeName}");
        stringBuilder.Append("<br>");
        stringBuilder.Append($"Submission ID: {submissionId}");
        stringBuilder.Append("<br>");
        stringBuilder.Append($"Request Date: {requestDate}");
        stringBuilder.Append("</p>");
        stringBuilder.Append("</td>");
        stringBuilder.Append("</tr>");

        stringBuilder.Append("</table>");

        return stringBuilder.ToString();
    }
}
