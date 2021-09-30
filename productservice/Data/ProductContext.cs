using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using productservice.Models;
using Microsoft.Extensions.Logging;

    public class ProductContext : DbContext
    {
        private readonly ILogger<ProductContext> _logger;

        public ProductContext (ILogger<ProductContext> logger, DbContextOptions<ProductContext> options)
            : base(options)
        {
            _logger = logger;
        }

        public DbSet<productservice.Models.Product> Product { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.LogTo(l => _logger.LogInformation(l));
    }
