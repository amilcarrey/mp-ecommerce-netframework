﻿using MercadoPago.DataStructures.Preference;
using MercadoPago.Resources;
using mp_ecommerce_netframework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mp_ecommerce_netframework.Controllers
{
    public class HomeController : Controller
    {
        List<Product> products = new List<Product>()
        {
            new Product {
                Id=1234,
                Name="Samsung S10",
                Description="Dispositivo móvil de Tienda e-commerce",
                Price="45000",
                Url="https://media-esp-buyviu-com.s3.amazonaws.com/products/354fe6a874d854d3b9f1a01e1c9fcab8_image_1.jpg" },
            new Product {
                Id=1234,
                Name="iPhone 11 pro",
                Description="Dispositivo móvil de Tienda e-commerce",
                Price="42000",
                Url="https://media-esp-buyviu-com.s3.amazonaws.com/products/e166ba0b9f18e08c078df16032ee4d42_image_1.png" }
        };
        public ActionResult Index()
        {
            return View(products);
        }

        public ActionResult Pay(Product item)
        {
            Preference preference = new Preference();

            preference.CollectorId = 469485398;

            //-Enviar la información del ítem que se está comprando.
            preference.Items.Add(
                new Item()
                {
                    Id = "1234",
                    Title = item.Name,
                    Description = item.Description,
                    PictureUrl = item.Url,
                    Quantity = 1,
                    UnitPrice = Convert.ToDecimal(item.Price)
                }
            );

            //- Enviar los datos del pagador.
            preference.Payer = new Payer()
            {
                Name = "Lalo",
                Surname = "Landa",
                Email = "test_user_63274575@testuser.com",
                Phone = new Phone()
                {
                    AreaCode = "11",
                    Number = "22223333"
                },
                Address = new Address()
                {
                    StreetName = "False",
                    StreetNumber = 123,
                    ZipCode = "1111"
                }
            };

            //- Enviar el número de orden (external_reference)
            preference.ExternalReference = "amilcarrey.ar@gmail.com";

            //
            preference.BackUrls = new BackUrls()
            {
                Success = "",
                Pending = "",
                Failure = ""
            };

            //- Enviar la URL donde se van a recibir las notificaciones de pago.
            preference.NotificationUrl = "";


            //- Limitar la cantidad de cuotas según lo solicitado.
            //-No ofrecer los medios de pago según lo solicitados.
            
            PaymentMethods paymentmethods = new PaymentMethods();
            paymentmethods.Installments = 6;

            List<MercadoPago.DataStructures.Preference.PaymentMethod> excludedPaymentMethod = new List<MercadoPago.DataStructures.Preference.PaymentMethod>();
            excludedPaymentMethod.Add(new MercadoPago.DataStructures.Preference.PaymentMethod()
            {
                Id = "amex"
            });

            paymentmethods.ExcludedPaymentMethods = excludedPaymentMethod;

            List<PaymentType> excludedPaymentType = new List<PaymentType>();
            excludedPaymentType.Add(new PaymentType()
            {
                Id = "atm"
            });

            paymentmethods.ExcludedPaymentTypes = excludedPaymentType;

            preference.AutoReturn = MercadoPago.Common.AutoReturnType.approved;
            
            preference.Save();

            Response.Redirect(preference.InitPoint);
            return View();
        }
        public ActionResult Success()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Failure()
        {
            ViewBag.Message = "Something goes wrong";

            return View();
        }
        public ActionResult Pending()
        {
            ViewBag.Message = "Your Payment is pending";

            return View();
        }
    }
}