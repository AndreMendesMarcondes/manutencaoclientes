using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RefriSilva.Data.Imp;
using RefriSilva.Models;

namespace RefriSilva.Controllers
{
    [Authorize]
    public class ServicoController : Controller
    {
        const string SessionClienteId = "_ClienteId";
        private readonly IServicoRepository _servicoRepository;
        private readonly IClienteRepository _clienteRepository;

        public ServicoController(IServicoRepository servicoRepository, IClienteRepository clienteRepository)
        {
            _servicoRepository = servicoRepository;
            _clienteRepository = clienteRepository;
        }

        public async Task<IActionResult> Index(string clienteUid)
        {
            if (!String.IsNullOrEmpty(clienteUid))
            {
                var cliente = await _clienteRepository.GetById(clienteUid);
                TempData["Cliente"] = cliente;

                var servicos = await _servicoRepository.Get(clienteUid);
                foreach (var item in servicos)
                {
                    item.ClienteId = clienteUid;
                }
                return View(servicos);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public async Task<IActionResult> Details(string clienteUid, string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servico = await _servicoRepository.GetById(clienteUid, id);

            if (servico == null)
            {
                return NotFound();
            }

            return View(servico);
        }

        public IActionResult Create(string id)
        {
            Servico servico = new Servico();
            servico.ClienteId = id;
            return View(servico);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Uid,Descricao,DataCriacao,Valor,DataProximaManutencao,DataProximaLimpeza,ClienteId")] Servico servico)
        {
            if (ModelState.IsValid)
            {
                servico.DataProximaLimpeza = Convert.ToDateTime(servico.DataProximaLimpeza).ToUniversalTime();
                servico.DataProximaManutencao = Convert.ToDateTime(servico.DataProximaManutencao).ToUniversalTime();
                await _servicoRepository.Create(servico.ClienteId, servico);
                return RedirectToAction(nameof(Index), new { clienteUid = servico.ClienteId });
            }
            return View(servico);
        }

        public async Task<IActionResult> Edit(string clienteUid, string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servico = await _servicoRepository.GetById(clienteUid, id);
            if (servico == null)
            {
                return NotFound();
            }
            return View(servico);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Uid,Descricao,DataCriacao,Valor,DataProximaManutencao,DataProximaLimpeza,ClienteId")] Servico servico)
        {
            if (id != servico.Uid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    servico.DataProximaLimpeza = Convert.ToDateTime(servico.DataProximaLimpeza).ToUniversalTime();
                    servico.DataProximaManutencao = Convert.ToDateTime(servico.DataProximaManutencao).ToUniversalTime();
                    await _servicoRepository.Update(servico.ClienteId, servico.Uid, servico);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await ServicoExists(servico.ClienteId, servico.Uid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { clienteUid = servico.ClienteId });
            }
            return View(servico);
        }

        public async Task<IActionResult> Delete(string clienteUid, string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servico = await _servicoRepository.GetById(clienteUid, id);
            TempData["DeleteClienteId"] = clienteUid;
            if (servico == null)
            {
                return NotFound();
            }

            return View(servico);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            string clienteUid = ((Guid)TempData["DeleteClienteId"]).ToString();
            await _servicoRepository.Delete(clienteUid, id);
            return RedirectToAction(nameof(Index), new { clienteUid = clienteUid });
        }

        private async Task<bool> ServicoExists(string clienteUid, string id)
        {
            return await _servicoRepository.GetById(clienteUid, id) != null;
        }
    }
}
