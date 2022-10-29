using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace webshop.Enums
{
    public enum OrderStatus
    {
        [Display(Name = "Uw order is ontvangen.")]
        Received,
        [Display(Name = "Uw order wordt gesorteerd.")]
        Gathering,
        [Display(Name = "Uw order is onderweg.")]
        Sent,
        [Display(Name = "Uw order is afgeleverd.")]
        Completed
    }
}