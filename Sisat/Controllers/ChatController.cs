using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sisat.Models;
using Sisat.ViewModels;
using SQLitePCL;
using System.Diagnostics;

namespace Sisat.Controllers
{
    public class ChatController : Controller
    {
        private readonly SisatContext _context;

        private readonly ProjetoListViewModel _projetoListViewModel;

        private readonly ChatViewModel _chatViewModel;

        public ChatController(SisatContext context, ChatViewModel chatViewModel)
        {
            _context = context;
            _chatViewModel = chatViewModel;
        }

        public IActionResult Index()
        {

            _chatViewModel.Chats = _context.Chat
                  .Where(c => c.IdRemetente == _chatViewModel.Usuario.Id) 
                  .OrderBy(c => c.DataEnvio)
                  .ToList();

            return View(_chatViewModel);
        }


        // GET: Chat/Conversation/{userId}
        public IActionResult AdminChat(int IdUsuario)
        {
            _chatViewModel.Chats = _context.Chat
                .Where(c => (c.IdRemetente == _chatViewModel.Usuario.Id && c.IdDestinatario == IdUsuario) ||
                            (c.IdDestinatario == IdUsuario && c.IdRemetente ==  _chatViewModel.Usuario.Id))
                .OrderBy(c => c.DataEnvio)
                .ToList();

            return View(_chatViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EnviaMensagem(int IdUsuario, string titulo, string textoMensagem, string resposta)
        {

            var chat = new Chat
            {
                IdRemetente = _chatViewModel.Usuario.Id,
                IdDestinatario = _chatViewModel.Usuario.IdNivAcesso == IdUsuario ? 4 : 1,
                RemetenteTipo = _chatViewModel.Usuario.IdNivAcesso == 1 ? 1 : 2,
                Titulo = titulo,
                Mensagem = textoMensagem,
                Resposta = resposta,
                DataEnvio = DateTime.Now,
                Visualizacao = false
            };

            _context.Chat.Add(chat);
            await _context.SaveChangesAsync();

            //if (_chatViewModel.Usuario.IdNivAcesso != 1),
            //{
            //    return RedirectToAction("Index", "Chat", new { id = chat });
            //}
            //else
            //{
            //    return RedirectToAction("AdminChat", "Chat", new { id = chat });
            //}

            return RedirectToAction("Index", "Chat", new { id = chat });

        }
    }
}