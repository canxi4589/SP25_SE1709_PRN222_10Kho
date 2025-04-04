﻿using CCP.Repositori.Entities;
using CCP.Service.Currency;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Globalization;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;

namespace CCP.Service.Vnpay
{
    public class VnPay : Ivnpay
    {
        private readonly string vnp_Url = "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html";
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public VnPay(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
            vnp_HashSecret = _configuration["VnPay:HashSecret"];
            vnp_TmnCode = _configuration["VnPay:TmnCode"];
        }

        private readonly string vnp_HashSecret;
        private readonly string vnp_TmnCode;
        public string CreatePaymentUrl1(decimal amount, string returnUrl)
        {
            returnUrl = "https://localhost:7143/api/Payment/PaymentReturn-VNPAY";

            var vnPay = new VnPayLibrary();
            var amount1 = (long)Math.Round(amount * 230000);

            vnPay.AddRequestData("vnp_Amount", (amount1).ToString());
            vnPay.AddRequestData("vnp_Command", "pay");
            vnPay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
            vnPay.AddRequestData("vnp_CurrCode", "VND");
            vnPay.AddRequestData("vnp_IpAddr", "127.0.0.1");
            vnPay.AddRequestData("vnp_Locale", "vn");
            vnPay.AddRequestData("vnp_OrderInfo", $"Thanh toan don hang: {Guid.NewGuid()}");
            vnPay.AddRequestData("vnp_OrderType", "other");
            vnPay.AddRequestData("vnp_ReturnUrl", returnUrl);
            vnPay.AddRequestData("vnp_TmnCode", "7CY9UEAF");
            vnPay.AddRequestData("vnp_TxnRef", Guid.NewGuid().ToString());
            vnPay.AddRequestData("vnp_Version", "2.1.0");

            string paymentUrl = vnPay.CreateRequestUrl(vnp_Url, "DIGHI9T61AVLTF4C28ZTV6BX4HKI027T");
            return paymentUrl;

        }
        public string CreatePaymentUrl1(Appointment a)
        {
            string returnUrl = "https://localhost:7022/success";
            var vnPay = new VnPayLibrary();
            ExchangRate exchangRate = new ExchangRate();
            double exchangeRate = exchangRate.GetUsdToVndExchangeRateAsync().Result;
            var AmountInUsd = Convert.ToDouble(a.Price, CultureInfo.InvariantCulture);
            double amountInVnd = Math.Round(exchangRate.ConvertUsdToVnd(AmountInUsd, exchangeRate));

            vnPay.AddRequestData("vnp_Amount", (amountInVnd).ToString());
            vnPay.AddRequestData("vnp_Command", "pay");
            vnPay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
            vnPay.AddRequestData("vnp_CurrCode", "VND");
            vnPay.AddRequestData("vnp_IpAddr", "127.0.0.1");
            vnPay.AddRequestData("vnp_Locale", "vn");
            vnPay.AddRequestData("vnp_OrderInfo", $"Thanh toan don hang: {Guid.NewGuid()}");
            vnPay.AddRequestData("vnp_OrderType", "other");
            vnPay.AddRequestData("vnp_ReturnUrl", returnUrl);
            vnPay.AddRequestData("vnp_TmnCode", "7CY9UEAF");
            vnPay.AddRequestData("vnp_TxnRef", Guid.NewGuid().ToString());
            vnPay.AddRequestData("vnp_Version", "2.1.0");

            string paymentUrl = vnPay.CreateRequestUrl(vnp_Url, "DIGHI9T61AVLTF4C28ZTV6BX4HKI027T");
            return paymentUrl;

        }
        public async Task<string> CreatePaymentUrlAsync(Appointment a)
        {
            string returnUrl = "https://localhost:7022/success";
            var vnPay = new VnPayLibrary();
            ExchangRate exchangRate = new ExchangRate();

            double exchangeRate = await exchangRate.GetUsdToVndExchangeRateAsync();

            var AmountInUsd = Convert.ToDouble(a.Price, CultureInfo.InvariantCulture);
            double amountInVnd = Math.Round(exchangRate.ConvertUsdToVnd(AmountInUsd, exchangeRate));

            vnPay.AddRequestData("vnp_Amount", (amountInVnd * 100).ToString());
            vnPay.AddRequestData("vnp_Command", "pay");
            vnPay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
            vnPay.AddRequestData("vnp_CurrCode", "VND");
            vnPay.AddRequestData("vnp_IpAddr", "127.0.0.1");
            vnPay.AddRequestData("vnp_Locale", "vn");
            vnPay.AddRequestData("vnp_OrderInfo", $"Thanh toan don hang: {Guid.NewGuid()}");
            vnPay.AddRequestData("vnp_OrderType", "other");
            vnPay.AddRequestData("vnp_ReturnUrl", returnUrl);
            vnPay.AddRequestData("vnp_TmnCode", "7CY9UEAF");
            vnPay.AddRequestData("vnp_TxnRef", a.Id.ToString());
            vnPay.AddRequestData("vnp_Version", "2.1.0");

            string paymentUrl = vnPay.CreateRequestUrl(vnp_Url, "DIGHI9T61AVLTF4C28ZTV6BX4HKI027T");
            return paymentUrl;
        }

        public bool ValidateSignature(string queryString, string vnp_HashSecret)
        {
            var vnPay = new VnPayLibrary();

            // check signature
            return vnPay.ValidateSignature(queryString, vnp_HashSecret);
        }

        public async Task<HttpResponseMessage> SendRefundRequestAsync(VnpayRefundRequest request, string url)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                return await client.PostAsync(url, content);
            }
        }


        public string GenerateSecureHash(VnpayRefundRequest request, string secretKey)
        {
            string data = $"{request.vnp_RequestId}|{request.vnp_Version}|{request.vnp_Command}|{request.vnp_TmnCode}|{request.vnp_TransactionType}|{request.vnp_TxnRef}|{request.vnp_Amount}|{request.vnp_TransactionNo}|{request.vnp_TransactionDate}|{request.vnp_CreateBy}|{request.vnp_CreateDate}|{request.vnp_IpAddr}|{request.vnp_OrderInfo}";
            using (var hmacsha256 = new HMACSHA256(Encoding.UTF8.GetBytes(secretKey)))
            {
                byte[] hashBytes = hmacsha256.ComputeHash(Encoding.UTF8.GetBytes(data));
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }


        private Dictionary<string, string> ParseResponse(string response)
        {
            return response.Split('&')
                           .Select(part => part.Split('='))
                           .ToDictionary(split => split[0], split => split[1]);
        }

        public string CreateDepositPaymentUrl(int amount, string returnUrl)
        {
            throw new NotImplementedException();
        }

        public class VnpayRefundRequest
        {
            public string vnp_RequestId { get; set; }
            public string vnp_Version { get; set; } = "2.1.0";
            public string vnp_Command { get; set; } = "refund";
            public string vnp_TmnCode { get; set; }
            public string vnp_TransactionType { get; set; }
            public string vnp_TxnRef { get; set; }
            public long vnp_Amount { get; set; }
            public string vnp_OrderInfo { get; set; }
            public string vnp_TransactionNo { get; set; }
            public string vnp_TransactionDate { get; set; }
            public string vnp_CreateBy { get; set; }
            public string vnp_CreateDate { get; set; }
            public string vnp_IpAddr { get; set; }
            public string vnp_SecureHash { get; set; }
        }
    }
}