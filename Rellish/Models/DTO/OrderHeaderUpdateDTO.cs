﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Rellish.Models.DTO
{
    public class OrderHeaderUpdateDTO
    {
        public int OrderHeaderId { get; set; }
        public string PickUpName { get; set; }
        public string PickUpPhoneNumber { get; set; }
        public string PickUpEmail { get; set; }
        public string StripePaymentIntentId { get; set; }
        public string Status { get; set; }

    }
}
