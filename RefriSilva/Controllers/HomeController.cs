using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RefriSilva.Data.Interface;
using RefriSilva.Models;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System;

namespace RefriSilva.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IServicoRepository _servicoRepository;

        public HomeController(IClienteRepository clienteRepository, IServicoRepository servicoRepository)
        {
            _clienteRepository = clienteRepository;
            _servicoRepository = servicoRepository;
        }

        public async Task<IActionResult> Index()
        {
            var clientes = await _clienteRepository.Get();
            await BuscarProximasLimpezasEManutencoes(clientes);

            return View(clientes);
        }

        private async Task BuscarProximasLimpezasEManutencoes(IEnumerable<Cliente> clientes)
        {
            var listaDeServicos = new List<Servico>();

            foreach (var item in clientes)
                listaDeServicos.AddRange(await _servicoRepository.Get(item.Uid));

            var ontem = DateTime.Now.AddDays(-1);

            ViewBag.Limpeza = listaDeServicos.Where(c=> c.DataProximaLimpeza >= ontem).OrderBy(c => c.DataProximaLimpeza).Take(5).ToList();
            ViewBag.Manutencao = listaDeServicos.Where(c => c.DataProximaManutencao >= ontem).OrderBy(c => c.DataProximaManutencao).Take(5).ToList();
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
        public async Task<IActionResult> DetailsServico(string clienteUid, string id)
        {
            return RedirectToAction("Details","Servico", new { clienteUid = clienteUid, id  = id });
        }
    }
}
