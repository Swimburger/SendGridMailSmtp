using System.Net;
using System.Net.Mail;
using System.Net.Mime;

using var message = new MailMessage();
message.From = new MailAddress(
    "[REPLACE WITH YOUR EMAIL]",
    "[REPLACE WITH YOUR NAME]"
);
message.To.Add(new MailAddress(
    "[REPLACE WITH DESIRED TO EMAIL]",
    "[REPLACE WITH DESIRED TO NAME]"
));
message.Subject = "Sending with Twilio SendGrid is Fun";
var textBody = "and easy to do anywhere, especially with C#";
var htmlBody = "and easy to do anywhere, <b>especially with C#</b>";

message.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(
    textBody, null, MediaTypeNames.Text.Plain)
);
message.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(
    htmlBody, null, MediaTypeNames.Text.Html)
);

// if you only want to send HTML email, simply use this
// message.Body = htmlBody;
// message.IsBodyHtml = true;

using var client = new SmtpClient(host: "smtp.sendgrid.net", port: 587);
client.EnableSsl = true; // IMPORTANT to send mails securely
client.Credentials = new NetworkCredential(
    userName: "apikey", // the userName is the exact string "apikey" and not the API key itself.
    password: Environment.GetEnvironmentVariable("SendGridApiKey") // password is the API key
);

Console.WriteLine("Sending email");
await client.SendMailAsync(message);
Console.WriteLine("Email sent");