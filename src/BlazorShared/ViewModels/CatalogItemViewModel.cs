﻿using ApplicationCore.Entities;
using System.Collections.Generic;

namespace BlazorShared.ViewModels
{

    public class CatalogItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PictureUri { get; set; }
        public decimal Price { get; set; }
    }
}
