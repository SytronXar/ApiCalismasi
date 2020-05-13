﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VatanAPI.Domain.Models;
using VatanAPI.Domain.Repositories;
using VatanAPI.Persistence.Contexts;

namespace VatanAPI.Persistence.Repositories
{
    public class SiparisRepository : BaseRepository, ISiparisRepository
    {
        public SiparisRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Siparis>> ListAsync()
        {
            return await _context.Siparis.ToListAsync();
        }
        public async Task AddAsync(Siparis siparis)
        {
            await _context.Siparis.AddAsync(siparis);
        }
        public async Task<Siparis> FindByIdAsync(int id)
        {
            return await _context.Siparis.FindAsync(id);
        }
        public void Update(Siparis siparis)
        {
            _context.Siparis.Update(siparis);
        }
        public void Remove(Siparis siparis)
        {
            _context.Siparis.Remove(siparis);
        }
    }
}