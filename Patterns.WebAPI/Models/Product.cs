using System;

namespace Patterns.WebAPI.Models
{
    public class Product
    {
        public string Id { get; private set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
    }
}