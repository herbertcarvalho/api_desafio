﻿using Backend.Erp.Skeleton.Domain.Entities;

namespace Backend.Erp.Skeleton.Application.DTOs.Response.Product
{
    public class GetProductsResponse : BaseIdResponse
    {
        public int IdCompany { get; set; }
        public int IdCategory { get; set; }
        public string Name { get; set; }
        public string NameCompany { get; set; }
        public string NameCategory { get; set; }
        public decimal Price { get; set; }
        public bool Active { get; set; }
        public string LinkImage { get; set; }

        public GetProductsResponse(Products products, string url)
        {
            Id = products.Id;
            Name = products.Name;
            IdCompany = products.IdCompany;
            NameCompany = products.Company.Name;
            IdCategory = products.IdCategory;
            NameCategory = products.Category.Name;
            Price = products.Price;
            Active = products.Status;
            LinkImage = url;
        }
    }
}
