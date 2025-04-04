﻿namespace CCP.Service.DTOs
{
    public class EmailBodyTemplate
    {
        public static string GetThankYouEmail(string userName)
        {
            return $@"
    <!DOCTYPE html>
    <html lang='en'>
    <head>
        <meta charset='UTF-8'>
        <meta name='viewport' content='width=device-width, initial-scale=1.0'>
        <title>Thank You</title>
        <style>
            body {{
                font-family: Arial, sans-serif;
                background-color: #f4f4f4;
                text-align: center;
                padding: 20px;
            }}
            .container {{
                background-color: #ffffff;
                padding: 20px;
                border-radius: 8px;
                box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.1);
                max-width: 600px;
                margin: auto;
            }}
            .content {{
                padding: 20px;
            }}
        </style>
    </head>
    <body>
        <div class='container'>
            <div class='content'>
                <h2>Thank You, {userName}!</h2>
                <p>We appreciate your time and effort in being a part of our community.</p>
                <p>If you have any questions, feel free to reach out.</p>
                <p>Best regards,<br> The Team</p>
            </div>
        </div>
    </body>
    </html>";
        }

        public static string GetRegistrationConfirmationEmail(string imgUrl, string email, string linkUrl)
        {
            return $@"
    <!DOCTYPE html>
    <html lang='en'>
    <head>
        <meta charset='UTF-8'>
        <meta name='viewport' content='width=device-width, initial-scale=1.0'>
        <title>Registration Confirmation</title>
        <style>
            body {{
                font-family: Arial, sans-serif;
                background-color: #f4f4f4;
                text-align: center;
                padding: 20px;
            }}
            .container {{
                background-color: #ffffff;
                padding: 20px;
                border-radius: 8px;
                box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.1);
                max-width: 600px;
                margin: auto;
            }}
            .banner {{
                width: 100%;
                max-height: 200px;
                object-fit: cover;
                border-radius: 8px 8px 0 0;
            }}
            .content {{
                padding: 20px;
            }}
            .button {{
                display: inline-block;
                background-color: #007bff;
                color: #ffffff;
                padding: 12px 20px;
                text-decoration: none;
                border-radius: 5px;
                font-weight: bold;
                margin-top: 20px;
            }}
        </style>
    </head>
    <body>
        <div class='container'>
            <img src='{imgUrl}' alt='Banner' class='banner'>
            <div class='content'>
                <h2>Welcome to ChildCare Platform!</h2>
                <p>Dear {email},</p>
                <p>Thank you for registering! Please confirm your email address by clicking the button below.</p>
                <a href='{linkUrl}' class='button'>Confirm Registration</a>
                <p>If you didn’t request this, you can safely ignore this email.</p>
                <p>Best regards,<br> The Team</p>
            </div>
        </div>
    </body>
    </html>";
        }
    }
}
