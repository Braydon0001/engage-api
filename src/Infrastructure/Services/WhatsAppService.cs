using Newtonsoft.Json;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Engage.Infrastructure.Services;

public class WhatsAppService : IWhatsAppService
{
    private readonly TwilioOptions _twilioOptions;
    public WhatsAppService(IOptions<TwilioOptions> twilioOptions)
    {
        _twilioOptions = twilioOptions.Value;
    }
    public async Task<OperationStatus> SendMessageAsync(List<string> toMobileNumbers, string fromMobileNumber, string message, string externalTemplateId, List<string> mediaUrls = null, Dictionary<string, string> parameters = null, CancellationToken cancellationToken = default)
    {
        TwilioClient.Init(_twilioOptions.AccountSid, _twilioOptions.AuthToken);
        //var templateSID = "HXb608f6fb686a1adaa552dca44722059c";
        //var templateSID = "HXaff434b4bcf761bc4f4f91709343536c";
        //var mediaTemplateSID = "HX242de9a8a291659644e8cb4f93742413";
        //var messagingSID = "MG53471f0a9a34be8286819950ececb666";
        //    var parameters = new Dictionary<string, string>
        //{
        //    {"userEmail", "engage@mail.com"},
        //    {"storeName", "PHARMACY AT SPAR SALT ROCK"},
        //    {"priorityName", "High"},
        //    {"typeName", "Information"},
        //    {"subTypeName", "Promotion"},
        //    {"categoryName", "Pet Food"},
        //    {"subCategoryName", "Cat Food"},
        //    {"supplierName", "Encore"},
        //    {"statusName", "Assigned"},
        //    {"assignedName", "khubonent@gmail.com"},
        //    {"shortDescription", "Test"},
        //    {"comment", "Test Comment"},
        //    {"iid", "166"},
        //    {"sid", "671"}
        //};

        var contentVariablesJson = JsonConvert.SerializeObject(parameters);
        var opStatus = new OperationStatus();

        foreach (var toMobileNumber in toMobileNumbers)
        {
            try
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    opStatus.Status = false;
                    opStatus.Message = "Operation Cancelled";
                    break;
                }

                var messageOptions = new CreateMessageOptions(new PhoneNumber($"whatsapp:{toMobileNumber}"))
                {
                    From = new PhoneNumber($"whatsapp:{fromMobileNumber}"),
                    ContentSid = externalTemplateId,
                    MessagingServiceSid = _twilioOptions.MessagingServiceSid,
                    ContentVariables = contentVariablesJson,
                };

                //if (mediaUrls != null && mediaUrls.Count != 0)
                //{
                //    messageOptions.MediaUrl = mediaUrls.Select(url => new Uri(url)).ToList();
                //}

                var response = await MessageResource.CreateAsync(messageOptions);
            }
            catch (Exception)
            {
                //Continue for now if message not sent
                //We should possibly send an email to the admin person about the message not being sent
                //The most common reason we get here is because the mobile number is not valid or in an incorrect format
                continue;
            }
        }

        return new OperationStatus { Status = true };
    }
}
