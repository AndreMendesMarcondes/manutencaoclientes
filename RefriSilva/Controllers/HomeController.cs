using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RefriSilva.Data;
using RefriSilva.Data.Imp;
using RefriSilva.Models;

namespace RefriSilva.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IClienteRepository _clienteRepository;

        public HomeController(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _clienteRepository.Get());
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _clienteRepository.GetById(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        public IActionResult Services(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return RedirectToAction("Index",
                          "Servico",
                          new { clienteUid = id });
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Uid,Nome,Email,Telefone,Bairro,Endereco,Numero,Complemento")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                await _clienteRepository.Create(cliente);
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _clienteRepository.GetById(id);
            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Uid,Nome,Email,Telefone,Bairro,Endereco,Numero,Complemento")] Cliente cliente)
        {
            if (id != cliente.Uid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _clienteRepository.Update(cliente.Uid, cliente);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await ClienteExists(cliente.Uid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = await _clienteRepository.GetById(id);
            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _clienteRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ClienteExists(string id)
        {
            return await _clienteRepository.GetById(id) != null;
        }
    }
}
